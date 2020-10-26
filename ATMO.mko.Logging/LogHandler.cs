//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.2.2008
//
//  Projekt.......: mko
//  Name..........: CLogHnd.cs
//  Aufgabe/Fkt...: Schnittstellen eines Protokollmediums.
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
//  Version 1.0...: 2004
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.2.2008
//  Änderungen....: Methoden SetSelfDeregister..., mit denen Methoden zum Abmelden
//                  der Eventhandler im Falle eines Schreibfehlers beim Protokollieren
//                  registriert werden können.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 14.7.2009
//  Änderungen....: Reduktion der Eventhandler OnErro, OnMsg, OnStatus auf OnLog
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 26.9.2017
//  Änderungen....: Log ITraceInfos hinzugefügt
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 8.3.2018
//  Änderungen....: Erweitert um Parameter logCounter
//
//</unit_history>
//</unit_header>    
using System;
using System.Collections.Generic;
using System.Text;

namespace ATMO.mko.Logging
{
    public delegate void DgDeregisterILogHnd(ILogHnd x);

    public interface ILogHnd
    {
        // Event Handler für ein Log- Ereignis im Logserver
        // mko, 8.3.2018
        // Erweitert um Parameter logCounter
        void OnLog(long logCounter, string userId, ILogInfo info);

        /// <summary>
        /// Aufzeichnen von ITraceInfo
        /// mko, 8.3.2018
        /// Erweitert um Parameter logCounter
        /// </summary>
        /// <param name="ti"></param>
        void OnLog(long logCounter, ITraceInfo ti);

        // Setzen eines Delegates, mit dem im Falle eines Fehler sich der Handler selbst deregistieren kann
        void SetSelfDeregisterDelegate(DgDeregisterILogHnd dg);
    }
}

