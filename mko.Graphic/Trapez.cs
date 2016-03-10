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
    public class Trapez : Shape
    {
        public Ekd.Vector Corner0 { get; set; }

        public Ekd.Vector Corner1 { get; set; }

        public Ekd.Vector Corner2 { get; set; }

        public Ekd.Vector Corner3 { get; set; }


        public Trapez(Ekd.Vector P0, Ekd.Vector P1, Ekd.Vector P2, Ekd.Vector P3, Brush brush, Pen pen)
            :base(brush, pen)
        {
            Corner0 = new Ekd.Vector(P0);
            Corner1 = new Ekd.Vector(P1);
            Corner2 = new Ekd.Vector(P2);
            Corner3 = new Ekd.Vector(P3);

        }

        public Trapez(Ekd.Line line0, Ekd.Line line1, Ekd.Line line2, Ekd.Line line3, Brush brush, Pen pen)
            : base(brush, pen)
        {
            Debug.Assert(line0.Dimensions == 2);
            Debug.Assert(line1.Dimensions == 2);
            Debug.Assert(line2.Dimensions == 2);

            Ekd.Vector corn0;
            if (!line0.IntersectionWith(line1, out corn0))
                throw new ArgumentException();

            Ekd.Vector corn1;
            if (!line1.IntersectionWith(line2, out corn1))
                throw new ArgumentException();

            Ekd.Vector corn2;
            if (!line2.IntersectionWith(line3, out corn2))
                throw new ArgumentException();

            Ekd.Vector corn3;
            if (!line3.IntersectionWith(line0, out corn3))
                throw new ArgumentException();

            Corner0 = corn0;
            Corner1 = corn1;
            Corner2 = corn2;
            Corner3 = corn3;
        }


        public override bool draw(IPlotter plotter)
        {
            using (var path = plotter.BeginPath(Brush, Pen))
            {
                path.moveTo(Corner0);
                path.lineTo(Corner1);
                path.lineTo(Corner2);
                path.lineTo(Corner3);
                path.lineTo(Corner0);

                if (Outline) path.stroke();
                if (Fill) path.fill();

                return true;
            }
        }
    }
}
