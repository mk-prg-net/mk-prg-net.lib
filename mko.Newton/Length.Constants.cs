using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Length
    {
        /// <summary>
        /// Kerndurchmesser Wasserstoff
        /// </summary>
        public static LengthInMeter<Mag.Femto> DiameterAtomicNucleusHydrogen
        {
            get
            {
                return Femtometer(1.75);
            }
        }

        /// <summary>
        /// Kerndurchmesser Uran
        /// </summary>
        public static LengthInMeter<Mag.Femto> DiameterAtomicNucleusUranium
        {
            get
            {
                return Femtometer(15.0);
            }
        }

        /// <summary>
        /// Durchmesser Planet Merkur
        /// </summary>
        public static LengthInMeter<Mag.Kilo> DiameterMercury
        {
            get
            {
                return Kilometer(2 * 2439);
            }
        }

        public static LengthInAU SemiMajorAxisMercury
        {
            get
            {
                return AU(0.387098);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterVenus
        {
            get
            {
                return Kilometer(2 * 6052);
            }
        }

        public static LengthInAU SemiMajorAxisVenus
        {
            get
            {
                return AU(0.723327);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterEarth
        {
            get {
                return Kilometer(2*6371);
            }
        }

        public static LengthInAU SemiMajorAxisEarth
        {
            get
            {
                return AU(1.0);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterEarthPolar
        {
            get
            {
                return Kilometer(2*6356);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterEarthMoon
        {
            get
            {
                return Kilometer(2.0*1738.0);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterEarthMoonPolar
        {
            get
            {
                return Kilometer(2.0 * 1736.0);
            }
        }

        public static LengthInAU SemiMajorAxisEarthMoon
        {
            get
            {
                return AU(Kilometer(3843399));
            }
        }


        public static LengthInMeter<Mag.Kilo> DiameterMars
        {
            get
            {
                return Kilometer(2*3396);
            }
        }
        

        public static LengthInAU SemiMajorAxisMars
        {
            get
            {
                return AU(1.523679);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterMarsPolar
        {
            get
            {
                return Kilometer(2*3376);
            }
        }

        public static LengthInAU SemiMajorAxisMoonPhobos
        {
            get
            {
                return AU(Kilometer(9377.2));
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterMarsMoonPhobos
        {
            get
            {
                return Kilometer(2 *11.1);
            }
        }

        public static LengthInAU SemiMajorAxisMoonDeimos
        {
            get
            {
                return AU(Kilometer(23460));
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterMarsMoonDeimos
        {
            get
            {
                return Kilometer(2 * 6.2);
            }
        }



        public static LengthInAU SemiMajorAxisJupiter
        {
            get
            {
                return AU(5.204267);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterJupiter
        {
            get
            {
                return Kilometer(2*71492);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterJupiterPolar
        {
            get
            {
                return Kilometer(2*66854);
            }
        }

        public static LengthInAU SemiMajorAxisJupiterMoonEuropa
        {
            get
            {
                return AU(Kilometer(670900));
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterJupiterMoonEuropa
        {
            get
            {
                return Kilometer(2 * 1560.8);
            }
        }


        public static LengthInAU SemiMajorAxisJupiterMoonGanymede
        {
            get
            {
                return AU(Kilometer(1070400));
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterJupiterMoonGanymede
        {
            get
            {
                return Kilometer(2 * 2634.1);
            }
        }



        public static LengthInAU SemiMajorAxisJupiterMoonIo
        {
            get
            {
                return AU(Kilometer(421700));
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterJupiterMoonIo
        {
            get
            {
                return Kilometer(2 * 1821.3);
            }
        }


        public static LengthInAU SemiMajorAxisJupiterMoonCallisto
        {
            get
            {
                return AU(Kilometer(1882700));
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterJupiterMoonCallisto
        {
            get
            {
                return Kilometer(2 * 2410);
            }
        }


        public static LengthInAU SemiMajorAxisSaturn
        {
            get
            {
                return AU(9.5820172);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterSaturn
        {
            get
            {
                return Kilometer(2*60268);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterSaturnPolar
        {
            get
            {
                return Kilometer(2*54364);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterSaturnMoonTitan
        {
            get
            {
                return Kilometer(5150);
            }
        }

        public static LengthInMeter<Mag.Kilo> SemiMajorAxisSaturnMoonTitan
        {
            get
            {
                return Kilometer(1221830.0);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterSaturnMoonEnceladus
        {
            get
            {
                return Kilometer(504.2);
            }
        }

        public static LengthInMeter<Mag.Kilo> SemiMajorAxisSaturnMoonEnceladus
        {
            get
            {
                return Kilometer(237948.0);
            }
        }




        public static LengthInAU SemiMajorAxisUranus
        {
            get
            {
                return AU(19.22941195);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterUranus
        {
            get
            {
                return Kilometer(2 * 25559);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterUranusPolar
        {
            get
            {
                return Kilometer(2 * 24973);
            }
        }

        public static LengthInAU SemiMajorAxisNeptune
        {
            get
            {
                return AU(30.44125206);
            }
        }


        public static LengthInMeter<Mag.Kilo> DiameterNeptune
        {
            get
            {
                return Kilometer(2*24764);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterNeptunePolar
        {
            get
            {
                return Kilometer(2*24341);
            }
        }

        public static LengthInMeter<Mag.Kilo> DiameterNeptuneMoonTriton
        {
            get
            {
                return Kilometer(2706.8);
            }
        }


        public static LengthInMeter<Mag.Kilo> SemiMajorAxisNeptuneMoonTriton
        {
            get
            {
                return Kilometer(354759);
            }
        }



        public static LengthInMeter<Mag.Kilo> DiameterSun
        {
            get
            {
                return Kilometer(6.96342e5);
            }
        }      


    }
}
