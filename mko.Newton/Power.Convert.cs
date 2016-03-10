using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;


namespace mko.Newton
{
    public partial class Power
    {
        public static PowerInWatt<Mag.Micro> MicroWatt(Power Value)
        {
            return new PowerInWatt<Mag.Micro>(Value);
        }

        public static PowerInWatt<Mag.Milli> MilliWatt(Power Value)
        {
            return new PowerInWatt<Mag.Milli>(Value);
        }

        // W
        public static PowerInWatt<Mag.One> Watt(Power Value)
        {
            return new PowerInWatt<Mag.One>(Value);
        }

        public static PowerInWatt<Mag.Kilo> KiloWatt(Power Value)
        {
            return new PowerInWatt<Mag.Kilo>(Value);
        }


        public static PowerInWatt<Mag.Mega> MegaWatt(Power Value)
        {
            return new PowerInWatt<Mag.Mega>(Value);
        }


        public static PowerInWatt<Mag.Giga> GigaWatt(Power Value)
        {
            return new PowerInWatt<Mag.Giga>(Value);
        }

        public static PowerInWatt<Mag.Terra> TerraWatt(Power Value)
        {
            return new PowerInWatt<Mag.Terra>(Value);
        }


    }
}
