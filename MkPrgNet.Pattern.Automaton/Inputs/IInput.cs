//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine
//  Name..........: IInput.cs
//  Aufgabe/Fkt...: Repräsentiert die Eingabe eines Zustandsautomaten.
//                            +-----------------+
//                  Input1 ---| Automat         |
//                            |-------->\       |
//                            |      +-> STF----|----+
//                            |      |          |    |
//                  Input2 ---|  CurrentState <-|----+
//                            |    |            |     
//                            |    |            |
//                  Input n---|    +---> Output-|--------> Ausgabe
//                            +-----------------+
//
//                  Stellt einen Funktor dar, dessen Funktion On
//                  true signalisiert, wenn die Eingabe anliegt.
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 7.8.2017
//  Änderungen....: Eigenschaft Name entfernt
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
    /// Represents an input of a state machine
    /// </summary>
    public interface IInput
    {
        /// <summary>
        /// Priority of Input.
        /// If more than one Input is activated (On == true),
        /// then input with highest priority will be processed in 
        /// the next transition.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// true, if input is done
        /// </summary>
        bool On
        {
            get;
        }


        /// <summary>
        /// Setzt den Eingang wieder zurück
        /// </summary>
        void Reset();
    }
}
