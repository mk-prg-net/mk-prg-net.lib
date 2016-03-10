//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 16.11.2010
//
//  Projekt.......: Godel Beton Laborsystem 1.0
//  Name..........: PageStateCreator.cs
//  Aufgabe/Fkt...: Der Zustand einer Seite kann im View- oder im Session- State
//                  gespeichert werden. Die Klasse stellt Methoden bereit, um 
//                  den einen typisierten Zustand anzulegen. Dabei gilt folgende
//                  Zuordnung:
//                  ViewState
//                      WebFormInstanz <--> Seitenzustand
//                  SessionState
//                      User           <--> Seitenzustand
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Diagnostics;

namespace mkoIt.Asp
{
    public class PageState<TPageState>
        where TPageState : new()
    {
        /// <summary>
        /// Der Zustandspeicher einer Seite wird im ViewState angelegt
        /// </summary>
        /// <param name="ViewState"></param>
        /// <returns></returns>
        public static TPageState Create(StateBag ViewState)
        {

            string TypeName = typeof(TPageState).FullName;
            if (ViewState[TypeName] == null)
                ViewState[TypeName] = new TPageState();

            Debug.Assert(ViewState[TypeName] is TPageState, "Im Viewstate wurde ein " + TypeName + "- Objekt erwartet, es existiert jedoch keins");
            return (TPageState)ViewState[TypeName];
        }

        /// <summary>
        /// Der Zustandspeicher einer Seite wird im SessionState angelegt
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static TPageState Create(HttpSessionState Session)
        {

            string TypeName = typeof(TPageState).FullName;
            if (Session[TypeName] == null)
                Session[TypeName] = new TPageState();

            Debug.Assert(Session[TypeName] is TPageState, "Im Sessionstate wurde ein " + TypeName + "- Objekt erwartet, es existiert jedoch keins");
            return (TPageState)Session[TypeName];
        }
    }
}