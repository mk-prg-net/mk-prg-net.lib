//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den November 2012
//
//  Projekt.......: mko.Asp
//  Name..........: IWebCtrlPropertySetter.cs
//  Aufgabe/Fkt...: Schnittstelle zu Objekten, deren Aufgabe das Setzen einer Eigenschaft
//                  in einem WebControl beim implementiern der Datenbindung ist.
//                  Ein WebCtrlPropertySetter wird in einem WebControl oder einem WebControl-
//                  dekorator aufbewahrt. Das Binden an die Daten wird durch den Aufruf der 
//                  Set- Methode erreicht.  
//                  Der WebCtrlPropertySetter kann die Bindung direkt implementiern, oder diesen
//                  z.B. über Datenbindungen aus mehreren Quellen berechnen. 
//                  Set soll folgende Varianten implementieren:
//                  1) WebCtrl.Property := DataSource
//                  2) WebCtrl.Property := MapToPropertyValue(DataSource1, DataSource2, ..., DataSourceN)
//                  Eine generische Implementierung eines Setters ist der WebCtrlPropertyBinder im
//                  gleichen Namensraum
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
    public interface IWebCtrlPropertySetter
    {
        /// <summary>
        /// Methode zum setzen einer Eigenschaft in einem Webcontrol
        /// </summary>
        /// <param name="ctrl"></param>
        void SetProperty(msWebCtrl.WebControl ctrl);        
    }
}
