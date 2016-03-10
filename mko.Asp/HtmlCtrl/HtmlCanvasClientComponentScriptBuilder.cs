using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Css = mkoIt.Xhtml.Css;

namespace mkoIt.Asp.HtmlCtrl
{
    public partial class Canvas
    {
        public class ClientComponentScriptBld : ClientComponentScriptBldBase
        {
            public System.Globalization.NumberFormatInfo jsNif = new System.Globalization.CultureInfo("en-US").NumberFormat;

            public ClientComponentScriptBld(string IdClientComponent)
                : base(IdClientComponent)
            {                
            }

            // Grafikkontext sichern/wiederherstellen

            public string save()
            {
                return CreateMethodCall("save");
            }

            public string restore()
            {
                return CreateMethodCall("restore");
            }

            // Transformationen im Grafikkontext setzen

            public string translate(double tx, double ty)
            {
                return CreateMethodCall("translate", tx.ToString(jsNif), ty.ToString(jsNif));
            }

            public string scale(double sx, double sy)
            {
                return CreateMethodCall("scale", sx.ToString(jsNif), sy.ToString(jsNif));
            }

            public string rotate(double angleInRad)
            {
                return CreateMethodCall("rotate", angleInRad.ToString(jsNif));
            }

            // Allgemeine Eigenschaften des Grafikkontextes

            public string SetSolidColorFill(Css.Color color)
            {
                return CreateMethodCall("setSolidColorFill", "\'" + color.ToHtmlColorString() + "\'");
            }


            public string SetSolidStrokeStyle(Css.Color color, Css.LengthPixel length)
            {
                return CreateMethodCall("setSolidStrokeStyle", "\'" + color.ToHtmlColorString() + "\'", length.Value.ToString(jsNif));
            }

            public string beginPath()
            {
                return CreateMethodCall("beginPath");
            }

            public string closePath()
            {
                return CreateMethodCall("closePath");
            }

            public string fillPath()
            {
                return CreateMethodCall("fillPath");
            }

            public string strokePath()
            {
                return CreateMethodCall("strokePath");
            }

            public string moveTo(double x, double y)
            {
                return CreateMethodCall("moveTo", x.ToString(jsNif), y.ToString(jsNif));
            }

            public string lineTo(double x, double y)
            {
                return CreateMethodCall("lineTo", x.ToString(jsNif), y.ToString(jsNif));
            }


            public string arcTo(double mx, double my, double r, double startAngle, double stopAngle)
            {
                return CreateMethodCall("arcTo", mx.ToString(jsNif), my.ToString(jsNif), r.ToString(jsNif), startAngle.ToString(jsNif), stopAngle.ToString(jsNif));
            }

            // Zeichenfunktionsscripts

            public string DrawRect(int left, int top, int widht, int height)
            {
                return CreateMethodCallAndReturn("drawRect", left.ToString(jsNif), top.ToString(jsNif), widht.ToString(jsNif), height.ToString(jsNif));
            }


        }
    }
}
