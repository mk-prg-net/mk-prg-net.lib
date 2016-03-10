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
        // mm/s
        public static VelocityInMeterPerSec<Mag.Milli> MillimeterPerSec(Length s, Time t)
        {
            return MillimeterPerSec(Length.Millimeter(s).Vector / Time.Sec(t).Value);
        }

        // cm/s
        public static VelocityInMeterPerSec<Mag.Centi> CentimeterPerSec(Length s, Time t)
        {
            return CentimeterPerSec(Length.Centimeter(s).Vector / Time.Sec(t).Value);
        }

        // m/s
        public static VelocityInMeterPerSec<Mag.One> MeterPerSec(Length s, Time t)
        {
            return MeterPerSec(Length.Meter(s).Vector / Time.Sec(t).Value);

        }

        // km/s
        public static VelocityInMeterPerSec<Mag.Kilo> KilometerPerSec(Length s, Time t)
        {
            return KilometerPerSec(Length.Kilometer(s).Vector / Time.Sec(t).Value);

        }

        // cm/min
        public static VelocityInMeterPerMinute<Mag.Centi> CentimeterPerMinute(Length s, Time t)
        {
            return CentimeterPerMinute(Length.Centimeter(s).Vector / Time.Minutes(t).Value);

        }

        // m/min
        public static VelocityInMeterPerMinute<Mag.One> MeterPerMinute(Length s, Time t)
        {
            return MeterPerMinute(Length.Meter(s).Vector / Time.Minutes(t).Value);
        }

        // km/Min
        public static VelocityInMeterPerMinute<Mag.Kilo> KilometerPerMinute(Length s, Time t)
        {
            return KilometerPerMinute(Length.Kilometer(s).Vector / Time.Minutes(t).Value);
        }

        // km/h        
        public static VelocityInKilometerPerHour KilometerPerHour(Length s, Time t)
        {
            return KilometerPerHour(Length.Kilometer(s).Vector / Time.Hours(t).Value);
        }

        //mph  
        public static VelocityInMilesPerHour MilesPerHour(Length s, Time t)
        {
            return MilesPerHour(Length.Miles(s).Vector / Time.Hours(t).Value);
        }

        // knots
        public static VelocityInKnots Knots(Length s, Time t)
        {
            return Knots(Length.NauticalMiles(s).Vector / Time.Hours(t).Value);
        }

    }
}
