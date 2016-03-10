//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, November 2012 
//
//  Projekt.......: mko.Asp
//  Name..........: WebCtrlDeco
//  Aufgabe/Fkt...: Generische Klasse für Dekoratoren von Webcontrols (Decorator Pattern).
//                  Ist von der Basisklasse WebCtrlDecoBase abgeleitet und implementiert  
//                  streng typisiert die Datenbindung mittels WebCtrlPropertyBinder.
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

namespace mkoIt.Asp.WebCtrlDecorator
{
    public class WebCtrlDeco<TWebCtrl> : WebCtrlDecoBase
        where TWebCtrl : msWebCtrl.WebControl
    {
        public WebCtrlDeco(TWebCtrl ctrl) : base(ctrl) { }

        public WebCtrlDeco(TWebCtrl ctrl, mkoIt.Asp.DataBind.IWebCtrlPropertySetter[] Bindings) 
            : base(ctrl) 
        {
            this.Bindings = Bindings;
        }

        mkoIt.Asp.DataBind.IWebCtrlPropertySetter[] Bindings;

        mkoIt.Asp.DataBind.IDataFieldUpdater[] Updater;

        /// <summary>
        /// Implementiert die Datenbindung
        /// </summary>
        public override void DataBind()
        {
            if (Bindings != null)
            {
                foreach (var binder in Bindings)
                {
                    binder.SetProperty(Ctrl);
                }
            }
        }

        public override void Update()
        {
            if (Updater != null)
            {
                foreach (var upd in Updater)
                {
                    upd.SetField(Ctrl);
                }
            }
        }
    }
}
