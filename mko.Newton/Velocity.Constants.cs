using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Velocity
    {

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfMercury
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(47.87);
            }
        }


        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfVenus
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(35.02);
            }
        }


        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfEarth
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(28.78); 
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfEarthMoon
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(1.022);
            }
        }


        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfMars
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(24.13);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfMarsMoonDeimos
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(1.35);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfMarsMoonPhobos
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(2.138);
            }
        }



        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfJupiter
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(13.07);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfJupiterMoonCallisto
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(8.204);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfJupiterMoonIo
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(17.334);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfJupiterMoonGanymede
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(10.88);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfJupiterMoonEuropa
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(13.74);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfSaturn
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(9.69);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfSaturnMoonTitan
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(5.57);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfSaturnMoonEnceladus
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(12.6353);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfUranus
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(6.81);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfNeptune
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(5.43);
            }
        }

        public static VelocityInMeterPerSec<Mag.Kilo> VelocityOfNeptuneMoonTriton
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.Kilo>(4.39);
            }
        }



        public static VelocityInMeterPerSec<Mag.One> VelocitiyOfLight
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(299792458);
            }
        }

        /// <summary>
        /// Schallgeschwindigkeit in Luft bei 20C und 1 Bar
        /// </summary>
        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Air_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(343);
            }
        }

        /// <summary>
        /// Schallgeschwindigkeit in Helium bei 20C und 1 Bar
        /// </summary>
        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Helium_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(981);
            }
        }

        /// <summary>
        /// Schallgeschwindigkeit in Wasserstoff bei 20C und 1 Bar
        /// </summary>
        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Hydrogen_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(1280);
            }
        }



        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Water_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(1484);
            }
        }

        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_WaterIce_Miuns4C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(3250);
            }
        }

        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Gold_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(3240);
            }
        }

        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Silver_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(3600);
            }
        }



        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Copper_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(4660);
            }
        }


        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Steel_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(5920);
            }
        }

        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Aluminium_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(6350);
            }
        }

        /// <summary>
        /// Schallgeschwindigkeit in Beton C20/25
        /// </summary>
        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Concrete_C20_25_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(3655);
            }
        }

        /// <summary>
        /// Schallgeschwindigkeit in Beton C30/37
        /// </summary>
        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Concrete_C30_37_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(3845);
            }
        }

        /// <summary>
        /// Schallgeschwindigkeit in Mamor
        /// </summary>
        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Marble_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(6150);
            }
        }


        public static VelocityInMeterPerSec<Mag.One> VelocityOfSound_Diamond_20C
        {
            get
            {
                return new VelocityInMeterPerSec<Mag.One>(18000);
            }
        }



    }
}
