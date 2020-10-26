using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 21.9.2017
    /// Loghandler, displays log messages using WinForm MessageBox
    /// </summary>
    public class MessageBoxLogHnd : ILogHnd
    {
        /// <summary>
        /// Constructor. Activats for error-, info- and statusmessages reporting in a WinForm MessageBox.
        /// Later, the behaviour can be adjusted via properties MessageBoxForReportingErrorOn, 
        /// MessageBoxForReportingInfoOn, MessageBoxForReportingStatusOn
        /// </summary>
        public MessageBoxLogHnd()
        {
            MessageBoxForReportingErrorOn = true;
            MessageBoxForReportingInfoOn = true;
            MessageBoxForReportingStatusOn = true;
        }

        /// <summary>
        /// Activates or deactivates MessageBoxes  for reporting Errors
        /// </summary>
        public bool MessageBoxForReportingErrorOn { get; set; }

        /// <summary>
        /// Activates or deactivates MessageBoxes  for reporting about non-critical events
        /// </summary>
        public bool MessageBoxForReportingInfoOn { get; set; }

        /// <summary>
        /// Activates or deactivates MessageBoxes  for reporting about status changes of system
        /// </summary>
        public bool MessageBoxForReportingStatusOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool MessageBoxForTracingStatusOn { get; set; }


        public void OnLog(long logCounter, string userId, ILogInfo info)
        {
            try
            {
                switch (info.LogType)
                {
                    case EnumLogType.Error:
                        if (MessageBoxForReportingErrorOn)
                            MessageBox.Show(info.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case EnumLogType.Message:
                        if (MessageBoxForReportingInfoOn)
                            MessageBox.Show(info.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case EnumLogType.Status:
                        if (MessageBoxForReportingStatusOn)
                            MessageBox.Show(info.Message, "Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        throw new Exception("unknown log type " + info.LogType);
                }
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }

        DgDeregisterILogHnd dgDeregisterILogHnd = null;

        public void SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if (dg == null)
                dgDeregisterILogHnd = dg;
        }

        void SelfDeregisterILogHnd()
        {
            if (dgDeregisterILogHnd != null)
                dgDeregisterILogHnd(this);
        }

        public void OnLog(long logCounter, ITraceInfo ti)
        {
            if (MessageBoxForTracingStatusOn)
            {
                MessageBox.Show(string.Format("{0,-20:s}# {1,4}# Trc# {2}.{3}.{4} \"{5}\"", ti.LogDate, ti.User, ti.AssemblyName, ti.TypeName, ti.FunctionName, ti.Message),
                    "Trace- Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
