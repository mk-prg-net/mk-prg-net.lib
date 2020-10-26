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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 1.3.2018
//  Änderungen....: Die Meldungen werden jetzt in polnischer Notation formatiert und ausgegeben.
//                  Damit können sie nachträglich durch einen RPN- Parser verarbeitet werden.
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using pnL = ATMO.mko.Logging.PNDocuTerms.Composer;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 26.4.2011
    /// Appends log messages to a file.
    /// mko, 1.3.2018
    /// Formats Messages in polish notation
    /// </summary>
    public class FileLogHandler : ILogHnd, IDisposable
    {
        StreamWriter writer;

        /// <summary>
        /// Zeichnet Meldungen des Logservers in einer Datei (Logfile auf)
        /// </summary>
        /// <param name="FileName">Name der Log- Datei</param>
        /// <param name="LogfilePerInstance">wenn true, dann wird für jede Programminstanz eine eigene Logdatei angelegt</param>
        public FileLogHandler(string FileName, bool LogfilePerInstance = false)
        {

            if (LogfilePerInstance)
            {
                var dir = Path.GetDirectoryName(FileName);
                var fn = $"{Path.GetFileNameWithoutExtension(FileName)}.{Guid.NewGuid()}{Path.GetExtension(FileName)}";
                var fullName = string.IsNullOrEmpty(dir) ? fn : $"{dir}\\{fn}";
                writer = new StreamWriter(fullName, true);
            } else
            {
                writer = new StreamWriter(FileName, true);
            }            
        }

        #region ILogHnd Member

        void ILogHnd.OnLog(long logCounter, string userId, ILogInfo info)
        {

            writer.Write($"{pnL.date(info.LogDate)} {pnL.time(info.LogDate)} ");

            switch (info.LogType)
            {
                case EnumLogType.Error:
                    
                    writer.WriteLine($"{pnL.i("Err", pnL.p("userId", userId), pnL.p("inf", info.Message))}");
                    break;
                case EnumLogType.Message:
                    writer.WriteLine($"{pnL.i("Msg", pnL.p("userId", userId), pnL.p("inf", info.Message))}");                    
                    break;
                case EnumLogType.Status:
                    writer.WriteLine($"{pnL.i("Sta", pnL.p("userId", userId), pnL.p("inf", info.Message))}");                    
                    break;
                default:
                    ;
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

        /// <summary>
        /// Zeichnet eine Trace- Meldung auf
        /// </summary>
        /// <param name="ti"></param>
        public void OnLog(long logCounter, ITraceInfo ti)
        {
            writer.Write($"{pnL.date(ti.LogDate)} {pnL.time(ti.LogDate)} ");
            writer.WriteLine($"{pnL.i("Trc", pnL.p("userId", ti.User), pnL.m($"{ti.AssemblyName}.{ti.TypeName}.{ti.FunctionName}"), pnL.p("inf", ti.Message))}");            
        }

        public void Dispose()
        {
            writer.Flush();
            writer.Close();
        }

        #endregion
    }
}