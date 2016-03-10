//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.4.2011
//
//  Projekt.......: mko
//  Name..........: FileLogHnd
//  Aufgabe/Fkt...: Implementierung eines Log- Handlers zur Ausgabe in einer Datei
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

using System.IO;

namespace mko.Log
{
    public class FileLogHandler :ILogHnd
    {
        StreamWriter writer;

        public FileLogHandler(string FileName)
        {
            writer = new StreamWriter(FileName, true);            
        }

        #region ILogHnd Member

        void ILogHnd.OnLog(string userId, ILogInfo info)
        {
            switch (info.LogType)
            {
                case EnumLogType.Error:
                    writer.WriteLine(string.Format("{0,-20:s}# {1,4}# Err# \"{2}\"", info.LogDate, userId, info.Message));
                    break;
                case EnumLogType.Message:
                    writer.WriteLine(string.Format("{0,-20:s}# {1,4}# Msg# \"{2}\"", info.LogDate, userId, info.Message));
                    break;
                case EnumLogType.Status:
                    writer.WriteLine(string.Format("{0,-20:s}# {1,4}# Sta# \"{2}\"", info.LogDate, userId, info.Message));
                    break;
                default: ;
                    break;
            }

            writer.Flush();
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
