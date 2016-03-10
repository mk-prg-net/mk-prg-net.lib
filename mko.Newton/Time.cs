using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public abstract partial class Time: MeasuredValue
    {

        public Time() { }
        public Time(double Value) : base(Value) { }
        public Time(Time Value) : base(Value) { }

        public override string SiBaseUnitId
        {
            get { return "s"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "The SI base unit of time, equal to the duration of 9192631770 periods of the radiation corresponding to the transition between the two hyperfine levels of the ground state of the caesium-133 atom."; }
        }
    }
}
