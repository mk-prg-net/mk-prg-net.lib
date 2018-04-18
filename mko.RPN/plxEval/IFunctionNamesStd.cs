using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.StdEval
{
    /// <summary>
    /// mko, 27.3.2018
    /// Function names of Property Expression Language (PLX)
    /// Bachus Naur Form: 
    /// 'plx := 'propName 'propValue
    /// 'propValue := 'const | 'List
    /// 'List := 'listStart plx0 ... plxN 'listEnd
    /// </summary>
    public interface IFunctionNamesStd : IFunctionNames
    {
        /// <summary>
        /// Signals beginnig of a list
        /// </summary>
        string ListStart { get; }
    }
}
