using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public abstract partial class Angle : MeasuredVector
    {

        public Angle() { }
        public Angle(params double[] coordinates) : base(coordinates) { }
        public Angle(Angle mVector) : base(mVector) { }

        public override string SiBaseUnitId
        {
            get { return "rad"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "A radian is equal to the angle subtended at the centre of a circle by an arc of circumference equal in length to the circle's radius. The radian is therefore formally dimensionless, as it is a ratio of two lengths. There are 2*pi radians in a complete circle. "; }
        }

    }
}
