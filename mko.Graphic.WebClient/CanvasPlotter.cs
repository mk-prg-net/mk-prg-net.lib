using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Css = mkoIt.Xhtml.Css;
using Asp = mkoIt.Asp.HtmlCtrl;
using System.Diagnostics;

namespace mko.Graphic.WebClient
{
    public class CanvasPlotter : IPlotter
    {
        public Asp.Canvas.ClientComponentScriptBld scriptBld;

        public CanvasPlotter(Asp.Canvas.ClientComponentScriptBld scriptBld)
        {
            this.scriptBld = scriptBld;
        }

        GraphicContext ctx = new GraphicContext();
        public GraphicContext Context
        {
            get
            {
                return ctx;
            }
        }

        public string GetScript(bool CreateControl)
        {
            if (CreateControl)
                return scriptBld.CreateScriptControl("mkoIt.Asp.Html.Graphic.D2") + ";" + Context.ToString() + ";";
            else
                return Context.ToString();
        }

        public IPath BeginPath(Brush brush, Pen pen)
        {
            brush.Set(this);
            pen.Set(this);

            return new CanvasPlotterPath(scriptBld, ctx);
        }


        public bool SetTrafo(Euklid.Transformations.RotationInCylindricalCoordinates t)
        {
            Context.Add(scriptBld.rotate(t.AngleInRad));
            return true;
        }

        public bool SetTrafo(Euklid.Transformations.RotationInSphericalCoordinates t)
        {
            throw new NotImplementedException();            
        }

        public bool SetTrafo(Euklid.Transformations.Scale t)
        {
            Debug.Assert(t.Dimensions >= 2);
            Context.Add(scriptBld.scale(t[0], t[1]));
            return true;
        }

        public bool SetTrafo(Euklid.Transformations.Translation t)
        {
            Debug.Assert(t.Dimensions >= 2);
            Context.Add(scriptBld.translate(t[0], t[1]));
            return true;
        }
    }
}
