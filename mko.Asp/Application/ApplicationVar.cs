using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;


namespace mkoIt.Asp
{
    public class ApplicationVar<T>
    {
        HttpApplicationState Application;
        string name;

        // Im Konstruktor wird eine Referenz auf die aktuelle Webform hinterlegt
        // Es wird der Name des Eintrags in der Session festgelegt
        public ApplicationVar(HttpApplicationState Application, string name)
        {
            this.Application = Application;
            this.name = name;

        }

        public ApplicationVar(HttpApplicationState Application, string name, T InitialValue)
        {
            this.Application = Application;
            this.name = name;
            Application[name] = InitialValue;
        }

        // Typisierter Zugriff auf die Sessions
        public T Value
        {
            get
            {
                return (T)Application[name];
            }
            set
            {
                Application[name] = value;
            }
        }
    }
}