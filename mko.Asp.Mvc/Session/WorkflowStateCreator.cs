//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 19.5.2014
//
//  Projekt.......: mko.Asp.Mvc 1.0
//  Name..........: WorkflowStateCreator.cs
//  Aufgabe/Fkt...: Der Zustand eines Workflows wird im Session- State
//                  gespeichert werden. Die Klasse stellt Methoden bereit, um 
//                  den einen typisierten Zustand anzulegen. 
//                  Für jeden Workflow ist eine Klasse zu implementieren, die 
//                  die Struktur seines Zustandes beschreibt. Der Name der Klasse
//                  dient zur Laufzeit als Schlüssel für den Zugriff im Sitzungszustand.
//                  Hervorgegangen aus mkoIt.Asp.PageState
//                  
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2008
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 16.11.2010
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
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace mkoIt.Asp
{
    public class WorkflowState<TWFState>
        where TWFState : new()
    {
        /// Zustand, der unter einem Typnamen abgelegt ist, abrufen.
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static TWFState Get(HttpSessionStateBase Session)
        {            
            string TypeName = typeof(TWFState).FullName;
            if (Session[TypeName] == null)
                Session[TypeName] = new TWFState();

            Debug.Assert(Session[TypeName] is TWFState, "Im Sessionstate wurde ein " + TypeName + "- Objekt erwartet, es existiert jedoch keins");
            return (TWFState)Session[TypeName];
        }

        /// <summary>
        /// Zustand, der unter einer Workflow- ID gespeichert ist, abrufen
        /// </summary>
        /// <param name="WFID"></param>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static TWFState Get(string WFID, HttpSessionStateBase Session)
        {
            string TypeName = typeof(TWFState).FullName;
            string Key = WFID + @"\"+ TypeName;
            if (Session[Key] == null)
                Session[Key] = new TWFState();
            
            return (TWFState)Session[Key];
        }

    }
}