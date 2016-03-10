using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60
{
    partial class Himmelskoerper : global::Kepler.IGalaxie
    {
        public double Masse_in_millionen_Sonnenmassen
        {
            get
            {
                return Masse_in_kg / (mko.Newton.Mass.MassOfSun.Value * 1e6);
            }
            set
            {
                Masse_in_kg = value * (mko.Newton.Mass.MassOfSun.Value * 1e6);
            }
        }
    }
}
