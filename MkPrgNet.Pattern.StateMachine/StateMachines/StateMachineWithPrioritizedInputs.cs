//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.8.2017
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
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
    public class StateMachineWithPrioritizedInputs : StateMachine, IStateMachine
    {
        public override void Transistion()
        {

            // Zustandsübergänge finden nur statt, wenn ein Eingang aktiviert ist
            if (_Inputs.Any(i => i.On))
            {
                // Innerhalb einer Transition wird im Falle mehrerer gleichzeitig
                // aktivierter Eingaben die mit der höchsten Priorität verarbeitet
                // Eingaben, die zuerst definiert wurden, haben eine höhere Priorität
                // als Eingaben, die später definiert wurden
                var input = _Inputs.First(r => r.On);
                var key = GetKey(input, CurrentState);
                var val = Transistions[key];
                _CurrentState = val.Item1;
                val.Item2.Write(input);

                // Eingang wieder zurücksetzen
                input.Reset();

            }

        }
    }
}
