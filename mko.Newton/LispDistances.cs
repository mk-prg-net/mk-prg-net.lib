using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;

namespace mko.Newton.Vector
{
    public class Distances : MeasuredValuesVector<Distance, Distance.UnitS>
    {
        
        public Distances(Distances v) : base(v) { }
        public Distances(int countValues) : base(countValues) { }
        public Distances(Distance.UnitS unit, int countValues) : base(unit, countValues) { }
        public Distances(Distance.UnitS unit, params double[] values) : base(unit, values) { }
        public Distances(Distance.UnitS unit, E.Vector Vector) : base(unit, Vector) { }
        public Distances(Distance.UnitS unit, params Distance[] values) : base(unit, values) { }
        
        /// <summary>
        /// Skalierung eines Länge
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="s1"></param>
        /// <returns></returns>
        public static Distances operator *(double factor, Distances s1)
        {
            return new Distances(s1.Unit, s1.Vector * factor);
        }

        public static Distances operator *(Distances s1, double factor)
        {
            return factor * s1;
        }

        /// <summary>
        /// Vektoraddition von gerichteten Abständen
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static Distances operator +(Distances s1, Distances s2)
        {
            if (s1.Unit == s2.Unit)
                return new Distances(s1.Unit, s1.Vector + s2.Vector);
            else
                return new Distances(s1.BaseUnit, s1.VectorInBaseUnit + s2.VectorInBaseUnit);
        }

        /// <summary>
        /// Differenzierung des Weges über der Zeit zur Geschwindigkeit
        /// </summary>
        /// <param name="ds">Zurückgelegter Weg</param>
        /// <param name="dt">Zeitintervall</param>
        /// <returns></returns>
        public static Velocities operator /(Distances ds, MeasuredValueT dt)
        {
            return new Velocities(MeasuredValueV.CreateUnit(ds.Unit, dt.Unit), ds.Vector * (1 / dt.Value));
        }
    }
}
