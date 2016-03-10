using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public partial class Angle
    {
        public static AngleInRad Rad(Angle mVector)
        {
            return new AngleInRad(mVector);
        }

        public static AngleInDegree Degree(Angle mVector)
        {
            return new AngleInDegree(mVector);
        }

    }
}
