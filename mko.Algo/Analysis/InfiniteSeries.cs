using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.Analysis
{
    public class InfiniteSeries
    {
        public static decimal Sin(decimal x, int iterations = 10)
        {
            decimal powX = x;
            decimal fact = 1;
            decimal sum = 0;
            bool plus = true;

            for (int k = 0; k < iterations; k++)
            {
                if (plus)
                    sum += powX / fact;
                else
                    sum -= powX / fact;

                plus = !plus;
                powX *= x * x;
                fact *= (2*k + 2) * (2*k + 3);

            }

            return sum;
        }

    }
}
