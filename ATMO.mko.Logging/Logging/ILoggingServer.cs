using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

namespace ATMO.mko.Logging.Logging
{

    //<unit_header>
    //----------------------------------------------------------------
    //
    // Martin Korneffel: IT Beratung/Softwareentwicklung
    // Stuttgart, den 18.2.2008
    //
    //  Projekt.......: mko
    //  Name..........: LogServer.cs
    //  Aufgabe/Fkt...: Klasse zur Protokollierung von Status-, Info- und Fehlermeldungen.
    //                  Protokollmethoden sind nach Meldungstyp gegliedert, und unabhängig
    //                  vom Protokollmedium. Das Protokollmedium wird über sog. "EventLogHandler"
    //                  bereitgestellt.
    //
    //
    //<unit_environment>
    //------------------------------------------------------------------
    //  Zielmaschine..: PC 
    //  Betriebssystem: Windows XP mit .NET 2.0
    //  Werkzeuge.....: Visual Studio 2005
    //  Autor.........: Martin Korneffel (mko)
    //  Version 1.0...: 2004
    //
    // </unit_environment>
    //
    //<unit_history>
    //------------------------------------------------------------------
    //
    //  Version.......: 1.1
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 13.5.2009
    //  Änderungen....: Protokollmethoden für ILogInfo hinzugefügt
    //
    //  Version.......: 2.0
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 14.7.2009
    //  Änderungen....: Klasse umbenannt von CLog in LogServer
    //
    //  Version.......: 2.1
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 13.2.2018
    //  Änderungen....: Erweitert um die Eigenschaft User. Für diesen erfolgen standardmäßig die Logmeldungen.
    //
    //  Version.......: 2.1
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 8.3.2018
    //  Änderungen....: Log- Zähler wird jetzt verwaltet. Für jede Meldung wird ein Zähler erhöht, und an die Loghandler
    //                  mitgegeben. so wird eine chronologische Reihenfolge aufrechterhalten, da Zeitstempel in der Regel zu ungenau sind.
    //
    //  Version.......: 18.12.x
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 19.12.2018
    //  Änderungen....: Komplette Neuimplementierung: 
    //                  Auftrennen von Spezifikation und implementierung durch Definition einer Schnittstelle.
    //
    //</unit_history>
    //</unit_header>    

    public interface ILoggingServer
    {
        /// <summary>
        /// mko, 19.12.2018
        /// System zur Aufzeichung von Laufzeitinformationen. Entstanden aus dem ATMO.mko.Logging.LogServer.
        /// Zeichnet Laufzeitinformationen auf.
        /// Informationen wie SessionId, UserId und innnerhalb einer Session die fortlaufende Nummer 
        /// werden bei der Instanziierung des Loggin- Servers definiert oder während des BEtriebes berechnet.
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        void Log(

            // Grobe Klassifikation der Meldung
            EnumLogTypeDFC logType,

            // Das die Meldung beschreibende Entity
            PNDocuTerms.DocuEntities.IDocuEntity docuEntity);

        /// <summary>
        /// Definiert den Benutzer, in dessen Kontext das Logging erfolgt
        /// </summary>
        /// <param name="userId"></param>
        void SetUserId(string userId);


        /// <summary>
        /// Definiert die Nummer der Sitzung, in deren Kontext die Logs erfasst werden.
        /// </summary>
        /// <param name="SessionId"></param>
        void SetSessionId(long SessionId);


        /// <summary>
        /// mko, 19.12.2018
        /// Hier sind die Funktionen zu registrieren, welche die Laufzeitinformationen auf speziellen medien  abspeichern
        /// Aufzeichnen aller Meldungen, die Zustandswechsel in der Geschäftslogik protokollieren.
        /// </summary>
        event Action<ILogInfo18_12> AppendToLogStateStream;

        /// <summary>
        /// Aufzeichnen aller Meldungen, die als Fehlermeldungen klassifiziert sind.
        /// </summary>
        event Action<ILogInfo18_12> AppendToLogErrorsStream;
        
        /// <summary>
        /// Auzeichnen aller Meldungen, die als allgemeine Informationen klassifiziert sind.
        /// </summary>
        event Action<ILogInfo18_12> AppendToLogInfosStream;

        /// <summary>
        /// Aufzeichen aller Meldungen, die zur Beobachtung interner Abläufe im Betrieb 
        /// (Telemetrie).
        /// </summary>
        event Action<ILogInfo18_12> AppendToLogTelemetryStream;


        /// <summary>
        /// Aufzeichnen aller Meldungen, die als Unternehmenskritisch gekennzeichnet sind
        /// </summary>
        event Action<ILogInfo18_12> AppendToLogMissionCriticalEventsStream;

    }
}
