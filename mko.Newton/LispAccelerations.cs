using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;

namespace mko.Newton.Vector
{
    public class Accelerations : MeasuredValuesVector<MeasuredValueAcc, MeasuredValueAcc.UnitAcc>
    {
        public Accelerations(Accelerations a) : base(a) { }
        public Accelerations(int countValues) : base(countValues) { }
        public Accelerations(MeasuredValueAcc.UnitAcc unit, int countValues) : base(unit, countValues) { }
        public Accelerations(MeasuredValueAcc.UnitAcc unit, params double[] values) : base(unit, values) { }
        public Accelerations(MeasuredValueAcc.UnitAcc unit, E.Vector Vector) : base(unit, Vector) { }
        public Accelerations(MeasuredValueAcc.UnitAcc unit, params MeasuredValueAcc[] values) : base(unit, values) { }

        /// <summary>
        /// Vectoraddition von Beschleunigungen
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static Accelerations operator +(Accelerations a1, Accelerations a2)
        {
            if (a1.Unit == a2.Unit)
                return new Accelerations(a1.Unit, a1.Vector + a2.Vector);
            else
                return new Accelerations(a1.BaseUnit, a1.VectorInBaseUnit + a2.VectorInBaseUnit);
        }



        /// <summary>
        /// Newtons Grundgesetz F = m * a
        /// </summary>
        /// <param name="a"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Forces operator *(Accelerations a, MeasuredValueM m)
        {

            return new Forces(MeasuredValueF.UnitF.N, a.VectorInBaseUnit * m.ValueInBaseUnit);
            
        }

        public static Forces operator *(MeasuredValueM m, Accelerations a)
        {
            return a * m;
        }

        
        /// <summary>
        /// Integration der Beschleunigung über der Zeit zur Geschwindigkeit
        /// </summary>
        /// <param name="da"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Velocities operator*(Accelerations da, MeasuredValueT dt){

            var q = MeasuredValueAcc.GetAccelerationUnitAsQuotient(da.Unit);            
            return new Velocities(q.Item1, da.Vector * dt.ConvertTo(q.Item2));
        }

        public static Velocities operator *(MeasuredValueT dt, Accelerations da)
        {
            return da * dt;
        }

        public Distances distanceAfter(MeasuredValueT dt)
        {

            return new Distances(Distance.UnitS.m, VectorInBaseUnit*(dt.ValueInBaseUnit*dt.ValueInBaseUnit/2.0));            

        }
    }
}
