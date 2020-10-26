using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Logging.LogHandler
{
    public class ConsoleLogHandler : ILoggingHandler
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
            Console.WriteLine($"Tele\t{obj.AssemblyName}.{obj.TypeName}.{obj.FunctionName}: {obj.Msg}");
        }

        private void LoggingServer_AppendToLogStateStream(ILogInfo18_12 obj)
        {
            Console.WriteLine($"Status\t{obj.AssemblyName}.{obj.TypeName}.{obj.FunctionName}: {obj.Msg}");
        }

        private void LoggingServer_AppendToLogMissionCriticalEventsStream(ILogInfo18_12 obj)
        {
            Console.WriteLine($"Crit.\t{obj.AssemblyName}.{obj.TypeName}.{obj.FunctionName}: {obj.Msg}");
        }

        private void LoggingServer_AppendToLogInfosStream(ILogInfo18_12 obj)
        {
            Console.WriteLine($"Infos\t{obj.AssemblyName}.{obj.TypeName}.{obj.FunctionName}: {obj.Msg}");
        }

        private void LoggingServer_AppendToLogErrorsStream(ILogInfo18_12 obj)
        {
            Console.WriteLine($"Error\t{obj.AssemblyName}.{obj.TypeName}.{obj.FunctionName}: {obj.Msg}");
        }
    }
}
