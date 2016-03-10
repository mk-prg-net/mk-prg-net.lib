using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Mass
    {
        public static MassInGram<Mag.Atto> Attogram(Mass Value)
        {
            return new MassInGram<Mag.Atto>(Value);
        }

        public static MassInGram<Mag.Femto> Femtogram(Mass Value)
        {
            return new MassInGram<Mag.Femto>(Value);
        }

        public static MassInGram<Mag.Pico> Picogram(Mass Value)
        {
            return new MassInGram<Mag.Pico>(Value);
        }

        public static MassInGram<Mag.Nano> Nanogram(Mass Value)
        {
            return new MassInGram<Mag.Nano>(Value);
        }

        public static MassInGram<Mag.Micro> Microgram(Mass Value)
        {
            return new MassInGram<Mag.Micro>(Value);
        }

        public static MassInGram<Mag.Milli> Milligram(Mass Value)
        {
            return new MassInGram<Mag.Milli>(Value);
        }

        public static MassInGram<Mag.One> Gram(Mass Value)
        {
            return new MassInGram<Mag.One>(Value);
        }

        public static MassInGram<Mag.Kilo> Kilogram(Mass Value)
        {
            return new MassInGram<Mag.Kilo>(Value);
        }

        public static MassInGram<Mag.Mega> Tons(Mass Value)
        {
            return new MassInGram<Mag.Mega>(Value);
        }

        public static MassInGram<Mag.Giga> Megatons(Mass Value)
        {
            return new MassInGram<Mag.Giga>(Value);
        }

        public static MassInGram<Mag.Terra> Gigatons(Mass Value)
        {
            return new MassInGram<Mag.Terra>(Value);
        }

        public static MassInEarthmasses EarthMasses(Mass Value)
        {
            return new MassInEarthmasses(Value);
        }


        public static MassInOunce Ounce(Mass Value)
        {
            return new MassInOunce(Value);
        }

        public static MassInCarat Carat(Mass Value)
        {
            return new MassInCarat(Value);
        }

    }
}
