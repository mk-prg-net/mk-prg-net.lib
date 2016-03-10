using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo
{
    public class Radian
    {
        public const decimal decPI = 3.14159265358979323846264338328m;

        public static decimal DegToRad(decimal deg)
        {
            return deg * decPI / 180m;
        }

        public static decimal RadToDeg(decimal deg)
        {
            return deg * 180m/decPI;
        }


    }
}
