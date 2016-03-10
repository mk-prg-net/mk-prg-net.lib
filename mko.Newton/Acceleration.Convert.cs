using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Acceleration
    {

        public static AccelerationInMeterPerSec<Mag.Centi> CentimeterPerSec2(Acceleration a)
        {
            return new AccelerationInMeterPerSec<Mag.Centi>(a);
        }

        public static AccelerationInMeterPerSec<Mag.One> MeterPerSec2(Acceleration a)
        {
            return new AccelerationInMeterPerSec<Mag.One>(a);
        }

        public static AccelerationInMeterPerSec<Mag.Kilo> KilometerPerSec2(Acceleration a)
        {
            return new AccelerationInMeterPerSec<Mag.Kilo>(a);
        }


    }
}
