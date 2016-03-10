using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    /// <summary>
    /// Css, Beendet 
    /// </summary>
    public class Clear : CssMeasure
    {
        public enum Unit
        {
            left,
            right,
            both,
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
            var sec = (Clear)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(Clear fst, Clear sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(Clear fst, Clear sec)
        {
            return !fst.Equals(sec);
        }



        public static Clear Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;
            txt = txt.ToLower();
            var nm = new Clear();

            switch (txt)
            {
                case "both":
                    nm.Value = Unit.both;
                    break;
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
                default: throw new FormatException("Unbekannter Clear-Wert: " + txt);
            }

            return nm;
        }

        protected override object _Clone()
        {
            return new Clear() { Value = this.Value };
        }

        // Objektfabriken

        public static Clear Both
        {
            get
            {
                return new Clear() { Value = Unit.both };
            }
        }

        public static Clear Left
        {
            get
            {
                return new Clear() { Value = Unit.left };
            }
        }

        public static Clear Right
        {
            get
            {
                return new Clear() { Value = Unit.right };
            }
        }

        public static Clear None
        {
            get
            {
                return new Clear() { Value = Unit.none };
            }
        }

        public static Clear Inherit
        {
            get
            {
                return new Clear() { Value = Unit.inherit };
            }
        }

    }

}
