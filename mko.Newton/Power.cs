using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public abstract partial class Power : MeasuredValue
    {
        public Power(double Value) : base(Value) { }
        public Power(Power P) : base(P) { }

        public override string SiBaseUnitId
        {
            get { return "W"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "Leistung"; }
        }

    }
}
