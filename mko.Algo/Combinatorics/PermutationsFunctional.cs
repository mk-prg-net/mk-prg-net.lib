//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.10.2012
//
//  Projekt.......: mko.Algo
//  Name..........: PErmutationsFunctional.cs
//  Aufgabe/Fkt...: Funktionale Implementierungen von Algorithmen aus der Kombinatorik
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using lisp = mko.Algo.Listprocessing;
using mko.Algo.NumberTheory;

using Llong = System.Collections.Generic.IEnumerable<long>;
using LLlong = System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<long>>;


namespace mko.Algo.Combinatorics.Permutations
{
    public static class Fn
    {
        /// <summary>
        /// Anzahl aller Permutationen. Z.B. die Anzahl aller Besuchsreihenfolgen 
        /// von n Kunden.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Fact(long n)
        {
            if (n == 0)
                return 1;
            else
                return n * Fact(n - 1);
        }

        /// <summary>
        /// Erste Permutation von n Elementen. Alle weiteren 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Llong FirstPermutation(long n)
        {
            if (n == 1)
                return lisp.Fn.L(n);
            else
                return lisp.Fn.Concat(FirstPermutation(n - 1), lisp.Fn.L(n));
        }

        /// <summary>
        /// Aus einer gegebenen Permutation wird mittels systematischer Vertauschung die nächste Permutation erzeugt
        /// Über den Parameter i wird jede der n! Permuationen adressiert. Möchte man alle n! Permutationen durchlaufen,
        /// dann kann NextPermutationen für i = 1...n! nacheinander aufgerufen werden.
        /// </summary>
        /// <param name="plist">Permutation als Folge von n Long- Werten</param>
        /// <param name="i">Nr der Permutation aus 1 ... n!</param>
        /// <returns></returns>
        public static  Llong NextPermutation(Llong plist, long i)
        {
            // Count(plist) wird bei der Berechnung häufig verwendet. Deshalb wird hier einmal 
            // Count(plist) berechnet und dann als Parameter an NextPermutationen mit 3 Parametern übergeben
            return NextPermutation(plist, i, lisp.Fn.Count(plist));
        }

        private static Llong NextPermutation(Llong plist, long i, long CountPlist)
        {
            if (i % CountPlist == 0)
                // Mit dem ursprünglich ersten Element wurde einmal vollständig von Rechts nach Links, oder 
                // von Links nach Rechts durchlaufen
                if ((i / CountPlist).IsOdd())                
                    // Tauschen am Anfang
                    return lisp.Fn.Swap(plist, 0, 1);                
                else                
                    // Tauschen am Ende
                    return lisp.Fn.Swap(plist, CountPlist - 1, CountPlist);                
            else
                // Tauschen innerhalb einer Liste
                if ((i / CountPlist).IsOdd())
                    // Tauschen am Anfang
                    return lisp.Fn.Swap(plist, CountPlist - i % CountPlist, CountPlist - i % CountPlist - 1);
                else
                    // Tauschen am Ende
                    return lisp.Fn.Swap(plist, i % CountPlist, i % CountPlist - 1);
        }

        public static LLlong AllPermutations(long n)
        {
            return CreateListOfPermutations(FirstPermutation(n), Fact(n));
        }

        private static LLlong CreateListOfPermutations(Llong plist, long i)
        {
            if (i == 1)
                return new Llong[]{plist};
            else
                return lisp.Fn.Concat(new Llong[] { plist }, CreateListOfPermutations(NextPermutation(plist, i - 1), i - 1));
        }       
 
    }
}
