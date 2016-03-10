using System;
using System.Collections.Generic;
using System.Text;

namespace mko
{
    public class FileLogHnd : ILogHnd, IUserLogHnd
    {
        // Pfad der Logdatei
        string pathErrLogFile;

        public FileLogHnd()
        {
            // Anlegen eines Standardlogfiles im Anwendungsverzeichnis
            pathErrLogFile = AppDomain.CurrentDomain.BaseDirectory + @"err.log";
        }

        public FileLogHnd(string PathLogFile)
        {
            pathErrLogFile = PathLogFile;
        }

        #region ILogHnd Members

        DgDeregisterILogHnd dgDeregisterILogHnd = null;

        void ILogHnd.SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if (dg != null)
                dgDeregisterILogHnd = dg;
        }

        void SelfDeregisterILogHnd()
        {
            if (dgDeregisterILogHnd != null)
                dgDeregisterILogHnd(this);
        }

        void Log(string context, string Message)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(pathErrLogFile, true);
            sw.WriteLine(string.Format("{0:s}\t{1}: {2}", DateTime.Now, context, Message));
            sw.Flush();
            sw.Close();
        }

        void ILogHnd.OnMsg(int errno, string msg)
        {
            try
            {
                Log("Msg", msg);
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
                Log("Err", msg);
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
                Log("Sta", msg);
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
                string msg = string.Format("user= {0}: {1} / {2}", userId, info.MessageCodeToString(), info.Message);
                Log("Msg", msg);
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
                string msg = string.Format("user= {0}: {1} / {2}", userId, info.MessageCodeToString(), info.Message);
                Log("Err", msg);
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
                string msg = string.Format("user= {0}: {1} / {2}", userId, info.MessageCodeToString(), info.Message);
                Log("Sta", msg);
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        #endregion

        #region IUserLogHnd Members

        DgDeregisterIUserLogHnd dgDeregisterIUserLogHnd = null;
        void IUserLogHnd.SetSelfDeregisterDelegate(DgDeregisterIUserLogHnd dg)
        {
            if (dg != null)
                dgDeregisterIUserLogHnd = dg;
        }

        void SelfDeregisterIUserLogHnd()
        {
            if (dgDeregisterIUserLogHnd != null)
                dgDeregisterIUserLogHnd(this);
        }


        void Log(string context, string user, string Message)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(pathErrLogFile, true);
            sw.WriteLine(string.Format("{0:s}\t{1}, {2}: {3}", DateTime.Now, user, context, Message));
            sw.Flush();
            sw.Close();
        }

        void IUserLogHnd.OnUserMsg(string userId, string msg)
        {
            try
            {
                Log("Msg", userId, msg);
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
                Log("Err", userId, msg);
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
                Log("Sta", userId, msg);
            }
            catch (Exception)
            {
                SelfDeregisterIUserLogHnd();
            }            
        }

        #endregion
    }
}
