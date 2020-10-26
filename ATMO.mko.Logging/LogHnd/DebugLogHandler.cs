//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 25.3.2013
//
//  Projekt.......: mko.Logserver
//  Name..........: mkoLogDebugLogHandler.cs
//  Aufgabe/Fkt...: Alle Meldungen über den Logserver werden in die Trace- und Debugprotokolle
//                  geschrieben.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.0
//  Werkzeuge.....: Visual Studio 2010
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 21.9.2017
//  Änderungen....: Integration in die DFC2- Tools
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ATMO.mko.Logging
{
    public class DebugLogHandler : ILogHnd
    {
        string Modulname = "";

        public DebugLogHandler() { }

        public DebugLogHandler(string Modulname)
        {
            this.Modulname = Modulname;
        }


        public void OnLog(long logCounter, string userId, ILogInfo info)
        {
#if(DEBUG)
            switch (info.LogType)
            {
                case EnumLogType.Error:
                    Debug.WriteLine(TraceHlp.FormatErrMsg(typeof(DebugLogHandler).Name, Modulname, userId, info.Message));
                    break;
                case EnumLogType.Message:
                    Debug.WriteLine(TraceHlp.FormatInfoMsg(typeof(DebugLogHandler).Name, Modulname, userId, info.Message));
                    break;
                case EnumLogType.Status:
                    Debug.WriteLine(TraceHlp.FormatWarningMsg(typeof(DebugLogHandler).Name, Modulname, userId, info.Message));
                    break;
                default: throw new ArgumentException();
            }
#endif
        }

        DgDeregisterILogHnd dgDeregisterILogHnd = null;
        public void SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if (dg != null)
                dgDeregisterILogHnd = dg;
        }

        public void OnLog(long logCounter, ITraceInfo ti)
        {
            Debug.WriteLine(string.Format("{0,-20:s}# {1,4}# Trc# {2}.{3}.{4} \"{5}\"", ti.LogDate, ti.User, ti.AssemblyName, ti.TypeName, ti.FunctionName, ti.Message));
        }
    }
}