using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ekd = mko.Euklid;
using Css = mkoIt.Xhtml.Css;
using System.Diagnostics;


namespace mko.Graphic
{
    public abstract class PenSolidColor : Pen
    {
        protected PenSolidColor()
        {
            DashStyle = Css.BorderStyle.Solid;
            Color = Css.Color.Black;
            Width = new Css.LengthPixel(1);
        }

        protected PenSolidColor(Css.Color color)
        {
            DashStyle = Css.BorderStyle.Solid;
            Color = color;
            Width = new Css.LengthPixel(1);
        }

        protected PenSolidColor(Css.Color color, Css.BorderStyle dash)
        {
            DashStyle = dash;
            Color = color;
            Width = new Css.LengthPixel(1);
        }

        protected PenSolidColor(Css.Color color, Css.BorderStyle dash, Css.LengthPixel width)
        {
            DashStyle = dash;
            Color = color;
            Width = width;
        }


        public Css.Color Color { get; set; }
    }
}
