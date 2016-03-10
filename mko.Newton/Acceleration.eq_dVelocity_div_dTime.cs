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
        public static Acceleration CentimeterPerSec2(Velocity dV, Time dt)
        {
            return CentimeterPerSec2(Velocity.CentimeterPerSec(dV).Vector * (1 / Time.Sec(dt).Value));
        }

        // m.s-2            

        public static Acceleration MeterPerSec2(Velocity dV, Time dt)
        {
            return MeterPerSec2(Velocity.MeterPerSec(dV).Vector * (1 / Time.Sec(dt).Value));
        }        

        // km.s-2
        public static Acceleration KilometerPerSec2(Velocity dV, Time dt)
        {
            return MeterPerSec2(Velocity.KilometerPerSec(dV).Vector * (1 / Time.Sec(dt).Value));
        }

    }
}
