using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ekd = mko.Euklid;
using Css = mkoIt.Xhtml.Css;
using System.Diagnostics;

namespace mko.Graphic
{
    public class Triangle : Shape
    {

        public Ekd.Vector Corner0 { get; set; }

        public Ekd.Vector Corner1 { get; set; }

        public Ekd.Vector Corner2 { get; set; }


        /// <summary>
        /// Dreieck, definiert durch drei Eckpunkte
        /// </summary>
        /// <param name="P0"></param>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        public Triangle(Ekd.Vector P0, Ekd.Vector P1, Ekd.Vector P2, Brush brush, Pen pen)
            : base(brush, pen)
        {
            Corner0 = new Ekd.Vector(P0);
            Corner1 = new Ekd.Vector(P1);
            Corner2 = new Ekd.Vector(P2);
        }


        /// <summary>
        /// Dreieck, definiert durch die Schnittpunkte dreier Linien
        /// </summary>
        /// <param name="line0"></param>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        public Triangle(Ekd.Line line0, Ekd.Line line1, Ekd.Line line2, double Epsilon, Brush brush, Pen pen)
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
            if (!line2.IntersectionWith(line0, out corn2))
                throw new ArgumentException();

            Corner0 = corn0;
            Corner1 = corn1;
            Corner2 = corn2;
        }

        public override bool draw(IPlotter plotter)
        {
            using(var path = plotter.BeginPath(Brush, Pen))
            {
                path.moveTo(Corner0);
                path.lineTo(Corner1);
                path.lineTo(Corner2);
                path.lineTo(Corner0);

                if(Outline) path.stroke();
                if (Fill) path.fill();

                return true;
            }
        }
    }
}
