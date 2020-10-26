using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;

namespace ATMO.mko.Logging.CSV
{
    /// <summary>
    /// mko, 28.6.2018
    /// Writes a csv- File
    /// </summary>
    public interface ICsvWriter : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Separator"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        IRCV2 WriteLine(char Separator, params string[] fields);

        IRCV2 WriteLine(char Separator, char NewLine, params string[] fields);


        /// <summary>
        /// Writes a new line of comma separeted values
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        IRCV2 WriteLine(params string[] fields);

    }
}
