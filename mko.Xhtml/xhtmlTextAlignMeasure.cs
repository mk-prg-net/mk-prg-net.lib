using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{

    public class TextAlign : CssMeasure
    {
        public enum Unit
        {
            left,
            right,
            center,
            justify,    // Blocksatz
            inherit
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (TextAlign)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(TextAlign fst, TextAlign sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(TextAlign fst, TextAlign sec)
        {
            return !fst.Equals(sec);
        }


        public static TextAlign Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var ta = new TextAlign();

            if (txt == "left" || txt == "start")
                ta.Value = Unit.left;
            else if (txt == "right")
                ta.Value = Unit.right;
            else if (txt == "center")
                ta.Value = Unit.center;
            else if (txt == "justify")
                ta.Value = Unit.justify;
            else if (txt == "inherit")
                ta.Value = Unit.inherit;
            else throw new FormatException("Unbekannter Art der Textausrichtung: " + txt);

            return ta;
        }

        public string Alignment
        {
            get
            {
                return Value.ToString("G");
            }
        }

        public override string TextValue
        {
            get { return Alignment; }
        }


        protected override object _Clone()
        {
            return new TextAlign() { Value = this.Value };
        }

        // Objektfabriken

        public static TextAlign Center
        {
            get
            {
                return new TextAlign() { Value = Unit.center };
            }
        }

        public static TextAlign Inherit
        {
            get
            {
                return new TextAlign() { Value = Unit.inherit };
            }
        }

        public static TextAlign Justify
        {
            get
            {
                return new TextAlign() { Value = Unit.justify };
            }
        }

        public static TextAlign Left
        {
            get
            {
                return new TextAlign() { Value = Unit.left };
            }
        }

        public static TextAlign Right
        {
            get
            {
                return new TextAlign() { Value = Unit.right };
            }
        }




    }

}
