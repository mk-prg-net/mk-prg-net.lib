//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.7.2007
//
//  Projekt.......: PSZ Porsche Software Zertifizierung
//  Name..........: SessionHistory.cs
//  Aufgabe/Fkt...: Das Drücken des [Zurück]- Buttons ist 
//                  eine Kontextsensitiv . Z. B. ist neuer "neuer Testprozess"
//                  erreichbar aus TestProcess/All und TestSzenarios/All. 
//                  Der Rücksprung muß das korrekte Ziel wählen, und den
//                  ursprünglichen Session- Kontext der Webform wiederherstellen.
//                  Zu Unterstützung diese Aufgabe wird die SessionHistory- Klasse entworfen.
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
//  Datum.........: 1.10.2007
//  Änderungen....: Methode RedirektOnSessionEnd(string url) hinzugefügt
//
//</unit_history>
//</unit_header>        

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Web.SessionState;

namespace mkoIt.Asp
{

    /// <summary>
    /// Zusammenfassungsbeschreibung für SessionHistory
    /// </summary>

    [Serializable]
    public class SessionHistory
    {
        public delegate void PostGoBackEventHandler();

        public class SessionHistoryEntry
        {
            public bool hasMultiViewRef = false;
            public int MultiViewIndex = -1;
            public bool hasUrl = false;
            public string Url;
            public object Context;
            public event PostGoBackEventHandler PostGoBackHnd;
            public void firePostGoBackEvent()
            {
                if (PostGoBackHnd != null)
                    PostGoBackHnd();
            }
        }

        private Stack<SessionHistoryEntry> SessionHistoryStack = new Stack<SessionHistoryEntry>();

        public static void Create(HttpSessionState Session)
        {
            Session["SessionHistory"] = new SessionHistory();
        }

        public static SessionHistory GetInstance(HttpSessionState Session)
        {
            return (SessionHistory)Session["SessionHistory"];
        }

        // Konstruktor
        private SessionHistory()
        {
        }

        // Zurücksetzen der History, wenn z.B. über SiteMap navigiert wird
        public void reset()
        {
            SessionHistoryStack.Clear();
        }

        // push    

        public void push(int ViewIndex, object context, PostGoBackEventHandler PostBackEventHnd)
        {
            SessionHistoryEntry entry = new SessionHistoryEntry();

            entry.hasMultiViewRef = true;
            entry.MultiViewIndex = ViewIndex;
            entry.PostGoBackHnd += PostBackEventHnd;

            SessionHistoryStack.Push(entry);
        }

        public void push(string Url, object Context, PostGoBackEventHandler PostGoBackEventHnd)
        {
            SessionHistoryEntry entry = new SessionHistoryEntry();

            entry.hasUrl = true;
            entry.Url = Url;
            entry.Context = Context;
            entry.PostGoBackHnd += PostGoBackEventHnd;

            SessionHistoryStack.Push(entry);
        }

        public void push(int ViewIndex, string Url, object Context, PostGoBackEventHandler PostBackEventHnd)
        {
            // 1. Eintrag beim Rücksprung in der angesprungenen WebForm
            //    verarbeitet werden
            SessionHistoryEntry entry1 = new SessionHistoryEntry();

            entry1.hasMultiViewRef = true;
            entry1.MultiViewIndex = ViewIndex;
            entry1.PostGoBackHnd += PostBackEventHnd;

            SessionHistoryStack.Push(entry1);

            // 2. Eintrag wird beim Rücksprung von der WebForm, aus der 
            //    Rücksprung startet, ausgewertet
            SessionHistoryEntry entry2 = new SessionHistoryEntry();

            entry2.hasUrl = true;
            entry2.Url = Url;

            entry2.Context = Context;

            SessionHistoryStack.Push(entry2);
        }

        public SessionHistoryEntry peek()
        {
            return SessionHistoryStack.Peek();
        }

        public bool pop(out SessionHistoryEntry entry)
        {
            if (SessionHistoryStack.Count > 0)
            {
                entry = SessionHistoryStack.Pop();
                return true;
            }
            else
            {
                entry = null;
                return false;
            }
        }

        // Methoden, die die Rücksprünge implementieren
        public bool GoBack(HttpResponse Response)
        {
            SessionHistory.SessionHistoryEntry entry;
            if (pop(out entry))
            {
                if (entry.hasUrl)
                {
                    entry.firePostGoBackEvent();
                    Response.Redirect(entry.Url);
                    return true;
                }
                else
                {
                    // Ursprünglichen Zustand wiederherstellen
                    SessionHistoryStack.Push(entry);
                }
            }

            return false;
        }

        public bool GoBack(MultiView MultiView1)
        {
            SessionHistory.SessionHistoryEntry entry;
            if (pop(out entry))
            {
                if (entry.hasMultiViewRef)
                {
                    // View des Rücksprungziels aktivieren                
                    MultiView1.ActiveViewIndex = entry.MultiViewIndex;
                    MultiView1.Views[MultiView1.ActiveViewIndex].DataBind();
                    entry.firePostGoBackEvent();

                    // eventuelle Steuerelemente an Daten binden
                    //if (entry.hasBoundControl)
                    //    entry.DataBoundControl.DataBind();

                    return true;
                }
                else
                {
                    // Ursprünglichen Zustand wiederherstellen
                    SessionHistoryStack.Push(entry);
                }
            }

            return false;
        }

        public bool GoBack(MultiView MultiView1, HttpResponse Response)
        {
            if (!GoBack(MultiView1))
            {
                if (!GoBack(Response))
                {
                    return false;
                }
            }

            return true;
        }

        //Standard- GoBack- Methode
        public void GoBackOrStartNewHistory(MultiView MultiView1, HttpResponse Response, View SartView)
        {
            if (!GoBack(MultiView1))
            {
                if (!GoBack(Response))
                {
                    reset();
                    MultiView1.SetActiveView(SartView);
                }
            }
        }

        // 
        public static void RedirectOnSessionEnd(Page myPage, string url)
        {
            if (myPage.Session.IsNewSession)
            {
                string sCookieHeader = myPage.Request.Headers["Cookie"];
                if ((null != sCookieHeader) && (sCookieHeader.IndexOf("ASP.NET_SessionId") >= 0))
                {
                    if (myPage.Request.IsAuthenticated)
                    {
                        FormsAuthentication.SignOut();
                    }

                    myPage.Response.Redirect(url);
                }

            }
        }
    }
}
