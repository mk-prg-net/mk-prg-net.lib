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
        public static VelocityInMeterPerSec<Mag.Milli> MillimeterPerSec(Acceleration a, Time t)
        {
            return MillimeterPerSec(Acceleration.CentimeterPerSec2(a), Time.Sec(t));
        }


        // cm/s
        public static VelocityInMeterPerSec<Mag.Centi> CentimeterPerSec(Acceleration a, Time t)
        {
            return CentimeterPerSec(AccelerationMulTime(Acceleration.CentimeterPerSec2(a), Time.Sec(t)));
        }


        // m/s
        public static VelocityInMeterPerSec<Mag.One> MeterPerSec(Acceleration a, Time t)
        {
            return MeterPerSec(AccelerationMulTime(Acceleration.MeterPerSec2(a), Time.Sec(t)));
        }


        // km/s
        public static VelocityInMeterPerSec<Mag.Kilo> KilometerPerSec(Acceleration a, Time t)
        {
            return KilometerPerSec(AccelerationMulTime(Acceleration.KilometerPerSec2(a), Time.Sec(t)));
        }

        // cm/min
        

        // m/min
        

        // km/Min
        

        // km/h
        

        public static VelocityInKilometerPerHour KilometerPerHour(Acceleration a, Time t)
        {
            return KilometerPerHour(AccelerationMulTime(Acceleration.MeterPerSec2(a), Time.Sec(t)));
        }

        //mph
       

        // knots
        
       

    }
}
