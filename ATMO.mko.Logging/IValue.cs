using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 25.7.2018
    /// Defines a Value Property. For purpose of standardisation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValue<T>
    {        
        T Value { get; }
    }
}
