//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
//                  
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
using msWebCtrl = System.Web.UI.WebControls;

namespace mkoIt.Asp.DataBind
{
    class WebCtrlPropertyBinder<TWebCtrl, TProperty> : IWebCtrlPropertySetter
        where TWebCtrl : msWebCtrl.WebControl
    {
        public WebCtrlPropertyBinder(Action<TWebCtrl, TProperty> SetPropertyOperator, Func<IEnumerable<object>, TProperty> MapToPropertyValue, params object[] DataSources)
        {
            this.SetPropertyOperator = SetPropertyOperator;
            this.MapToPropertyValue = MapToPropertyValue;
            this.DataSources = DataSources;            
        }

        public WebCtrlPropertyBinder(Action<TWebCtrl, TProperty> SetPropertyOperator, Func<IEnumerable<object>, TProperty> MapToPropertyValue, IEnumerable<object> DataSources)
        {
            this.SetPropertyOperator = SetPropertyOperator;
            this.MapToPropertyValue = MapToPropertyValue;
            this.DataSources = DataSources;
        }


        /// <summary>
        /// Implementierung von IWebCtrlPropertySetter
        /// </summary>
        /// <param name="ctrl"></param>
        public void SetProperty(System.Web.UI.WebControls.WebControl ctrl)
        {
            SetPropertyOperator((TWebCtrl)ctrl, MapToPropertyValue(DataSources));
        }

        Action<TWebCtrl, TProperty> SetPropertyOperator;

        /// <summary>
        /// Bildet die Datenquellen auf einen gültigen Eigenschaftswert ab.
        /// Der Zugriff auf die Datenquellen und die Zusammenfassung zu einem Eigenschaftswert sind hier
        /// zu definieren (z.B. als Lambda- Ausdruck)
        /// </summary>
        Func<IEnumerable<object>, TProperty> MapToPropertyValue;

        /// <summary>
        /// Liste der Datenquellen
        /// </summary>
        IEnumerable<object> DataSources;
    }
}
