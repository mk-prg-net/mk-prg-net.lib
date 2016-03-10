using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    public class TextDecoration : CssMeasure
    {
        public enum Unit
        {
            none,
            underline,
            overline,
            line_through,
            blink,
            inherit
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (TextDecoration)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(TextDecoration fst, TextDecoration sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(TextDecoration fst, TextDecoration sec)
        {
            return !fst.Equals(sec);
        }


        public static TextDecoration Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var fs = new TextDecoration();
            if (txt == "none")
                fs.Value = Unit.none;
            else if (txt == "underline")
                fs.Value = Unit.underline;
            else if (txt == "overline")
                fs.Value = Unit.overline;
            else if (txt == "line-through")
                fs.Value = Unit.line_through;
            else if (txt == "blink")
                fs.Value = Unit.blink;
            else if (txt == "inherit")
                fs.Value = Unit.inherit;
            else throw new FormatException("Unbekannter TextDecorationMeasure: " + txt);

            return fs;
        }

        public string Style
        {
            get
            {
                return Value.ToString("G").Replace("_", "-");
            }
        }

        public override string TextValue
        {
            get { return Style; }
        }

        protected override object _Clone()
        {
            return new TextDecoration() { Value = this.Value };
        }


        // Objektfabriken

        public static TextDecoration Blink
        {
            get
            {
                return new TextDecoration() { Value = Unit.blink };
            }
        }

        public static TextDecoration Inherit
        {
            get
            {
                return new TextDecoration() { Value = Unit.inherit };
            }
        }

        public static TextDecoration LineThrough
        {
            get
            {
                return new TextDecoration() { Value = Unit.line_through };
            }
        }

        public static TextDecoration None
        {
            get
            {
                return new TextDecoration() { Value = Unit.none };
            }
        }

        public static TextDecoration Overline
        {
            get
            {
                return new TextDecoration() { Value = Unit.overline };
            }
        }

        public static TextDecoration Underline
        {
            get
            {
                return new TextDecoration() { Value = Unit.underline };
            }
        }







    }
}
