using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using mko.Euklid;
using mko.Newton;

namespace mko.Ballistik
{
    public class Auftrieb
    {
        public static Force Linear(Velocity V, double Auftriebsfaktor)
        {

            var N = Vector.Normale2D(V.Vector);
            double af = Velocity.MeterPerSec(V).Vector.Length * Auftriebsfaktor;            

            return Force.N(af * N);
        }
    }
}
