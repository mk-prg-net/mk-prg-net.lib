//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.4.2011
//
//  Projekt.......: Godel Beton Laborsystem 1.0
//  Name..........: DbLogHnd
//  Aufgabe/Fkt...: Log- Handler, der Meldungen in Tabellen der GblDb- Datenbank schreibt
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
//  Datum.........: 6.5.2014
//  Änderungen....: Eigenschaft ConnectionString hinzugefügt. Über diese
//                  kann jetzt eine Datenbankverbindung explizit konfiguriert 
//                  werden.
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace mkoIt.Db
{
    public class DbLogHnd : mko.Log.ILogHnd
    {
        static System.Xml.Linq.XName ErrElem = System.Xml.Linq.XName.Get("Err", "mkoEventLog");
        static System.Xml.Linq.XName MsgElem = System.Xml.Linq.XName.Get("Msg", "mkoEventLog");
        static System.Xml.Linq.XName StaElem = System.Xml.Linq.XName.Get("Sta", "mkoEventLog");

        public System.Data.SqlClient.SqlConnectionStringBuilder ConnectionStringBld = new System.Data.SqlClient.SqlConnectionStringBuilder();

        #region ILogHnd Member

        public void OnLog(string userId, mko.Log.ILogInfo info)
        {
            try
            {
                var ctx = new EventLogDb.DtxEventLogDataContext(ConnectionStringBld.ConnectionString);

                var entity = new EventLogDb.EventLog();
                entity.author = userId;
                entity.created = info.LogDate;

                switch (info.LogType)
                {
                    case mko.Log.EnumLogType.Error:
                        entity.EventLogType_id = ctx.EventLogTypes.Where(r => r.name == "Error").First().id;
                        entity.log = new System.Xml.Linq.XElement(ErrElem);
                        break;
                    case mko.Log.EnumLogType.Message:
                        entity.EventLogType_id = ctx.EventLogTypes.Where(r => r.name == "Message").First().id;
                        entity.log = new System.Xml.Linq.XElement(MsgElem);
                        break;
                    case mko.Log.EnumLogType.Status:
                        entity.EventLogType_id = ctx.EventLogTypes.Where(r => r.name == "Status").First().id;
                        entity.log = new System.Xml.Linq.XElement(StaElem);
                        break;
                    default:
                        Debug.Fail("Unbekannter LogTyp");
                        SelfDeregisterDelegate(this);
                        break;
                }

                entity.log.Add(info.Message);
                ctx.EventLog.InsertOnSubmit(entity);
                ctx.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Fail("In DbLogHnd.OnLog: " + ex.Message);
                throw new Exception("In DbLogHnd.OnLog:", ex);
            }

        }

        mko.Log.DgDeregisterILogHnd SelfDeregisterDelegate;
        public void SetSelfDeregisterDelegate(mko.Log.DgDeregisterILogHnd dg)
        {
            SelfDeregisterDelegate = dg;
        }

        #endregion
    }
}
