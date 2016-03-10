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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.2.2008
//  Änderungen....: Methoden SetSelfDeregister..., mit denen Methoden zum Abmelden
//                  der Eventhandler im Falle eines Schreibfehlers beim Protokollieren
//                  registriert werden können.
//
//</unit_history>
//</unit_header>    
using System;
using System.Collections.Generic;
using System.Text;

namespace mko
{
    public delegate void DgDeregisterILogHnd(ILogHnd x);

    public interface ILogHnd
    {
        void OnMsg(int errno, string msg);
        void OnError(int errno, string msg);
        void OnStatus(int errno, string msg);

        void OnMsg(string userId, ILogInfo info);
        void OnError(string userId, ILogInfo info);
        void OnStatus(string userId, ILogInfo info);

        // Setzen eines Delegates, mit dem im Falle eines Fehler sich der Handler selbst deregistieren kann
        void SetSelfDeregisterDelegate(DgDeregisterILogHnd dg);       
    }

    public delegate void DgDeregisterIUserLogHnd(IUserLogHnd x); 

    public interface IUserLogHnd
    {
        void OnUserMsg(string userId, string msg);
        void OnUserError(string userId, string msg);
        void OnUserStatus(string userId, string msg);

        // Setzen eines Delegates, mit dem im Falle eines Fehler sich der Handler selbst deregistieren kann
        void SetSelfDeregisterDelegate(DgDeregisterIUserLogHnd dg);       

    }
}
