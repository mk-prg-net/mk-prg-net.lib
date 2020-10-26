using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Tools
{
    /// <summary>
    /// mko, 14.6.2018
    /// Helper for maintaining enums.
    /// </summary>
    public class EnumExt
    {
        /// <summary>
        /// mko, 14.6.2018
        /// Returns all listed values of an enum as an array of values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] EnumAsArrayOf<T>()
            where T : struct
        {
            return ((T[])Enum.GetValues(typeof(T)));
        }
    }
}
