using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Time
    {
        public static TimeInSec<Mag.Pico> PicoSec(Time value)
        {
            return new TimeInSec<Mag.Pico>(value);
        }

        public static TimeInSec<Mag.Nano> NanoSec(Time value)
        {
            return new TimeInSec<Mag.Nano>(value);
        }

        public static TimeInSec<Mag.Micro> MicroSec(Time value)
        {
            return new TimeInSec<Mag.Micro>(value);
        }

        public static TimeInSec<Mag.Milli> MilliSec(Time value)
        {
            return new TimeInSec<Mag.Milli>(value);
        }

        public static TimeInSec<Mag.One> Sec(Time value)
        {
            return new TimeInSec<Mag.One>(value);
        }

        public static TimeInMinutes Minutes(Time value)
        {
            return new TimeInMinutes(value);
        }

        public static TimeInHours Hours(Time value)
        {
            return new TimeInHours(value);
        }

        public static TimeInDays Days(Time value)
        {
            return new TimeInDays(value);
        }

    }
}
