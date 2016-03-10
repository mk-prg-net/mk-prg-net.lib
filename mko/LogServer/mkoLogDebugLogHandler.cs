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
using System.Diagnostics;

namespace mko.Log
{
    public class DebugLogHandler : ILogHnd
    {
        string Modulname = "";

        public DebugLogHandler() { }

        public DebugLogHandler(string Modulname)
        {
            this.Modulname = Modulname;
        }


        public void OnLog(string userId, ILogInfo info)
        {
#if(DEBUG)
            switch (info.LogType)
            {
                case EnumLogType.Error:
                    Debug.WriteLine(mko.TraceHlp.FormatErrMsg(typeof(DebugLogHandler).Name, Modulname, userId, info.Message));
                    break;
                case EnumLogType.Message:
                    Debug.WriteLine(mko.TraceHlp.FormatInfoMsg(typeof(DebugLogHandler).Name, Modulname, userId, info.Message));
                    break;
                case EnumLogType.Status:
                    Debug.WriteLine(mko.TraceHlp.FormatWarningMsg(typeof(DebugLogHandler).Name, Modulname, userId, info.Message));
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
    }
}
