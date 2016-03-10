using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    /// <summary>
    /// Messwerte von erfassten Wegen/Strecken
    /// </summary>
    public abstract partial class Length : MeasuredVector
    {
        public Length(int Dimension) : base(Dimension) { }
        public Length(params double[] coordinates) : base(coordinates) { }
        
        public Length(Length mVector) : base(mVector) { }

        public override string SiBaseUnitId
        {
            get { return "s"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "The SI base unit of length, defined as the length of the path travelled by light in absolute vacuum during 1/299792458 of a second."; }
        }                   


    }
}
