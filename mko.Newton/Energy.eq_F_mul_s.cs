using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Energy
    {
        public static EnergyInNm<Mag.One> Nm(Force F, Length s)
        {
            return Nm(Force.N(F).Vector * Length.Meter(s).Vector);
        }         

    }
}
