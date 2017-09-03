//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine
//  Name..........: IState.cs
//  Aufgabe/Fkt...: Struktur von Zuständen
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

namespace MkPrgNet.Pattern.StateMachine
{
    /// <summary>
    /// Basic structure of state 
    /// </summary>
    public interface IState
    {

        string Name
        {
            get;
        }

        /// <summary>
        /// Wenn True, dann ist der Zustand ein Startzustand
        /// </summary>
        bool IsStart
        {
            get;
        }

        /// <summary>
        /// Wenn True, dann ist der Zustand ein Endzustand
        /// </summary>
        bool IsFinal
        {
            get;
        }
    }
}
