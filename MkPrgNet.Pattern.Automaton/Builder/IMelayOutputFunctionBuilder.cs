//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: IMelayOutputFunctionBuilder.cs
//  Aufgabe/Fkt...: Definiert die Ausgabefuntionen für jede Transition.
//                  Anschließend kann ein Melay- Automat erzeugt werden.
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

namespace MkPrgNet.Pattern.Automaton
{
        /// <summary>
        /// 
        /// </summary>
    public interface IMelayOutputFunctionBuilder<TStateEnum>
        where TStateEnum : struct
    {
        /// <summary>
        /// Defines the output functor for a given transistion (state x input -> state)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        void DefineOutputFunctorFor(TStateEnum state, IInput input, IOutput output);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IAutomaton<TStateEnum> CreateMelayAutomaton();

    }
}
