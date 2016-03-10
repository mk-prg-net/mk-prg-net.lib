using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    public class TableShowEmptyCells : CssMeasure
    {
        public enum Unit
        {
            show,
            hide,
            inherit
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (TableShowEmptyCells)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(TableShowEmptyCells fst, TableShowEmptyCells sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(TableShowEmptyCells fst, TableShowEmptyCells sec)
        {
            return !fst.Equals(sec);
        }


        public static TableShowEmptyCells Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var ta = new TableShowEmptyCells();

            if (txt == "show")
                ta.Value = Unit.show;
            else if (txt == "hide")
                ta.Value = Unit.hide;
            else if (txt == "inherit")
                ta.Value = Unit.inherit;
            else throw new FormatException("Unbekannter Art für TableShowEmptyCells: " + txt);

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
            return new TableShowEmptyCells() { Value = this.Value };
        }


        // Objektfabriken

        public static TableShowEmptyCells Hide
        {
            get
            {
                return new TableShowEmptyCells() { Value = Unit.hide };
            }
        }

        public static TableShowEmptyCells Show
        {
            get
            {
                return new TableShowEmptyCells() { Value = Unit.show };
            }
        }

        public static TableShowEmptyCells Inherit
        {
            get
            {
                return new TableShowEmptyCells() { Value = Unit.inherit };
            }
        }


    }

}
