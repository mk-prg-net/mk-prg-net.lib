using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{

    public class BorderStyle : CssMeasure
    {
        public enum Unit
        {
            none,
            hidden,
            dotted,
            dashed,
            solid,
            _double,
            groove,
            ridge,
            inset,
            outset
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (BorderStyle)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(BorderStyle fst, BorderStyle sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(BorderStyle fst, BorderStyle sec)
        {
            return !fst.Equals(sec);
        }

        public static BorderStyle Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();
            var bs = new BorderStyle();
            if (txt == "none")
                bs.Value = Unit.none;
            else if (txt == "hidden")
                bs.Value = Unit.hidden;
            else if (txt == "dotted")
                bs.Value = Unit.dotted;
            else if (txt == "dashed")
                bs.Value = Unit.dashed;
            else if (txt == "solid")
                bs.Value = Unit.solid;
            else if (txt == "double")
                bs.Value = Unit._double;
            else if (txt == "groove")
                bs.Value = Unit.groove;
            else if (txt == "ridge")
                bs.Value = Unit.ridge;
            else if (txt == "inset")
                bs.Value = Unit.inset;
            else if (txt == "outset")
                bs.Value = Unit.outset;
            else throw new FormatException("Unbekannter Border Style: " + txt);

            return bs;
        }

        public string Style
        {
            get
            {
                string txtVal = Value.ToString("G");
                return txtVal.StartsWith("_") ? txtVal.Substring(1) : txtVal;
            }
        }

        public override string TextValue
        {
            get { return Style; }
        }


        protected override object _Clone()
        {
            return new BorderStyle() { Value = this.Value };
        }


        // Objektfabriken

        public static BorderStyle Double
        {
            get
            {
                return new BorderStyle() { Value = Unit._double };
            }
        }

        public static BorderStyle Dashed
        {
            get
            {
                return new BorderStyle() { Value = Unit.dashed };
            }
        }

        public static BorderStyle Dotted
        {
            get
            {
                return new BorderStyle() { Value = Unit.dotted };
            }
        }

        public static BorderStyle Groove
        {
            get
            {
                return new BorderStyle() { Value = Unit.groove };
            }
        }

        public static BorderStyle Hidden
        {
            get
            {
                return new BorderStyle() { Value = Unit.hidden };
            }
        }

        public static BorderStyle Inset
        {
            get
            {
                return new BorderStyle() { Value = Unit.inset };
            }
        }

        public static BorderStyle Outset
        {
            get
            {
                return new BorderStyle() { Value = Unit.outset };
            }
        }

        public static BorderStyle None
        {
            get
            {
                return new BorderStyle() { Value = Unit.none };
            }
        }

        public static BorderStyle Ridge
        {
            get
            {
                return new BorderStyle() { Value = Unit.ridge };
            }
        }

        public static BorderStyle Solid
        {
            get
            {
                return new BorderStyle() { Value = Unit.solid };
            }
        }

    }

}
