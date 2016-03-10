using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public interface IMeasuredValue
    {
        /// <summary>
        /// Messwert
        /// </summary>
        Double Value { get; set; }

        /// <summary>
        /// Messwert, in die Basiseinheit umgerechnet
        /// </summary>
        Double ValueInBaseUnit { get; }
    }
}
