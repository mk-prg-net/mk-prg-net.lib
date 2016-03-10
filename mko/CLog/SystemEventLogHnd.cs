//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.2.2008
//
//  Projekt.......: mko
//  Name..........: SystemEventLogHnd.cs
//  Aufgabe/Fkt...: Implementierung eines Log- Handlers zur Ausgabe im 
//                  Wiondows- Systemlog
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
//  Version 1.0...: 2004
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.2.2008
//  Änderungen....: Fehlerbehandlung implementiert, die die Eventhandler selbstständig
//                  abhängt.
//</unit_history>
//</unit_header>    
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace mko
{
    public class SystemEventLogHnd : ILogHnd, IUserLogHnd
    {
        // Protokollierung von Meldungen in den Eventlogs des Systems

        System.Diagnostics.EventLog log;

        public bool MsgInEventLogSchreiben = false;
        public bool StatusInEventLogSchreiben = false;

        public SystemEventLogHnd(string SourceName)
        {
            // Berechtigung anfordern: lesen, schreiben
            //System.Security.IPermission perm = new System.Diagnostics.EventLogPermission(System.Diagnostics.EventLogPermissionAccess.Write, ".");
            //perm.Demand();

            // Eine Quelle Registrieren
            if (!System.Diagnostics.EventLog.SourceExists(SourceName))
            {
                //EventSourceCreationData dat = new EventSourceCreationData(SourceName, "Application");
                System.Diagnostics.EventLog.CreateEventSource(SourceName, "Application", ".");
            }

            log = new System.Diagnostics.EventLog();
            log.Source = SourceName;
        }


        #region ILogHnd Member

        DgDeregisterILogHnd dgDeregisterILogHnd = null;
        void ILogHnd.SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if (dg != null)
                dgDeregisterILogHnd = dg;
        }

        void SelfDeregisterILogHnd()
        {
            if (dgDeregisterILogHnd != null)
                dgDeregisterILogHnd(this);
        }

        void ILogHnd.OnMsg(int errno, string msg)
        {
            if (MsgInEventLogSchreiben)
            {
                string descr = string.Format("Info: {0}", msg);
                log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Information);
            }
        }

        void ILogHnd.OnError(int errno, string msg)
        {
            try
            {
                string descr = string.Format("Fehler: {0}", msg);
                log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void ILogHnd.OnStatus(int errno, string msg)
        {
            try
            {
                if (StatusInEventLogSchreiben)
                {
                    string descr = string.Format("Status: {0}", msg);
                    log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Information);
                }
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }



        void ILogHnd.OnMsg(string userId, ILogInfo info)
        {
            try
            {
                if (MsgInEventLogSchreiben)
                {
                    string descr = string.Format("Msg: user= {0}: {1} / {2}", userId, info.MessageCodeToString(), info.Message);
                    Debug.WriteLine("SysteEventLogHnd: " + descr);
                    log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Information);
                }
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void ILogHnd.OnError(string userId, ILogInfo info)
        {
            try
            {
                string descr = string.Format("Err: user= {0}: {1} / {2}", userId, info.MessageCodeToString(), info.Message);
                Debug.Fail("SysteEventLogHnd: " + descr);
                log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void ILogHnd.OnStatus(string userId, ILogInfo info)
        {
            try
            {
                if (StatusInEventLogSchreiben)
                {
                    string descr = string.Format("Sta: user= {0}: {1} / {2}", userId, info.MessageCodeToString(), info.Message);
                    Debug.WriteLine("SysteEventLogHnd: " + descr);
                    log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Information);
                }
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        #endregion

        #region IUserLogHnd Member

        DgDeregisterIUserLogHnd dgDeregisterIUserLogHnd = null;
        void IUserLogHnd.SetSelfDeregisterDelegate(DgDeregisterIUserLogHnd dg)
        {
            if (dg != null)
                dgDeregisterIUserLogHnd = dg;
        }

        void SelfDeregisterIUserLogHnd()
        {
            if (dgDeregisterIUserLogHnd != null)
                dgDeregisterIUserLogHnd(this);
        }

        void IUserLogHnd.OnUserMsg(string userId, string msg)
        {
            try
            {
                string descr = string.Format("Info(User: {0}): {1}", userId, msg);
                log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception)
            {
                SelfDeregisterIUserLogHnd();
            }
        }

        void IUserLogHnd.OnUserError(string userId, string msg)
        {
            try
            {
                string descr = string.Format("Error(User: {0}): {1}", userId, msg);
                log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception)
            {
                SelfDeregisterIUserLogHnd();
            }
        }

        void IUserLogHnd.OnUserStatus(string userId, string msg)
        {
            try
            {
                string descr = string.Format("Status(User: {0}): {1}", userId, msg);
                log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception)
            {
                SelfDeregisterIUserLogHnd();
            }
        }

        #endregion
    }
}
