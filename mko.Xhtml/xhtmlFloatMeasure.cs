//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.2.2012
//
//  Projekt.......: mkoItXhtml
//  Name..........: xhtmlFloatMeasure.cs
//  Aufgabe/Fkt...: CSS-Eigenschaft, die den Textumlauf um ein Element (z.B. Div- Block)
//                  definiert.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.0
//  Werkzeuge.....: Visual Studio 2010
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

namespace mkoIt.Xhtml.Css
{

    public class Float : CssMeasure
    {
        public enum Unit
        {
            left,
            right,
            none,
            inherit
        }

        public Unit Value
        {
            get;
            set;
        }

        public string ValueTxt
        {
            get
            {
                return Value.ToString("G");
            }
        }

        public override string TextValue
        {
            get { return ValueTxt; }
        }

        public override bool Equals(object obj)
        {
            var sec = (Float)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(Float fst, Float sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(Float fst, Float sec)
        {
            return !fst.Equals(sec);
        }



        public static Float Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;
            txt = txt.ToLower();
            var nm = new Float();

            switch (txt)
            {
                case "left":
                    nm.Value = Unit.left;
                    break;
                case "right":
                    nm.Value = Unit.right;
                    break;
                case "none":
                    nm.Value = Unit.none;
                    break;
                case "inherit":
                    nm.Value = Unit.inherit;
                    break;
                default:
                    throw new FormatException("Unbekannter float-Wert: " + txt);
            }

            return nm;
        }


        protected override object _Clone()
        {
            return new Float() { Value = this.Value };

        }


        // Objektfabriken

        public static Float Left
        {
            get
            {
                return new Float() { Value = Unit.left };
            }
        }

        public static Float Right
        {
            get
            {
                return new Float() { Value = Unit.right };
            }
        }

        public static Float None
        {
            get
            {
                return new Float() { Value = Unit.none };
            }
        }

        public static Float Inherit
        {
            get
            {
                return new Float() { Value = Unit.inherit };
            }
        }
    }

}
