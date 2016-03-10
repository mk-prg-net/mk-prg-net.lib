using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{

    public class BorderCollapse : CssMeasure
    {
        public BorderCollapse() {
            Value = Unit.inherit;
        }

        public BorderCollapse(Unit value)
        {
            Value = value;
        }

        public enum Unit
        {
            collapse,
            separate,
            inherit
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (BorderCollapse)obj;

            return Value == sec.Value;
        }

        public static bool operator ==(BorderCollapse fst, BorderCollapse sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(BorderCollapse fst, BorderCollapse sec)
        {
            return !fst.Equals(sec);
        }

        public static BorderCollapse Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var ta = new BorderCollapse();

            if (txt == "collapse")
                ta.Value = Unit.collapse;
            else if (txt == "separate")
                ta.Value = Unit.separate;
            else if (txt == "inherit")
                ta.Value = Unit.inherit;
            else throw new FormatException("Unbekannter Art für BorderCollapse: " + txt);

            return ta;
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

        protected override object _Clone()
        {
            return new BorderCollapse() { Value = this.Value };
        }


        // Objektfabriken


        public static BorderCollapse Collapse
        {
            get
            {
                return new BorderCollapse() { Value = Unit.collapse };
            }
        }

        public static BorderCollapse Inherit
        {
            get
            {
                return new BorderCollapse() { Value = Unit.inherit };
            }
        }

        public static BorderCollapse Separate
        {
            get
            {
                return new BorderCollapse() { Value = Unit.separate };
            }
        }


    }

}
