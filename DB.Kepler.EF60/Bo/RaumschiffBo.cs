using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60
{
    partial class Himmelskoerper : global::Kepler.IRaumschiff
    {
        public string Heimatland
        {
            get
            {
                return RaumschiffeTab.Land.Name;
            }
            set
            {
                RaumschiffeTab.Land.Name = value;
            }
        }
    }
}
