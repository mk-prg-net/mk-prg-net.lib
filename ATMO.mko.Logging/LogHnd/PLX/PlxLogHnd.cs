using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.LogHnd.PLX
{
    /// <summary>
    /// mko, 28.3.2018
    /// </summary>
    public class PlxLogHnd : ILogHnd
    {
        IView view;

        public bool ShowError { get; set; }
        public bool ShowMessage { get; set; }
        public bool ShowStatus { get; set; }

        PNDocuTerms.DocuEntities.HTMLFormater fmtHtml = new PNDocuTerms.DocuEntities.HTMLFormater();

        public PlxLogHnd(IView view)
        {
            this.view = view;
            ShowError = true;
            ShowStatus = true;
            ShowMessage = true;
        }

        public void OnLog(long logCounter, string userId, ILogInfo info)
        {
            try
            {
                if ((ShowMessage && info.LogType == EnumLogType.Message)
                 || (ShowStatus && info.LogType == EnumLogType.Status)
                 || (ShowError && info.LogType == EnumLogType.Error))
                {
                    view.ShowLogMsg(logCounter, info.LogDate, userId, info.LogType, info.Message);
                }
                

            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        void SelfDeregisterILogHnd()
        {
            if (dgDeregisterILogHnd != null)
                dgDeregisterILogHnd(this);
        }


        public void OnLog(long logCounter, ITraceInfo ti)
        {

        }

        delegate void DGHnd(long logCounter, string userId, ILogInfo info);

        DgDeregisterILogHnd dgDeregisterILogHnd = null;
        void ILogHnd.SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if (dg != null)
                dgDeregisterILogHnd = dg;
        }
    }
}
