using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Monitoring
{
    /// <summary>
    /// mko, 8.11.2018
    /// Trheadsafe implementation of a Job
    /// </summary>
    public class Job : IJob
    {
        public Job()
        {
            // mko, 8.3.2019
            // Startet immer im Zustand running
            _JobState = JobState.running;
        }

        long _JobId;
        JobState _JobState;
        string _Title;
        long _EstimatedEffort;
        long _CurrentProgress;
        DateTime _Created;

        public long JobId {
            get
            {
                lock(this)
                {
                    return _JobId;
                }
            }
            set
            {
                lock (this)
                {
                    _JobId = value;
                }
            }
        }

        public JobState State {
            get
            {
                lock (this)
                {
                    return _JobState;
                }
            }
            set
            {
                lock (this)
                {
                    // Prüfen, ob Zustandsübergang gültig ist. Sonst vrebleibt Job im alten Zustand !
                    if (   (_JobState != JobState.running || value == JobState.stopped || value == JobState.aborted || value == JobState.completed)
                        && (_JobState != JobState.stopped || (value == JobState.aborted || value == JobState.completed || value == JobState.running))
                        && (_JobState != JobState.aborted || (value == JobState.aborted || value == JobState.completed))
                        && (_JobState != JobState.aborted || value == JobState.completed))
                    {
                        // state transist to stopped only possible if current JobState is running
                        // if jobstate is aborted no statuschange is possible
                        _JobState = value;
                    }                     
                }
                
            }
        }

        public string Title {
            get
            {
                lock (this)
                {
                    return _Title;
                }
            }
            set
            {
                lock (this)
                {
                    _Title = value;
                }
            }
        }

        public long EstimatedEffort {
            get
            {
                lock (this)
                {
                    return _EstimatedEffort;
                }
            }
            set
            {
                lock (this)
                {
                    _EstimatedEffort = value;
                }
            }
        }

        public long CurrentProgress {
            get
            {
                lock (this)
                {
                    return _CurrentProgress;
                }
            }
            set
            {
                lock (this)
                {
                    _CurrentProgress = value;
                }
            }
        }

        public DateTime Created
        {
            get
            {
                lock (this)
                {
                    return _Created;
                }

            }
            set
            {
                lock (this)
                {
                    _Created = value;
                }
            }
        }

        public int CurrentProgressInPercent => (int)(100.0 * CurrentProgress / EstimatedEffort);
    }
}
