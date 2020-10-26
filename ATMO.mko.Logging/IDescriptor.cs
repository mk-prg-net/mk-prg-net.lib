using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 8.6.2018
    /// Common Interface for describing content
    /// </summary>
    public interface IDescriptor
    {
        /// <summary>
        /// Informal description of an item.
        /// </summary>
        string Description { get; }
    }
}
