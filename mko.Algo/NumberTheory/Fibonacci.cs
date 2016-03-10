//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 24.1.2014
//
//  Projekt.......: mko.Algo
//  Name..........: Fibonacci
//  Aufgabe/Fkt...: Berechnet die Fibonacci- Zahlen
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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
using System.Threading.Tasks;

namespace mko.Algo.NumberTheory
{
    public class Fibonacci
    {
        // Hilfsmittel für Berechnung von Fibonacci- Zahlen
        long[] Fib = { 1, 1 };
        int FibCounter = 2;

        /// <summary>
        /// Startet die Fibonacci- Berechnung mit Next neu.
        /// </summary>
        public void Reset()
        {
            FibCounter = 2;
            Fib[0] = 1;
            Fib[1] = 1;
        }

        /// <summary>
        /// Liefert die nächste Fibonacci- Zahl
        /// </summary>
        /// <returns>Tupel(i, Fibbonacci i)</returns>
        public Tuple<long, long> Next()
        {
            long FibN = Fib[0] + Fib[1];

            FibCounter++;
            Fib[FibCounter % 2] = FibN;

            return new Tuple<long, long>(FibCounter, FibN);
        }

    }
}
