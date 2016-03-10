using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    public class TableLayout : CssMeasure
    {
        public enum Unit
        {
            auto,
            _fixed,
            inherit
        }

        public Unit Value { get; set; }

        public static TableLayout Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var ta = new TableLayout();

            if (txt == "auto")
                ta.Value = Unit.auto;
            else if (txt == "fixed")
                ta.Value = Unit._fixed;
            else if (txt == "inherit")
                ta.Value = Unit.inherit;
            else throw new FormatException("Unbekannter Art für Tabellenlayout: " + txt);

            return ta;
        }

        public string ValueTxt
        {
            get
            {
                return Value.ToString("G");
            }
        }

        public override bool Equals(object obj)
        {
            var sec = (TableLayout)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(TableLayout fst, TableLayout sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(TableLayout fst, TableLayout sec)
        {
            return !fst.Equals(sec);
        }


        public override string TextValue
        {
            get { return ValueTxt; }
        }

        protected override object _Clone()
        {
            return new TableLayout() { Value = this.Value };
        }

        // Objektfabriken

        /// <summary>
        /// Das Layout der Tabelle wird aus den Werten der Tabellen-, Spalten-,
        /// und Rahmenbreiten sowie den Zellabständen errechnet. Da die Breite bereits
        /// in der ersten Zeile festgelegt ist, wird die Tabelle vom Browser schneller gerendert
        /// als bei table-layout:auto; und der Zelleninhalt wird evtl. abgeschnitten,
        /// wenn er nicht in die angegebene Breite passt. 
        /// </summary>
        public static TableLayout Fixed
        {
            get
            {
                return new TableLayout() { Value = Unit._fixed };
            }
        }

        /// <summary>
        /// html: Standard
        /// Der Inhalt der Zellen beeinflußt die Breite der Zellen und somit der Tabellen.
        /// Deshalb muß die Tabelle zunächst vollständig eingelesen werden,
        /// so daß der Aufbau der Tabelle langsamer ist als bei table-layout:fixed;.
        /// Dafür wird die Tabelle verbreitert, wenn der Inhalt nicht in die Zelle paßt. 
        /// </summary>
        public static TableLayout Auto
        {
            get
            {
                return new TableLayout() { Value = Unit.auto };
            }
        }

        public static TableLayout Inherit
        {
            get
            {
                return new TableLayout() { Value = Unit.inherit };
            }
        }


    }

}
