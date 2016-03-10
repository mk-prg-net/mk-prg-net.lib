using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public partial class Angle
    {
        public static AngleInRad Rad(params double[] coordinates)
        {
            return new AngleInRad(coordinates);
        }

        public static AngleInDegree Degree(params double[] coordinates)
        {
            return new AngleInDegree(coordinates);
        }
    }
}
