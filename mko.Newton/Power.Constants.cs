using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Power
    {
        /// <summary>
        /// Mittlere eingestrahlte Leistung auf der Erdoberfläche pro Quadratmeter ohne den Einfluss der 
        /// Atmosphäre
        /// </summary>
        public static PowerInWatt<Mag.One> SolarConstantExtraterrestic
        {
            get
            {
                return Watt(1367.0);
            }
        }

        /// <summary>
        /// Mittlere eingestrahlte Leistung auf der Erdoberfläche pro Quadratmeter nach Absorbtion von
        /// 46% der Leistung durch die Atmosphäre
        /// </summary>

        public static PowerInWatt<Mag.One> SolarConstantTerrestic
        {
            get
            {
                return Watt(740.0);
            }
        }

        /// <summary>
        /// Elektrische Leistung eines der größten Landgestützen Windräder: Enercon E-126
        /// http://de.wikipedia.org/wiki/Enercon
        /// </summary>
        public static PowerInWatt<Mag.Mega> Windrad_Enercon_E_126
        {
            get
            {
                return MegaWatt(7.6);
            }
        }


        /// <summary>
        /// Elektrische Leistung des KKW Nekarwestheim
        /// </summary>
        public static PowerInWatt<Mag.Mega> KKW_Nekarwestheim_Block_II_elektr
        {
            get
            {
                return MegaWatt(1400.0);
            }
        }


    }
}
