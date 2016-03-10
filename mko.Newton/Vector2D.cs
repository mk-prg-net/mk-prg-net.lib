using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    /// <summary>
    /// Rechnen mit gerichteten Größen in der Ebene
    /// </summary>
    public class Vector2D
    {
        /// <summary>
        /// Vektoraddition (z.B. von zwei Kraftpfeilen)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Tuple<T, T> Add<T>(Tuple<T, T> a, Tuple<T, T> b)
            where T : IMeasuredValue, new()
        {
            return new Tuple<T, T>(
                new T() { Value = a.Item1.ValueInBaseUnit + b.Item1.ValueInBaseUnit },
                new T() { Value = a.Item2.ValueInBaseUnit + b.Item2.ValueInBaseUnit }
            );
        }

        /// <summary>
        /// Scalierung/Streckung eines Vektors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="k"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Tuple<TB, TB> Scale<TA, TB>(double k, Tuple<TA, TA> a)
            where TA : IMeasuredValue, new()
            where TB : IMeasuredValue, new()
        {
            return new Tuple<TB, TB>(
                new TB() { Value = k * a.Item1.ValueInBaseUnit },
                new TB() { Value = k * a.Item2.ValueInBaseUnit }
                );
        }

        /// <summary>
        /// Berechnet das innere Produkt (Skalarprodukt) auf zwei gerichteten Messgrößen
        /// (z.B. Kraft und Weg -> Arbeit)
        /// </summary>
        /// <typeparam name="TA"></typeparam>
        /// <typeparam name="TB"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static TR Inner<TA, TB, TR>(Tuple<TA, TA> a, Tuple<TB, TB> b)
            where TA : IMeasuredValue, new()
            where TB : IMeasuredValue, new()
            where TR : IMeasuredValue, new()
        {
            return new TR() {
                Value = a.Item1.ValueInBaseUnit * b.Item1.ValueInBaseUnit + a.Item2.ValueInBaseUnit * b.Item2.ValueInBaseUnit
            };
        }

        /// <summary>
        /// Berechnet euklidischen Abstand vom Nullpunkt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        public static T Length<T>(Tuple<T, T> a)
            where T : IMeasuredValue, new()
        {
            Func<T, double> sqr = (x) => x.ValueInBaseUnit * x.ValueInBaseUnit;
            return new T() { Value = Math.Sqrt(sqr(a.Item1) + sqr(a.Item2)) };
        }

    }
}
