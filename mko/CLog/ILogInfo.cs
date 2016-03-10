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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Text;

namespace mko
{
    public enum EnumLogType
    {
        Error,          // Die Aufzeichnung beschreibt eine Fehlermeldung
        Message,        // Die Aufzeichnung beschreibt eine Erfolgsmeldung
        Status          // Dir Aufzeichnung beschreibt eine Zustandsmeldung
    }

    public interface ILogInfo
    {       

        // 
        EnumLogType LogType
        {
            get;
        }

        // Die aufzuzeichnende Textmeldung
        string Message { 
            get;
        }

        // Die ID als Text. Ist die ID Teil eines enums, dann ist der Text gleich
        // dem Symbol für die ID im Quelltext
        string MessageCodeToString();

    }
}
