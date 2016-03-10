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
//</unit_history>
//</unit_header>    

using System;


namespace mko.Log
{

    /// <summary>
    /// Zusammenfassung für CLog.
    /// </summary>	


    // Abstrakte Basisklasse von Klassen, die Fehlermeldungen darstellen und protokollieren
    [Serializable]
    public class LogServer
    {

        // Ereignisse für Fehler und Nachrichten
        public delegate void DGLogILogInfo(string userId, ILogInfo info);

        // 14.05.2009, mko
        // Ereignisse für die ILogInfo- Schnittstelle, an das Abonnenten Routinen zur
        // Protokollierung und Darstellung von Fehler-, Info und Statusmeldungen binden können
        public event DGLogILogInfo EventLog;

        public void Log(string userId, ILogInfo info)
        {
            lock (this)
            {
                if (EventLog != null)
                    EventLog(userId, info);
            }
        }

        public void Log(ILogInfo info)
        {
            Log("*", info);
        }


        // Registrieren eines Objektes mit Routinen zur Behandlung von Ereignissen
        // (Eventhandler)
        public void registerLogHnd(ILogHnd iLogHnd)
        {
            EventLog += new LogServer.DGLogILogInfo(iLogHnd.OnLog);
            iLogHnd.SetSelfDeregisterDelegate(new DgDeregisterILogHnd(deregisterLogHnd));
        }

        // Deregistrieren eines Objektes mit Routinen zur Behandlung von Ereignissen
        // (Eventhandler)
        public void deregisterLogHnd(ILogHnd iLogHnd)
        {
            EventLog -= new LogServer.DGLogILogInfo(iLogHnd.OnLog);
        }
    }
}


