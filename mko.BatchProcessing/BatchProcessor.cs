//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.5.2008
//
//  Projekt.......: Stapelverarbeitung
//  Name..........: BatchProcessor
//  Aufgabe/Fkt...: Generischer Stapelverarbeitungsprozess.
//                  Zu verarbeitende Jobs m�ssen von der Klasse DMS.Job abgeleitet sein.
//                  Das den Job ausf�hrende Objekt mu� die Klasse DMS.IWorker implementieren.
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
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
//  Datum.........: 9.5.2011
//  �nderungen....: Threadsicherheit in DeliverFinishedJobs erh�ht.
//                  Jobs, bereits vor der Verarbeitung abgebrochen werden (Abort),
//                  werden jetzt bei der Bearbeitung �bersprungen. Auch werden abgebrochene
//                  Jobs nicht mehr automatisch gel�scht. Automatisch werden nur noch OneWay-
//                  Jobs gel�scht.
//                  Implementierung der neuen Schnittstellenfunktion RemoveFinishedOrAbortedJob.
//
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 25.3.2013
//  �nderungen....: Aus dem Namensraum DMS in den Namensraum mko.BatchProcessing verschoben
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

using mko.Log;

namespace mko.BatchProcessing
{
    [Serializable]
    public class BatchProcessor<TWorker> : MarshalByRefObject, IBatchProcessing
        where TWorker : IWorker
    {
        // Protokollierung 
        [NonSerialized]
        protected mko.Log.LogServer log;

        [NonSerialized]
        TraceSwitch ts = new TraceSwitch("TraceBatchProcessor", "Diagnoseprotokolle des Batchprocessors");

        // Objekt zur Verarbeitung von Jobs 
        [NonSerialized]
        IAsyncResult asyncResultDoJob;

        [NonSerialized]
        TWorker worker;

        [NonSerialized]
        DGworkerdoIt asyncDoIt;

        // Referenz auf in Bearbeitung befindlichen Job
        [NonSerialized]
        Job currentJob;

        // Zuletzt vergebene JobId
        static int lastJobId = 0;

        // Wenn Verarbeitungsstation f�r Job frei ist, dann wird busy auf true gesetzt,
        // sonst ist busy false
        bool busy = false;

        // Wenn true, dann wird die Jobverarbeitung angehalten
        bool _pause = false;

        // Konstruktoren
        public BatchProcessor(mko.Log.LogServer log, TWorker worker)
        {
            this.log = log;
            this.worker = worker;

            // Delegate f�r Asynchronen Start von DoIt initialisieren
            asyncDoIt = new DGworkerdoIt(this.worker.doIt);

            JobFertigEvent = new System.Threading.AutoResetEvent(false);            
        }

        // Implementierung des Stapleverarbeitungsprozesses
        #region IBatchProcessing Member

        Queue<Job> JobQueue = new Queue<Job>();
        Dictionary<int, Job> JobStorage = new Dictionary<int, Job>();

        // Semaphore zum signalisieren, das ein Job fertiggestellt wurde
        [NonSerialized]
        System.Threading.AutoResetEvent JobFertigEvent;


        int IBatchProcessing.NewJobId()
        {
            lock (this)
            {
                lastJobId += 1;
                return lastJobId;
            }
        }

        void IBatchProcessing.pushJob(Job job)
        {

            try
            {
                lock (JobQueue)
                {
                    JobStorage.Add(job.JobId, job);
                    JobQueue.Enqueue(job);

                    job.SetWaiting();

                    Trace.WriteLineIf(ts.TraceInfo, string.Format("Job id= {0:D} in Warteschlange gestellt", job.JobId));

                    // Wenn der Server ohne Arbeite ist, wird sofort die Bearbeitung eines neuen 
                    // Jobs gestartet
                    if (!busy)
                    {
                        ProcessNextJob();
                    }
                }
            }
            catch (Exception ex)
            {

                log.Log(RC.CreateError("PushJob: " + ex.Message));
                Trace.Fail("PushJob: " + ex.Message);
            }
        }

        // Wurde ein Job asynchron durchgef�hrt vom Worker, dann wird diese Methode zur�ckgerufen
        void JobFinished(IAsyncResult ar)
        {
            if (ar.IsCompleted)
            {
                JobFinished();
            }
            else
            {
                log.Log(RC.CreateError("BatchProcessor.JobFinished: Ein Thread, der einen Job macht, wurde vorzeitig abgebrochen. JobId: " + currentJob.JobId));
            }
        }

        /// <summary>
        /// 2011.05.09, mko
        /// In vorausgegangenen Versionen wurde der Fall nicht ber�cksichtigt, das ein auf die
        /// Verarbeitung wartender Job bereits abgebrochen werden kann. Durch eine Schleife,
        /// die alle bereits abgebrochenen Jobs �berspringt, wurde dies jetzt korrigiert
        /// </summary>
        private void ProcessNextJob()
        {
            // Sind weitere Jobs in der Job-Queue, dann wird die Arbeit mit dem n�chsten Job
            // aus der Queue unmittelbar fortgesetzt
            if (JobQueue.Count > 0 && !_pause)
            {
                // Alle abgebrochenen Jobs �berspringen
                do
                {
                    currentJob = JobQueue.Dequeue();
                    if (currentJob.JobState == Job.JobStates.aborted)
                    {
                        if (currentJob.OneWay)
                        {
                            JobStorage.Remove(currentJob.JobId);
                        }
                        currentJob = null;
                    }
                } while (JobQueue.Count > 0 && currentJob == null);

                if (currentJob != null)
                {
                    currentJob.SetProcessing();

                    // Konfigurieren der FeatureCollectoren
                    if (worker.setup(currentJob))
                    {
                        busy = true;
                        asyncResultDoJob = asyncDoIt.BeginInvoke(currentJob, new AsyncCallback(JobFinished), null);
                    }
                    else
                    {
                        // Job abbrechen 
                        currentJob.SetAborted();
                        JobFinished();
                    }
                }
            }
        }

        // 2011.05.09, mko : Parameterlose Version, um nach einem Abort eines Jobs zu reagieren
        void JobFinished()
        {
            try
            {
                lock (JobQueue)
                {
                    // 2011.05.09, mko: Abgebrochene Jobs werden hier nicht mehr gel�scht !
                    if (currentJob.JobState != Job.JobStates.aborted)
                    {
                        currentJob.SetFinished();
                        Trace.WriteLineIf(ts.TraceInfo, string.Format("Job id= {0:N0} fertiggestellt", currentJob.JobId));
                    }

                    // OneWay Jobs automatisch l�schen. Nicht OneWay Jobs verbleiben
                    // bis zur Auslieferung im JobStorage
                    // 2011.05.09, mko: Unabh�ngig vom Zustand werden (z.B. Finished od. aborted)
                    // werden One- Way Jobs gel�scht (vorher nur Finished Jobs. Aborted wurden immer 
                    // gel�scht)
                    if (currentJob.OneWay)
                    {
                        JobStorage.Remove(currentJob.JobId);
                    }

                    busy = false;

                    // Signalisieren an WaitUntilJobFinished, das ein Job fertiggestellt wurde
                    JobFertigEvent.Set();

                    // N�chten arbeitsbereiten Job zur Ausf�hrung bringen
                    ProcessNextJob();

                } // End Lock

            }
            catch (Exception ex)
            {
                log.Log(RC.CreateError("BatchProcessing.JobFinished: " + ex.Message));
                Trace.Fail("BatchProcessing.JobFinished: " + ex.Message);
            }
        }


        JobProgressInfo IBatchProcessing.GetProgressInfo(int JobId)
        {
            lock (JobQueue)
            {

                if (JobStorage.ContainsKey(JobId))
                {
                    Job aJob = JobStorage[JobId];
                    lock (aJob)
                    {
                        JobProgressInfo pinfo = worker.GetProgressInfo(aJob);
                        return pinfo;
                    }
                }

            }

            return null;

        }

        bool IBatchProcessing.WaitUntilJobFinished(int jobId, int timeout)
        {
            bool contain = false;
            lock (JobQueue)
            {
                contain = JobStorage.ContainsKey(jobId);
            }

            if (contain)
            {
                Job ajob = null;
                lock (JobQueue)
                {
                    ajob = JobStorage[jobId];
                }

                if (ajob.JobState == Job.JobStates.finished)
                    return true;
                else
                {

                    int i = timeout;
                    while (i > 0 || timeout == -1)
                    {

                        // Warten, bis der n�chste Job beendet wurde, und nachschauen, ob 
                        // dies unser Job war
                        JobFertigEvent.WaitOne(100, false);

                        if (ajob.JobState == Job.JobStates.finished)
                            return true;

                        i -= 100;
                    } //End While

                }// End If
            } // End If

            return false;
        }

        bool IBatchProcessing.DeliverFinishedJob(int JobId, out Job job)
        {
            lock (JobQueue)
            {

                if (JobStorage.ContainsKey(JobId))
                {
                    if (JobStorage[JobId].JobState == Job.JobStates.finished)
                    {
                        job = JobStorage[JobId];
                        JobStorage.Remove(JobId);
                        return true;
                    }
                }
            }

            // Undefinierten Job ausgeben
            job = new Job(-1);
            Trace.WriteLineIf(ts.TraceWarning, string.Format("BatchProcessor.DeliverFinishedJob: Zur JobId {0:D} existiert kein Job", JobId));
            return false;
        }

        bool IBatchProcessing.RemoveFinishedOrAbortedJob(int JobId)
        {
            lock (JobQueue)
            {
                if (JobStorage.ContainsKey(JobId))
                {
                    var job = JobStorage[JobId];
                    if (job.JobState == Job.JobStates.finished || job.JobState == Job.JobStates.aborted)
                    {
                        // Befindet sich der Job noch in der Warteaschlange, dann wird er zum L�schen 
                        // markiert. Sonst wird der Job sofort gel�scht
                        if (job.JobState == Job.JobStates.aborted && JobQueue.Contains(job))
                            job.OneWay = true;
                        else
                            JobStorage.Remove(JobId);
                        return true;
                    }
                    return false;
                }
                else
                    return false;
            }
        }

        bool IBatchProcessing.Abort(int JobId)
        {
            lock (JobQueue)
            {
                if (JobStorage.ContainsKey(JobId))
                {
                    // Falls der Job in Bearbeitung ist, dann wird die Bearbeitung gestoppt
                    if (JobStorage[JobId].JobState != Job.JobStates.finished)
                    {
                        if (JobStorage[JobId].Equals(currentJob))
                        {
                            //findFile.StopDirTree();
                        }
                        JobStorage[JobId].SetAborted();
                        return true;
                    }
                    else
                    {
                        // Job wird gel�scht                        
                        JobStorage.Remove(JobId);
                    }
                }
            } //End SyncLock
            Trace.WriteLineIf(ts.TraceWarning, string.Format("BatchProcessor.Abort: Die Bearbeitung des Jobs {0:D} konnte nicht abgebrochen werden", JobId));
            return false;
        }

        bool IBatchProcessing.Pause()
        {
            lock (JobQueue)
            {
                _pause = true;
            }
            return true;
        }

        bool IBatchProcessing.Resume()
        {
            try
            {
                lock (JobQueue)
                {
                    _pause = false;
                    // Sind weitere Jobs in der Job-Queue, dann wird die Arbeit mit dem n�chsten Job
                    // aus der Queue unmittelbar fortgesetzt
                    ProcessNextJob();

                } // End Lock
                return true;
            }

            catch (Exception ex)
            {
                log.Log(RC.CreateError("BatchProcessing.JobFinished: " + ex.Message));
                Trace.Fail("BatchProcessing.JobFinished: " + ex.Message);
                return false;
            }
        }

        bool IBatchProcessing.Idle()
        {
            lock (JobQueue)
            {
                if (JobQueue.Count == 0)
                    return true;
                else
                    return false;
            }
        }

        int[] IBatchProcessing.AllJobs()
        {
            lock (JobQueue)
            {
                if (JobStorage.Count > 0)
                {
                    int[] JobIDs = new int[JobStorage.Count];

                    int i = 0;
                    foreach (int JobId in JobStorage.Keys)
                    {
                        JobIDs[i] = JobId;
                        i++;
                    }
                    return JobIDs;
                }
                else return null;
            }
        }

        #endregion

        // Initialisierungen nach dem Deserialisierungsprozess
        [OnDeserialized]
        void InitAutomatic(StreamingContext ctx)
        {
            // Wiederherstellen des Delegaten f�r den Workerprozess
            ts = new TraceSwitch("TraceBatchProcessor", "Diagnoseprotokolle des Batchprocessors");

            JobFertigEvent = new System.Threading.AutoResetEvent(false);
        }

        public void InitAfterDeserialization(mko.Log.LogServer log, TWorker worker)
        {
            // Verweisen auf die Objekte aus dem Kontext
            this.log = log;
            this.worker = worker;
            asyncDoIt = new DGworkerdoIt(worker.doIt);
        }

    }
}
