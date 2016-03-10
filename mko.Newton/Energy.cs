using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{

    /// <summary>
    /// Messwerte der Energie (z.B. Ablesung vom Stromzähler)
    /// </summary>
    public abstract partial class Energy : MeasuredValue
    {

        public Energy(double Value) : base(Value) { }
        public Energy(Energy Value) : base(Value) { }

        public override string SiBaseUnitId
        {
            get { return "J"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "Energie"; }
        }

        
    }
}
