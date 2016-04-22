//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 21.4.2016
//
//  Projekt.......: mko.BI
//  Name..........: IClassFactory.cs
//  Aufgabe/Fkt...: Spezielle Form des Klassenfabrik- Patterns in C#
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
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

namespace mko.BI
{
    public interface IClassFactory<TBo>
    {
        /// <summary>
        /// Erzeugt ein neues Geschäftsobjekt
        /// </summary>
        /// <returns></returns>
        TBo create();
    }
}
