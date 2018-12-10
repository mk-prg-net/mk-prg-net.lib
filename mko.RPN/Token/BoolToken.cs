//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.6.2016
//
//  Projekt.......: mko.RPN
//  Name..........: IntToken.cs
//  Aufgabe/Fkt...: Token für Boolean
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
//  Datum.........: 18.5.2017
//  Änderungen....: Implizite Konvertierung in bool hinzugefügt
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
    public class BoolToken : NoFunctionToken
    {
        public BoolToken(bool Value, int CountOfEvaluatedTokens = 1)
            : base(CountOfEvaluatedTokens)
        {
            _Value = Value;
        }

        bool _Value;

        public bool ValueAsBool
        {
            get
            {
                return _Value;
            }
        }

        protected override string ValueToString
        {
            get { return _Value.ToString(); }
        }

        public override bool IsInteger
        {
            get { return false; }
        }

        public override bool IsBoolean
        {
            get { return true; }
        }

        public override bool IsNummeric
        {
            get { return false; }
        }

        public override IToken Copy()
        {
            return new BoolToken(_Value, CountOfEvaluatedTokens);
        }

        public override string ToString()
        {
            return "Bool(" + ValueToString + ")";
        }

        public static implicit operator bool(BoolToken bt)
        {
            return bt.ValueAsBool;
        }
    }
}
