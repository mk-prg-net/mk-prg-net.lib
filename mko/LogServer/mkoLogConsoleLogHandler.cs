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

namespace mko.Log
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

        void ILogHnd.OnLog(string userId, ILogInfo info)
        {
            switch (info.LogType)
            {
                case EnumLogType.Error:
                    Console.WriteLine(string.Format("{0,-20:s}# {1,4}# Err# {2} \"{3}\"", info.LogDate, userId, Modulname, info.Message));
                    break;
                case EnumLogType.Message:
                    Console.WriteLine(string.Format("{0,-20:s}# {1,4}# Msg# {2} \"{3}\"", info.LogDate, userId, Modulname, info.Message));
                    break;
                case EnumLogType.Status:
                    Console.WriteLine(string.Format("{0,-20:s}# {1,4}# Sta# {2} \"{3}\"", info.LogDate, userId, Modulname, info.Message));
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

        #endregion
    }
}
