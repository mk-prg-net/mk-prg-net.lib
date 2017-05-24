//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 10.6.2016
//
//  Projekt.......: mko.RPN
//  Name..........: BufferedTokenizer.cs
//  Aufgabe/Fkt...: Tokenizer, der auf einer Liste von Tokens operiert.
//                  
//
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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    public class BufferedTokenizer : ITokenizer
    {

        class BufferedToken
        {

        }

        LinkedList<IToken> TokenList = new LinkedList<IToken>();
        LinkedListNode<IToken> _current;

        /// <summary>
        ///  Erzeugt einen leeren BufferedTokenizer
        /// </summary>
        public BufferedTokenizer()
        {
            _current = TokenList.First;
        }

        /// <summary>
        /// Liest alle Token mittels eines Tokenizer ein und puffert sie
        /// </summary>
        /// <param name="Tokenizer"></param>
        public BufferedTokenizer(ITokenizer Tokenizer)
        {
            Tokenizer.Read();
            while (!Tokenizer.EOF && Tokenizer.Token != null)
            {
                TokenList.AddLast(Tokenizer.Token);
                Tokenizer.Read();
            }

            _current = TokenList.First;
        }

        //==================================================================================
        // Einfügeoperationen

        /// <summary>
        /// Ein weiteres Token puffern
        /// </summary>
        /// <param name="Token"></param>
        public void Add(IToken Token)
        {
            TokenList.AddLast(Token);
            _current = TokenList.Last;
        }

        /// <summary>
        /// Anzahl der gepufferten Tokens
        /// </summary>
        public long Count
        {
            get
            {
                return TokenList.Count;
            }
        }

        /// <summary>
        /// Alle gepufferten Tokens löschen
        /// </summary>
        public void Reset()
        {
            TokenList.Clear();
            _current = TokenList.Last;
        }


        //==================================================================================
        // Stelle, ab der Read liest, im Buffer verschieben
        public void Seek(System.IO.SeekOrigin origin, long pos)
        {
            switch (origin)
            {
                case System.IO.SeekOrigin.Begin:
                    {
                        _current = TokenList.First;
                        SeekForeward(pos);
                    }
                    break;
                case System.IO.SeekOrigin.Current:
                    {
                        if (pos >= 0)
                        {
                            SeekForeward(pos);
                        }
                        else
                        {
                            SeekBackward(pos);
                        }

                    }
                    break;
                case System.IO.SeekOrigin.End:
                    {
                        _current = TokenList.Last;
                        SeekBackward(pos);
                    }
                    break;
                default:
                    break;
            }
        }

        private void SeekBackward(long pos)
        {
            if (pos <= 0)
            {
                for (long count = 0;
                    count > pos && _current.Previous != null;
                    count--, _current = _current.Previous) ;

            }
        }

        private void SeekForeward(long pos)
        {
            if (pos >= 0)
            {
                for (long count = 0;
                    count < pos && _current.Next != null;
                    count++, _current = _current.Next) ;

            }
        }

        //==================================================================================
        // ITokenizer Implementierung
        public void Read()
        {
            if (_current != null)
            {
                _current = _current.Next;
            }
        }

        public bool EOF
        {
            get { return _current == null; }
        }

        public IToken Token
        {
            get
            {
                if (_current != null)
                {
                    return _current.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Liste der geparsten Tokens
        /// </summary>
        public IToken[] Tokens
        {
            get
            {
                return TokenList.ToArray();
            }
        }
    }
}
