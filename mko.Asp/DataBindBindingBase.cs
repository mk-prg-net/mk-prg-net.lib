//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den März 2012
//
//  Projekt.......: mko.Asp
//  Name..........: DataBindBindingBase
//  Aufgabe/Fkt...: Basisklasse für Definitionen von Datenbindungen. 
//                  Eine Datenbindung kann ein WebSteuerelement an ein Datenfeld aus einer Datenquelle binden.
//                  Umgekehrt kann ein Datenfeld an die Werte von WebSteuerelementen gebunden werden.
//                  Für jede Richtung gibt es eine eigene Klasse
//
//                  Field   1-- BindingWebCtrl   --1> WebCtrl
//                  WebCtrl N-- BindingDataField --1> Field
//                   
//                  Wie aus den Diagrammen ersichtlich, kann durch eine Bindung jeweils ein Feld an ein WebControl gebunden 
//                  werden. Das schließt auch die Möglichkeit ein, einen Feldwert gleichzeitig in N WebControls darzustellen.
//                  Umgekehrt kann sich der Wert eines Feldes aus den Eingaben in N WebControls zusammensetzen.
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
//  Datum.........: 21.11.2012
//  Änderungen....: Eigenschaft WebCtrl hinzugefügt.
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
    public class BindingBase
    {
        public BindingBase() { }

        public msWebCtrl.WebControl WebCtrl { get; set; }
    }
}
