using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{

    public class FontStyle : CssMeasure
    {
        public enum Unit
        {
            italic,
            obelique,
            inherit
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (FontStyle)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(FontStyle fst, FontStyle sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(FontStyle fst, FontStyle sec)
        {
            return !fst.Equals(sec);
        }



        public static FontStyle Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var fs = new FontStyle();
            if (txt == "italic")
                fs.Value = Unit.italic;
            else if (txt == "obelique")
                fs.Value = Unit.obelique;
            else if (txt == "inherit")
                fs.Value = Unit.inherit;
            else throw new FormatException("Unbekannter FontStyle: " + txt);

            return fs;
        }

        public string Style
        {
            get
            {
                return Value.ToString("G");
            }
        }

        public override string TextValue
        {
            get { return Style; }
        }

        // Objektfabriken

        public static FontStyle Italic
        {
            get
            {
                return new FontStyle() { Value = Unit.italic };
            }
        }

        public static FontStyle Obelique
        {
            get
            {
                return new FontStyle() { Value = Unit.obelique };
            }
        }

        public static FontStyle Inherit
        {
            get
            {
                return new FontStyle() { Value = Unit.inherit };
            }
        }


        protected override object _Clone()
        {
            return new FontStyle() { Value = this.Value };
        }
    }

}
