//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.6.2014
//
//  Projekt.......: mko.Asp.Mvc
//  Name..........: ControllerHelper.cs
//  Aufgabe/Fkt...: Hilffunktionen, die in MVC- Controllern eingesetzt werden.
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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
using System.Threading.Tasks;

namespace mko.Asp.Mvc
{
    public class ControllerHelper
    {
        public static int PageNumberNormalize(int PageNumberRaw, int PageSize, int CountEntities) {
            // Neue Seitennummer setzen

            int PageNumber =  PageNumberRaw;

            if (PageNumberRaw < 1)
                PageNumber = 1;
            else if ((PageNumberRaw - 1) * PageSize > CountEntities)
                PageNumber = 1 + CountEntities / PageSize;

            return PageNumber;
        }
    }
}
