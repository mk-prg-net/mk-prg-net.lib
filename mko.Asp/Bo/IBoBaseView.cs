//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 21.11.2011
//
//  Projekt.......: mkoItAsp
//  Name..........: IBoBaseView.cs
//  Aufgabe/Fkt...: Schnittstelle aller Views von Geschäftsobjekten
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

namespace mkoIt.Asp
{
    public interface IBoBaseView<TEntity>
    {
        /// <summary>
        /// Die Daten aus der View werden auf das Entity abgebildet
        /// </summary>
        /// <param name="entity"></param>
        void SetEntity(TEntity entity);
    }
}
