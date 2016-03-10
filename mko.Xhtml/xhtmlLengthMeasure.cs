using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using N = mko.Newton;

namespace mkoIt.Xhtml.Css
{
        public abstract class Length : CssMeasure
        {
            public abstract string length
            {
                get;
            }

            public override bool Equals(object obj)
            {
                var sec = (Length)obj;
                return length == sec.length;
            }

            public static bool operator ==(Length fst, Length sec)
            {
                return fst.Equals(sec);
            }

            public static bool operator !=(Length fst, Length sec)
            {
                return !fst.Equals(sec);
            }


            public override string TextValue
            {
                get { return length; }
            }


            protected override object _Clone()
            {
                if (this is LengthAbsolute)
                    return new LengthAbsolute() { Value = ((LengthAbsolute)this).Value };
                else if (this is LengthPixel)
                    return new LengthPixel() { Value = ((LengthPixel)this).Value };
                else
                    throw new NotSupportedException("Der LengthMeasure- Typ ist der Clone- Funktion unbekannt");
            }


            // Objektfabriken

            public static LengthPixel Pixel(int CountPix)
            {
                return new LengthPixel(CountPix);
            }

            public static LengthAbsolute pt(double length)
            {
                return new LengthAbsolute(N.Length.Point(length));
            }

            public static LengthAbsolute mm(double length)
            {
                return new LengthAbsolute(N.Length.Millimeter(length));
            }

            public static LengthAbsolute cm(double length)
            {
                return new LengthAbsolute(N.Length.Centimeter(length));
            }

            public static LengthAbsolute dm(double length)
            {
                return new LengthAbsolute(N.Length.Decimeter(length));
            }

            public static LengthAbsolute m(double length)
            {
                return new LengthAbsolute(N.Length.Meter(length));
            }

            public static LengthRealtive Percent(double percent)
            {
                return new LengthRealtive(percent);
            }
        }


        public class LengthAbsolute : Length
        {
            public LengthAbsolute() { }

            public LengthAbsolute(mko.Newton.Length Value)
            {
                this.Value = Value;
            }

            //public LengthAbsolute(double value, mko.Newton.MeasuredValueS.UnitS unit) 
            //{
            //    Value = new mko.Newton.MeasuredValueS() { Value = value, Unit = unit };
            //}

            public mko.Newton.Length Value { get; set; }            

            public static LengthAbsolute Parse(string txt)
            {
                if (string.IsNullOrEmpty(txt))
                    return null;

                return new LengthAbsolute() { Value = mko.Newton.Length.Parse(txt, mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo) };
            }

            public override string length
            {
                get {
                    return Value.ToString("#.###", mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo);
                    ;
                }
            }
        }


        public class LengthPixel : Length
        {
            public LengthPixel() { }
            public LengthPixel(int value)
            {
                Value = value;
            }

            public int Value { get; set; }

            public static LengthPixel Parse(string txt)
            {
                if (string.IsNullOrEmpty(txt))
                    return null;

                return new LengthPixel() { Value = int.Parse(txt, mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo) };
            }

            public override string length
            {
                get {
                    return string.Format(mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo, "{0:D}px", Value);
                }
            }
        }

        public class LengthRealtive : Length
        {
            public LengthRealtive() { }
            public LengthRealtive(double value)
            {
                Value = value;
            }

            public double Value { get; set; }

            public static LengthRealtive Parse(string txt)
            {
                if (string.IsNullOrEmpty(txt))
                    return null;

                return new LengthRealtive() { Value = double.Parse(txt, mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo) };
            }

            public override string length
            {
                get
                {
                    return string.Format(mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo, "{0:#.##}%", Value);
                }
            }


        }
    

}
