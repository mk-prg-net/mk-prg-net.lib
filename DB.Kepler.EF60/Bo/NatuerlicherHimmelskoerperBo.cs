using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60
{
    public partial class Himmelskoerper : global::Kepler.INatuerlicherHimmelskoerper
    {
        public double Aequatordurchmesser_in_km
        {
            get
            {
                return Sterne_Planeten_MondeTab.Aequatordurchmesser_in_km;
            }
            set
            {
                Sterne_Planeten_MondeTab.Aequatordurchmesser_in_km = value;
            }
        }

        public double Polardurchmesser_in_km
        {
            get
            {
                return Sterne_Planeten_MondeTab.Polardurchmesser_in_km;
            }
            set
            {
                Sterne_Planeten_MondeTab.Polardurchmesser_in_km = value;
            }
        }

        public double Oberflaechentemperatur_in_K
        {
            get
            {
                return Sterne_Planeten_MondeTab.Oberflaechentemperatur_in_K;
            }
            set
            {
                Sterne_Planeten_MondeTab.Oberflaechentemperatur_in_K = value;
            }
        }

        public double Rotationsperiode_in_Stunden
        {
            get
            {
                return Sterne_Planeten_MondeTab.Rotationsperiode_in_Stunden;
            }
            set
            {
                Sterne_Planeten_MondeTab.Rotationsperiode_in_Stunden = value;
            }
        }

        public double Fallbeschleunigung_in_meter_pro_sec
        {
            get
            {
                return Sterne_Planeten_MondeTab.Fallbeschleunigung_in_meter_pro_sec;
            }
            set
            {
                Sterne_Planeten_MondeTab.Fallbeschleunigung_in_meter_pro_sec = value;
            }
        }

        public double Rotationsachsenneigung_in_Grad
        {
            get
            {
                return Sterne_Planeten_MondeTab.Rotationsachsenneigung_in_Grad;
            }
            set
            {
                Sterne_Planeten_MondeTab.Rotationsachsenneigung_in_Grad = value;
            }
        }


        global::Kepler.IUmlaufbahn global::Kepler.IHimmelskoerper.Umlaufbahn
        {
            get
            {
                return Umlaufbahn;
            }
            set
            {
                var ub = (Umlaufbahn)value;
                ub.Trabant = this;
                Umlaufbahn = ub;
            }
        }
    }
}
