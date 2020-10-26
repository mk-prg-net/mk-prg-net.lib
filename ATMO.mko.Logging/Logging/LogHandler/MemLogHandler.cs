using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Logging.LogHandler
{
    /// <summary>
    /// mko, 4.1.2019
    /// Loghandler für Testzwecke
    /// 
    /// mko, 5.3.2019
    /// List/ILoginfo18_12/ durch LinkedList/ILoginfo18_12/ ausgetauscht.
    /// </summary>
    public class MemLogHandler : ILoggingHandler, IEnumerable<ILogInfo18_12>, IDisposable
    {

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
            Mem.AddLast(obj);
        }

        private void LoggingServer_AppendToLogStateStream(ILogInfo18_12 obj)
        {
            Mem.AddLast(obj);
        }

        private void LoggingServer_AppendToLogMissionCriticalEventsStream(ILogInfo18_12 obj)
        {
            Mem.AddLast(obj);
        }

        private void LoggingServer_AppendToLogInfosStream(ILogInfo18_12 obj)
        {
            Mem.AddLast(obj);
        }

        private void LoggingServer_AppendToLogErrorsStream(ILogInfo18_12 obj)
        {
            Mem.AddLast(obj);
        }

        public IEnumerator<ILogInfo18_12> GetEnumerator()
        {
            foreach (var li in Mem)
                yield return li;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var li in Mem)
                yield return li;
        }

        public void Dispose()
        {
            
        }

        LinkedList<ILogInfo18_12> Mem = new LinkedList<ILogInfo18_12>();
        
    }
}
