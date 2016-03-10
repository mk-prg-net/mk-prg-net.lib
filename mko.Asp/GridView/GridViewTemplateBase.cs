//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 21.2.2012
//
//  Projekt.......: mkoIt.Asp
//  Name..........: GridViewTemplateBase.cs
//  Aufgabe/Fkt...: TemplateBase: Basisklasse für GridView- Templates
//                  
//
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

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using mkx = mkoIt.Xhtml.Xhtml;
using css = mkoIt.Xhtml.Css;


namespace mkoIt.Asp.GridView
{
    public abstract class TemplateBase : ITemplate
    {
        /// <summary>
        /// Css- Klasse für den Rahmen eines Item- Templates
        /// </summary>
        public string CssClassFrame { get; set; }

        /// <summary>
        /// Css- Stylesheets des Rahmens
        /// </summary>
        public css.StyleBuilder StyleFrame { get; set; }

        public TemplateBase()
        {
            StyleFrame = new css.StyleBuilder()
            {
                ForeColor = css.Color.Black                
            };
        }

        /// <summary>
        /// Erzeugt den Inhalt eines ItemTemplates
        /// </summary>
        /// <returns></returns>
        protected abstract void CreateContent(Control NamingContainer, ControlCollection content);

        public void InstantiateIn(Control container)
        {
            // Rahmen definieren
            var divFrame = new mkoIt.Asp.HtmlCtrl.DIV();
            divFrame.CssClassName = CssClassFrame;
            divFrame.CssStyleBld = StyleFrame;
            container.Controls.Add(divFrame);

            // Inhalt aufbauen
            CreateContent(container.NamingContainer, divFrame.Controls);
        }
    }
}
