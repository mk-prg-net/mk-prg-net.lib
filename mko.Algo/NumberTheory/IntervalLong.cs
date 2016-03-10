
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2012
//
//  Projekt.......: mko.Algo
//  Name..........: IntervalLong.cs
//  Aufgabe/Fkt...: Spezielles Interval für Long mit zusätzlichen 
//                  Eigenschaften und Methoden
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

namespace mko.Algo.NumberTheory
{
    public class IntervalLong : mko.Algo.Interval<long>
    {

        public IntervalLong() { }
        public IntervalLong(long Begin, long End)
            : base(Begin, End)
        {
        }

        /// <summary>
        /// Gibt true zurück, wenn das übergebene Intevall im Intervall enthalten ist
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public bool Contains(IntervalLong SubInv)
        {
            return Begin <= SubInv.Begin && SubInv.Begin <= SubInv.End && SubInv.End <= End;
        }

        /// <summary>
        /// Gibt das i-te Element im Intervall, gezählt von Beginn an
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public long this[long i] {
            get{
                if (Begin + i > End)
                    throw new ArgumentOutOfRangeException();
                return Begin + i;
            }
        }

        /// <summary>
        /// Anzahl der Elemente im Zahlenbereich
        /// </summary>
        public long Count
        {
            get
            {
                if (Begin > End)
                    throw new Exception();
                return (End - Begin) + 1;
            }
        }

    }
}
