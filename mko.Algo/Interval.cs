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
//  Version 1.0...: 2010
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 30.4.2014
//  Änderungen....: Klasse mko.Intervall aus Projekt mko in Klasse mko.Algo.Interval in 
//                  Projekt mko.Algo übertragen
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;


namespace mko.Algo
{
    [Serializable]
    public class Interval<T>
         where T : IComparable<T>
    {
        /// <summary>
        /// Defaultkonstruktor:
        /// Erzeugt ein leeres Intervall
        /// </summary>
        /// <returns></returns>
        public Interval()
        {
            Empty = true;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        public Interval(T Begin, T End)
        {
            this.Begin = Begin;
            this.End = End;
            // Intervall ist leer, wenn Begin > End ist ist
            // Begin == End ist ein Intervall, das genau ein Element enthält
            Empty = Begin.CompareTo(End) > 0;
        }



        /// <summary>
        /// Wenn true, dann ist das Intervall leer, egal was Begin oder end anzeigt
        /// </summary>
        public bool Empty;

        T _Begin;
        public T Begin
        {
            get
            {
                //Debug.Assert(_Begin.CompareTo(_End) <= 0, "GblDbLayer.Interval: Begin > End");
                return _Begin;
            }
            set
            {
                // Intervall ist leer, wenn Begin > End ist ist
                // Begin == End ist ein Intervall, das genau ein Element enthält
                Empty = value.CompareTo(_End) > 0;
                _Begin = value;
            }
        }

        T _End;
        public T End
        {
            get
            {
                //Debug.Assert(_Begin.CompareTo(_End) <= 0, "GblDbLayer.Interval: Begin > End");
                return _End;
            }
            set
            {
                Empty = _Begin.CompareTo(value) > 0;
                // Intervall ist leer, wenn Begin > End ist ist
                // Begin == End ist ein Intervall, das genau ein Element enthält
                _End = value;
            }
        }

        public override string ToString()
        {
            return "(" + Begin.ToString() + ", " + End.ToString() + ")";
        }

        public bool Contains(T value)
        {
            return (_Begin.CompareTo(value) <= 0 && _End.CompareTo(value) >= 0);

        }

        /// <summary>
        /// Berechnet das größte Intervall, dass gemeinsam in beiden Intervallen enthalten ist
        /// 
        /// Fälle:
        /// 1)
        /// -----[aaaaaa]----------------
        /// ---------[bbbbb]-------------
        /// ---------[rr]---------------- Result
        /// 
        /// 2)
        /// ---------[aaaaaa]------------
        /// -------[bbbbb]---------------
        /// ---------[rrr]--------------- Result
        /// 
        /// 3)
        /// ---------[aaaaaa]------------
        /// -------[bbbbbbbbbb]----------
        /// ---------[rrrrrr]------------ Result
        /// 
        /// 4)
        /// -------[aaaaaaaaaa]----------
        /// ---------[bbbbbb]------------
        /// ---------[rrrrrr]------------ Result
        /// 
        /// 5)
        /// ---[aaaaaa]------------------
        /// ---------------[bbbbb]-------
        /// -----------[]---------------- Result
        /// 
        /// </summary>
        /// <param name="Second"></param>
        /// <returns></returns>
        public Interval<T> IntersectWith(Interval<T> Second)
        {
            if (Empty || Second.Empty)
                /// Fall: Schnittmenge mit leeren Intervallen            
                return new Interval<T>();
            else if (Begin.CompareTo(Second.Begin) < 0 && End.CompareTo(Second.End) <= 0)
                /// 1)
                /// -----[aaaaaa]----------------
                /// ---------[bbbbb]-------------
                /// ---------[rr]---------------- Result
                return new Interval<T>(Second.Begin, End);
            else if (Begin.CompareTo(Second.Begin) >= 0 && End.CompareTo(Second.End) > 0)
                /// 2)
                /// ---------[aaaaaa]------------
                /// -------[bbbbb]---------------
                /// ---------[rrr]--------------- Result
                return new Interval<T>(Begin, Second.End);
            else if (Begin.CompareTo(Second.Begin) >= 0 && End.CompareTo(Second.End) <= 0)
                /// 3)
                /// ---------[aaaaaa]------------
                /// -------[bbbbbbbbbb]----------
                /// ---------[rrrrrr]------------ Result
                return new Interval<T>(Begin, End);
            else if (Begin.CompareTo(Second.Begin) < 0 && End.CompareTo(Second.End) > 0)
                /// 4)
                /// -------[aaaaaaaaaa]----------
                /// ---------[bbbbbb]------------
                /// ---------[rrrrrr]------------ Result
                return new Interval<T>(Second.Begin, Second.End);
            else
                /// 5)
                /// ---[aaaaaa]------------------
                /// ---------------[bbbbb]-------
                /// -----------[]---------------- Result
                return new Interval<T>();

        }

    }
}
