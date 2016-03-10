//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 31.1.2012
//
//  Projekt.......: mkoItAsp
//  Name..........: HtmlBR.cs
//  Aufgabe/Fkt...: Gibt einen HTML- Zeilenumbruch aus
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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mkoIt.Asp.HtmlCtrl
{    
    [ToolboxData("<{0}:BR runat=server />")]
    public class BR : HtmlGenericSelfClosing
    {
        public BR()
            : base("br") {
            
        }
        
    }
}
