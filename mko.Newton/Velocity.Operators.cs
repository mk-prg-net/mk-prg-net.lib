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
        //------------------------------------------------------------------------------------------------------------
        // Operatoren

        public static Velocity AccelerationMulTime<TAcceleration, TTime>(TAcceleration A, TTime t)
            where TAcceleration : Acceleration
            where TTime : Time            
        {
            Debug.Assert(A.T.GetType() == t.GetType());
            return SCALE(t.Value, A.V);
        }

    }
}
