//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.7.2007
//
//  Projekt.......: PSZ Porsche Software Zertifizierung
//  Name..........: SessionHistory.cs
//  Aufgabe/Fkt...: Das Drücken des [Zurück]- Buttons ist 
//                  kontextsensitiv . Z. B. ist neuer "neuer Testprozess"
//                  erreichbar aus TestProcess/All und TestSzenarios/All. 
//                  Der Rücksprung muss das korrekte Ziel wählen, und den
//                  ursprünglichen Session- Kontext wiederherstellen.
//                  Diese Aufgabe erfüllt die NavHistory
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
//  Version.......: 1.0, PSZ Porsche Software Zertifizierung
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 12.07.2007
//  Änderungen....: Methode RedirektOnSessionEnd(string url) hinzugefügt
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 1.10.2007
//  Änderungen....: Methode RedirektOnSessionEnd(string url) hinzugefügt
//
//</unit_history>
//</unit_header>        



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Asp.Mvc
{
    public class NavHistoryController : System.Web.Mvc.Controller
    {


        [Serializable]
        public class ActionDescriptor
        {
            public int ActionID { get; set; }

            /// <summary>
            /// Anzeigename für Action, an die aus einer tieferen Verarbeitungsstufe zurückzusrpingen ist
            /// </summary>
            public string DisplayName { get; set; }


            public string RouteName { get; set; }

            /// <summary>
            /// Objekt mit den Routen- Infos. Definiert die Action und die Parameter, an die zurückgesprungen wird.
            /// </summary>
            /// 
            public System.Web.Routing.RouteValueDictionary RouteInfo { get; set; }

            public static ActionDescriptor Create(object ControllerClass, string ActionName, string RouteName, string DisplayName, System.Web.Routing.RouteValueDictionary RouteInfo)
            {
                return Create(ControllerClass.GetType(), ActionName, RouteName, DisplayName, RouteInfo);
            }

            public static ActionDescriptor Create(Type ControllerClassType, string ActionName, string RouteName, string DisplayName, System.Web.Routing.RouteValueDictionary RouteInfo)
            {
                int ixControllerStart = ControllerClassType.Name.IndexOf("Controller");
                string controllerName = ControllerClassType.Name.Substring(0, ixControllerStart);
                RouteInfo["controller"] = controllerName;
                RouteInfo["action"] = ActionName;

                // 1) Prüfen: gibt es die Action bereits
                int ActionID = GetActionID(ControllerClassType, ActionName);

                return new ActionDescriptor()
                {
                    ActionID = ActionID,
                    DisplayName = DisplayName,
                    RouteName = RouteName,
                    RouteInfo = RouteInfo
                };

            }

        }

        public System.Web.Routing.RouteValueDictionary CreateRoutInfo(string WfID)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID } };
        }

        public System.Web.Routing.RouteValueDictionary CreateRoutInfo(string WfID, long id)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, { "id", id }};
        }

        public System.Web.Routing.RouteValueDictionary CreateRoutInfo(string WfID, long id, long subId)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, {"id", id}, {"subId", subId} };

        }

        // Routeinfo mit einer WorkflowID erzeugen
        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID } };
        }

        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID, string Controller)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, {"controller", Controller} };
        }

        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID, string Controller, string Action)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, { "controller", Controller }, {"action", Action} };
        }

        // Routeinfo mit einer WorkflowID und id erzeugen
        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID, long id)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, { "id", id } };
        }

        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID, long id, string Controller)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, { "id", id }, { "controller", Controller } };
        }

        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID, long id, string Controller, string Action)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, { "id", id }, { "controller", Controller }, { "action", Action } };
        }

        // Routeinfo mit einer WorkflowID, id und subId erzeugen
        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID, long id, long subId)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, { "id", id }, { "subId", subId } };
        }

        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID, long id, long subId, string Controller)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, { "id", id }, { "subId", subId }, { "controller", Controller } };
        }

        public static System.Web.Routing.RouteValueDictionary CreateRoutInfoS(string WfID, long id, long subId, string Controller, string Action)
        {
            return new System.Web.Routing.RouteValueDictionary() { { "WfID", WfID }, { "id", id }, { "subId", subId }, { "controller", Controller }, { "action", Action } };
        }


        /// <summary>
        /// Liefert den Namen der aktuellen Controllerklasse ohne das Controller Suffix
        /// </summary>
        public string ControllerName
        {
            get
            {
                // Name des Controllers aus der Typinformation zum Controllerobjekt extrahieren
                string ControllerName = GetType().Name;
                int startControllerSuffix = ControllerName.IndexOf("Controller");
                ControllerName = ControllerName.Substring(0, startControllerSuffix);
                return ControllerName;
            }
        }


        /// <summary>
        /// Berechnet eine ID für eine Action
        /// </summary>
        /// <param name="ControllerClass"></param>
        /// <param name="ActionName"></param>
        /// <returns></returns>
        public static int GetActionID(Type ControllerClass, string ActionName)
        {
            var ActionInfo = ControllerClass.GetMethod(ActionName);
            return ActionInfo.GetHashCode();
        }

        /// <summary>
        /// Gesamte Navigationsgeschichte löschen
        /// </summary>
        public void NavHistoryReset()
        {
            var NavHistoryStack = mkoIt.Asp.WorkflowState<Stack<ActionDescriptor>>.Get("NavHistory", Session);
            NavHistoryStack.Clear();
        }

        /// <summary>
        /// Zurückstutzen der Navigationsgeschichte auf die ersten n
        /// </summary>
        /// <param name="StackDeepth"></param>
        public void NavHistoryCuttingDownTo(int StackDeepth)
        {
            var NavHistoryStack = mkoIt.Asp.WorkflowState<Stack<ActionDescriptor>>.Get("NavHistory", Session);
            while (NavHistoryStack.Count > StackDeepth)
                NavHistoryStack.Pop();
        }

        /// <summary>
        /// Zeichnet eine Action in einem Stapelspeicher, der im Sitzungszustand gehalten wird, auf. 
        /// Wurde die Action bereits einmal aufgezeichnet, dann wird der Stapel zurückgestuzt bis zu dieser Aufzeichnung.
        /// </summary>
        /// <param name="ControllerClass"></param>
        /// <param name="ActionName"></param>
        /// <param name="RouteName"></param>
        /// <param name="DisplayName"></param>
        /// <param name="RouteInfo"></param>
        public void RecordAction(object ControllerClass, string ActionName, string RouteName, string DisplayName, System.Web.Routing.RouteValueDictionary RouteInfo)
        {
            var NavHistoryStack = mkoIt.Asp.WorkflowState<Stack<ActionDescriptor>>.Get("NavHistory", Session);

            // 1) Prüfen: gibt es die Action bereits
            int ActionID = GetActionID(ControllerClass.GetType(), ActionName);
            if (NavHistoryStack.Any(r => r.ActionID == ActionID))
            {
                // 1.1) Action bereits vorhanden => Stack bis zur vorhandenen Action abbauen
                while (NavHistoryStack.Peek().ActionID != ActionID)
                    NavHistoryStack.Pop();
            }
            else
            {
                // 1.2) Action noch nicht aufgezeichnet: Stack um Action erweitern
                var newEntry = ActionDescriptor.Create(ControllerClass, ActionName, RouteName, DisplayName, RouteInfo);
                NavHistoryStack.Push(newEntry);
            }
        }

        /// <summary>
        /// Liefert alle aufgezeichneten Actions in historischer Reihenfolge
        /// </summary>
        /// <returns></returns>
        public ActionDescriptor[] GetActionsPath()
        {
            var NavHistoryStack = mkoIt.Asp.WorkflowState<Stack<ActionDescriptor>>.Get("NavHistory", Session);

            if (NavHistoryStack.Count == 0)
                return new ActionDescriptor[] { };
            else
            {
                return NavHistoryStack.ToArray();
            }
        }

        /// <summary>
        /// Basisklasse aller Modelle in MVC, wenn die NavigationHistory genutzt werden soll
        /// </summary>
        public class NavHistoryModelBase 
        {
            /// <summary>
            /// aktueller Navigationspfad 
            /// </summary>
            public mko.Asp.Mvc.NavHistoryController.ActionDescriptor[] NavPath { get; set; }
        }

        /// <summary>
        /// Erzeugt und initialisiert ein Model mit NavigationHistory- Daten
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateModel<T>()
            where T : NavHistoryModelBase, new()
        {
            T Model = new T();
            Model.NavPath = GetActionsPath();
            return Model;
        }

        /// <summary>
        /// Zeichnet eine Action auf und erzeugt ein Modell für eine View, das mit Navigationsdaten gefüllt ist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ControllerClass"></param>
        /// <param name="ActionName"></param>
        /// <param name="RouteName"></param>
        /// <param name="DisplayName"></param>
        /// <param name="RouteInfo"></param>
        /// <returns></returns>
        public T RecordActionAndCreateModel<T>(object ControllerClass, string ActionName, string RouteName, string DisplayName, System.Web.Routing.RouteValueDictionary RouteInfo)
               where T : NavHistoryModelBase, new()
        {
            RecordAction(ControllerClass, ActionName, RouteName, DisplayName, RouteInfo);
            return CreateModel<T>();
        }

        /// <summary>
        /// Zeichnet eine Action auf und initialisiert ein Modell für eine View, das mit Navigationsdaten gefüllt ist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="ControllerClass"></param>
        /// <param name="ActionName"></param>
        /// <param name="RouteName"></param>
        /// <param name="DisplayName"></param>
        /// <param name="RouteInfo"></param>
        /// <returns></returns>
        public T RecordActionAndInitModel<T>(T Model, object ControllerClass, string ActionName, string RouteName, string DisplayName, System.Web.Routing.RouteValueDictionary RouteInfo)
               where T : NavHistoryModelBase, new()
        {
            RecordAction(ControllerClass, ActionName, RouteName, DisplayName, RouteInfo);
            Model.NavPath = GetActionsPath();
            return Model;
        }

        /// <summary>
        /// Initialisiert ein Modell mit Navigationsdaten
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public T NavHistoryInitModel<T>(T Model)
            where T : NavHistoryModelBase, new()
        {
            Model.NavPath = GetActionsPath();
            return Model;
        }

    }
}
