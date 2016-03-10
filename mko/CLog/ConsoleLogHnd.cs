//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.2.2008
//
//  Projekt.......: mko
//  Name..........: ConsoleLogHnd
//  Aufgabe/Fkt...: Implementierung eines Log- Handlers zur Ausgabe auf der 
//                  Kommandozeile
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
//  Änderungen....: Fehlerbehandlung implementiert, die die Eventhandler selbstständig
//                  abhängt.
//</unit_history>
//</unit_header>    
using System;
using System.Collections.Generic;
using System.Text;

namespace mko
{
    public class ConsoleLogHnd : ILogHnd, IUserLogHnd
    {
        static int msgCounter;

        #region ILogHnd Member

        DgDeregisterILogHnd dgDeregisterILogHnd = null;
        void ILogHnd.SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if(dg != null)
                dgDeregisterILogHnd = dg;
        }

        void SelfDeregisterILogHnd()
        {
            if (dgDeregisterILogHnd != null)
                dgDeregisterILogHnd(this);
        }

        void ILogHnd.OnMsg(int errno, string msg)
        {
            try
            {
                Console.WriteLine("Msg {2,4:D4}: {0,3:D3}: {1}", errno, msg, msgCounter);
                msgCounter++;
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void ILogHnd.OnError(int errno, string msg)
        {
            try
            {
                Console.WriteLine("Err {2,4:D4}: {0,3:D3}: {1}", errno, msg, msgCounter);
                msgCounter++;
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void ILogHnd.OnStatus(int errno, string msg)
        {
            try
            {
                Console.WriteLine("Sta {2,4:D4}: {0,3:D3}: {1}", errno, msg, msgCounter);
                msgCounter++;
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void ILogHnd.OnMsg(string userId, ILogInfo info)
        {
            try
            {
                Console.WriteLine("Msg {0,4:D4}, user= {1}: {2} / {3}", msgCounter, userId, info.MessageCodeToString(), info.Message);
                msgCounter++;
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void ILogHnd.OnError(string userId, ILogInfo info)
        {
            try
            {
                Console.WriteLine("Err {0,4:D4}, user= {1}: {2} / {3}", msgCounter, userId, info.MessageCodeToString(), info.Message);
                msgCounter++;
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void ILogHnd.OnStatus(string userId, ILogInfo info)
        {
            try
            {
                Console.WriteLine("Sta {0,4:D4}, user= {1}: {2} / {3}", msgCounter, userId, info.MessageCodeToString(), info.Message);
                msgCounter++;
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        #endregion

        #region IUserLogHnd Member

        DgDeregisterIUserLogHnd dgDeregisterIUserLogHnd = null;
        void IUserLogHnd.SetSelfDeregisterDelegate(DgDeregisterIUserLogHnd dg)
        {
            if(dg != null)
                dgDeregisterIUserLogHnd = dg;
        }

        void SelfDeregisterIUserLogHnd()
        {
            if (dgDeregisterIUserLogHnd != null)
                dgDeregisterIUserLogHnd(this);
        }

        void IUserLogHnd.OnUserMsg(string userId, string msg)
        {
            try
            {
                Console.WriteLine("Msg {0}: {1}", userId, msg);
            }
            catch (Exception)
            {
                SelfDeregisterIUserLogHnd();
            }
        }

        void IUserLogHnd.OnUserError(string userId, string msg)
        {
            try
            {
                Console.WriteLine("Msg {0}: {1}", userId, msg);
            }
            catch (Exception)
            {
                SelfDeregisterIUserLogHnd();
            }
        }

        void IUserLogHnd.OnUserStatus(string userId, string msg)
        {
            try
            {
                Console.WriteLine("Sta {0}: {1}", userId, msg);
            }
            catch (Exception)
            {
                SelfDeregisterIUserLogHnd();
            }
        }

        #endregion
    }
}
