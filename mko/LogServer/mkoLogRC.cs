//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.02.2008
//
//  Projekt.......: mko
//  Name..........: mkoLogRC.cs
//  Aufgabe/Fkt...: Klasse für Objekte, die Loginformation aufnehmen können.
//                  Die Objekte dienen der Implementierung von Containern, die 
//                  Loginformationen aufzeichnen.
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
//  Datum.........: 24.1.2014
//  Änderungen....: Create- Methode hinzugefügt zwecks Implementierung von mko.Log.RCContainerLogHnd
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace mko.Log
{
    public class RC : mko.Log.ILogInfo
    {
        string _msg;
        DateTime _date;
        mko.Log.EnumLogType _logType;

        private RC()
        {
            _date = DateTime.Now;
        }

        /// <summary>
        /// Liest über die ILogInfo- Schnittstelle die Daten aus einem Objekt mit Loginformationen aus 
        /// und erzeugt ein RC- Objekt daraus
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static RC Create(ILogInfo info)
        {
            RC rc = new RC();
            rc._logType = info.LogType;
            rc._date = info.LogDate;
            rc._msg = info.Message;

            return rc;
        }


        public static RC CreateError(string descr)
        {
            RC rc = new RC();
            rc._logType = mko.Log.EnumLogType.Error;
            rc._msg = descr;
            return rc;
        }

        /// <summary>
        /// Serialisiert eine Exception in ein XML- Fragment
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="depht"></param>
        /// <returns></returns>
        static XElement ErrorDetail(Exception ex, int depht) {
            if (ex != null)
            {
                depht++;

                if (ex.InnerException == null)
                    return new XElement(XName.Get("E"),
                                        new XAttribute(XName.Get("innerException"), depht),
                                        new XElement(XName.Get("msg"), ex.Message));
                else
                    return new XElement(XName.Get("E"),
                                        new XAttribute(XName.Get("innerException"), depht),
                                        new XElement(XName.Get("msg"), ex.Message),
                                        ErrorDetail(ex.InnerException, depht));
            }
            else
                return new XElement(XName.Get("E"), new XAttribute(XName.Get("innerException"), depht));
        }

        public static RC CreateError(string descr, Exception ex)
        {
            RC rc = new RC();
            rc._logType = mko.Log.EnumLogType.Error;
            rc._msg = new XElement(XName.Get("E"), new XElement(XName.Get("msg"), descr), ErrorDetail(ex, 0)).ToString();

            return rc;
        }


        public static RC CreateStatusOk()
        {
            RC rc = new RC();
            rc._logType = mko.Log.EnumLogType.Status;
            rc._msg = "ok";
            return rc;
        }

        public static RC CreateStatus(string descr)
        {
            RC rc = new RC();
            rc._logType = mko.Log.EnumLogType.Status;
            rc._msg = descr;
            return rc;
        }

        public static RC CreateMsg(string descr)
        {
            RC rc = new RC();
            rc._logType = mko.Log.EnumLogType.Message;
            rc._msg = descr;
            return rc;
        }

        #region ILogInfo Member

        public EnumLogType LogType
        {
            get { return _logType; }
        }

        public DateTime LogDate
        {
            get { return _date; }
        }

        public string Message
        {
            get { return _msg; }
        }

        #endregion
    }
}

