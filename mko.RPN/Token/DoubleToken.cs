//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.6.2016
//
//  Projekt.......: mko.RPN
//  Name..........: IntToken.cs
//  Aufgabe/Fkt...: Token für Double
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
//  Änderungen....: Erweitert um double- Konvertierungsoperator
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 1.11.2018
//  Änderungen....: Formatprovider, mit dem ein Double in einen kulturspezifischen String
//                  gewandelt wird, wird jetzt via Konstruktor injeziert
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
    public class DoubleToken : NoFunctionToken
    {
        protected readonly IFormatProvider pfmt;


        public DoubleToken(double Value, IFormatProvider pfmt, int CountOfEvaluatedTokens = 1)
            : base(CountOfEvaluatedTokens)
        {
            _Value = Value;
            this.pfmt = pfmt;
        }

        double _Value;

        public double ValueAsDouble
        {
            get
            {
                return _Value;
            }
        }

        protected override string ValueToString
        {
            get { return _Value.ToString(pfmt); }
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
            get { return true; }
        }

        public override IToken Copy()
        {
            return new DoubleToken(_Value, pfmt, CountOfEvaluatedTokens);
        }

        public override string ToString()
        {
            return "dbl(" + ValueToString + ")";
        }

        public static explicit operator int(DoubleToken tok)
        {
            return (int)tok.ValueAsDouble;
        }

        public static implicit operator double(DoubleToken tok)
        {
            return tok.ValueAsDouble;
        }


    }
}
