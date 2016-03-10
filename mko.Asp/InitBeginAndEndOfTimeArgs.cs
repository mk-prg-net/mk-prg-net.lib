using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp
{
    public class InitBeginAndEndOfTimeArgs
    {
        ITimeIntervalCtrl ctrl;
        public InitBeginAndEndOfTimeArgs(ITimeIntervalCtrl vonBisCtrl)
        {
            ctrl = vonBisCtrl;
        }

        public DateTime BeginningOfTime
        {
            set
            {
                ctrl.BeginningOfTime = value;
            }
        }

        public DateTime EndOfTime
        {
            set
            {
                ctrl.EndOfTime = value;
            }
        }


    }
}
