//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 17.12.2016
//
//  Projekt.......: MkPrgeN.Pattern.StateMachine
//  Name..........: OutputFunctionWrapper.cs
//  Aufgabe/Fkt...: Wrapper, um als Labdaausdruck definierte Ausgabefunktion 
//                  als Ausgabefunktion für Zustandsautomaten zu ermöglichen.
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

namespace MkPrgNet.Pattern.StateMachine.Outputs
{
    public class OutputFunctionWrapper : IOutput
    {
        public OutputFunctionWrapper(Action<IInput> outputFunction)
        {
            _outputFunction = outputFunction;
        }

        Action<IInput> _outputFunction;

        public void Write(IInput input)
        {
            _outputFunction(input);
        }
    }
}
