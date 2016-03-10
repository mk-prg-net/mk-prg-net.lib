using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ekd = mko.Euklid;
using Css = mkoIt.Xhtml.Css;

namespace mko.Graphic
{
    public class Line : Shape
    {
        Ekd.Vector P1;
        Ekd.Vector P2;

        public Line(Ekd.Vector P1, Ekd.Vector P2, Brush brush, Pen pen)
            : base(brush, pen)
        {
            this.P1 = new Ekd.Vector(P1);
            this.P2 = new Ekd.Vector(P2);
        }

        public Line(Ekd.Line line, Brush brush, Pen pen)
            : base(brush, pen)
        {
            P1 = new Ekd.Vector(line.P1);
            P2 = new Ekd.Vector(line.P2);
        }

        public Line(Ekd.Line line, double factP1, double factP2, Brush brush, Pen pen)
            : base(brush, pen)
        {
            P1 = new Ekd.Vector(line.P1 + (line.Direction * factP1));
            P2 = new Ekd.Vector(line.P2 + (line.Direction * factP2));
        }

        public override bool draw(IPlotter plotter)
        {
            using (var path = plotter.BeginPath(Brush, Pen))
            {
                path.moveTo(P1);
                path.lineTo(P2);

                if(Outline) path.stroke();
                if (Fill) path.fill();

                return true;
            }
        }
    }
}
