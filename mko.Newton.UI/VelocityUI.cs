using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using N = mko.Newton;

using F = mko.Algo.FunctionalProgramming.Fn;

namespace mko.Newton.UI
{
    public class Velocity
    {
        /// <summary>
        /// Aufzählung aller Geschwindigkeitseinheiten, die für eine Benutzeroberfläche bereitgestellt werden
        /// </summary>
        public enum Unit
        {
            m_per_s,
            km_per_s,
            m_per_min,
            km_per_hour,
            mi_per_hour,
            knots
        }

        /// <summary>
        /// Einlesen eines Geschwindigkeitsvetors aus einem Text, wobei die Wunscheinheit durch ein
        /// Unit- Enum definiert wird
        /// </summary>
        /// <param name="ValueTxt"></param>
        /// <param name="unit"></param>
        /// <param name="V"></param>
        /// <returns></returns>
        public static bool TryParse(string ValueTxt, Unit unit, out N.Velocity V)
        {

            V = null;
            double Value;
            if (double.TryParse(ValueTxt, out Value))
            {
                V = Constructor[unit](Value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Einlesen eines Geschwindigkeitsvetors aus einem Text, wobei die Wunscheinheit durch ein
        /// Unit- Enum definiert wird.
        /// Der Geschwindigkeitsvektor kann aus mehreren Komponenten bestehen
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="V"></param>
        /// <param name="ValuesTxt"></param>
        /// <returns></returns>
        public static bool TryParse(Unit unit, out N.Velocity V, params string[] ValuesTxt)
        {

            V = null;
            double[] Values = new double[ValuesTxt.Length];
            for (int i = 0; i < ValuesTxt.Length; i++)
            {
                if (!double.TryParse(ValuesTxt[i], out Values[i]))
                {
                    return false;
                }

            }
            V = Constructor[unit](Values);
            return true;
        }



        /// <summary>
        /// Konstruktoren: Erzeugen aus einem Koordinatenarray einen Geschwindigkeitsvektor
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        public delegate N.Velocity DgConstructor(params double[] Values);

        /// <summary>
        /// Zuordnung aller 
        /// </summary>
        public static Dictionary<Unit, DgConstructor> Constructor =
            new Dictionary<Unit, DgConstructor>
            {
                {Unit.km_per_hour, new DgConstructor(F.Curry<Func<double[], N.VelocityInKilometerPerHour>, double[], N.Velocity>(Adapter, N.Velocity.KilometerPerHour))},
                {Unit.km_per_s, new DgConstructor(F.Curry<Func<double[], N.VelocityInMeterPerSec<N.OrderOfMagnitude.Kilo>>, double[], N.Velocity>(Adapter, N.Velocity.KilometerPerSec))},
                {Unit.knots, new DgConstructor(F.Curry<Func<double[], N.VelocityInKnots>, double[], N.Velocity>(Adapter, N.Velocity.Knots))},
                {Unit.m_per_min, new DgConstructor(F.Curry<Func<double[], N.VelocityInMeterPerMinute<N.OrderOfMagnitude.One>>, double[], N.Velocity>(Adapter, N.Velocity.MeterPerMinute))},
                {Unit.m_per_s, new DgConstructor(F.Curry<Func<double[], N.VelocityInMeterPerSec<N.OrderOfMagnitude.One>>, double[], N.Velocity>(Adapter, N.Velocity.MeterPerSec))},
                {Unit.mi_per_hour, new DgConstructor(F.Curry<Func<double[], N.VelocityInMilesPerHour>, double[], N.Velocity>(Adapter, N.Velocity.MilesPerHour))},
            };

        static N.Velocity Adapter<To>(Func<double[], To> Constructor, double[] Values)            
            where To : N.Velocity
        {
            return Constructor(Values);
        }




        /// <summary>
        /// Converter: Wandeln Geschwindigkeitswerte, gemessen in einer Einheit A in einen Geschwindigkeitswert, gemessen in 
        /// einer Einheit B um
        /// </summary>
        /// <param name="V"></param>
        /// <returns></returns>
        public delegate N.Velocity DgConverter(N.Velocity V);

        public static Dictionary<Unit, DgConverter> Converter =
        new Dictionary<Unit, DgConverter>
            {
                {Unit.km_per_hour, new DgConverter(F.Curry<Func<N.Velocity, N.VelocityInKilometerPerHour>, N.Velocity, N.Velocity>(Adapter, N.Velocity.KilometerPerHour))},
                {Unit.km_per_s, new DgConverter(F.Curry<Func<N.Velocity, N.VelocityInMeterPerSec<N.OrderOfMagnitude.Kilo>>, N.Velocity, N.Velocity>(Adapter, N.Velocity.KilometerPerSec)) },
                {Unit.knots, new DgConverter(F.Curry<Func<N.Velocity, N.VelocityInKnots>, N.Velocity, N.Velocity>(Adapter, N.Velocity.Knots))},
                {Unit.m_per_min, new DgConverter(F.Curry<Func<N.Velocity, N.VelocityInMeterPerMinute<N.OrderOfMagnitude.One>>, N.Velocity, N.Velocity>(Adapter, N.Velocity.MeterPerMinute))},
                {Unit.m_per_s, new DgConverter(F.Curry<Func<N.Velocity, N.VelocityInMeterPerSec<N.OrderOfMagnitude.One>>, N.Velocity, N.Velocity>(Adapter, N.Velocity.MeterPerSec))},
                {Unit.mi_per_hour, new DgConverter(F.Curry<Func<N.Velocity, N.VelocityInMilesPerHour>, N.Velocity, N.Velocity>(Adapter, N.Velocity.MilesPerHour))},
            };


        static N.Velocity Adapter<Ti, To>(Func<N.Velocity, To> Constructor, Ti Vin)
            where Ti : N.Velocity
            where To : N.Velocity
        {
            return Constructor(Vin);
        }
    }
}
