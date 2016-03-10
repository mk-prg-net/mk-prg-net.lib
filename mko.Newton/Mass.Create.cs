using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag= mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Mass : MeasuredValue
    {
        public static MassInGram<Mag.Atto> Attogram(double Value)
        {
            return new MassInGram<Mag.Atto>(Value);
        }

        public static MassInGram<Mag.Femto> Femtogram(double Value)
        {
            return new MassInGram<Mag.Femto>(Value);
        }

        public static MassInGram<Mag.Pico> Picogram(double Value)
        {
            return new MassInGram<Mag.Pico>(Value);
        }

        public static MassInGram<Mag.Nano> Nanogram(double Value)
        {
            return new MassInGram<Mag.Nano>(Value);
        }

        public static MassInGram<Mag.Micro> Microgram(double Value)
        {
            return new MassInGram<Mag.Micro>(Value);
        }

        public static MassInGram<Mag.Milli> Milligram(double Value)
        {
            return new MassInGram<Mag.Milli>(Value);
        }

        public static MassInGram<Mag.One> Gram(double Value)
        {
            return new MassInGram<Mag.One>(Value);
        }

        public static MassInGram<Mag.Kilo> Kilogram(double Value)
        {
            return new MassInGram<Mag.Kilo>(Value);
        }

        public static MassInGram<Mag.Mega> Tons(double Value)
        {
            return new MassInGram<Mag.Mega>(Value);
        }

        public static MassInGram<Mag.Giga> Megatons(double Value)
        {
            return new MassInGram<Mag.Giga>(Value);
        }

        public static MassInGram<Mag.Terra> Gigatons(double Value)
        {
            return new MassInGram<Mag.Terra>(Value);
        }

        public static MassInEarthmasses EarthMasses(double Value)
        {
            return new MassInEarthmasses(Value);
        }

        public static MassInOunce Ounce(double Value)
        {
            return new MassInOunce(Value);
        }

        public static MassInCarat Carat(double Value)
        {
            return new MassInCarat(Value);
        }

    }
}
