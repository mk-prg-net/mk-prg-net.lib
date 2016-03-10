//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.6.2012
//
//  Projekt.......: mko.Euklid
//  Name..........: Space.cs
//  Aufgabe/Fkt...: Konzept des euklidischen Raumes
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

namespace mko.Euklid
{
    public abstract class Space
    {
        int _dimensions;

        public Space(int dimensions)
        {
            _dimensions = dimensions;
        }

        /// <summary>
        /// Anzahl der Raumdimensionen
        /// </summary>
        public int Dimensions
        {
            get
            {
                return _dimensions;
            }
        }  
      
        /// <summary>
        /// Prädikat, welches true zurückgibt, wenn der Raum den Punkt enthält
        /// </summary>
        /// <param name="P">Punkt, für den die Zugehörigkeit zum Raum entschieden werden soll</param>
        /// <param name="Epsilon">untere Schranke für die Näherung an den Punkt</param>
        /// <returns></returns>
        public abstract bool Contains(Vector P, double Epsilon);

        /// <summary>
        /// Kleinster Abstand zwischen Raumpunkten, ab dem zwei Raumpunkte als verschieden gelten
        /// </summary>
        public static double Resolution = 2 * 0.000000000000001;

    }
}
