using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial  class Energy
    {

        public static EnergyInSKE Benzin_1kg
        {
            get
            {
                return SKE(1.486);
            }
        }

        public static EnergyInSKE LPG_1kg
        {
            get
            {
                return SKE(1.6);
            }
        }

        public static EnergyInSKE Rohoel_1kg
        {
            get
            {
                return SKE(1.428);
            }
        }

        public static EnergyInSKE Rohoel_1Liter
        {
            get
            {
                return SKE(1.224);
            }
        }

        public static EnergyInSKE Holz_1kg
        {
            get
            {
                return SKE(0.5);
            }
        }

        public static EnergyInSKE Steinkohle_1kg
        {
            get
            {
                return SKE(1.016);
            }
        }

        public static EnergyInSKE Braunkohle_1kg
        {
            get
            {
                return SKE(0.29);
            }
        }

        public static EnergyInSKE Natururan_1kg
        {
            get
            {
                return SKE(15000.0);
            }
        }

        public static EnergyInSKE U235_1kg
        {
            get
            {
                return SKE(2700000);
            }
        }

        public static EnergyInSKE DeuteriumTritium_1kg
        {
            get
            {
                return SKE(12280000);
            }
        }

        public static EnergyInSKE Antimaterie_1kg
        {
            get
            {
                return SKE(1533310000);
            }
        }


        /// <summary>
        /// Stromproduktion des Windparks Alpha Ventus. !2 Windräder a 5MW
        /// http://de.wikipedia.org/wiki/Alpha_ventus
        /// </summary>
        public static EnergyInWh<Mag.Giga> Windpark_Alpha_Ventus_Produktion_2011_elektr
        {
            get
            {
                return GigaWh(267.0);
            }
        }

        /// <summary>
        /// Stromproduktion der Kernkraftwerk Nekarwestheim in 2010, Block II
        /// http://www.enbw.com/content/de/der_konzern/enbw_gesellschaften/enbw_kernkraft/standorte/neckarwestheim/index.jsp
        /// </summary>
        public static EnergyInWh<Mag.Terra> KKW_Nekarwestheim_Block_II_Produktion_2010_elektr
        {
            get
            {
                return TerraWh(10.9);
            }
        }
    }
}
