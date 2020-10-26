using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;

namespace ATMO.mko.Logging.Attr
{
    /// <summary>
    /// mko, 28.6.2018
    /// Writer for attribute files
    /// </summary>
    public interface IAttrFileWriter: IDisposable
    {
        /// <summary>
        /// mko, 28.6.2018
        /// Writes an attributname/value pair in line separated by predefined separator
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <param name="AttributeValue"></param>
        /// <returns></returns>
        void Write(string AttributeName, string AttributeValue);


        /// <summary>
        /// mko, 28.6.2018
        /// Writes an attributename/value pair in line separated by predefined separator
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <param name="Separator"></param>
        /// <param name="AttributeValue"></param>
        /// <returns></returns>
        void Write(string AttributeName, string Separator, string AttributeValue);

    }
}
