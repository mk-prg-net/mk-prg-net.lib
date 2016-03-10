using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Css = mkoIt.Xhtml.Css;
using Nwt = mko.Newton;


namespace mko.Graphic.WebClient
{
    public class PenSolidColor : mko.Graphic.PenSolidColor
    {
        public PenSolidColor() : base() { }
        public PenSolidColor(Css.Color color) : base(color) { }
        public PenSolidColor(Css.Color color, Css.BorderStyle dash) : base(color, dash) { }
        public PenSolidColor(Css.Color color, Css.BorderStyle dash, Css.LengthPixel width) : base(color, dash, width) { }

        public override bool Set(IPlotter plotter)
        {
            var webPlotter = plotter as CanvasPlotter;
            webPlotter.Context.Add(webPlotter.scriptBld.SetSolidStrokeStyle(Color, Width));
            return true;
        }
    }
}
