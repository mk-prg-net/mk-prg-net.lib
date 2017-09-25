//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 25.9.2017
//
//  Projekt.......: MkPrgNet.Tools.Async
//  Name..........: ProgressorStatisticallyBasedPrediction.cs
//  Aufgabe/Fkt...: Ermöglicht die asynchrone Ausführung von Funktionen.
//                  Die Arbeitslast durch die asynchrone Funktion wir mit 
//                  zunächst geschätzt. Bei der Ausführung wird der Schätzwert 
//                  durch den realen erstzt.
//                  Nachfolgende Aufrufe beginnen die Abschätzung mit den Messwerten 
//                  der vorausgegangenen Aufrufe
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Tools.Async
{
    /// <summary>
    /// Implements a statictical estimation for workload,
    /// if it can not be determined exactly
    /// </summary>
    public class ProgressorStatisticallyBasedPrediction : IProgressor
    {
        /// <summary>
        /// Reflecting the amount of work done
        /// </summary>
        public void FireWorkDone()
        {
            lock (_workDoneLock)
            {
                // ensure that the end is signalled by returning a 100
                if (Math.Abs(_estimatedWorkload - _WorkDone) < 0.001)
                    WorkDone?.Invoke(100);
                else
                    WorkDone?.Invoke((int)(_WorkDone * 100.0 / _estimatedWorkload));
            }
        }

        object _workDoneLock = new object();
        double _WorkDone;
        double _workUnitDone;

        /// <summary>
        /// estimated Workload in ms
        /// </summary>
        double _estimatedWorkload;

        System.Timers.Timer timer = new System.Timers.Timer();

        public event Action<int> WorkDone;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estimatedWorkload">First estimated workload in ms</param>
        /// <param name="timerInterval">Timer interval ion ms</param>
        public ProgressorStatisticallyBasedPrediction(double estimatedWorkload, int timerInterval = 500)
        {
            _estimatedWorkload = estimatedWorkload;
            timer.Interval = timerInterval;
            timer.Enabled = false;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (_workDoneLock)
            {
                _WorkDone += _workUnitDone;
                if (_WorkDone >= _estimatedWorkload)
                {
                    // Work not finished. The estimated workload is too low. 
                    // Doubling corrects the prediction.
                    _estimatedWorkload *= 2.0;
                }
                FireWorkDone();
            }
        }

        /// <summary>
        /// Starts  a long runnig procedure asynchronly
        /// </summary>
        /// <param name="longRunningProc"></param>
        /// <returns></returns>
        public async Task StartAsync(Action longRunningProc)
        {
            lock (_workDoneLock)
            {
                _WorkDone = 0;

                // calculate fraction of work amount
                _workUnitDone = timer.Interval;
            }

            timer.Start();

            await Task.Run(longRunningProc);

            timer.Stop();

            lock (_workDoneLock)
            {
                var q = _WorkDone / _estimatedWorkload;
                if (q < 0.75 || q > 1.25)
                {
                    _estimatedWorkload = _WorkDone;
                }

                // Signal end of work
                _WorkDone = _estimatedWorkload;
                FireWorkDone();
            }
        }
    }
}
