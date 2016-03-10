using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60
{
    partial class Umlaufbahn : global::Kepler.IUmlaufbahn
    {

        global::Kepler.IHimmelskoerper global::Kepler.IUmlaufbahn.Zentralobjekt
        {
            get
            {
                return Zentralobjekt;
            }
            set
            {
                ((Himmelskoerper)value).TrabantenUmlaufbahnen.Add(this);
            }
        }
    }
}
