//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.2.2012
//
//  Projekt.......: mkoItAsp
//  Name..........: HtmlICss.cs
//  Aufgabe/Fkt...: Schnittstelle, die Eigenschaften von Html- Objekten definiert, über die programatisch der CssStyle 
//                  definiert werden kann.
//                  Zudem werden Eigenschaften geliefert, mit denen ein CssOutputGenerator- Klasse die Erzeugung der Css- Styles
//                  möglich wird
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
using System.Linq;
using System.Text;

using msWebUi = System.Web.UI;
using msHtml = System.Web.UI.HtmlControls;
using css = mkoIt.Xhtml.Css;

namespace mkoIt.Asp.HtmlCtrl
{
    /// <summary>
    /// Schnittstelle, die Eigenschaften von Html- Objekten definiert, über die programatisch der CssStyle 
    /// definiert werden kann.
    /// Zudem werden
    /// </summary>
    public interface ICss
    {
        /// <summary>
        /// Name der Css- Klasse, die auf das HtmlObjekt anzuwenden ist
        /// </summary>
        string CssClassName { get; set; }

        /// <summary>
        /// Stylebuilder, der die Stilregeln für das aktuelle HtmlObjekt definiert
        /// </summary>
        css.StyleBuilder CssStyleBld { get; set; }

        ///-------------------------------------------------------------------------------------------------------
        /// Eigenschaften und Methoden, die für die Implementierung des Algos benötigt werden, die die Styles erzeugen

        /// <summary>
        /// Liefert Referenz vom Typ HtmlControl auf das Objekt, welches diese ICss- Schnittstelle implementiert 
        /// </summary>
        msWebUi.Control Ctrl
        {
            get;
        }

        msWebUi.AttributeCollection CtrlAttributes
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        void PreRenderReworking();
    }
}
