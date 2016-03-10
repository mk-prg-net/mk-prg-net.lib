using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    public class Position : CssMeasure
    {

        public enum Unit
        {
            _static,
            absolute,
            relative,
            _fixed,
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
                if (Value == Unit._fixed)
                    return "fixed";
                else if (Value == Unit._static)
                    return "static";
                else
                    return Value.ToString("G");
            }
        }

        public override bool Equals(object obj)
        {
            var sec = (Position)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(Position fst, Position sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(Position fst, Position sec)
        {
            return !fst.Equals(sec);
        }


        protected override object _Clone()
        {
            return new Position() { Value = this.Value };
        }

        public override string TextValue
        {
            get { return ValueTxt; }
        }

        public static Position Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;
            txt = txt.ToLower();
            var nm = new Position();

            switch (txt)
            {
                case "static":
                    nm.Value = Unit._static;
                    break;
                case "absolute":
                    nm.Value = Unit.absolute;
                    break;
                case "relative":
                    nm.Value = Unit.relative;
                    break;
                case "fixed":
                    nm.Value = Unit._fixed;
                    break;
                case "inherit":
                    nm.Value = Unit.inherit;
                    break;
                default:
                    throw new FormatException("Unbekannter position-Wert: " + txt);
            }

            return nm;
        }



        // Objektfabriken

        public static Position Fixed
        {
            get
            {
                return new Position() { Value = Unit._fixed };
            }
        }

        public static Position Static
        {
            get
            {
                return new Position() { Value = Unit._static };
            }
        }

        public static Position Absolute
        {
            get
            {
                return new Position() { Value = Unit.absolute };
            }
        }

        public static Position Relative
        {
            get
            {
                return new Position() { Value = Unit.relative };
            }
        }

        public static Position Inherit
        {
            get
            {
                return new Position() { Value = Unit.inherit };
            }
        }
    }

}
