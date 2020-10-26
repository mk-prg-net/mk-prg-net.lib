using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

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
    //                  Auftrennen von Spezifikation und Implementierung durch Definition einer Schnittstelle.
    //                  Klassifizierung der Logmeldungen ist jetzt an die Anforderungen von DFC angepasst.
    //                  Log werden asynchron im Hintergrund geschrieben. 
    //</unit_history>
    //</unit_header>    



    /// <summary>
    /// mko, 19.12.2018
    /// Schlankere Implementierung eines LogServers. Das schreiben der Logmeldungen ist in Tasks ausgelagert,
    /// wodurch das Schreiben auf verschiedene Medien ein nicht blockierender Prozess ist.
    /// </summary>
    public class LoggingServerV18_12 : ILoggingServer
    {

        long SessionId;
        string UserId;

        /// <summary>
        /// Benutzer definieren, in dessen Kontext die Laufzeitinformationen aufgezeichnet werden
        /// </summary>
        /// <param name="userId"></param>
        public void SetUserId(string userId)
        {
            this.UserId = userId;
        }


        /// <summary>
        /// Sitzungsnummer definieren, in deren Kontext die Laufzeitinformationen aufgezeichnet werden.
        /// </summary>
        /// <param name="SessionId"></param>
        public void SetSessionId(long SessionId)
        {
            this.SessionId = SessionId;
        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="SessionId"></param>
        /// <param name="UserId"></param>
        public LoggingServerV18_12(long SessionId, string UserId)
        {
            this.SessionId = SessionId;
            this.UserId = UserId;
        }
        

        /// <summary>
        /// mko, 19.12.2018
        /// Fortlaufende Nummer der Log- Einträge
        /// </summary>
        public long LogCounter
        {
            get
            {
                return _LogCounter;
            }
        }
        long _LogCounter;

        class Info : ILogInfo18_12
        {
            public DateTime TimeStamp { get; set; }

            public long SessionId { get; set; }

            public EnumLogTypeDFC LogType { get; set; }

            public long LogCounter { get; set; }

            public string AssemblyName { get; set; }

            public string TypeName { get; set; }

            public string FunctionName { get; set; }

            public IDocuEntity Msg { get; set; }
        }


        /// <summary>
        /// Aufzeichnen von Laufzeitinformationen
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="docuEntity"></param>
        public void Log(EnumLogTypeDFC logType, IDocuEntity docuEntity)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            var logCounter = System.Threading.Interlocked.Increment(ref _LogCounter);

            // Lognachricht erzeugen
            var info = new Info()
            {
                TimeStamp = DateTime.Now,
                SessionId = SessionId,
                LogType = logType,
                LogCounter = logCounter,
                AssemblyName = assembly,
                TypeName = cls,
                FunctionName = mth.Name,
                Msg = docuEntity
            };

            // Die Meldungen werden bezüglich ihrer Klassifizierung in klassenspezifische Medien 
            // geschrieben.
            switch (logType)
            {
                case EnumLogTypeDFC.Error:
                    WriteToStream(info, AppendToLogErrorsStream);
                    break;
                case EnumLogTypeDFC.Info:
                    WriteToStream(info, AppendToLogInfosStream);
                    break;
                case EnumLogTypeDFC.Log:
                    WriteToStream(info, AppendToLogMissionCriticalEventsStream);
                    break;
                case EnumLogTypeDFC.State:
                    WriteToStream(info, AppendToLogStateStream);
                    break;
                case EnumLogTypeDFC.Telemetry:
                    WriteToStream(info, AppendToLogTelemetryStream);
                    break;
                default: { }
                    break;
            }
        }

        private void WriteToStream(Info info, Action<ILogInfo18_12> AppendToStream)
        {
            if (AppendToStream != null)
            {
                Delegate[] invocationList = AppendToStream.GetInvocationList();

                // Schreiben in den Stream über ausgelagerte fire- and forget - Methoden
                // Ein und dieselbe Nachricht wird auf alle Medien kopiert.
                for (int i = 0; i < invocationList.Length; i++)
                {
                    // Achtung: i darf nicht direkt als Closure verwendet werden, da 
                    //          der alle Tasks auf das gleiche i zugreifen, welches in jedem Schleifendurchlauf 
                    //          um eins erhöht wird. Die lokale Variable ii bekommt einen bestimmten i- wert zugewiesen
                    //          und hält diesen wie eine Konstante.
                    var ii = i;
                    Task.Run(() => ((Action<ILogInfo18_12>)invocationList[ii])(info));
                }
            }
        }


        /// <summary>
        /// mko, 19.12.2018
        /// Hier sind die Funktionen zu registrieren, welche die Laufzeitinformationen auf speziellen medien  abspeichern
        /// </summary>

        public event Action<ILogInfo18_12> AppendToLogStateStream;
        public event Action<ILogInfo18_12> AppendToLogErrorsStream;
        public event Action<ILogInfo18_12> AppendToLogInfosStream;
        public event Action<ILogInfo18_12> AppendToLogTelemetryStream;
        public event Action<ILogInfo18_12> AppendToLogMissionCriticalEventsStream;
    }
}
