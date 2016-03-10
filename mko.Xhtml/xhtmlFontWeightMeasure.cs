using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{

    public class FontWeight : CssMeasure
    {
        public enum Unit
        {
            _100,
            _200,
            _300,
            _400,   // == normal
            _500,
            _600,
            _700,   // == bold
            _800,
            _900,
            lighter,
            normal,
            bold,
            bolder,
            inherit
        }

        public Unit Value { get; set; }

        public override bool Equals(object obj)
        {
            var sec = (FontWeight)obj;
            return Value == sec.Value;
        }

        public static bool operator ==(FontWeight fst, FontWeight sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(FontWeight fst, FontWeight sec)
        {
            return !fst.Equals(sec);
        }
        

        public static FontWeight Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            var fs = new FontWeight();
            if (txt == "100")
                fs.Value = Unit._100;
            else if (txt == "200")
                fs.Value = Unit._200;
            else if (txt == "300")
                fs.Value = Unit._300;
            else if (txt == "400")
                fs.Value = Unit._400;
            else if (txt == "500")
                fs.Value = Unit._500;
            else if (txt == "600")
                fs.Value = Unit._600;
            else if (txt == "700")
                fs.Value = Unit._700;
            else if (txt == "800")
                fs.Value = Unit._800;
            else if (txt == "900")
                fs.Value = Unit._900;
            else if (txt == "lighter")
                fs.Value = Unit.lighter;
            else if (txt == "normal")
                fs.Value = Unit.normal;
            else if (txt == "bold")
                fs.Value = Unit.bold;
            else if (txt == "bolder")
                fs.Value = Unit.bolder;
            else if (txt == "inherit")
                fs.Value = Unit.inherit;
            else throw new FormatException("Unbekannter FontWeigth: " + txt);

            return fs;
        }


        public string Weight
        {
            get
            {

                string txtVal = Value.ToString("G");
                if (txtVal.StartsWith("_"))
                    txtVal = txtVal.Substring(1);

                return txtVal;
            }
        }

        public override string TextValue
        {
            get { return Weight; }
        }


        // Objektfabriken

        public static FontWeight Bold
        {
            get
            {
                return new FontWeight() { Value = Unit.bold };
            }
        }

        public static FontWeight Bolder
        {
            get
            {
                return new FontWeight() { Value = Unit.bolder };
            }
        }

        public static FontWeight Lighter
        {
            get
            {
                return new FontWeight() { Value = Unit.lighter };
            }
        }

        public static FontWeight Normal
        {
            get
            {
                return new FontWeight() { Value = Unit.normal };
            }
        }

        public static FontWeight Inherit
        {
            get
            {
                return new FontWeight() { Value = Unit.inherit };
            }
        }

        protected override object _Clone()
        {
            return new FontWeight() { Value = this.Value };
        }
    }

}
