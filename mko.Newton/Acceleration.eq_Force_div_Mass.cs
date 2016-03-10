using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;


namespace mko.Newton
{
    public partial class Acceleration
    {
        // cm/s2
        public static Acceleration CentimeterPerSec2(Force F, Mass m)
        {
            return CentimeterPerSec2(Force.N(F).A.Vector / (1 / Mass.Kilogram(m).Value));
        }

        // m.s-2
        public static Acceleration MeterPerSec2(Force F, Mass m)
        {
            return MeterPerSec2(Force.N(F).A.Vector / (1 / Mass.Kilogram(m).Value));
        }

        // km.s-2
        public static Acceleration KilometerPerSec2(Force F, Mass m)
        {
            return KilometerPerSec2(Force.N(F).A.Vector / (1 / Mass.Kilogram(m).Value));

        }


    }
}
