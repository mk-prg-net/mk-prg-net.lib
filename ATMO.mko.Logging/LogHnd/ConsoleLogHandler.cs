//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.4.2011
//
//  Projekt.......: mko
//  Name..........: FileLogHnd
//  Aufgabe/Fkt...: Implementierung eines Log- Handlers zur Ausgabe auf der Kommandozeile
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

namespace ATMO.mko.Logging
{
    public class ConsoleLogHandler : ILogHnd
    {
        string Modulname = "";
        public ConsoleLogHandler() { }

        public ConsoleLogHandler(string Modulname)
        {
            this.Modulname = Modulname;
        }

        #region ILogHnd Member

        void ILogHnd.OnLog(long logCounter, string userId, ILogInfo info)
        {
            switch (info.LogType)
            {
                case EnumLogType.Error:
                    {
                        var colorBackup = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(string.Format("{0,-8:HH.mm.ss} Err {1} {2}", info.LogDate, Modulname, info.Message));
                        Console.ForegroundColor = colorBackup;
                    }
                    break;
                case EnumLogType.Message:
                    Console.WriteLine(string.Format("{0,-8:HH.mm.ss} Msg {1} {2}", info.LogDate, Modulname, info.Message));
                    break;
                case EnumLogType.Status:
                    Console.WriteLine(string.Format("{0,-8:HH.mm.ss} Sta {1} {2}", info.LogDate, Modulname, info.Message));
                    break;
                default: throw new ArgumentException();
            }
        }

        DgDeregisterILogHnd dgDeregisterILogHnd = null;
        void ILogHnd.SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if (dg != null)
                dgDeregisterILogHnd = dg;
        }

        public void OnLog(long logCounter, ITraceInfo ti)
        {
            Console.WriteLine(string.Format("{0,-8:HH.mm.ss}/Trc/{1}.{2}.{3}/\"{4}\"", ti.LogDate, ti.AssemblyName, ti.TypeName, ti.FunctionName, ti.Message));
        }

        #endregion
    }
}