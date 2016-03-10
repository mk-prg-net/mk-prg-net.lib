//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.11.2014
//
//  Projekt.......: mko.Algo
//  Name..........: FinitStateMachin.cs
//  Aufgabe/Fkt...: Minimalistischer Zustandsautomat
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
using System.Threading.Tasks;

namespace mko.Algo.Automaton.StateMachine
{
    public abstract class FinitStateMachine<TStateFactory>
    {
        /// <summary>
        /// Klassenfabrik zum Erzeugen von Zuständen
        /// </summary>
        public abstract TStateFactory StateFactory { get; }



        /// <summary>
        /// VErweist auf den aktuellen Zustand, in dem sich der Automat befindet
        /// </summary>
        public State ActiveState { get; set; }

        /// <summary>
        /// Ermöglicht den Zugriff auf den aktiven Zustand streng typisiert. Falls die Konvertierung fehlschlägt, wird eine
        /// spezielle Exception geworfen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T CastToExpectedStateType<T>()
            where T : State
        {
            if (ActiveState is T)
                return (T)ActiveState;
            else
                throw new mko.Algo.Automaton.StateMachine.UnexcpectedStateException(ActiveState);
        }


    }
}
