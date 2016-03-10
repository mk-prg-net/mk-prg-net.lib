using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    public class Visiblity : CssMeasure
    {
        public Visiblity()
        {
            Value = Unit.inherit;
        }

        public Visiblity(Unit value)
        {
            Value = value;
        }

        public enum Unit
        {
            visible,
            collapse,
            hidden,
            inherit
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (Visiblity)obj;
            return Value == sec.Value;
        }
        
        public static bool operator ==(Visiblity fst, Visiblity sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(Visiblity fst, Visiblity sec)
        {
            return !fst.Equals(sec);
        }


        public static Visiblity Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var ta = new Visiblity();

            if (txt == "collapse")
                ta.Value = Unit.collapse;
            else if (txt == "visible")
                ta.Value = Unit.visible;
            else if (txt == "hidden")
                ta.Value = Unit.hidden;
            else if (txt == "inherit")
                ta.Value = Unit.inherit;
            else throw new FormatException("Unbekannte Art für Visibility: " + txt);

            return ta;
        }

        public string ValueTxt
        {
            get
            {
                return Value.ToString("G");
            }
        }

        /// <summary>
        /// Implementiert CssMeasure
        /// </summary>
        public override string TextValue
        {
            get { return ValueTxt; }
        }

        protected override object _Clone()
        {
            return new Visiblity() { Value = this.Value };
        }


        public static Visiblity Visible
        {
            get
            {
                return new Visiblity(Unit.visible);
            }
        }

        public static Visiblity Collapse
        {
            get
            {
                return new Visiblity(Unit.collapse);
            }
        }

        public static Visiblity Hidden
        {
            get
            {
                return new Visiblity(Unit.hidden);
            }
        }

        public static Visiblity Inherit
        {
            get
            {
                return new Visiblity(Unit.inherit);
            }
        }

    }
}
