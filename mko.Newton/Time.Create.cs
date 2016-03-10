using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Time
    {
        public static TimeInSec<Mag.Pico> PicoSec(double value)
        {
            return new TimeInSec<Mag.Pico>(value);
        }

        public static TimeInSec<Mag.Nano> NanoSec(double value)
        {
            return new TimeInSec<Mag.Nano>(value);
        }

        public static TimeInSec<Mag.Micro> MicroSec(double value)
        {
            return new TimeInSec<Mag.Micro>(value);
        }

        public static TimeInSec<Mag.Milli> MilliSec(double value)
        {
            return new TimeInSec<Mag.Milli>(value);
        }

        public static TimeInSec<Mag.One> Sec(double value)
        {
            return new TimeInSec<Mag.One>(value);
        }

        public static TimeInMinutes Minutes(double value)
        {
            return new TimeInMinutes(value);
        }

        public static TimeInHours Hours(double value)
        {
            return new TimeInHours(value);
        }

        public static TimeInDays Days(double value)
        {
            return new TimeInDays(value);
        }
    }
}
