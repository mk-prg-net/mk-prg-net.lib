using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60
{
    public partial class Himmelskoerper : global::Kepler.IPlanet
    {

        public double Masse_in_Erdmassen
        {
            get
            {
                return Masse_in_kg / mko.Newton.Mass.MassOfEarth.Value;
            }
            set
            {
                Masse_in_kg = value * mko.Newton.Mass.EarthMasses(1.0).Value;
            }
        }


        public IEnumerable<global::Kepler.IMond> Monde
        {
            get
            {
                return TrabantenUmlaufbahnen.Select(r => r.Trabant);
            }
        }
    }
}
