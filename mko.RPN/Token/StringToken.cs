//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2017
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 24.2.2017
//  Änderungen....: Test auf Stringtoken implementiert
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
    public class StringToken : NoFunctionToken
    {
        public StringToken(string Value, int CountOfEvaluatedTokens = 1)
            : base(CountOfEvaluatedTokens)
        {
            _Value = Value;
        }        

        
        string _Value;

        protected override string ValueToString
        {
            get { return _Value; }
        }

        public override bool IsInteger
        {
            get { return false; }
        }

        public override bool IsBoolean
        {
            get { return false; }
        }

        public override bool IsNummeric
        {
            get { return false; }
        }

        public override IToken Copy()
        {
            return new StringToken(_Value, CountOfEvaluatedTokens);
        }


        /// <summary>
        /// Prüft, ob das Token ein Stringtoken ist
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Test(IToken token)
        {
            return !token.IsBoolean && !token.IsFunctionName && !token.IsNummeric;
        }

        public override string ToString()
        {
            return "Str(" + Value + ")";
        }

        public static explicit operator bool(StringToken tok)
        {
            mko.TraceHlp.ThrowArgExIfNot(tok.Value == "true" || tok.Value == "false", Properties.Resources.boolean_literal_expected);
            return tok.Value == "true";
        }




    }
}
