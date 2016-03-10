using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    public class VerticalAlign : CssMeasure
    {
        public enum Unit
        {
            baseline,
            sub,
            super,
            text_top,
            middle,
            text_bottom,
            inherit
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (VerticalAlign)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(VerticalAlign fst, VerticalAlign sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(VerticalAlign fst, VerticalAlign sec)
        {
            return !fst.Equals(sec);
        }


        public static VerticalAlign Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var ta = new VerticalAlign();

            if (txt == "baseline")
                ta.Value = Unit.baseline;
            else if (txt == "sub")
                ta.Value = Unit.sub;
            else if (txt == "super")
                ta.Value = Unit.super;
            else if (txt == "text-top")
                ta.Value = Unit.text_top;
            else if (txt == "middle")
                ta.Value = Unit.middle;
            else if (txt == "text-bottom")
                ta.Value = Unit.text_bottom;
            else if (txt == "inherit")
                ta.Value = Unit.inherit;
            else throw new FormatException("Unbekannter Art der Vertikalen Ausrichtung: " + txt);

            return ta;
        }

        public string ValueTxt
        {
            get
            {
                if (Value == Unit.text_bottom)
                    return "text-bottom";
                else if (Value == Unit.text_top)
                    return "text-top";
                else
                    return Value.ToString("G");
            }
        }

        public override string TextValue
        {
            get { return ValueTxt; }
        }

        protected override object _Clone()
        {
            return new VerticalAlign() { Value = this.Value };
        }

        // Objektfabriken

        public static VerticalAlign Baseline
        {
            get
            {
                return new VerticalAlign() { Value = Unit.baseline };
            }
        }

        public static VerticalAlign Inherit
        {
            get
            {
                return new VerticalAlign() { Value = Unit.inherit };
            }
        }

        public static VerticalAlign Middle
        {
            get
            {
                return new VerticalAlign() { Value = Unit.middle };
            }
        }

        public static VerticalAlign Sub
        {
            get
            {
                return new VerticalAlign() { Value = Unit.sub };
            }
        }

        public static VerticalAlign Super
        {
            get
            {
                return new VerticalAlign() { Value = Unit.super };
            }
        }

        public static VerticalAlign TextBottom
        {
            get
            {
                return new VerticalAlign() { Value = Unit.text_bottom };
            }
        }

        public static VerticalAlign TextTop
        {
            get
            {
                return new VerticalAlign() { Value = Unit.text_top };
            }
        }
    }

}
