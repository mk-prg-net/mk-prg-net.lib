using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60
{
    partial class Himmelskoerper : global::Kepler.IStern
    {
        public double Masse_in_Sonnenmassen
        {
            get
            {
                return Masse_in_kg / mko.Newton.Mass.MassOfSun.Value;
            }
            set
            {
                Masse_in_kg = value * mko.Newton.Mass.MassOfSun.Value;
            }
        }

        global::Kepler.ISpektralklasse global::Kepler.IStern.Spektralklasse
        {
            get
            {
                return Spektralklasse;
            }
            set
            {
                Spektralklasse = (Spektralklasse)value;
            }
        }


        public IEnumerable<global::Kepler.IPlanet> Planeten
        {
            get { return TrabantenUmlaufbahnen.Select(r => r.Trabant); }
        }
    }
}
