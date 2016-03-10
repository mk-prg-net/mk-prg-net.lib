using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace mko.Log
{
    public class WinFormListBoxLogHnd : ILogHnd
    {
        // Protokollierung von Meldungen in den Eventlogs des Systems

        ListBox _lbx;

        public bool ShowMessage = true;
        public bool ShowStatus = true;

        public WinFormListBoxLogHnd(ListBox lbx)
        {
            Debug.Assert(lbx != null, "Dem Konstruktor von mko.Log.WinFormListBoxLogHnd muss eine Listbox- Referenz übergeben werden");
            _lbx = lbx;
        }


        #region ILogHnd Member

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

        int _counter = 0;
        public void reset()
        {
            _lbx.Items.Clear();
            _counter = 0;
        }

        delegate void DGHnd(string userId, ILogInfo info);

        void ILogHnd.OnLog(string userId, ILogInfo info)
        {
            try
            {
                // Threadsicherer Aufruf der Listbox Add Methode
                if (_lbx.InvokeRequired)
                {
                    ILogHnd logHnd = this;
                    DGHnd dg = new DGHnd(logHnd.OnLog);
                    _lbx.BeginInvoke(dg, new object[] { userId, info });
                }
                else
                    switch (info.LogType)
                    {
                        case EnumLogType.Error:
                            {
                                string descr = string.Format("Err: user= {0:s}: {1} / {2}", info.LogDate, userId, info.Message);
                                Debug.Fail("WinFormListBoxLogHnd: " + descr);
                                _lbx.Items.Add(_counter.ToString() + '\t' + descr);
                                _counter++;
                            }
                            break;
                        case EnumLogType.Message:
                            if (ShowMessage)
                            {
                                string descr = string.Format("Msg: user= {0:s}: {1} / {2}", info.LogDate, userId, info.Message);
                                Debug.WriteLine("WinFormListBoxLogHnd: " + descr);
                                _lbx.Items.Add(_counter.ToString() + '\t' + descr);
                                _counter++;


                            }
                            break;
                        case EnumLogType.Status:
                            if (ShowStatus)
                            {
                                string descr = string.Format("Sta: user= {0}: {1} / {2}", info.LogDate, userId, info.Message);
                                Debug.WriteLine("WinFormListBoxLogHnd: " + descr);
                                _lbx.Items.Add(_counter.ToString() + '\t' + descr);
                                _counter++;
                            }
                            break;
                        default:
                            {
                                string descr = string.Format("Unbekannter Logtyp: user= {0:s}: {1} / {2}", info.LogDate, userId, info.Message);
                                _lbx.Items.Add(_counter.ToString() + '\t' + descr);
                                Debug.Fail("WinFormListBoxLogHnd: " + descr);
                            }
                            break;
                    }
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        #endregion

    }
}
