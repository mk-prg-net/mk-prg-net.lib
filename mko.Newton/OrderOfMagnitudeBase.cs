using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public abstract class OrderOfMagnitudeBase
    {
        /// <summary>
        /// Speichert die Größenordnung, in der ein Wert angegeben ist
        /// </summary>
        public abstract Mag.OrderOfMagnitudeEnum OrderOfMagnitude
        {
            get;
        }

        public double OrderOfMagnitudeFactor
        {
            get
            {
                return Mag.OrderOfMagnitudeFactor[OrderOfMagnitude];
            }
        }

        public string OrderOfMagnitudeId
        {
            get
            {
                return Mag.OrderOfMagnitudeId[OrderOfMagnitude];
            }
        }

        public static Dictionary<Type, OrderOfMagnitudeBase> Instance = new Dictionary<Type, OrderOfMagnitudeBase>();

    }
}
