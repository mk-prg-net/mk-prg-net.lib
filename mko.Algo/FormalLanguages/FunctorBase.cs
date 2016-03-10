//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.11.2011
//
//  Projekt.......: Algorithmen
//  Name..........: FunctorBase.cs
//  Aufgabe/Fkt...: Basisklasse von Funktoren, die zur Berechnung geklammerter Ausdrücke dienen
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

namespace mko.Algo.fml
{
    public abstract class FunctorBase<T>
    {
        /// <summary>
        /// Funktionale Abbildung des Funktors berechnen
        /// </summary>
        /// <returns></returns>
        public abstract T map();
    }
}
