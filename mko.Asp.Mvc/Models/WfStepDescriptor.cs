//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den Feb. 2015
//
//  Projekt.......: PahmImport2015
//  Name..........: WFStepDescriptor.cs
//  Aufgabe/Fkt...: Fasst alle Informationen zu einem Workflowstep zusammen, die 
//                  für eine graphische Darstellung benötigt werden.
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
//  Datum.........: 17.7.2015
//  Änderungen....: Anpassung an das GKStatReportViewer Programm
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Asp.Mvc.Models
{
    public abstract class WfStepDescriptor
    {
        public WfStepDescriptor(mko.BI.StateMachine.State State)
        {
            _State = State;
        }

        public virtual string Name
        {
            get
            {
                return State.Name;
            }
        }

        public mko.BI.StateMachine.State State
        {
            get
            {
                return _State;
            }

        }
        mko.BI.StateMachine.State _State;

    }
}
