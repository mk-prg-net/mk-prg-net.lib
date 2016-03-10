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
    public abstract class BrushSolidColor : Brush
    {
        protected BrushSolidColor()
        {
            Color = Css.Color.Gray;
        }

        protected BrushSolidColor(Css.Color color)
        {
            Color = color;
        }


        public Css.Color Color { get; set; }

    }
}
