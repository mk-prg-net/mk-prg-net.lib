using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;
using System.Diagnostics;

namespace mko.Newton
{
    public partial class Velocity
    {
        // mm/s
        public static VelocityInMeterPerSec<Mag.Milli> MillimeterPerSec(params double[] coordinates)
        {
            return new VelocityInMeterPerSec<Mag.Milli>(coordinates);
        }

        public static VelocityInMeterPerSec<Mag.Milli> MillimeterPerSec(E.Vector V)
        {
            return new VelocityInMeterPerSec<Mag.Milli>(V);
        }



        // cm/s
        public static VelocityInMeterPerSec<Mag.Centi> CentimeterPerSec(params double[] coordinates)
        {
            return new VelocityInMeterPerSec<Mag.Centi>(coordinates);
        }

        public static VelocityInMeterPerSec<Mag.Centi> CentimeterPerSec(E.Vector V)
        {
            return new VelocityInMeterPerSec<Mag.Centi>(V);
        }



        // m/s
        public static VelocityInMeterPerSec<Mag.One> MeterPerSec(params double[] coordinates)
        {
            return new VelocityInMeterPerSec<Mag.One>(coordinates);
        }

        public static VelocityInMeterPerSec<Mag.One> MeterPerSec(E.Vector V)
        {
            return new VelocityInMeterPerSec<Mag.One>(V);
        }


        // km/s
        public static VelocityInMeterPerSec<Mag.Kilo> KilometerPerSec(params double[] coordinates)
        {
            return new VelocityInMeterPerSec<Mag.Kilo>(coordinates);
        }

        public static VelocityInMeterPerSec<Mag.Kilo> KilometerPerSec(E.Vector V)
        {
            return new VelocityInMeterPerSec<Mag.Kilo>(V);
        }


        // cm/min
        public static VelocityInMeterPerMinute<Mag.Centi> CentimeterPerMinute(params double[] coordinates)
        {
            return new VelocityInMeterPerMinute<Mag.Centi>(coordinates);
        }

        public static VelocityInMeterPerMinute<Mag.Centi> CentimeterPerMinute(E.Vector V)
        {
            return new VelocityInMeterPerMinute<Mag.Centi>(V);
        }


        // m/min
        public static VelocityInMeterPerMinute<Mag.One> MeterPerMinute(params double[] coordinates)
        {
            return new VelocityInMeterPerMinute<Mag.One>(coordinates);
        }

        public static VelocityInMeterPerMinute<Mag.One> MeterPerMinute(E.Vector V)
        {
            return new VelocityInMeterPerMinute<Mag.One>(V);
        }


        // km/Min
        public static VelocityInMeterPerMinute<Mag.Kilo> KilometerPerMinute(params double[] coordinates)
        {
            return new VelocityInMeterPerMinute<Mag.Kilo>(coordinates);
        }

        public static VelocityInMeterPerMinute<Mag.Kilo> KilometerPerMinute(E.Vector V)
        {
            return new VelocityInMeterPerMinute<Mag.Kilo>(V);
        }


        // km/h
        public static VelocityInKilometerPerHour KilometerPerHour(params double[] coordinates)
        {
            return new VelocityInKilometerPerHour(coordinates);
        }

        public static VelocityInKilometerPerHour KilometerPerHour(E.Vector V)
        {
            return new VelocityInKilometerPerHour(V);
        }



        //mph
        public static VelocityInMilesPerHour MilesPerHour(params double[] coordinates)
        {
            return new VelocityInMilesPerHour(coordinates);
        }

        public static VelocityInMilesPerHour MilesPerHour(E.Vector V)
        {
            return new VelocityInMilesPerHour(V);
        }


        // knots
        public static VelocityInKnots Knots(params double[] coordinates)
        {
            return new VelocityInKnots(coordinates);
        }

        public static VelocityInKnots Knots(E.Vector V)
        {
            return new VelocityInKnots(V);
        }
    }
}
