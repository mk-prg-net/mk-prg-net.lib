using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Testdata.XY
{
    public interface IGenerator
    {
        double[] GenerateX(mko.BI.Bo.Interval<double> Interval, int SampleCount);

        double[] GenerateY(mko.BI.Bo.Interval<double> Interval, int SampleCount);
    }
}
