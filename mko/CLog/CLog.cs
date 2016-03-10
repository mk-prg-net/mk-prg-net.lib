//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.2.2008
//
//  Projekt.......: mko
//  Name..........: CLog.cs
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
//</unit_history>
//</unit_header>    

using System;


namespace mko
{

    /// <summary>
    /// Zusammenfassung für CLog.
    /// </summary>	


    // Abstrakte Basisklasse von Klassen, die Fehlermeldungen darstellen und protokollieren
    [Serializable]
    public class CLog
    {

        // Ereignisse für Fehler und Nachrichten
        public delegate void DGLog(int no, string msg);
        public delegate void DGUserLog(string userId, string msg);

        public delegate void DGLogILogInfo(string userId, ILogInfo info);

        // 14.05.2009, mko
        // Ereignisse für die ILogInfo- Schnittstelle, an das Abonnenten Routinen zur
        // Protokollierung und Darstellung von Fehler-, Info und Statusmeldungen binden können
        public event DGLogILogInfo EventErrorILogInfo;
        public event DGLogILogInfo EventMsgILogInfo;
        public event DGLogILogInfo EventStatusILogInfo;

        public void Log(string userId, ILogInfo info)
        {
            lock (this)
            {
                switch (info.LogType)
                {
                    case EnumLogType.Error:
                        {
                            if (EventErrorILogInfo != null)
                                EventErrorILogInfo(userId, info);
                        } break;
                    case EnumLogType.Message:
                        {
                            if (EventMsgILogInfo != null)
                                EventMsgILogInfo(userId, info);
                        } break;
                    case EnumLogType.Status:
                        {
                            if (EventStatusILogInfo != null)
                                EventStatusILogInfo(userId, info);
                        } break;
                    default: throw new NotImplementedException("unbekannter LogType- Enum");                        
                }
            }
        }

        public void Log(ILogInfo info)
        {
            Log("*", info);
        }

        // Vealtete Status und Fehlermeldungen

        // Ereignis, an das Abonnenten Routinen zur Protokollierung und Darstellung von 
        // Fehlermeldungen binden können
        public event DGLog EventError;

        // Allgemeine Methode, über die Protokollierung und Darstellung von 
        // Fehlermeldungen angestoßen wird
        public void LogError(int errno, string msg)
        {
            lock (this)
            {
                if (EventError != null)
                    EventError(errno, msg);
            }
        }


        // Methode zur Protokollierung von Fehlern pro Benutzer
        public event DGUserLog EventUserError;

        public void LogUserError(string userId, string msg)
        {
            lock (this)
            {
                if (EventUserError != null)
                    EventUserError(userId, msg);

            }
        }

        // Ereignis, an das Abonnenten Routinen zur Protokollierung und Darstellung von 
        // allgemeinen Meldungen binden können
        public event DGLog EventMsg;

        // Allgemeine Methode, über die Protokollierung und Darstellung von 
        // allg. Meldungen angestoßen wird
        public void LogMsg(int msgno, string msg)
        {
            lock (this)
            {
                if (EventMsg != null)
                    EventMsg(msgno, msg);
            }
        }

        // Methode zur Protokollierung von Infomeldungen pro Benutzer
        public event DGUserLog EventUserMsg;

        public void LogUserMsg(string userId, string msg)
        {
            lock (this)
            {
                if (EventUserMsg != null)
                    EventUserMsg(userId, msg);

            }
        }

        // 15.9.2005, mko
        // Ereignis, an das Abonnenten Routinen zur Protokollierung und Darstellung von 
        // allgemeinen Meldungen binden können
        public event DGLog EventStatus;

        // Allgemeine Methode, über die Protokollierung und Darstellung von 
        // Programmzuständen angestoßen wird
        public void LogStatus(int statusno, string status)
        {
            lock (this)
            {
                if (EventStatus != null)
                    EventStatus(statusno, status);
            }
        }

        // Methode zur Protokollierung von Infomeldungen pro Benutzer
        public event DGUserLog EventUserStatus;

        public void LogUserStatus(string userId, string msg)
        {
            lock (this)
            {
                if (EventUserStatus != null)
                    EventUserStatus(userId, msg);

            }
        }




        // Registrieren eines Objektes mit Routinen zur Behandlung von Ereignissen
        // (Eventhandler)
        public void registerLogHnd(ILogHnd iLogHnd)
        {
            EventError += new CLog.DGLog(iLogHnd.OnError);
            EventErrorILogInfo += new CLog.DGLogILogInfo(iLogHnd.OnError);

            EventMsg += new CLog.DGLog(iLogHnd.OnMsg);
            EventMsgILogInfo += new CLog.DGLogILogInfo(iLogHnd.OnMsg);

            EventStatus += new CLog.DGLog(iLogHnd.OnStatus);
            EventStatusILogInfo += new CLog.DGLogILogInfo(iLogHnd.OnStatus);

            iLogHnd.SetSelfDeregisterDelegate(new DgDeregisterILogHnd(deregisterLogHnd));
        }

        // Deregistrieren eines Objektes mit Routinen zur Behandlung von Ereignissen
        // (Eventhandler)
        public void deregisterLogHnd(ILogHnd iLogHnd)
        {
            EventError -= new CLog.DGLog(iLogHnd.OnError);
            EventErrorILogInfo -= new CLog.DGLogILogInfo(iLogHnd.OnError);

            EventMsg -= new CLog.DGLog(iLogHnd.OnMsg);
            EventMsgILogInfo -= new CLog.DGLogILogInfo(iLogHnd.OnMsg);

            EventStatus -= new CLog.DGLog(iLogHnd.OnStatus);
            EventStatusILogInfo -= new CLog.DGLogILogInfo(iLogHnd.OnStatus);
        }

        // Registrieren eines Objektes mit Routinen zur Behandlung von Benutzerereignissen
        // (Eventhandler)
        public void registerUserLogHnd(IUserLogHnd iLogHnd)
        {
            EventUserError += new CLog.DGUserLog(iLogHnd.OnUserError);
            EventUserMsg += new CLog.DGUserLog(iLogHnd.OnUserMsg);
            EventUserStatus += new CLog.DGUserLog(iLogHnd.OnUserStatus);

            iLogHnd.SetSelfDeregisterDelegate(new DgDeregisterIUserLogHnd(deregisterUserLogHnd));
        }

        // Deregistrieren eines Objektes mit Routinen zur Behandlung von Benutzerereignissen
        // (Eventhandler)
        public void deregisterUserLogHnd(IUserLogHnd iLogHnd)
        {
            EventUserError -= new CLog.DGUserLog(iLogHnd.OnUserError);

            EventUserMsg -= new CLog.DGUserLog(iLogHnd.OnUserMsg);

            EventUserStatus -= new CLog.DGUserLog(iLogHnd.OnUserStatus);
        }


    }
}
