//<unit_header>
//----------------------------------------------------------------
// Copyright 2016 Martin Korneffel
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 22.2.2016
//
//  Projekt.......: mko.NaLisp
//  Name..........: IFactory.cs
//  Aufgabe/Fkt...: Fabrikmethoden für logische Operatoren
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

namespace mko.NaLisp.BoolOps
{
    /// <summary>
    /// Fabrikmethoden für logische Operatoren
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Erzeugt eine logische UND- Operation
        /// </summary>
        /// <param name="Elements"></param>
        /// <returns></returns>
        BoolOps.AND AND(params Core.INaLisp[] Elements);

        /// <summary>
        /// Erzeugt eine logische ODER- Operation
        /// </summary>
        /// <param name="Elements"></param>
        /// <returns></returns>
        BoolOps.OR OR(params Core.INaLisp[] Elements);

        /// <summary>
        /// Erzeugt eine logische NEGATION
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        BoolOps.NOT NOT(Core.INaLisp Element);
    }
}
