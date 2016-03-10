//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.5.2014
//
//  Projekt.......: mko.Alog
//  Name..........: mko.Algo.Automaton.StateBase.cs
//  Aufgabe/Fkt...: Basisklasse aller Zusände von endlichen Automaten
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

namespace mko.Algo.Automaton
{
    public abstract class StateBase<TName>
    {
        /// <summary>
        /// Name des aktuellen Zustandes
        /// </summary>
        public abstract TName Name { get; }


        /// <summary>
        /// Arten von Zuständen
        /// </summary>
        public enum KindsOfStates
        {
            initial,
            normal,
            final
        }

        /// <summary>
        /// Art des Zustandes
        /// </summary>
        public abstract KindsOfStates KindOfState { get; }


        /// <summary>
        /// Liste der Folgezustände zu diesem Zustand auf
        /// </summary>
        public StateBase<TName>[] Next { get; set; }


        /// <summary>
        /// Definiert alle Folgezustände 
        /// </summary>
        /// <param name="ResultStates"></param>
        public void AddResultStates(params StateBase<TName>[] ResultStates)
        {
            this.Next = ResultStates;
        }



    }
}
