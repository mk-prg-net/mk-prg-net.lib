//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart 2010
//
//  Projekt.......: mko
//  Name..........: Intervall.cs
//  Aufgabe/Fkt...: Implementierung von Intervallen auf beliebigen Ordnungen
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
using System.Diagnostics;

namespace mko
{
    [Serializable]
    public struct Interval<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Klassenfabriken. Da Interval eine Struct ist, und der Default- Konstruktor nicht
        /// überschrieben werden kann, wird das Defaultkonstruktorverhalten mittels Create nachgebildet 
        /// </summary>
        /// <returns></returns>
        public static Interval<T> Create()
        {
            return new Interval<T>() { Empty = true };
        }

        public static Interval<T> Create(T Begin, T End)
        {
            return new Interval<T>() { Begin = Begin, End = End, Empty = Begin.CompareTo(End) < 0 };
        }



        /// <summary>
        /// Wenn true, dann ist das Intervall leer, egal was Begin oder end anzeigt
        /// </summary>
        public bool Empty;

        T _Begin;
        public T Begin {
            get
            {
                //Debug.Assert(_Begin.CompareTo(_End) <= 0, "GblDbLayer.Interval: Begin > End");
                return _Begin;
            }
            set
            {
                Empty = _Begin.CompareTo(_End) > 0;
                _Begin = value;
            }
        }

        T _End;
        public T End {
            get
            {
                //Debug.Assert(_Begin.CompareTo(_End) <= 0, "GblDbLayer.Interval: Begin > End");
                return _End;
            }
            set
            {
                Empty = _Begin.CompareTo(_End) > 0;
                _End = value;
            }
        }

        public override string ToString()
        {
            return "(" + Begin.ToString() + ", " + End.ToString() + ")";
        }

        public bool contains(T value)
        {
            return (_Begin.CompareTo(value) <= 0 && _End.CompareTo(value) >= 0);

        }


    }
}
