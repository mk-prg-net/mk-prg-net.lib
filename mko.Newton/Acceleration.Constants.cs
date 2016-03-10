using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Acceleration
    {
        /// <summary>
        /// Fallbeschleunigung auf dem Merkur
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnMercury
        {
            get
            {
                return MeterPerSec2(3.7);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf der Venus
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnVenus
        {
            get
            {
                return MeterPerSec2(8.87);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf der Erde
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnEarth
        {
            get
            {
                return MeterPerSec2(9.81);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Erdmond
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnEarthMoon
        {
            get
            {
                return MeterPerSec2(1.6249);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Mars
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnMars
        {
            get
            {
                return MeterPerSec2(3.711);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Marsmond Phobos
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnMarsMoonPhobos
        {
            get
            {
                return MeterPerSec2(0.0084);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Marsmond Deimos
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnMarsMoonDeimos
        {
            get
            {
                return MeterPerSec2(0.0039);
            }
        }


        /// <summary>
        /// Fallbeschleunigung auf dem Jupiter
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnJupiter
        {
            get
            {
                return MeterPerSec2(24.79);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Jupitermond Ganymede
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnJupiterMoonGanymede
        {
            get
            {
                return MeterPerSec2(1.428);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Jupitermond Europa
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnJupiterMoonEuropa
        {
            get
            {
                return MeterPerSec2(1.314);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Jupitermond Io
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnJupiterMoonIo
        {
            get
            {
                return MeterPerSec2(1.796);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Jupitermond Callisto
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnJupiterMoonCallisto
        {
            get
            {
                return MeterPerSec2(1.235);
            }
        }


        /// <summary>
        /// Fallbeschleunigung auf dem Saturn
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnSaturn
        {
            get
            {
                return MeterPerSec2(10.44);
            }
        }

        public static AccelerationInMeterPerSec<Mag.One> GravityOnSaturnMoonTitan
        {
            get
            {
                return MeterPerSec2(1.35);
            }
        }

        public static AccelerationInMeterPerSec<Mag.One> GravityOnSaturnMoonEnceladus
        {
            get
            {
                return MeterPerSec2(0.114);
            }
        }



        /// <summary>
        /// Fallbeschleunigung auf dem Uranus
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnUranus
        {
            get
            {
                return MeterPerSec2(8.69);
            }
        }

        /// <summary>
        /// Fallbeschleunigung auf dem Uranus
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnNeptune
        {
            get
            {
                return MeterPerSec2(11.15);
            }
        }

        public static AccelerationInMeterPerSec<Mag.One> GravityOnNeptuneMoonTriton
        {
            get
            {
                return MeterPerSec2(0.779);
            }
        }



        /// <summary>
        /// Fallbeschleunigung auf der Sonne
        /// </summary>
        public static AccelerationInMeterPerSec<Mag.One> GravityOnSun
        {
            get
            {
                return MeterPerSec2(274.0);
            }
        }

    }
}
