using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 22.9.2017
    /// Dient zur Berechnung des Prozessfortschrittes in langlaufenden Tasks
    /// </summary>
    public class ProgressFunctor
    {
        double _workDone = 0;
        double _workloadPerWorkUnit = 100.0;

        public ProgressFunctor(int fullWorkload)
        {
            _workloadPerWorkUnit = 100.0 / fullWorkload;
        }

        /// <summary>
        /// Signalisiert, dass eine weiteres Stück Arbeit geleistet wurde
        /// </summary>
        public void WorkUnitCompleted()
        {
            _workDone += _workloadPerWorkUnit;
        }


        /// <summary>
        /// Liefert die bereits geleistete Menge an Arbeit in Prozent
        /// </summary>
        public int workDoneInPercent
        {
            get
            {
                return (int)Math.Round(_workDone);
            }
        }
    }

}
