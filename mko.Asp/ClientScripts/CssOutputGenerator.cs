//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.2.2012
//
//  Projekt.......: mkoItAsp
//  Name..........: CssOutputGenerator
//  Aufgabe/Fkt...: HtmlObjekte, die die ICss- Schnittstelle implementieren,
//                  werden mittels der Funktionen dieser Klasse so konfiguriert,
//                  das die Stilregeln, definiert in den ICss- Membern, an
//                  den Browser ausgegeben werden
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.0
//  Werkzeuge.....: Visual Studio 2010
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

using msWebUi = System.Web.UI;
using css = mkoIt.Xhtml.Css;

namespace mkoIt.Asp.HtmlCtrl
{
    public class CssOutputGenerator
    {
        public static void config(ICss CtrlWithCss)
        {
            CtrlWithCss.Ctrl.PreRender += (sender, e) =>
            {

                if (!string.IsNullOrEmpty(CtrlWithCss.CssClassName)) CtrlWithCss.CtrlAttributes.Add("class", CtrlWithCss.CssClassName);
                if (CtrlWithCss.CssStyleBld != null) CtrlWithCss.CtrlAttributes.Add("style", CtrlWithCss.CssStyleBld.ToString());

                // Überarbeitung des Html- Elementes vor der Auslieferung in abgeleiteten Klassen
                CtrlWithCss.PreRenderReworking();
            };
        }
    }
}
