using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    /// <summary>
    /// Messwerte von Massen
    /// </summary>
    public abstract partial class Mass : MeasuredValue
    {

        public Mass() { }
        public Mass(double Value) : base(Value) { }
        public Mass(Mass Value) : base(Value) { }

        public override string SiBaseUnitId
        {
            get { return "Kg"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "The SI base unit of mass, defined as being equal to the mass of the international prototype of the kilogram."; }
        } 

    }
}
