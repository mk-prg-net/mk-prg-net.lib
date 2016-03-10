using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Css = mkoIt.Xhtml.Css;
using Asp = mkoIt.Asp.HtmlCtrl;
using Nwt = mko.Newton;
using System.Diagnostics;

namespace mko.Graphic.WebClient
{
    public class CanvasPlotterPath : IPath
    {
        Asp.Canvas.ClientComponentScriptBld scriptBld;
        GraphicContext ctx;

        public CanvasPlotterPath(Asp.Canvas.ClientComponentScriptBld scriptBld, GraphicContext ctx)
        {
            
            this.scriptBld = scriptBld;
            this.ctx = ctx;

            Debug.WriteLine(scriptBld.beginPath());
            ctx.Add(scriptBld.beginPath());
        }

        public void moveTo(Euklid.Vector p)
        {
            Debug.WriteLine(scriptBld.moveTo(p[0], p[1]));
            ctx.Add(scriptBld.moveTo(p[0], p[1]));
        }

        public void lineTo(Euklid.Vector p)
        {
            Debug.WriteLine(scriptBld.lineTo(p[0], p[1]));
            ctx.Add(scriptBld.lineTo(p[0], p[1]));
        }

        public void circle(Euklid.Vector center, double radius)
        {
            Debug.WriteLine(scriptBld.arcTo(center[0], center[1], radius, 0.0, 2 * Math.PI));
            ctx.Add(scriptBld.arcTo(center[0], center[1], radius, 0.0, 2 * Math.PI));
        }

        public void arc(Euklid.Vector center, double radius, mko.Newton.Angle startAngle, mko.Newton.Angle endAngleRad)
        {
            Debug.WriteLine(scriptBld.arcTo(center[0], center[1], radius, Nwt.Angle.Rad(startAngle).Vector[0], Nwt.Angle.Rad(endAngleRad).Vector[0]));
            ctx.Add(scriptBld.arcTo(center[0], center[1], radius, Nwt.Angle.Rad(startAngle).Vector[0], Nwt.Angle.Rad(endAngleRad).Vector[0]));
        }

        public void arcAnticlockwise(Euklid.Vector center, double radius, mko.Newton.Angle startAngle, mko.Newton.Angle endAngleRad)
        {
            throw new NotImplementedException();
        }

        public void fill()
        {
            Debug.WriteLine(scriptBld.fillPath());
            ctx.Add(scriptBld.fillPath());
        }

        public void stroke()
        {
            Debug.WriteLine(scriptBld.strokePath());
            ctx.Add(scriptBld.strokePath());
        }

        public void Dispose()
        {
            Debug.WriteLine(scriptBld.closePath());
            ctx.Add(scriptBld.closePath());
            
        }
    }
}
