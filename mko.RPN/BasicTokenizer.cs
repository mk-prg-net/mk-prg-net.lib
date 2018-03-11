
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2016
//
//  Projekt.......: mko.RPN
//  Name..........: BasicTokenizer.cs
//  Aufgabe/Fkt...: Basisimplementierung eines Tokenizers. List die Terme von 
//                  einem Datenstrom als Zeichenketten ein. Erzeugt aus ihnen 
//                  spezielle IToken- Objekte (IntToken, DoubleToken etc.).
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 27.2.2017
//  Änderungen....: Explizites setzen des in Tokens aufzulösenden Datenstroms/Strings
//                  mittels 
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 14.3.2017
//  Änderungen....: Definieren aller Funktionsnamen von FunctionTokens mittels DefFunctionNames.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 15.3.2017
//  Änderungen....: Kultur der Tokens jetzt über den Konstruktor definierbar (vorher fest US- Kultur)
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 24.3.2017
//  Änderungen....: Durch '...' begrenzte Stringliterale implementiert. 
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 11.3.2017
//  Änderungen....: TokenizePN und TokenizeRPN als statische Funktionen implementiert
//                  (ursprünglich in class Parser)
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.Logging;

namespace mko.RPN
{
    /// <summary>
    /// Basisimplementierung eines Tokenizers. List die Terme von 
    /// einem Datenstrom als Zeichenketten ein. Erzeugt aus ihnen 
    /// spezielle IToken- Objekte (IntToken, DoubleToken etc.).
    /// </summary>
    public class BasicTokenizer : ITokenizer
    {
        System.IO.StreamReader streamReader;

        /// <summary>
        /// Begrenzungszeichen von Strings
        /// </summary>
        public const char Delimiter = '\'';

        static BasicTokenizer()
        {
            rpnCult = new System.Globalization.CultureInfo("en-US");
        }

        public BasicTokenizer()
        {
        }

        /// <summary>
        /// Konstruktor der Basisimplementierung eines Tokenizers
        /// </summary>
        /// <param name="reader">Datenstrom, die Typ 2 Sprachterme in Reverse Polish Notation (RPN) enthält</param>
        public BasicTokenizer(System.IO.StreamReader reader)
        {
            streamReader = reader;
        }

        ///// <summary>
        ///// Konstruktor der Basisimplementierung eines Tokenizers
        ///// </summary>
        ///// <param name="reader"></param>
        ///// <param name="cult"></param>
        //public BasicTokenizer(System.IO.StreamReader reader, System.Globalization.CultureInfo cult)
        //{
        //    rpnCult = cult;
        //    streamReader = reader;
        //}

        /// <summary>
        /// Konstruktor der Basisimplementierung eines Tokenizers
        /// </summary>
        /// <param name="term">Zeichenkette, die Typ 2 Sprachterme in Reverse Polish Notation (RPN) enthält</param>
        public BasicTokenizer(string term)
        {
            rpnCult = new System.Globalization.CultureInfo("en-US");
            SetSource(term);
        }

        public BasicTokenizer(string term, System.Globalization.CultureInfo cult)
        {
            rpnCult = cult;
            SetSource(term);
        }




        /// <summary>
        /// Ein RPN Term als String wird als zu tokenisierender Ausdruck festgelegt.
        /// </summary>
        /// <param name="term"></param>
        public void SetSource(string term)
        {
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            var TermWriter = new System.IO.StreamWriter(memStream);
            TermWriter.Write(term);
            TermWriter.Flush();
            memStream.Seek(0, System.IO.SeekOrigin.Begin);

            streamReader = new System.IO.StreamReader(memStream);
        }

        /// <summary>
        /// Der Datenstrom, aus dem der zu tokenisierende RPN- Ausdruck eingelesen werden soll, wird festgelegt.
        /// </summary>
        /// <param name="stream"></param>
        public void SetSource(System.IO.Stream stream)
        {
            streamReader = new System.IO.StreamReader(stream);
        }

        /// <summary>
        /// Der StreamReader, mit dem der zu tokenisierende RPN- Ausdruck eingelesen werden soll, wird festgelegt.
        /// </summary>
        /// <param name="streamReader"></param>
        public void SetSource(System.IO.StreamReader streamReader)
        {
            this.streamReader = streamReader;
        }

        string[] FunctionNames = null;

        /// <summary>
        /// 14.3.2017, mko
        /// Menge der Funktionsnamen definieren
        /// </summary>
        /// <param name="FunctionNames"></param>
        internal void DefFunctionNames(params string[] FunctionNames)
        {
            this.FunctionNames = FunctionNames;
        }


        /// <summary>
        /// Erweiterrungspunkt zum Einlesen benutzerdefinierter/aufgabenspezifischer Token
        /// wie Funktionen, durch Sonderzeichen markierte Token.
        /// </summary>
        /// <param name="rawToken"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        protected virtual bool TryParseFunctionName(string rawToken, out IToken token)
        {
            // Wenn Funktionsnamen definiert sind, diese hier einsetzen
            if (FunctionNames != null && FunctionNames.Any(r => r == rawToken))
            {
                token = new FunctionNameToken(rawToken);
                return true;
            }
            else
            {
                token = null;
                return false;
            }
        }

        /// <summary>
        /// Kultur, in der die Ausdrücke definiert wurden (z.B. en-US)
        /// </summary>
        public static System.Globalization.CultureInfo rpnCult;

        public void Read()
        {
            mko.TraceHlp.ThrowArgExIfNot(streamReader != null, Properties.Resources.missing_tokenizer_input_stream);

            try
            {
                eatWhitespace();
                if (!streamReader.EndOfStream)
                {

                    // Lerraumzeichen oder '...' begrenzen die Tokens
                    bool stringBeg = (char)streamReader.Peek() == Delimiter;
                    if (stringBeg)
                    {
                        // Delimiter übersrpingen
                        streamReader.Read();
                    }

                    // Token einlesen bis zum abschiessenden Delimiter
                    var bld = new System.Text.StringBuilder();
                    while (!streamReader.EndOfStream
                            && ((!stringBeg && !char.IsWhiteSpace((char)streamReader.Peek())) || (stringBeg && Delimiter != (char)streamReader.Peek())))
                    {
                        bld.Append((char)streamReader.Read());
                    }

                    // Prüfen, ob ein begrenzter String eingelesen wurde
                    if (stringBeg)
                    {
                        mko.TraceHlp.ThrowExIf(streamReader.EndOfStream, Properties.Resources.tokenizing_failed_final_string_delimiter_missing);

                        // Abschlissenden Delimiter aus dem Datenstrom entfernen
                        streamReader.Read();
                        _currentToken = new StringToken(bld.ToString());

                    } else
                    {
                        // Einlesen unbegrenzter Tokens

                        var rawToken = bld.ToString();

                        int intValue = 0;
                        double dblValue = 0.0;
                        bool boolValue = false;
                        IToken token = null;

                        var cultBackup = System.Threading.Thread.CurrentThread.CurrentCulture;

                        System.Threading.Thread.CurrentThread.CurrentCulture = rpnCult;

                        try
                        {
                            if (!string.IsNullOrEmpty(rawToken))
                            {
                                if (TryParseFunctionName(rawToken, out token))
                                {
                                    _currentToken = token;
                                }
                                else if (int.TryParse(rawToken, out intValue))
                                {
                                    _currentToken = new IntToken(intValue);
                                }
                                else if (double.TryParse(rawToken, out dblValue))
                                {
                                    _currentToken = new DoubleToken(dblValue);
                                }
                                else if (bool.TryParse(rawToken, out boolValue))
                                {
                                    _currentToken = new BoolToken(boolValue);
                                }
                                else
                                {
                                    _currentToken = new StringToken(rawToken);
                                }
                            }
                        }
                        finally
                        {
                            System.Threading.Thread.CurrentThread.CurrentCulture = cultBackup;
                        }
                    }
                }
                else
                {
                    _EOF = true;
                }
            }
            catch (Exception ex)
            {
                mko.TraceHlp.ThrowEx(Properties.Resources.tokenizing_failed, ex);
            }
        }

        private void eatWhitespace()
        {
            while (!streamReader.EndOfStream && char.IsWhiteSpace((char)streamReader.Peek()))
            {
                streamReader.Read();
            }
        }

        public bool EOF
        {
            get { return _EOF; }
        }
        bool _EOF = false;

        public IToken Token
        {
            get { return _currentToken; }
        }
        IToken _currentToken;


        /// <summary>
        /// mko, 11.3.2018
        /// Löst einen Quelltext in der umgekehrten polnischen Notation in eine Liste von 
        /// Tokens auf. Die Token- Liste wird in der RPN zurückgegeben !
        /// Achtung: in RPN ist der erste Parameter der unmittelbar links neben dem 
        /// Funktionsname stehende.
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        public static RC<IToken[]> TokenizeRPN(string sourceCode, params string[] functionNames)
        {
            var rc = RC<IToken[]>.Failed(value: new IToken[] { });

            var tokenizer = new BasicTokenizer(sourceCode);
            tokenizer.DefFunctionNames(functionNames);

            var tokens = new LinkedList<IToken>();

            try
            {   
                tokenizer.Read();
                while (!tokenizer.EOF)
                {
                    tokens.AddLast(tokenizer.Token);
                    tokenizer.Read();
                }

                rc = RC<IToken[]>.Ok(value: tokens.ToArray());
            }
            catch (Exception ex)
            {
                rc = RC<IToken[]>.Failed(value: tokens.ToArray(), ErrorDescription: mko.ExceptionHelper.FlattenExceptionMessages(ex));                
            }
            return rc;
        }

        /// <summary>
        /// mko, 11.3.2018
        /// Löst einen Quelltext in der polnischen Notation in eine Liste von 
        /// Tokens auf. Die Token- Liste wird in der umgekehrt polnischen Notation zurückgegeben !
        /// Achtung: in RPN ist der erste Parameter der unmittelbar links neben dem 
        /// Funktionsname stehende.
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns>list of tokens</returns>
        public static RC<IToken[]> TokenizePN(string sourceCode, params string[] functionNames)
        {
            var rc = RC<IToken[]>.Failed(value: new IToken[] { });

            var tokenizer = new BasicTokenizer(sourceCode);
            tokenizer.DefFunctionNames(functionNames);

            var tokens = new LinkedList<IToken>();

            try
            {
                tokenizer.Read();
                while (!tokenizer.EOF)
                {
                    tokens.AddFirst(tokenizer.Token);
                    tokenizer.Read();
                }

                rc = RC<IToken[]>.Ok(value: tokens.ToArray());

            }
            catch (Exception ex)
            {
                rc = RC<IToken[]>.Failed(value: tokens.ToArray(), ErrorDescription: mko.ExceptionHelper.FlattenExceptionMessages(ex));
            }

            return rc;
        }


    }
}
