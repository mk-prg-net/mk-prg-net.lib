using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace mkoIt.Asp
{

    public class StatusLabel : Label, mko.Log.ILogHnd
    {
        #region ILogHnd Member

        mko.Log.DgDeregisterILogHnd dgDeregisterILogHnd = null;

        void mko.Log.ILogHnd.OnLog(string userId, mko.Log.ILogInfo info)
        {
            try
            {
                switch (info.LogType)
                {
                    case mko.Log.EnumLogType.Error:
                        {
                            string descr = string.Format("Err: {0}: {1} / {2}", info.LogDate, userId, info.Message);
                            Debug.WriteLine("StatusLabel: " + descr);
                            Text = descr;                            
                        }


                        break;
                    case mko.Log.EnumLogType.Message:
                        {
                            string descr = string.Format("Msg: {0}: {1} / {2}", info.LogDate, userId, info.Message);
                            Debug.WriteLine("StatusLabel: " + descr);
                            Text = descr;
                        }
                        break;
                    case mko.Log.EnumLogType.Status:
                        {
                            string descr = string.Format("Sta: {0}: {1} / {2}", info.LogDate, userId, info.Message);
                            Debug.WriteLine("StatusLabel: " + descr);
                            Text = descr;
                        }
                        break;
                    default:
                        {
                            string descr = string.Format("Unbekannter Logtyp: user= {0:s}: {1} / {2}", info.LogDate, userId, info.Message);
                            Text = descr;
                            Debug.Fail("StatusLabel: " + descr);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                dgDeregisterILogHnd(this);
            }
        }


        void mko.Log.ILogHnd.SetSelfDeregisterDelegate(mko.Log.DgDeregisterILogHnd dg)
        {
            if (dg != null)
                dgDeregisterILogHnd = dg;
        }

        #endregion
    }
}
