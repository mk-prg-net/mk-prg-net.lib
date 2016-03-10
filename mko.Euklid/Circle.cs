using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Euklid
{
    public class Circle : Space
    {
        Vector _m;        

        public Circle(int dimensions)
            : base(dimensions)
        {
            _m = new Vector(dimensions);
            Radius = 1.0;
        }

        public Circle(Circle circle)
            : base(circle.Dimensions)
        {
            _m = new Vector(circle.Center);
            Radius = circle.Radius;
        }


        public Circle(Circle circle, Transformations.Transformation trafo)
            : base(circle.Dimensions)
        {
            _m = trafo.apply(circle.Center);

            // Radius vom Kreis im Bildbereich messen
            var vr = new Vector(circle.Center);
            vr[0] += circle.Radius;
            var vvr = trafo.apply(vr);
            Radius = (_m - vvr).Length;

        }

        public Vector Center
        {
            get
            {
                return _m;
            }
        }

        public double Radius { get; set; }

        public override bool Contains(Vector P, double Epsilon)
        {
            if (Vector.DistanceBetween(_m, P) <= Radius + Epsilon)
                return true;

            return false;

        }
    }
}

