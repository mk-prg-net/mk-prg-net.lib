using System;
using System.Collections.Generic;
using System.Text;

namespace mko.BatchProcessing.Test
{
    [Serializable]
    class TestJobProgressInfo : mko.BatchProcessing.JobProgressInfo
    {
        // Die bis Dato verstrichene Bearbeitungszeit in der Arbeitsstation
        public readonly int elapsedProcessTime;

        public TestJobProgressInfo(int jobId, mko.BatchProcessing.Job.JobStates state, int elapsedTime)
            : base(jobId, state)
        {
            elapsedProcessTime = elapsedTime;
        }
    }
}
