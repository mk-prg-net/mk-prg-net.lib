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
//</unit_history>
//</unit_header>    

using System;


namespace ATMO.mko.Logging
{

    /// <summary>
    /// Zusammenfassung für CLog.
    /// </summary>	


    // Abstrakte Basisklasse von Klassen, die Fehlermeldungen darstellen und protokollieren
    [Serializable]
    public class LogServer
    {
        static long LogCounter {
            get {
                return _LogCounter;
            }
        }

        static long _LogCounter;        

        static void IncLogCounter()
        {
            System.Threading.Interlocked.Increment(ref _LogCounter);
        }


        

        // 14.05.2009, mko
        // Ereignisse für Fehler und Nachrichten
        // Ereignisse für die ILogInfo- Schnittstelle, an das Abonnenten Routinen zur
        // Protokollierung und Darstellung von Fehler-, Info und Statusmeldungen binden können
        //
        // 8.3.2018, mko
        // Erweitert um 1. Parameter long == LogCounter
        public event Action<long, string, ILogInfo> EventLog;

        /// <summary>
        /// Ereignisse für TraceInfo- Meldungen
        /// </summary>
        public event Action<long, ITraceInfo> TiLog;

        /// <summary>
        /// User, who is currently active
        /// </summary>
        public string User => _User;
        string _User = "-";

        /// <summary>
        /// mko, 13.2.2018
        /// Defines the user, who is currently active
        /// </summary>
        /// <param name="UserName"></param>
        public void SetUser(string UserName)
        {
            _User = UserName;
        }


        public void Log(string userId, ILogInfo info)
        {
            lock (this)
            {
                IncLogCounter();
                EventLog?.Invoke(_LogCounter, userId, info);
            }
        }

        /// <summary>
        /// Aufzeichnen von Fehler, Status und allg. Meldungen
        /// </summary>
        /// <param name="info"></param>
        public void Log(ILogInfo info)
        {
            Log(_User, info);
        }

        /// <summary>
        /// Aufzeichnen von Trace- Meldungen
        /// </summary>
        /// <param name="traceInfo"></param>
        public void LogTi(ITraceInfo traceInfo)
        {
            lock (this)
            {
                TiLog?.Invoke(_LogCounter, traceInfo);
            }
        }


        // Registrieren eines Objektes mit Routinen zur Behandlung von Ereignissen
        // (Eventhandler)
        public void registerLogHnd(ILogHnd iLogHnd)
        {
            EventLog += iLogHnd.OnLog;
            TiLog += iLogHnd.OnLog;
            iLogHnd.SetSelfDeregisterDelegate(new DgDeregisterILogHnd(deregisterLogHnd));
        }

        // Deregistrieren eines Objektes mit Routinen zur Behandlung von Ereignissen
        // (Eventhandler)
        public void deregisterLogHnd(ILogHnd iLogHnd)
        {
            EventLog -= iLogHnd.OnLog;
            TiLog -= iLogHnd.OnLog;
        }
    }
}
