using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace mkoIt.Xhtml.Css
{
    public abstract class FontSize : CssMeasure
    {
        public abstract string Size { get; }

        public override string TextValue
        {
            get { return Size; }
        }

        public override bool Equals(object obj)
        {
            var sec = (FontSize)obj;
            return Size == sec.Size;
        }

        public static bool operator ==(FontSize fst, FontSize sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(FontSize fst, FontSize sec)
        {
            return !fst.Equals(sec);
        }



        protected override object _Clone()
        {
            if (this is FontSizeAbsolute)
                return new FontSizeAbsolute() { AbsoluteValue = ((FontSizeAbsolute)this).AbsoluteValue };
            else if (this is FontSizeInPixel)
                return new FontSizeInPixel() { AbsoluteValueInPixel = ((FontSizeInPixel)this).AbsoluteValueInPixel };
            else if (this is FontSizeRelativeEm)
                return new FontSizeRelativeEm() { RelativeValueInEm = ((FontSizeRelativeEm)this).RelativeValueInEm };
            else if (this is FontSizeInPercent)
                return new FontSizeInPercent() { RelativeValueInPercent = ((FontSizeInPercent)this).RelativeValueInPercent };
            else
                throw new NotSupportedException("Der FontSizeMeasure- Typ ist der Clone- Funktion unbekannt");

        }

        // Klassenfabriken zum einfachen erstellen von Objekten
        
        public static FontSizeInPixel Pixel(int Size)
        {
            Debug.Assert(Size >= 0);
            return new FontSizeInPixel() { AbsoluteValueInPixel = Size };
        }

        public static FontSizeAbsolute Point(double Size)
        {
            Debug.Assert(Size >= 0.0);
            return new FontSizeAbsolute() { AbsoluteValue = mko.Newton.Length.Point(Size) };
        }

        public static FontSizeInPercent Percent(double Size)
        {
            Debug.Assert(Size >= 0.0);
            return new FontSizeInPercent() { RelativeValueInPercent = Size };
        }
    }


    public class FontSizeInPixel : FontSize
    {
        public int AbsoluteValueInPixel { get; set; }

        public override string Size
        {
            get
            {
                return string.Format(mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo, "{0:D}px", AbsoluteValueInPixel);
            }
        }
    }

    public class FontSizeAbsolute : FontSize
    {
        public mko.Newton.Length AbsoluteValue { get; set; }

        public static FontSizeAbsolute Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            return new FontSizeAbsolute() { AbsoluteValue = mko.Newton.Length.Parse(txt, mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo) };
        }

        public override string Size
        {
            get
            {
                return AbsoluteValue.ToString("#.##", mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo);
            }
        }
    }


    public class FontSizeInPercent : FontSize
    {
        public double RelativeValueInPercent { get; set; }

        public override string Size
        {
            get
            {

                return string.Format(mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo, "{0:#.##}%", RelativeValueInPercent);
            }
        }
    }

    public class FontSizeRelativeEm : FontSize
    {
        public double RelativeValueInEm { get; set; }

        public override string Size
        {
            get
            {
                return string.Format(mkoIt.Xhtml.Xhtml.XhtmlNumberFormatInfo, "{0:#.##}em", RelativeValueInEm);
            }
        }
    }


}
