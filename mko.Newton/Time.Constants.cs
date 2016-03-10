using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Time
    {

        public static TimeInHours SideralRotationPeriodSun
        {
            get
            {
                return Hours(Days(25.05));
            }
        }


        public static TimeInDays OrbitalPeriodMercury
        {
            get
            {
                return Days(87.969);
            }
        }

        public static TimeInHours SideralRotationPeriodMercury
        {
            get
            {
                return Hours(Days(58.646));
            }
        }

        public static TimeInDays OrbitalPeriodVenus
        {
            get
            {
                return Days(224.698);
            }
        }

        public static TimeInHours SideralRotationPeriodVenus
        {
            get
            {
                return Hours(Days(243.018));
            }
        }


        /// <summary>
        /// Dauer einer vollständigen Umrundung der Sonne in Tagen
        /// </summary>
        public static TimeInDays OrbitalPeriodEarth
        {
            get
            {
                return Days(365.256363004);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodEarth
        {
            get
            {
                return Hours(Days(0.99726968));
            }
        }

        /// <summary>
        /// Dauer einer vollständigen Umrundung der Erde in Tagen
        /// </summary>
        public static TimeInDays OrbitalPeriodEarthMoon
        {
            get
            {
                return Days(27.321582);
            }
        }



        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodEarthMoon
        {
            get
            {
                return Hours(Days(27.321582));
            }
        }



        public static TimeInDays OrbitalPeriodMars
        {
            get
            {
                return Days(686.971);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodMars
        {
            get
            {
                return Hours(Days(1.025957));
            }
        }

        public static TimeInDays OrbitalPeriodMarsMoonDeimos
        {
            get
            {
                return Days(1.26244);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodMarsMoonDeimos
        {
            get
            {
                return Hours(Days(1.26244));
            }
        }

        public static TimeInDays OrbitalPeriodMarsMoonPhobos
        {
            get
            {
                return Days(0.31891023);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodMarsMoonPhobos
        {
            get
            {
                return Hours(Days(0.31891023));
            }
        }



        public static TimeInDays OrbitalPeriodJupiter
        {
            get
            {
                return Days(4332.59);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodJupiter
        {
            get
            {
                return Hours(9.925);
            }
        }

        public static TimeInDays OrbitalPeriodJupiterMoonEuropa
        {
            get
            {
                return Days(3.551181);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodJupiterMoonEuropa
        {
            get
            {
                return Hours(Days(3.551181));
            }
        }

        public static TimeInDays OrbitalPeriodJupiterMoonGanymede
        {
            get
            {
                return Days(7.15455296);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodJupiterMoonGanymede
        {
            get
            {
                return Hours(Days(7.15455296));
            }
        }

        public static TimeInDays OrbitalPeriodJupiterMoonIo
        {
            get
            {
                return Days(1.769137786);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodJupiterMoonIo
        {
            get
            {
                return Hours(Days(1.769137786));
            }
        }

        public static TimeInDays OrbitalPeriodJupiterMoonCallisto
        {
            get
            {
                return Days(16.6890184);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodJupiterMoonCallisto
        {
            get
            {
                return Hours(Days(16.6890184));
            }
        }


        public static TimeInDays OrbitalPeriodSaturn
        {
            get
            {
                return Days(10759.22);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodSaturn
        {
            get
            {
                return Hours(10.57);
            }
        }


        public static TimeInDays OrbitalPeriodUranus
        {
            get
            {
                return Days(30799.095);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodUranus
        {
            get
            {
                return Hours(Days(-0.71833));
            }
        }


        public static TimeInDays OrbitalPeriodNeptune
        {
            get
            {
                return Days(60190.03);
            }
        }

        /// <summary>
        /// Dauer einer vollständige Drehung um die eigenen Achse in Stunden
        /// </summary>
        public static TimeInHours SideralRotationPeriodNeptune
        {
            get
            {
                return Hours(Days(0.6713));
            }
        }

    }
}
