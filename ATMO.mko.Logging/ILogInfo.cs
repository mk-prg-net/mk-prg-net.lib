//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 13.5.2009
//
//  Projekt.......: mko.CLog
//  Name..........: ILogInfo
//  Aufgabe/Fkt...: Schnittstelle, um zu protokollierenden Infos zur
//                  Laufzeit zu einem Objekt abzurufen (z.B. einen mko.ReturnCode)
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 14.7.2009
//  Änderungen....: - Eigenschaft MessageCodeToString entfernt
//                  - Eigenschaft LogDate hinzugefügt
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Text;

namespace ATMO.mko.Logging
{
    public enum EnumLogType
    {
        Error,          // Die Aufzeichnung beschreibt eine Fehlermeldung
        Message,        // Die Aufzeichnung beschreibt eine Erfolgsmeldung
        Status,         // Die Aufzeichnung beschreibt eine Zustandsmeldung
    }

    public interface ILogInfo
    {

        // Typ des Logbucheintrages
        EnumLogType LogType
        {
            get;
        }

        // Das Datum des Eintrages
        DateTime LogDate
        {
            get;
        }

        // Der aufzuzeichnende Logbucheintrag
        string Message
        {
            get;
        }
    }
}

