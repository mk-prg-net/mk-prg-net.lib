using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60
{
    partial class Himmelskoerper : global::Kepler.IMond
    {
        public double Masse_in_Erdmondmassen
        {
            get
            {
                return Masse_in_kg / mko.Newton.Mass.MassOfEarthMoon.Value;
            }
            set
            {
                Masse_in_kg = value * mko.Newton.Mass.MassOfEarthMoon.Value;
            }
        }
    }
}
