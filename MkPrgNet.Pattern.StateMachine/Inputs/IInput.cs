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
    /// Represents an input of a state machine
    /// </summary>
    public interface IInput
    {
        /// <summary>
        /// Name of input function
        /// </summary>
        string Name
        {
            get;

        }

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
