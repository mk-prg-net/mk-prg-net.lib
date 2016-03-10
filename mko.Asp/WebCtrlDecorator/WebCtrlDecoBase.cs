//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, November 2012 
//
//  Projekt.......: mko.Asp
//  Name..........: WebCtrlDecoBase
//  Aufgabe/Fkt...: Basisklasse für Dekoratoren von Webcontrols (Decorator Pattern).
//                  Die Basisklasse implementiert allgemeine Funktionen auf der 
//                  Basisklasse WebControl. Alle WebControlDecorators können in Listen über dieser
//                  Basisklasse verwaltet werden.
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
using msWebCtrl = System.Web.UI.WebControls;
using Css = mkoIt.Xhtml.Css;

namespace mkoIt.Asp.WebCtrlDecorator
{
    public abstract class WebCtrlDecoBase
    {
        public WebCtrlDecoBase(msWebCtrl.WebControl ctrl)
        {
            this.Ctrl = ctrl;
            Ctrl.DataBinding += new EventHandler(Ctrl_DataBinding);
            Ctrl.PreRender += new EventHandler(Ctrl_PreRender);

            _CssStyleBuilder = new Css.StyleBuilder();
        }

        void Ctrl_PreRender(object sender, EventArgs e)
        {            
            Ctrl.Attributes.Add("style", CssStyle.ToString());
        }

        public WebCtrlDecoBase(msWebCtrl.WebControl ctrl, Css.StyleBuilder CssStyleBuilder)
        {
            this.Ctrl = ctrl;
            Ctrl.DataBinding += new EventHandler(Ctrl_DataBinding);
            Ctrl.PreRender += new EventHandler(Ctrl_PreRender);

            _CssStyleBuilder = CssStyleBuilder;
        }

        void Ctrl_DataBinding(object sender, EventArgs e)
        {
            DataBind();
        }

        /// <summary>
        /// Verweis auf das Control, das durch diesen Dekorator erweitert wird
        /// </summary>
        protected msWebCtrl.WebControl Ctrl
        {
            get;
            set;
        }

        /// <summary>
        /// Implementierung der Datenbindung für WebCtrlDecoBase. WebCtrlDecoBase ist nicht 
        /// von der Klasse System.Web.UI.Controls oder .BaseDataBoundControl abgeleitet !
        /// Damit ist diese Implementierung der Datenbindung hier unabhängig von der in ASP.NET implementierten.
        /// Analog wie in der ASP.NET Implementierung der Datenbindung wird mittels DataBind der
        /// Datenabgleich von Quelle zu den Eigenschaften des vom Dekorator verwalteten WebControls 
        /// initiiert. 
        /// Das WebControl selber kann auch im Kontext eines datengebundenen Steuerelementes
        /// (z.B. ItemTemlate einer GridView) deklariert sein. Dann wird durch die ASP.NET Implementierung
        /// der Datenbindung automat. das Event DataBinding vom Webcontrol gefeuert, wenn für den Kontext die 
        /// klassische Datenbindung ausgeführt wird. Für diesen Fall registriert der WebCtrlDecoBase im 
        /// Konstruktor einen Event- Handler, der diese DataBind Methode aufruft.
        /// Ein WebControl, was durch einen WebCtrlDecoBase verwaltet wird, sollte wg. der zuvor erläuterten 
        /// eigenständigen Implementierung der Datenbindung im WebCtrlDecoBase keine klassischen 
        /// Datenbindungsausdrücke im Markup enthalten. Sonst besteht die Gefahr, eine Eigenschaft doppelt
        /// zu binden.
        /// </summary>
        public abstract void DataBind();

        /// <summary>
        /// Aktualisiert die Felder eines Datenobjekts mit Werten, die aus den Eigenschaften des vom Decorator verwalteten
        /// WebControls errechnen
        /// </summary>
        public abstract void Update();


        /// <summary>
        /// Visuelle Gestaltung mittels CSS- Stilregeln
        /// </summary>
        public Css.StyleBuilder CssStyle
        {
            get
            {
                return _CssStyleBuilder;
            }
        }
        Css.StyleBuilder _CssStyleBuilder;

        /// <summary>
        /// Speziellen Eventhandler für den Zeitpunkt der Bindung an die Felder eines Datenobjektes setzen.
        /// Achtung: Abgeleitete Klassen wie WebCtrlPropertyBinder haben bereits einen Eventhandler gesetzt,
        /// der die Datenbindung implementiert
        /// </summary>
        public EventHandler SetOnDataBinding
        {
            set
            {
                Ctrl.DataBinding += value;
            }
        }

        /// <summary>
        /// Den OnLoad Eventhandler setzen
        /// </summary>
        public EventHandler SetOnLoad
        {
            set
            {
                Ctrl.Load += value;
            }
        }

        /// <summary>
        /// Speziellen Eventhandler für den Zeitpunkt vor dem Erzeugen von HTML- Code setzen.
        /// Achtung: Zum setzten des Css style- Attributes wurde bereits ein Eventhandler gesetzt.
        /// </summary>
        public EventHandler SetOnPreRender{         
            set
            {
                Ctrl.PreRender += value;
            }
        }
    }
}
