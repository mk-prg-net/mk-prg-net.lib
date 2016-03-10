using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;


namespace mko.Newton
{
    public partial class Energy
    {
        public static EnergyInWs<Mag.One> Ws(Power P, Time t)
        {
            return Ws(Power.Watt(P).Value * Time.Sec(t).Value);
        }

        public static EnergyInWh<Mag.One> Wh(Power P, Time t)
        {
            return Wh(Power.Watt(P).Value * Time.Hours(t).Value);
        }

        public static EnergyInWh<Mag.Kilo> KiloWh(Power P, Time t)
        {
            return KiloWh(Power.KiloWatt(P).Value * Time.Hours(t).Value);
        }



    }
}
