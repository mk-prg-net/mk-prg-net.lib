using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;


namespace mko.Newton
{
    public partial class Energy
    {

        // Nm
        public static EnergyInNm<Mag.One> Nm(double Value)
        {
            return new EnergyInNm<Mag.One>(Value);
        }


        // J
        public static EnergyInJoule<Mag.Micro> MicroJoule(double Value)
        {
            return new EnergyInJoule<Mag.Micro>(Value);
        }

        public static EnergyInJoule<Mag.Milli> MilliJoule(double Value)
        {
            return new EnergyInJoule<Mag.Milli>(Value);
        }

        public static EnergyInJoule<Mag.One> Joule(double Value)
        {
            return new EnergyInJoule<Mag.One>(Value);
        }

        public static EnergyInJoule<Mag.Kilo> KiloJoule(double Value)
        {
            return new EnergyInJoule<Mag.Kilo>(Value);
        }

        public static EnergyInJoule<Mag.Mega> MegaJoule(double Value)
        {
            return new EnergyInJoule<Mag.Mega>(Value);
        }

        public static EnergyInJoule<Mag.Giga> GigaJoule(double Value)
        {
            return new EnergyInJoule<Mag.Giga>(Value);
        }

        public static EnergyInJoule<Mag.Terra> TerraJoule(double Value)
        {
            return new EnergyInJoule<Mag.Terra>(Value);
        }

        public static EnergyInJoule<Mag.Peta> PetaJoule(double Value)
        {
            return new EnergyInJoule<Mag.Peta>(Value);
        }

        // Ws
        public static EnergyInWs<Mag.Micro> MicroWs(double Value)
        {
            return new EnergyInWs<Mag.Micro>(Value);
        }

        public static EnergyInWs<Mag.Milli> MilliWs(double Value)
        {
            return new EnergyInWs<Mag.Milli>(Value);
        }

        public static EnergyInWs<Mag.One> Ws(double Value)
        {
            return new EnergyInWs<Mag.One>(Value);        }


        public static EnergyInWs<Mag.Kilo> KiloWs(double Value)
        {
            return new EnergyInWs<Mag.Kilo>(Value);
        }

        public static EnergyInWs<Mag.Mega> MegaWs(double Value)
        {
            return new EnergyInWs<Mag.Mega>(Value);
        }

        public static EnergyInWs<Mag.Giga> GigaWs(double Value)
        {
            return new EnergyInWs<Mag.Giga>(Value);
        }

        public static EnergyInWs<Mag.Terra> TerraWs(double Value)
        {
            return new EnergyInWs<Mag.Terra>(Value);
        }

        public static EnergyInWs<Mag.Peta> PetaWs(double Value)
        {
            return new EnergyInWs<Mag.Peta>(Value);
        }

        // Wh

        public static EnergyInWh<Mag.Micro> MicroWh(double Value)
        {
            return new EnergyInWh<Mag.Micro>(Value);
        }

        public static EnergyInWh<Mag.Milli> MilliWh(double Value)
        {
            return new EnergyInWh<Mag.Milli>(Value);
        }

        public static EnergyInWh<Mag.One> Wh(double Value)
        {
            return new EnergyInWh<Mag.One>(Value);
        }

        public static EnergyInWh<Mag.Kilo> KiloWh(double Value)
        {
            return new EnergyInWh<Mag.Kilo>(Value);
        }


        public static EnergyInWh<Mag.Mega> MegaWh(double Value)
        {
            return new EnergyInWh<Mag.Mega>(Value);
        }

        public static EnergyInWh<Mag.Giga> GigaWh(double Value)
        {
            return new EnergyInWh<Mag.Giga>(Value);
        }

        public static EnergyInWh<Mag.Terra> TerraWh(double Value)
        {
            return new EnergyInWh<Mag.Terra>(Value);
        }

        public static EnergyInWh<Mag.Peta> PetaWh(double Value)
        {
            return new EnergyInWh<Mag.Peta>(Value);
        }

        // SKE

        public static EnergyInSKE SKE(double Value)
        {
            return new EnergyInSKE(Value);
        }
    }
}
