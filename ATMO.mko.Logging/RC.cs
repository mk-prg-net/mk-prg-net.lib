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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 24.1.2014
//  Änderungen....: Create- Methode hinzugefügt zwecks Implementierung von mko.Log.RCContainerLogHnd
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 26.9.2017
//  Änderungen....: CreateError Methoden erweitert um automatische Erfassung der Assembly.Klasse.Methode
//                  in welcher der Fehler auftrat.
//                  CreateError(string descr, Ecxeption ex) durch CreateError(Exception ex, string descr="") 
//                  ersetzt.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 3.11.2017
//  Änderungen....: ToString() Formatierung überarbeitet. Gibt nun für jede Meldung die verstrichene Zeit in ms
//                  seit Programmstart zurück.
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

//using pnL = ATMO.mko.Logging.PNDocuTerms.Composer;

namespace ATMO.mko.Logging
{
    public class RC : ATMO.mko.Logging.ILogInfo
    {
        PNDocuTerms.DocuEntities.IDocuEntity _msg;
        DateTime _date;
        EnumLogType _logType;

        PNDocuTerms.DocuEntities.Composer pnL = new PNDocuTerms.DocuEntities.Composer();
        PNDocuTerms.DocuEntities.PNFormater fmt = new PNDocuTerms.DocuEntities.PNFormater();


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

            var rcP = PNDocuTerms.Parser.Parser.Parse(info.Message, PNDocuTerms.Fn._);


            if (rcP.Succeeded)
            {
                rc._msg = rcP.Value;
            } else
            {
                rc._msg = rc.pnL.txt(info.Message.Replace("#", ""));
            }            

            return rc;
        }


        /// <summary>
        /// mko, 
        /// Zeichnet eine Exception auf. Assembly, Klasse, Methoden werden automatisch bestimmt
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static RC CreateError(Exception ex, string descr = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = EnumLogType.Error;

            if (string.IsNullOrWhiteSpace(descr))
            {
                rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eFails(TraceHlp.FlattenExceptionMessagesPN(ex)));
            } else
            {
                rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}",                                        
                            rc.pnL.eFails(rc.pnL.txt(descr)),
                            rc.pnL.eFails(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }            

            return rc;
        }

        /// <summary>
        /// mko, 10.11.2017
        /// 
        /// mko, 24.9.2018
        /// descr has no defaultvalue anymore. With default value an coverd compiletime occurs:
        /// CreateError(PNDocuTerms.DocuEntities.IDocuEntity ErrorDescriptionAsDocuEntityInstance)
        /// will never bound, because ITraceInfo is a part of IDocuEntity.  
        /// </summary>
        /// <param name="ti"></param>
        /// <param name="descr"></param>
        /// <returns></returns>
        public static RC CreateError(ITraceInfo ti, string descr) 
        {
            var mth = ti.FunctionName;
            var cls = ti.TypeName;
            var assembly = ti.AssemblyName;

            RC rc = new RC();
            rc._logType = EnumLogType.Error;

            if (string.IsNullOrEmpty(descr))
            {
                rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth}", rc.pnL.eFails(rc.pnL.txt(ti.ToString())));                            
            }
            else
            {
                rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth}",                                
                            rc.pnL.eFails(rc.pnL.txt(ti.ToString())),
                            rc.pnL.eFails(rc.pnL.txt(descr)));
            }

            return rc;
        }

        public static RC CreateError(string descr)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = EnumLogType.Error;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eFails(rc.pnL.txt(descr)));                
            return rc;
        }

        /// <summary>
        /// mko, 10.4.2018
        /// </summary>
        /// <param name="ErrorDescriptionAsDocuEntityInstance"></param>
        /// <returns></returns>
        public static RC CreateError(PNDocuTerms.DocuEntities.IDocuEntity ErrorDescriptionAsDocuEntityInstance)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = EnumLogType.Error;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eFails(ErrorDescriptionAsDocuEntityInstance));
            return rc;
        }


        /// <summary>
        /// Serialisiert eine Exception in ein XML- Fragment
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="depht"></param>
        /// <returns></returns>
        static XElement ErrorDetail(Exception ex, int depht)
        {
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

        public static RC CreateStatusOk()
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = ATMO.mko.Logging.EnumLogType.Status;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eSucceeded());
            return rc;
        }

        public static RC CreateStatus(string descr)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = EnumLogType.Status;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eInfo(rc.pnL.txt(descr)));
            return rc;
        }

        public static RC CreateStatus(PNDocuTerms.DocuEntities.IDocuEntity StatusDescriptionAsDocuEntityInstance)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = EnumLogType.Status;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eInfo(StatusDescriptionAsDocuEntityInstance));
            return rc;
        }

        public static RC CreateMsg(string descr)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = EnumLogType.Message;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eInfo(rc.pnL.txt(descr)));
            return rc;
        }

        /// <summary>
        /// mko, 10.4.2018
        /// </summary>
        /// <param name="MsgAsDocuEntityInstance"></param>
        /// <returns></returns>
        public static RC CreateMsg(PNDocuTerms.DocuEntities.IDocuEntity MsgAsDocuEntityInstance)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = EnumLogType.Message;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eInfo(MsgAsDocuEntityInstance));
            return rc;
        }

        public static RC CreateMsgStatusOk()
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = ATMO.mko.Logging.EnumLogType.Message;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eSucceeded());
            return rc;
        }

        public static RC CreateMsgStatusOk(string info)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            RC rc = new RC();
            rc._logType = ATMO.mko.Logging.EnumLogType.Message;
            rc._msg = rc.pnL.i($"{assembly}.{cls}.{mth.Name}", rc.pnL.eSucceeded(), rc.pnL.p("info", rc.pnL.txt(info)));
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
            get {                
                return fmt.Print(_msg);
            }
        }

        #endregion

        /// <summary>
        /// mko, 19.10.2017
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString("*");
        }

        /// <summary>
        /// mko, 19.10.2017
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string ToString(string userId)
        {            
            var msg = pnL.i((LogType == EnumLogType.Error ? "Err" : LogType == EnumLogType.Message ? "Msg" : "Sta"), 
                    pnL.p("tStartMs", StartTimeSingleton.TimeDifferenceToStartTimeInMs(LogDate).ToString("D9")),
                    pnL.p("User", userId),
                    pnL.p("msg", _msg));

            return fmt.Print(msg);
        }
    }
}

