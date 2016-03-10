using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using mko.Euklid;
using mko.Newton;

namespace mko.Ballistik
{
    public class Widerstand
    {
        public static Force Luft(Velocity V, double Widerstandsfaktor)
        {
            var Vp = Velocity.MeterPerSec(V);
            double Vabs = Vp.Vector.Length;
            var Vn = Vp.Vector * (-1/Vabs);
            return Force.N(Vn * Vabs * Vabs* Widerstandsfaktor);
        }
    }
}
