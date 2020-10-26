using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;


namespace ATMO.mko.Logging.Logging.LogHandler
{

    /// <summary>
    /// mko, 26.4.2011
    /// Appends log messages to a file.
    /// mko, 1.3.2018
    /// Formats Messages in polish notation
    /// 
    /// mko, 5.3.2019
    /// Instead of ILogHnd interface now implements ILoggingHandler.
    /// </summary>
    public class FileLogHnd : ILoggingHandler, IDisposable
    {
        StreamWriter writer;
        IComposer pnL;
        PNFormater fmt = new PNFormater();

        bool firstCall = true;

        object MyLock = new object();

        public FileLogHnd(string FileName, IComposer pnL, bool LogfilePerInstance = false)
        {
            this.pnL = pnL;

            if (LogfilePerInstance)
            {
                var dir = Path.GetDirectoryName(FileName);
                var fn = $"{Path.GetFileNameWithoutExtension(FileName)}.{Guid.NewGuid()}{Path.GetExtension(FileName)}";
                var fullName = string.IsNullOrEmpty(dir) ? fn : $"{dir}\\{fn}";
                writer = new StreamWriter(fullName, true);
            }
            else
            {
                writer = new StreamWriter(FileName, true);
            }
        }

        /// <summary>
        /// Helper 
        /// </summary>
        /// <param name="obj"></param>
        void Write(ILogInfo18_12 obj)
        {
            writer.WriteLine(fmt.Print(pnL.i($"{obj.AssemblyName}.{obj.TypeName}.{obj.FunctionName}",
                                pnL.p("Type", obj.LogType.ToString()),
                                pnL.p("LogCounter", obj.LogCounter),
                                pnL.time(new TimeSpan(obj.TimeStamp.Hour, obj.TimeStamp.Minute, obj.TimeStamp.Second)),
                                pnL.p("msg", obj.Msg))));
        }

        private void FirstCallInitialisation(ILogInfo18_12 obj)
        {
            if (firstCall)
            {
                writer.WriteLine(fmt.Print(pnL.i("Session", pnL.p("SessionId", obj.SessionId), pnL.date(obj.TimeStamp))));
                firstCall = false;
            }
        }

        public void Dispose()
        {
            writer.Flush();
            writer.Dispose();
        }

        public void Register(ILoggingServer loggingServer)
        {
            loggingServer.AppendToLogErrorsStream += LoggingServer_AppendToLogErrorsStream;
            loggingServer.AppendToLogInfosStream += LoggingServer_AppendToLogInfosStream;
            loggingServer.AppendToLogMissionCriticalEventsStream += LoggingServer_AppendToLogMissionCriticalEventsStream;
            loggingServer.AppendToLogStateStream += LoggingServer_AppendToLogStateStream;
            loggingServer.AppendToLogTelemetryStream += LoggingServer_AppendToLogTelemetryStream;
        }

        private void LoggingServer_AppendToLogTelemetryStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }


        private void LoggingServer_AppendToLogStateStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }

        private void LoggingServer_AppendToLogMissionCriticalEventsStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }

        private void LoggingServer_AppendToLogInfosStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }

        private void LoggingServer_AppendToLogErrorsStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }
    }
}
