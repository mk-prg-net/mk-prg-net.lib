using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp
{
    public delegate void InitBeginAndEndOfTimeEventHandler(object sender, InitBeginAndEndOfTimeArgs args);

    public interface ITimeIntervalCtrl
    {
        event InitBeginAndEndOfTimeEventHandler InitBeginAndEndOfTime;

        // Ältestes Datum, das als Intervalluntergrenze zulässig ist
        DateTime BeginningOfTime { get; set; }

        // Modernstes Datum, das als Intervallobergrenz zulässig ist
        DateTime EndOfTime { get; set; }

        event EventHandler VonBisChanged;

        mko.Interval<DateTime> VonBis { get; set; }

        bool Restriktion { get; set; }

    }
}
