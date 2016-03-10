using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public partial class Force
    {
        // mN
        public static ForceInNewton<Mag.Milli> mN(params double[] coordinates)
        {
            return new ForceInNewton<Mag.Milli>(coordinates);
        }

        public static ForceInNewton<Mag.Milli> mN(E.Vector V)
        {
            return mN(V.coordinates);
        }

        // N
        public static ForceInNewton<Mag.One> N(params double[] coordinates)
        {
            return new ForceInNewton<Mag.One>(coordinates);
        }

        public static ForceInNewton<Mag.One> N(E.Vector V)
        {
            return N(V.coordinates);
        }


        // KN
        public static ForceInNewton<Mag.Kilo> KN(params double[] coordinates)
        {
            return new ForceInNewton<Mag.Kilo>(coordinates);
        }

        public static ForceInNewton<Mag.Kilo> KN(E.Vector V)
        {
            return KN(V.coordinates);
        }


        // MN
        public static ForceInNewton<Mag.Mega> MN(params double[] coordinates)
        {
            return new ForceInNewton<Mag.Mega>(coordinates);
        }

        public static ForceInNewton<Mag.Mega> MN(E.Vector V)
        {
            return MN(V.coordinates);
        }

        // GN
        public static ForceInNewton<Mag.Giga> GN(params double[] coordinates)
        {
            return new ForceInNewton<Mag.Giga>(coordinates);
        }

        public static ForceInNewton<Mag.Giga> GN(E.Vector V)
        {
            return GN(V.coordinates);
        }



    }
}
