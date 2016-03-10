using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using N = mko.Newton;
using F = mko.Algo.FunctionalProgramming.Fn;

namespace mko.Newton.UI
{
    public class Mass
    {
        public enum Unit
        {
            mg,
            g,
            kg,
            t,
            ct,
            oz
        }

        /// <summary>
        /// Einlesen eines Gewichtes aus einem Text, wobei die Wunscheinheit durch ein
        /// Unit- Enum definiert wird
        /// </summary>
        /// <param name="ValueTxt"></param>
        /// <param name="unit"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool TryParse(string ValueTxt, Unit unit, out N.Mass t)
        {

            t = null;
            double Value;
            if (double.TryParse(ValueTxt, out Value))
            {
                t = Constructor[unit](Value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Konstruktoren: Erzeugen aus einem Koordinatenarray einen Geschwindigkeitsvektor
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        public delegate N.Mass DgConstructor(double Value);

        /// <summary>
        /// Zuordnung aller 
        /// </summary>
        public static Dictionary<Unit, DgConstructor> Constructor =
            new Dictionary<Unit, DgConstructor>
            {
                {Unit.ct, new DgConstructor(F.Curry<Func<double, N.MassInCarat>, double, N.Mass>(Adapter, N.Mass.Carat))},
                {Unit.g, new DgConstructor(F.Curry<Func<double, N.MassInGram<N.OrderOfMagnitude.One>>, double, N.Mass>(Adapter, N.Mass.Gram))},
                {Unit.mg, new DgConstructor(F.Curry<Func<double, N.MassInGram<N.OrderOfMagnitude.Milli>>, double, N.Mass>(Adapter, N.Mass.Milligram))},
                {Unit.oz, new DgConstructor(F.Curry<Func<double, N.MassInOunce>, double, N.Mass>(Adapter, N.Mass.Ounce))},
                {Unit.t, new DgConstructor(F.Curry<Func<double, N.MassInGram<N.OrderOfMagnitude.Mega>>, double, N.Mass>(Adapter, N.Mass.Tons))},                
                {Unit.kg, new DgConstructor(F.Curry<Func<double, N.MassInGram<N.OrderOfMagnitude.Kilo>>, double, N.Mass>(Adapter, N.Mass.Kilogram))},                
            };

        static N.Mass Adapter<To>(Func<double, To> Constructor, double Values)
            where To : N.Mass
        {
            return Constructor(Values);
        }

    }
}
