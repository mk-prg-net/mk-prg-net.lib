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
    public interface IView
    {
        /// <summary>
        /// Displays the log message
        /// </summary>
        /// <param name="usrId"></param>
        /// <param name="logType"></param>
        /// <param name="msg"></param>
        void ShowLogMsg(long LogCount, DateTime logDate, string usrId, EnumLogType logType, string msg);


    }
}
