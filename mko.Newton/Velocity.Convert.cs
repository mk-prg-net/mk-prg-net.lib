using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;
using System.Diagnostics;


namespace mko.Newton
{
    public partial class Velocity
    {
        // per Sec
        public static VelocityInMeterPerSec<Mag.Milli> MillimeterPerSec(Velocity V)
        {
            return new VelocityInMeterPerSec<Mag.Milli>(V);
        }

        public static VelocityInMeterPerSec<Mag.Centi> CentimeterPerSec(Velocity V)
        {
            return new VelocityInMeterPerSec<Mag.Centi>(V);
        }

        public static VelocityInMeterPerSec<Mag.One> MeterPerSec(Velocity V)
        {
            return new VelocityInMeterPerSec<Mag.One>(V);
        }

        public static VelocityInMeterPerSec<Mag.Kilo> KilometerPerSec(Velocity V)
        {
            return new VelocityInMeterPerSec<Mag.Kilo>(V);
        }

        // per Minute

        public static VelocityInMeterPerMinute<Mag.Milli> MillimeterPerMinute(Velocity V)
        {
            return new VelocityInMeterPerMinute<Mag.Milli>(V);
        }

        public static VelocityInMeterPerMinute<Mag.Centi> CentimeterPerMinute(Velocity V)
        {
            return new VelocityInMeterPerMinute<Mag.Centi>(V);
        } 

        public static VelocityInMeterPerMinute<Mag.One> MeterPerMinute(Velocity V)
        {
            return new VelocityInMeterPerMinute<Mag.One>(V);
        }

        public static VelocityInMeterPerMinute<Mag.Kilo> KilometerPerMinute(Velocity V)
        {
            return new VelocityInMeterPerMinute<Mag.Kilo>(V);
        }


        // per Hour
        public static VelocityInKilometerPerHour KilometerPerHour(Velocity V)
        {
            return new VelocityInKilometerPerHour(V);

        }


        // mph
        public static VelocityInMilesPerHour MilesPerHour(Velocity V)
        {
            return new VelocityInMilesPerHour(V);
        }

        // Knots
        public static VelocityInKnots Knots(Velocity V)
        {
            return new VelocityInKnots(V);
        }


    }
}
