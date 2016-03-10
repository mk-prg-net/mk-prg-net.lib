using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;

namespace mko.Newton.Vector
{
    public class Velocities : MeasuredValuesVector<MeasuredValueV, MeasuredValueV.UnitV>
    {
        public Velocities(Velocities v) : base(v) { }
        public Velocities(int countValues) : base(countValues) { }
        public Velocities(MeasuredValueV.UnitV unit, int countValues) : base(unit, countValues) { }
        public Velocities(MeasuredValueV.UnitV unit, params double[] values) : base(unit, values) { }
        public Velocities(MeasuredValueV.UnitV unit, E.Vector Vector) : base(unit, Vector) { }
        public Velocities(MeasuredValueV.UnitV unit, params MeasuredValueV[] values) : base(unit, values) { }

        /// <summary>
        /// Einheit von V als Quotient Einheit Weg/Einheit Zeit
        /// </summary>
        public Tuple<Distance.UnitS, MeasuredValueT.UnitT> UnitAsQuotient
        {
            get
            {
                return MeasuredValueV.GetVelocityUnitAsQuotient(Unit);
            }
        }
        
        /// <summary>
        /// Vectoraddition von Geschwindigkeiten
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static Velocities operator +(Velocities v1, Velocities v2)
        {
            if (v1.Unit == v2.Unit)
                return new Velocities(v1.Unit, v1.Vector + v2.Vector);
            else
                return new Velocities(v1.BaseUnit, v1.VectorInBaseUnit + v2.VectorInBaseUnit);
        }


        /// <summary>
        /// Integration des Weges über der Zeit
        /// </summary>
        /// <param name="dv">Momentangeschwindigkeit</param>
        /// <param name="dt">Zeitintervall</param>
        /// <returns>Weg in der Einheit, die im Zähler der Einheit von dv steht</returns>
        public static Distances operator *(Velocities dv, MeasuredValueT dt)
        {
            var q = MeasuredValueV.GetVelocityUnitAsQuotient(dv.Unit);            
            return new Distances(q.Item1, dv.Vector * dt.ConvertTo(q.Item2));
        }

        public static Distances operator *(MeasuredValueT dt, Velocities dv)
        {
            return dv * dt;
        }

        /// <summary>
        /// Differenzenqutient der Geschwindigkeit über der Zeit
        /// </summary>
        /// <param name="dv">Momentangeschwindigkeit</param>
        /// <param name="dt">Zeitintervall</param>
        /// <returns></returns>
        public static Accelerations operator /(Velocities dv, MeasuredValueT dt)
        {
            var q = dv.UnitAsQuotient;            
            return new Accelerations(MeasuredValueAcc.CreateUnit(q.Item1, dt.Unit), dv.Vector * (1/dt.ConvertTo(q.Item2)));
        }
    }
}
