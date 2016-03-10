//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 4.4.2012
//
//  Projekt.......: mkoitAsp
//  Name..........: ClientComponentScriptBldBase
//  Aufgabe/Fkt...: Basisklasse für Builder von Scriptkomponenten eines
//                  Websteuerelementes
//
//
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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace mkoIt.Asp.HtmlCtrl
{
    public class ClientComponentScriptBldBase
    {
        /// <summary>
        /// Hilfsklassen, mit denen MSAjax $create- Methoden erstellt werden
        /// </summary>
        /// 

        // Erzeugt eine MSAjax Scriptcomponente, die an ein Dom- Element gebunden ist
        class MSAjaxControlCreator : System.Web.UI.ScriptControlDescriptor
        {
            public MSAjaxControlCreator(string TypeNameOfControl, string ClientId)
                : base(TypeNameOfControl, ClientId) { }

            public string GetCreateScript() {
                return GetScript();
            }
        }

        // Erzeugt eine allgemeine MSAjax Scriptcomponente
        //class MSAjaxComponentCreator : System.Web.UI.ScriptComponentDescriptor
        //{
        //    public string GetCreateScript()
        //    {
        //        return GetScript();
        //    }
        //}


        string IdClientComponent;

        public ClientComponentScriptBldBase(string IdClientComponent)
        {
            this.IdClientComponent = IdClientComponent;
        }

        /// <summary>
        /// Erzeugt eine Liste mit JavaScript Methodenaufrufen aus der MSAjax Bibliothek, durch die 
        /// ein ClientControl- Objekt vom angeforderten Typ für das Dom Element mit der IdClientComponent erzeugt
        /// </summary>
        /// <param name="TypeNameOfClientScriptControl"></param>
        /// <returns></returns>
        public string CreateScriptControl(string TypeNameOfClientScriptControl) {

            return new MSAjaxControlCreator(TypeNameOfClientScriptControl, IdClientComponent).GetCreateScript();
        }


        public string CreateMSAjaxPageLoadEventHandler(string ClientScriptBlock, bool encapsulateInHtmlScriptElement)
        {
            if(encapsulateInHtmlScriptElement)
                return " <script type='text/javascript'>  function pageLoad() { "  + ClientScriptBlock + " ; } <" + '/' + "script> ";
            else
                return " function pageLoad() { " + ClientScriptBlock + "; } ";
        }

        /// <summary>
        /// Erzeugt einen JavaScript Funktion. Es müssen mindestens 2 Parameter übergeben werden
        /// (funktionsname, funktionsrumpf). Werden mehr als zwei Parameter übergeben, dann werden die restlichen
        /// als Parameter eingesetzt
        /// </summary>
        /// <param name="mparams"></param>
        /// <returns></returns>
        public string CreateFunction(params string[] mparams)
        {
            Debug.Assert(mparams.Length >= 2);
            var bld = new StringBuilder();

            bld.Append(" function ");
            bld.Append(mparams[0]);
            bld.Append("(");
            if (mparams.Length > 2)
            {
                for (int i = 2; i < mparams.Length; i++)
                {
                    bld.Append(mparams[i]);
                    bld.Append(", ");
                }
                bld.Remove(bld.Length - 1, 1);
            }
            bld.Append(") { ");
            bld.Append(mparams[1]);
            bld.Append(" } ");

            return bld.ToString();
        }

        public string Eval(params string[] javaScriptStatements)
        {
            var bld = new StringBuilder();
            bld.Append(" eval(\"");
            foreach (var statement in javaScriptStatements)
            {
                bld.Append(statement);
                bld.Append("; ");
            }
            bld.Append("\") ");
            return bld.ToString();
        }


        /// <summary>
        /// Zugriff auf die Script- Komponente
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return "$find('" + IdClientComponent + "', null)";
        }

        public string CreateReturnFalse()
        {
            return "return false;";
        }

        /// <summary>
        /// Erzeugt einen Methodeneufruf einer Scriptkomponente
        /// </summary>
        /// <param name="mparams"></param>
        /// <returns></returns>
        public string CreateMethodCall(params string[] mparams)
        {

            string mcall = Get() + "." + mparams[0] + "(";

            // Parameter einbauen
            for (int i = 1; i < mparams.Length; i++)
                mcall += mparams[i] + ",";

            if (mcall.EndsWith(","))
                mcall = mcall.Substring(0, mcall.Length - 1);

            return mcall + ");";
        }

        /// <summary>
        /// Erzeugt einen Methodeneufruf einer Scriptkomponente
        /// </summary>
        /// <param name="mparams"></param>
        /// <returns></returns>
        public string CreateMethodCallAndReturn(params string[] mparams)
        {

            string mcall = Get() + "." + mparams[0] + "(";

            // Parameter einbauen
            for (int i = 1; i < mparams.Length; i++)
                mcall += mparams[i] + ",";

            if (mcall.EndsWith(","))
                mcall = mcall.Substring(0, mcall.Length - 1);

            return mcall + ");return false;";

        }
    }
}
