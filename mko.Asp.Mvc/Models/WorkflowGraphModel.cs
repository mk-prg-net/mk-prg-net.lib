//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 6.2.2015
//
//  Projekt.......: Gbl.Lab.Concrete.PahmImport.Web
//  Name..........: WorkflowGraphModel.cs
//  Aufgabe/Fkt...: Model der Workflow.cs Partial View
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
//  Datum.........: 17.7.2015
//  Änderungen....: Angepasst auf GKStatReportViewer
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
    public abstract class WorkflowGraphModel
    {

        public WorkflowGraphModel(mko.BI.StateMachine.State HighlightedState)
        {
            this.HighlightedState = HighlightedState;
        }

        /// <summary>
        /// Beschreibungen aller Workflow- Schritte für Workflow- Diagramme
        /// </summary>
        public abstract WfStepDescriptor[] WfStepDescriptors
        {
            get;
        }

        public mko.BI.StateMachine.State HighlightedState { get; set; }

    }
}
