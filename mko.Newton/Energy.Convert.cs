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
        public static EnergyInNm<Mag.One> Nm(Energy Value)
        {
            return new EnergyInNm<Mag.One>(Value);
        }

        // J
        public static EnergyInJoule<Mag.Micro> MicroJoule(Energy Value)
        {
            return new EnergyInJoule<Mag.Micro>(Value);
        }

        public static EnergyInJoule<Mag.Milli> MilliJoule(Energy Value)
        {
            return new EnergyInJoule<Mag.Milli>(Value);
        }

        public static EnergyInJoule<Mag.One> Joule(Energy Value)
        {
            return new EnergyInJoule<Mag.One>(Value);
        }

        public static EnergyInJoule<Mag.Kilo> KiloJoule(Energy Value)
        {
            return new EnergyInJoule<Mag.Kilo>(Value);
        }

        public static EnergyInJoule<Mag.Mega> MegaJoule(Energy Value)
        {
            return new EnergyInJoule<Mag.Mega>(Value);
        }

        public static EnergyInJoule<Mag.Giga> GigaJoule(Energy Value)
        {
            return new EnergyInJoule<Mag.Giga>(Value);
        }

        public static EnergyInJoule<Mag.Terra> TerraJoule(Energy Value)
        {
            return new EnergyInJoule<Mag.Terra>(Value);
        }

        public static EnergyInJoule<Mag.Peta> PetaJoule(Energy Value)
        {
            return new EnergyInJoule<Mag.Peta>(Value);
        }

        // Ws
        public static EnergyInWs<Mag.Micro> MicroWs(Energy Value)
        {
            return new EnergyInWs<Mag.Micro>(Value);
        }

        public static EnergyInWs<Mag.Milli> MilliWs(Energy Value)
        {
            return new EnergyInWs<Mag.Milli>(Value);
        }

        public static EnergyInWs<Mag.One> Ws(Energy Value)
        {
            return new EnergyInWs<Mag.One>(Value);
        }

        public static EnergyInWs<Mag.Kilo> KiloWs(Energy Value)
        {
            return new EnergyInWs<Mag.Kilo>(Value);
        }

        public static EnergyInWs<Mag.Mega> MegaWs(Energy Value)
        {
            return new EnergyInWs<Mag.Mega>(Value);
        }

        public static EnergyInWs<Mag.Giga> GigaWs(Energy Value)
        {
            return new EnergyInWs<Mag.Giga>(Value);
        }

        public static EnergyInWs<Mag.Terra> TerraWs(Energy Value)
        {
            return new EnergyInWs<Mag.Terra>(Value);
        }

        public static EnergyInWs<Mag.Peta> PetaWs(Energy Value)
        {
            return new EnergyInWs<Mag.Peta>(Value);
        }


        // Wh

        public static EnergyInWh<Mag.Micro> MicroWh(Energy Value)
        {
            return new EnergyInWh<Mag.Micro>(Value);
        }

        public static EnergyInWh<Mag.Milli> MilliWh(Energy Value)
        {
            return new EnergyInWh<Mag.Milli>(Value);
        }

        public static EnergyInWh<Mag.One> Wh(Energy Value)
        {
            return new EnergyInWh<Mag.One>(Value);
        }

        public static EnergyInWh<Mag.Kilo> KiloWh(Energy Value)
        {
            return new EnergyInWh<Mag.Kilo>(Value);
        }

        public static EnergyInWh<Mag.Mega> MegaWh(Energy Value)
        {
            return new EnergyInWh<Mag.Mega>(Value);
        }

        public static EnergyInWh<Mag.Giga> GigaWh(Energy Value)
        {
            return new EnergyInWh<Mag.Giga>(Value);
        }

        public static EnergyInWh<Mag.Terra> TerraWh(Energy Value)
        {
            return new EnergyInWh<Mag.Terra>(Value);
        }

        public static EnergyInWh<Mag.Peta> PetaWh(Energy Value)
        {
            return new EnergyInWh<Mag.Peta>(Value);
        }

        // SKE

        public static EnergyInSKE SKE(Energy Value)
        {
            return new EnergyInSKE(Value);
        }


    }
}
