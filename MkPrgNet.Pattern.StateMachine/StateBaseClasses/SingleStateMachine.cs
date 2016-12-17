//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine
//  Name..........: SingleStateMachine.cs
//  Aufgabe/Fkt...: Implementiert den Zustand eine Zustandsautomaten mit genau einem 
//                  Zustand
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
    public class SingleStateMachine : StateBase
    {
        public override bool IsStart
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsFinal
        {
            get { throw new NotImplementedException(); }
        }
    }
}
