//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 23.11.2014
//
//  Projekt.......: mko
//  Name..........: mkoLogMemLogHandler.cs
//  Aufgabe/Fkt...: Protokolliert Fehler und Statusmeldungen in einer List im Speicher.
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
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace mko.Log
{
    [Serializable]
    public class MemLogHandler : ILogHnd, IEnumerable<MemLogHandler.Entry>
    {
        /// <summary>
        /// Datenstruktur der Logeinträge im Speicher
        /// </summary>
        public class Entry : ILogInfo
        {
            public Entry(string UserId, ILogInfo Info)
            {
                _UserId = UserId;
                _LogType = Info.LogType;
                _LogDate = Info.LogDate;
                _Message = Info.Message;
            }

            public string UserId 
            {
                get
                {
                    return _UserId;
                }
            }
            string _UserId;


            public EnumLogType LogType
            {
                get { 
                    return _LogType;
                }
            }
            EnumLogType _LogType;

            public DateTime LogDate
            {
                get { 
                    return _LogDate;
                }
            }
            DateTime _LogDate;

            public string Message
            {
                get { 
                    return _Message;
                }
            }
            string _Message;
        }

        List<Entry> Mem = new List<Entry>();

        public void Clear()
        {
            Mem.Clear();
        }

        void ILogHnd.OnLog(string userId, ILogInfo info)
        {
            Mem.Add(new Entry(userId, info));
        }

        DgDeregisterILogHnd dgDeregisterILogHnd = null;
        void ILogHnd.SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if (dg != null)
                dgDeregisterILogHnd = dg;
        }

        /// <summary>
        /// Implementiert die IEnumerable Schnittstelle so, dass jeder Schleifendurchlauf 
        /// einen Protokolleintrag zurückliefert
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Entry> GetEnumerator()
        {
            foreach (var li in Mem)
                yield return li;
        }

        /// <summary>
        /// Implementiert die IEnumerable Schnittstelle so, dass jeder Schleifendurchlauf 
        /// einen Protokolleintrag zurückliefert
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
 	        foreach(var li in Mem)
                yield return li;
        }
    }
}
