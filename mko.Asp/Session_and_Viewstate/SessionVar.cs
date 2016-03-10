//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.04.2007
//
//  Projekt.......: PSZ Porsche Software Zertifizierung
//  Name..........: SessionVar.cs
//  Aufgabe/Fkt...: Diese generische Klasse ermöglicht den typisierten
//                  Zugriff auf Session- Variablen. 
//                  Ein weiterer Vorteil beim Einsatz dieser Klasse entsteht
//                  durch die einmalige Festlegung des alphanumerischen 
//                  Zugriffsschlüssels für die Sessionvariable bei der 
//                  Konstruktion
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
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;

namespace mkoIt.Asp
{
    public class SessionVar<T>
    {
        HttpSessionState Session;
        string name;

        // Im Konstruktor wird eine Referenz auf die aktuelle Webform hinterlegt
        // Es wird der Name des Eintrags in der Session festgelegt
        public SessionVar(HttpSessionState Session, string name)
        {
            this.Session = Session;
            this.name = name;

        }

        public SessionVar(HttpSessionState Session, string name, T InitialValue)
        {
            this.Session = Session;
            this.name = name;
            Session[name] = InitialValue;
        }

        // Typisierter Zugriff auf die Sessions
        public T Value
        {
            get
            {
                return (T)Session[name];
            }
            set
            {
                Session[name] = value;
            }
        }
    }
}
