//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: IMelayAutomatonBuilder.cs
//  Aufgabe/Fkt...: Defniert zunächst alle Zustandsübergnge 
//                  zu gegebenen Eingaben. Anschließend 
//                  Können die Ausgabefuntkionen, die mit    
//                  Zustandsübergängen assoziiert sind, 
//                  definiert werden.
//                  Die Zustände sind als Enum zu definieren.
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
    /// Builder, that decorates some states as final or start states.
    /// </summary>
    /// <typeparam name="TState">Enum, that defines all states of automaton</typeparam>
    public interface IMelayAutomatonBuilder<TState> : IStateDecoratorBuilder<TState>
        where TState : struct
    {
        /// <summary>
        /// Creates a Builder, that defines for every state a output function
        /// </summary>
        /// <returns></returns>
        IMelayTransistionFunctionBuilder<TState> CreateTransistionFunctionBuilder();

    }
}
