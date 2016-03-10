using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;


namespace mko.Newton
{
    public partial class Energy
    {
        public static EnergyInNm<Mag.One> EpotInNm(Mass m, Acceleration g, Length height)
        {
            return Energy.Nm(Force.N(g, m).Vector * Length.Meter(height).Vector);
        }

        /// <summary>
        /// Für eine gegebene Masse und einen Beschleunigungsvektor wird der Hubweg als Vektor berechnet,
        /// welche der gegebenen potentiellen Energie entspricht.
        /// </summary>
        /// <param name="Epot"></param>
        /// <param name="g"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static LengthInMeter<Mag.One> LiftUpVectorInMeter(Energy Epot, Acceleration g, Mass m)
        {
            var gInMeterPerSec = Acceleration.MeterPerSec2(g).Vector;
            double gAbs = gInMeterPerSec.Length;
            gAbs *= gAbs;
            double fact = Energy.Nm(Epot).Value / (Mass.Kilogram(m).Value * gAbs);
            return Length.Meter((fact * gInMeterPerSec).coordinates);
        }
    }
}
