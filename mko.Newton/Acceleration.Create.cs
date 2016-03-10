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
        public static AccelerationInMeterPerSec<Mag.Centi> CentimeterPerSec2(params double[] coordinates)
        {
            return new AccelerationInMeterPerSec<Mag.Centi>(coordinates);
        }

        public static AccelerationInMeterPerSec<Mag.Centi> CentimeterPerSec2(E.Vector V)
        {
            return CentimeterPerSec2(V.coordinates);
        }


        // m.s-2

        public static AccelerationInMeterPerSec<Mag.One> MeterPerSec2(params double[] coordinates)
        {
            return new AccelerationInMeterPerSec<Mag.One>(coordinates);
        }

        public static AccelerationInMeterPerSec<Mag.One> MeterPerSec2(E.Vector V)
        {
            return MeterPerSec2(V.coordinates);
        }
        
        // km.s-2
        public static AccelerationInMeterPerSec<Mag.Kilo> KilometerPerSec2(params double[] coordinates)
        {
            return new AccelerationInMeterPerSec<Mag.Kilo>(coordinates);
        }

        public static AccelerationInMeterPerSec<Mag.Kilo> KilometerPerSec2(E.Vector V)
        {
            return KilometerPerSec2(V.coordinates);
        }



        

    }
}
