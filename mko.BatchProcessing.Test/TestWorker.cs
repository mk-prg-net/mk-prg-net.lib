using System;
using System.Collections.Generic;
using System.Text;
using mko.Log;

namespace mko.BatchProcessing.Test
{
    [Serializable]
    public class TestWorker : mko.BatchProcessing.IWorker
    {
        // Referenz auf Protokollobjekt
        [NonSerialized]
        LogServer log;

        // Zeitstempel zum Messen der Zeitspanne 
        DateTime WorkStartedAt;
        DateTime WorkDoneAt;

        // Referenz auf den aktuell zu bearbeitenden Job
        [NonSerialized]
        TestJob currentTestJob = null;

        public TestWorker(LogServer log)
        {
            this.log = log;
        }

        #region IWorker Member

        bool IWorker.setup(Job currentJob)
        {
            try
            {
                // downcast in den TestJob- Type
                currentTestJob = (TestJob)currentJob;

                // Festlegen der Zeitpunkte des Arbeitsbeginns und des Arbeitsendes
                WorkStartedAt = DateTime.Now;
                WorkDoneAt = WorkStartedAt.AddMilliseconds(currentTestJob.processingDuration);
                return true;

            }
            catch (InvalidCastException)
            {
                log.Log(RC.CreateError("worker.setup: Der übergebene Job ist nicht vom Typ TestJob"));
                return false;
            }
            catch (Exception ex)
            {
                log.Log(RC.CreateError(string.Format("worker.setup: Allgemeiner Fehler: {0}", ex.Message)));
                return false;
            }
        }

        int ElapsedProcessTime()
        {
            if (DateTime.Now.Ticks > WorkStartedAt.Ticks)
            {
                TimeSpan tsp = new TimeSpan(DateTime.Now.Ticks - WorkStartedAt.Ticks);
                return tsp.Milliseconds;
            }
            else
                return 0;
        }

        void IWorker.doIt(Job currentJob)
        {
            // Solange in der Warteschleife verweilen, bis der Job getan ist
            while (WorkDoneAt.Ticks > DateTime.Now.Ticks)
            {
                currentTestJob.elapsedProcessTime = ElapsedProcessTime();
                System.Threading.Thread.Sleep(100);
            }
        }

        JobProgressInfo IWorker.GetProgressInfo(Job job)
        {
            try
            {
                TestJobProgressInfo pinfo;
                if (job.JobId == currentTestJob.JobId)
                {                    
                    pinfo = new TestJobProgressInfo(currentTestJob.JobId, currentTestJob.JobState, ElapsedProcessTime());
                }
                else
                {
                    TestJob tjob = (TestJob)job;
                    pinfo = new TestJobProgressInfo(tjob.JobId, tjob.JobState, tjob.elapsedProcessTime);
                }

                return pinfo;
            }
            catch (InvalidCastException)
            {
                log.Log(RC.CreateError("worker.GetProgressInfo: Der übergebene Job ist nicht vom Typ TestJob"));
                return null;
            }           
            catch (Exception ex)
            {
                log.Log(RC.CreateError(string.Format("worker.GetProgressInfo: Allgemeiner Fehler: {0}", ex.Message)));
                return null;
            }

        }

        #endregion

        // Initialisierungroutine, die nach der Deserialisierung aufgerufen werden muß
        public void InitAfterDeserialization(LogServer log)
        {
            this.log = log;
        }
    }
}
