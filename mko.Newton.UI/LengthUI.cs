using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using N=mko.Newton;
using F = mko.Algo.FunctionalProgramming.Fn;

namespace mko.Newton.UI
{
    public class Length
    {
        /// <summary>
        /// Aufzählung aller Wegeinheiten, die für eine Benutzeroberfläche bereitgestellt werden
        /// </summary>
        public enum Unit
        {
            am,
            fm,
            pm,
            nm,
            µm,
            pt,
            mm,
            inch,
            cm,
            dm,
            m,
            km,
            mi,
            nmi,
            AU
        }

        public static bool TryParse(string ValueTxt, Unit unit, out N.Length len) {

            len = null;
            double Value;
            if (double.TryParse(ValueTxt, out Value))
            {
                len = Constructor[unit](Value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Konstruktoren: Erzeugen aus einem Koordinatenarray einen Ortsvektor
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public delegate N.Length DgConstructor(params double[] Value);

        /// <summary>
        /// Konstruktorliste als Dictionary UnitS -> DgLengthConstruktor. 
        /// Für die Konvertierung von Eingaben in Text/Listbox- Kombinationen in Längenwerte
        /// </summary>
        public static Dictionary<Unit, DgConstructor> Constructor = 
        new Dictionary<Unit, DgConstructor>{
            {Unit.am, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Atto>>, double[], N.Length>(Adapter, N.Length.Attometer))},
            {Unit.fm, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Femto>>, double[], N.Length>(Adapter, N.Length.Femtometer))},
            {Unit.pm, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Pico>>, double[], N.Length>(Adapter, N.Length.Picometer))},
            {Unit.nm, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Nano>>, double[], N.Length>(Adapter, N.Length.Nanometer))},
            {Unit.µm, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Micro>>, double[], N.Length>(Adapter, N.Length.Micrometer))},
            {Unit.pt, new DgConstructor(F.Curry<Func<double[], N.LengthInPoint>, double[], N.Length>(Adapter, N.Length.Point))},
            {Unit.mm, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Milli>>, double[], N.Length>(Adapter, N.Length.Millimeter))},
            {Unit.cm, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Centi>>, double[], N.Length>(Adapter, N.Length.Centimeter))},
            {Unit.inch, new DgConstructor(F.Curry<Func<double[], N.LengthInInch>, double[], N.Length>(Adapter, N.Length.Inch))},
            {Unit.dm, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Deci>>, double[], N.Length>(Adapter, N.Length.Decimeter))},
            {Unit.m, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.One>>, double[], N.Length>(Adapter, N.Length.Meter))},
            {Unit.km, new DgConstructor(F.Curry<Func<double[], N.LengthInMeter<N.OrderOfMagnitude.Kilo>>, double[], N.Length>(Adapter, N.Length.Kilometer))},
            {Unit.mi, new DgConstructor(F.Curry<Func<double[], N.LengthInMiles>, double[], N.Length>(Adapter, N.Length.Miles))},
            {Unit.nmi, new DgConstructor(F.Curry<Func<double[], N.LengthInNauticalMiles>, double[], N.Length>(Adapter, N.Length.NauticalMiles))},
            {Unit.AU, new DgConstructor(F.Curry<Func<double[], N.LengthInAU>, double[], N.Length>(Adapter, N.Length.AU))},
        };

        static N.Length Adapter<To>(Func<double[], To> Constructor, double[] Values)
            where To : N.Length
        {
            return Constructor(Values);
        }


        /// <summary>
        /// Konverter: Wandeln Länge, gemessen in einer Einheit A in eine Länge, gemessen in 
        /// einer Einheit B um
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public delegate N.Length DgConverter(N.Length L);

        /// <summary>
        /// Konstruktorliste als Dictionary UnitS -> DgLengthConstruktor. 
        /// Für die Konvertierung von Eingaben in Text/Listbox- Kombinationen in Längenwerte
        /// </summary>
        public static Dictionary<Unit, DgConverter> Converter =
        new Dictionary<Unit, DgConverter>{
            {Unit.am, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Atto>>, N.Length, N.Length>(Adapter, N.Length.Attometer))},
            {Unit.fm, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Femto>>, N.Length, N.Length>(Adapter, N.Length.Femtometer))},
            {Unit.pm, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Pico>>, N.Length, N.Length>(Adapter, N.Length.Picometer))},
            {Unit.nm, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Nano>>, N.Length, N.Length>(Adapter, N.Length.Nanometer))},
            {Unit.µm, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Micro>>, N.Length, N.Length>(Adapter, N.Length.Micrometer))},
            {Unit.pt, new DgConverter(F.Curry<Func<N.Length, N.LengthInPoint>, N.Length, N.Length>(Adapter, N.Length.Point))},
            {Unit.mm, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Milli>>, N.Length, N.Length>(Adapter, N.Length.Millimeter))},
            {Unit.cm, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Centi>>, N.Length, N.Length>(Adapter, N.Length.Centimeter))},
            {Unit.inch, new DgConverter(F.Curry<Func<N.Length, N.LengthInInch>, N.Length, N.Length>(Adapter, N.Length.Inch))},
            {Unit.dm, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Deci>>, N.Length, N.Length>(Adapter, N.Length.Decimeter))},
            {Unit.m, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.One>>, N.Length, N.Length>(Adapter, N.Length.Meter))},
            {Unit.km, new DgConverter(F.Curry<Func<N.Length, N.LengthInMeter<N.OrderOfMagnitude.Kilo>>, N.Length, N.Length>(Adapter, N.Length.Kilometer))},
            {Unit.mi, new DgConverter(F.Curry<Func<N.Length, N.LengthInMiles>, N.Length, N.Length>(Adapter, N.Length.Miles))},
            {Unit.nmi, new DgConverter(F.Curry<Func<N.Length, N.LengthInNauticalMiles>, N.Length, N.Length>(Adapter, N.Length.NauticalMiles))},
            {Unit.AU, new DgConverter(F.Curry<Func<N.Length, N.LengthInAU>, N.Length, N.Length>(Adapter, N.Length.AU))},
        };

        static N.Length Adapter<Ti, To>(Func<N.Length, To> Converter, Ti Lin)
            where Ti : N.Length
            where To : N.Length
        {
            return Converter(Lin);
        }




    }
}
