using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;
namespace mko.Newton
{
    public partial class Force
    {
        public static ForceInNewton<Mag.Milli> mN(Acceleration a, Mass m)
        {
            return mN(Acceleration.MeterPerSec2(a).Vector * Mass.Kilogram(m).Value);
        }

        public static ForceInNewton<Mag.One> N(Acceleration a, Mass m)
        {
            return N(Acceleration.MeterPerSec2(a).Vector * Mass.Kilogram(m).Value);
        }

        public static ForceInNewton<Mag.Kilo> KN(Acceleration a, Mass m)
        {
            return KN(Acceleration.MeterPerSec2(a).Vector * Mass.Kilogram(m).Value);
        }

        public static ForceInNewton<Mag.Mega> MN(Acceleration a, Mass m)
        {
            return MN(Acceleration.MeterPerSec2(a).Vector * Mass.Kilogram(m).Value);
        }

        public static ForceInNewton<Mag.Giga> GN(Acceleration a, Mass m)
        {
            return GN(Acceleration.MeterPerSec2(a).Vector * Mass.Kilogram(m).Value);
        }
    }
}
