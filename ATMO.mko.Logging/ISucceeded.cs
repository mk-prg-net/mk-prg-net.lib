using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 2.11.2017
    /// Indicates the success of a function call.
    /// </summary>
    public interface ISucceeded
    {
        bool Succeeded { get; }
    }     
}
