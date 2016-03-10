using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using N = mko.Newton;
using F = mko.Algo.FunctionalProgramming.Fn;

namespace mko.Newton.UI
{
    public class Time
    {
        public enum Unit
        {
            ps,
            ns,
            micro_s,
            ms,
            s,
            min,
            h,
            d
        }

        /// <summary>
        /// Einlesen eines Geschwindigkeitsvetors aus einem Text, wobei die Wunscheinheit durch ein
        /// Unit- Enum definiert wird
        /// </summary>
        /// <param name="ValueTxt"></param>
        /// <param name="unit"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool TryParse(string ValueTxt, Unit unit, out N.Time t)
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
        public delegate N.Time DgConstructor(double Value);

        /// <summary>
        /// Zuordnung aller 
        /// </summary>
        public static Dictionary<Unit, DgConstructor> Constructor =
            new Dictionary<Unit, DgConstructor>
            {
                {Unit.d, new DgConstructor(F.Curry<Func<double, N.TimeInDays>, double, N.Time>(Adapter, N.Time.Days))},
                {Unit.h, new DgConstructor(F.Curry<Func<double, N.TimeInHours>, double, N.Time>(Adapter, N.Time.Hours))},
                {Unit.micro_s, new DgConstructor(F.Curry<Func<double, N.TimeInSec<N.OrderOfMagnitude.Micro>>, double, N.Time>(Adapter, N.Time.MicroSec))},
                {Unit.min, new DgConstructor(F.Curry<Func<double, N.TimeInMinutes>, double, N.Time>(Adapter, N.Time.Minutes))},
                {Unit.ms, new DgConstructor(F.Curry<Func<double, N.TimeInSec<N.OrderOfMagnitude.Milli>>, double, N.Time>(Adapter, N.Time.MilliSec))},
                {Unit.ns, new DgConstructor(F.Curry<Func<double, N.TimeInSec<N.OrderOfMagnitude.Nano>>, double, N.Time>(Adapter, N.Time.NanoSec))},
                {Unit.s, new DgConstructor(F.Curry<Func<double, N.TimeInSec<N.OrderOfMagnitude.One>>, double, N.Time>(Adapter, N.Time.Sec))},
                {Unit.ps, new DgConstructor(F.Curry<Func<double, N.TimeInSec<N.OrderOfMagnitude.Pico>>, double, N.Time>(Adapter, N.Time.PicoSec))},
            };

        static N.Time Adapter<To>(Func<double, To> Constructor, double Values)
            where To : N.Time
        {
            return Constructor(Values);
        }


    }
}
