using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Css = mkoIt.Xhtml.Css;
using Nwt = mko.Newton;


namespace mko.Graphic.WebClient
{
    public class BrushSolidColor : mko.Graphic.BrushSolidColor
    {
        public BrushSolidColor() : base() { }
        public BrushSolidColor(Css.Color color) : base(color) {}

        public override bool Set(IPlotter plotter)
        {
            var webPlotter = plotter as CanvasPlotter;
            webPlotter.Context.Add(webPlotter.scriptBld.SetSolidColorFill(Color));
            return true;
        }
    }
}
