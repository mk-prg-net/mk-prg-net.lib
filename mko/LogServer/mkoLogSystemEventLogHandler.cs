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
//
//  Version.......: 2.0
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 14.7.2009
//  Änderungen....: Anpassung an neue LogHnd- Schnittstelle, die nur noch den Eventhandler OnLog
//                  besitzt
//</unit_history>
//</unit_header>    
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace mko.Log
{
    public class SystemEventLogHnd : ILogHnd
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

        void ILogHnd.OnLog(string userId, ILogInfo info)
        {
            try
            {
                switch (info.LogType)
                {
                    case EnumLogType.Error:
                        {
                            string descr = string.Format("Err: user= {0:s}: {1} / {2}", info.LogDate, userId, info.Message);
                            Debug.Fail("SysteEventLogHnd: " + descr);
                            log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Error);
                        }


                        break;
                    case EnumLogType.Message:
                        if (MsgInEventLogSchreiben)
                        {
                            string descr = string.Format("Msg: user= {0:s}: {1} / {2}", info.LogDate, userId, info.Message);
                            Debug.WriteLine("SysteEventLogHnd: " + descr);
                            log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Information);
                        }
                        break;
                    case EnumLogType.Status:
                        if (StatusInEventLogSchreiben)
                        {
                            string descr = string.Format("Sta: user= {0}: {1} / {2}", info.LogDate, userId, info.Message);
                            Debug.WriteLine("SysteEventLogHnd: " + descr);
                            log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Information);
                        }
                        break;
                    default:
                        {
                            string descr = string.Format("Unbekannter Logtyp: user= {0:s}: {1} / {2}", info.LogDate, userId, info.Message);
                            log.WriteEntry(descr, System.Diagnostics.EventLogEntryType.Error);
                            Debug.Fail("SysteEventLogHnd: " + descr);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        #endregion

    }
}

