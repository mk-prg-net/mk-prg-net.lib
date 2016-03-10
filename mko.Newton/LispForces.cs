using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;

namespace mko.Newton.Vector
{
    public class Forces : MeasuredValuesVector<MeasuredValueF, MeasuredValueF.UnitF>
    {
        public Forces(Forces f) : base(f) { }
        public Forces(int countValues) : base(countValues) { }
        public Forces(MeasuredValueF.UnitF unit, int countValues) : base(unit, countValues) { }
        public Forces(MeasuredValueF.UnitF unit, params double[] values) : base(unit, values) { }
        public Forces(MeasuredValueF.UnitF unit, E.Vector Vector) : base(unit, Vector) { }
        public Forces(MeasuredValueF.UnitF unit, params MeasuredValueF[] values) : base(unit, values) { }

        /// <summary>
        /// Vectoraddition von Kräften
        /// </summary>
        /// <param name="F1"></param>
        /// <param name="F2"></param>
        /// <returns></returns>
        public static Forces operator +(Forces F1, Forces F2)
        {
            if (F1.Unit == F2.Unit)
                return new Forces(F1.Unit, F1.Vector + F2.Vector);
            else
                return new Forces(F1.BaseUnit, F1.VectorInBaseUnit + F2.VectorInBaseUnit);
        }



        /// <summary>
        /// Definition der Beschleunigung nach Newtons Grundgesetz F = m*a
        /// </summary>
        /// <param name="F"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Accelerations operator /(Forces F, MeasuredValueM m)
        {
            return new Accelerations(new MeasuredValueAcc().BaseUnit, F.VectorInBaseUnit * (1 / m.ValueInBaseUnit));
        }

        /// <summary>
        /// Definition der Masse (Trägheit) nach Newton (Skalar)
        /// </summary>
        /// <param name="m"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        public static MeasuredValueM operator /(Forces F, Accelerations a)
        {
            return new MeasuredValueM() { Value = F.VectorInBaseUnit.Length / a.VectorInBaseUnit.Length };
        }



    }
}
