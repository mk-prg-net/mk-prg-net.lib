using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Mass
    {
        /// <summary>
        /// Ruhemasse des Elektron
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfElectron
        {
            get
            {
                return Kilogram(9.10938291e-31);
            }
        }

        /// <summary>
        /// Ruhemasse des Protons
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfProton
        {
            get
            {
                return Kilogram(1.672621777e-27);
            }
        }

        /// <summary>
        /// Masse des Neutrons
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfNeutron
        {
            get
            {
                return Kilogram(1.674927351e-27);
            }
        }

        public static MassInGram<Mag.Kilo> MassOfMercury
        {
            get
            {
                return Kilogram(3.3022e23);
            }
        }

        public static MassInGram<Mag.Kilo> MassOfVenus
        {
            get
            {
                return Kilogram(4.8685e24);
            }
        }

        /// <summary>
        /// Masse der Erde
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfEarth
        {
            get
            {
                return Kilogram(5.9736e24);
            }
        }


        /// <summary>
        /// Masse des Erdmondes
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfEarthMoon
        {
            get
            {
                return Kilogram(7.3477e22);
            }
        }


        /// <summary>
        /// Masse des Mars
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfMars
        {
            get
            {
                return Kilogram(6.4185e23);
            }
        }

        /// <summary>
        /// Masse des Marsmondes Phobos
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfMarsMoonPhobos
        {
            get
            {
                return Kilogram(1.072e16);
            }
        }

        /// <summary>
        /// Masse des Marsmondes Deimos
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfMarsMoonDeimos
        {
            get
            {
                return Kilogram(1.48e15);
            }
        }

        /// <summary>
        /// Masse des Jupiters
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfJupiter
        {
            get
            {
                return Kilogram(1.8986e27);
            }
        }

        /// <summary>
        /// Masse des Jupiters
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfJupiterMoonEuropa
        {
            get
            {
                return Kilogram(4.7998e22);
            }
        }

        /// <summary>
        /// Masse des Jupiters
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfJupiterMoonGanymede
        {
            get
            {
                return Kilogram(1.4819e23);
            }
        }

        /// <summary>
        /// Masse des Jupiters
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfJupiterMoonIo
        {
            get
            {
                return Kilogram(8.9319e22);
            }
        }

        /// <summary>
        /// Masse des Jupiters
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfJupiterMoonCallisto
        {
            get
            {
                return Kilogram(1.075e23);
            }
        }


        public static MassInGram<Mag.Kilo> MassOfSaturn
        {
            get
            {
                return Kilogram(5.6846e26);
            }
        }

        public static MassInGram<Mag.Kilo> MassOfSaturnMoonTitan
        {
            get
            {
                return Kilogram(1.345e23);
            }
        }

        public static MassInGram<Mag.Kilo> MassOfSaturnMoonEnceladus
        {
            get
            {
                return Kilogram(1.08022e20);
            }
        }



        public static MassInGram<Mag.Kilo> MassOfUranus
        {
            get
            {
                return Kilogram(8.681e25);
            }
        }

        public static MassInGram<Mag.Kilo> MassOfNeptune
        {
            get
            {
                return Kilogram(1.0243e26);
            }
        }

        public static MassInGram<Mag.Kilo> MassOfNeptuneMoonTriton
        {
            get
            {
                return Kilogram(2.147e22);
            }
        }



        /// <summary>
        ///  Masse der Sonne
        /// </summary>
        public static MassInGram<Mag.Kilo> MassOfSun
        {
            get
            {
                return Kilogram(1.9891e30);
            }
        }


        public static MassInGram<Mag.Kilo> GesamtmasseBodenseewasser
        {
            get
            {
                return Kilogram(4.8e13);
            }
        }

    }
}
