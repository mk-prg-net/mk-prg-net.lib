using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Tools.Async
{
    /// <summary>
    /// (c) Martin Korneffel, Stuttgart 2017
    /// Manages long runnig Tasks. Informs any time about processing status    
    /// </summary>
    public interface IProgressor
    {
        /// <summary>
        /// Amount of work in percent alredy done.
        /// If WorkDone returns 100, long-running procedure
        /// ended.
        /// </summary>
        event Action<int> WorkDone;         


        /// <summary>
        /// Starts long running Procedure asynchron
        /// </summary>
        /// <param name="longRunningProc"></param>
        Task StartAsync(Action longRunningProc);

    }
}
