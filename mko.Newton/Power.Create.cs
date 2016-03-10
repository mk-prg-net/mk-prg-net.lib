using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Power
    {
        public static PowerInWatt<Mag.Micro> MicroWatt(double Value)
        {
            return new PowerInWatt<Mag.Micro>(Value);
        }

        public static PowerInWatt<Mag.Milli> MilliWatt(double Value)
        {
            return new PowerInWatt<Mag.Milli>(Value);
        }

        // W
        public static PowerInWatt<Mag.One> Watt(double Value)
        {
            return new PowerInWatt<Mag.One>(Value);
        }


        public static PowerInWatt<Mag.Kilo> KiloWatt(double Value)
        {
            return new PowerInWatt<Mag.Kilo>(Value);
        }


        public static PowerInWatt<Mag.Mega> MegaWatt(double Value)
        {
            return new PowerInWatt<Mag.Mega>(Value);
        }


        public static PowerInWatt<Mag.Giga> GigaWatt(double Value)
        {
            return new PowerInWatt<Mag.Giga>(Value);
        }

        public static PowerInWatt<Mag.Terra> TerraWatt(double Value)
        {
            return new PowerInWatt<Mag.Terra>(Value);
        }


        public static PowerInHorsePower HorsePower(double Value)
        {
            return new PowerInHorsePower(Value);
        }


        public static PowerInPS PS(double Value)
        {
            return new PowerInPS(Value);
        }
        
    }
}
