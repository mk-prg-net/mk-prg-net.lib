//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: IStateDecoratorBuilder.cs
//  Aufgabe/Fkt...: Definiert für einen Automatenzustand zussätzliche 
//                  Eigenschaften.
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

namespace MkPrgNet.Pattern.Automaton
{

    /// <summary>
    /// Adds additional properties to automaton states
    /// </summary>
    /// <typeparam name="TStateEnum"></typeparam>
    public interface IStateDecoratorBuilder<TStateEnum>
        where TStateEnum : struct
    {
        /// <summary>
        /// Defines a state as inital state of automaton
        /// </summary>
        /// <param name="state"></param>
        void DefineStateAsStart(TStateEnum state);


        /// <summary>
        /// Defines a state as on his final states
        /// </summary>
        /// <param name="state"></param>
        void DefineStateAsFinal(TStateEnum state);
        
        
                
    }
}
