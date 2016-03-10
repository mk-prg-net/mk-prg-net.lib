using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp
{
    public interface IDblIntervalCtrl
    {
        double Maximum { get; set; }

        double Minimum { get; set; }

        event EventHandler VonBisChanged;

        mko.Interval<double> VonBis { get; set; }

        bool Restriktion { get; set; }

    }
}
