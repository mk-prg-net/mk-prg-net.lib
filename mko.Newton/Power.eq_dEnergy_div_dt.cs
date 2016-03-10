using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Power
    {
        public static PowerInWatt<Mag.One> Watt(Energy E, Time t)
        {
            return new PowerInWatt<Mag.One>(Energy.Ws(E).Value / Time.Sec(t).Value);
        }

    }
}
