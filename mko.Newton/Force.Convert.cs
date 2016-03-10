using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Force
    {
        public static ForceInNewton<Mag.Milli> mN(Force F)
        {
            return new ForceInNewton<Mag.Milli>(F);
        }

        public static ForceInNewton<Mag.One> N(Force F)
        {
            return new ForceInNewton<Mag.One>(F);
        }

        public static ForceInNewton<Mag.Kilo> KN(Force F)
        {
            return new ForceInNewton<Mag.Kilo>(F);
        }

        public static ForceInNewton<Mag.Mega> MN(Force F)
        {
            return new ForceInNewton<Mag.Mega>(F);
        }

        public static ForceInNewton<Mag.Giga> GN(Force F)
        {
            return new ForceInNewton<Mag.Giga>(F);
        }


    }
}
