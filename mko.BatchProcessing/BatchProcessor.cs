//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.5.2008
//
//  Projekt.......: Stapelverarbeitung
//  Name..........: BatchProcessor
//  Aufgabe/Fkt...: Generischer Stapelverarbeitungsprozess.
//                  Zu verarbeitende Jobs müssen von der Klasse DMS.Job abgeleitet sein.
//                  Das den Job ausführende Objekt muß die Klasse DMS.IWorker implementieren.
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
//  Änderungen....: Threadsicherheit in DeliverFinishedJobs erhöht.
//                  Jobs, bereits vor der Verarbeitung abgebrochen werden (Abort),
//                  werden jetzt bei der Bearbeitung übersprungen. Auch werden abgebrochene
//                  Jobs nicht mehr automatisch gelöscht. Automatisch werden nur noch OneWay-
//                  Jobs gelöscht.
//                  Implementierung der neuen Schnittstellenfunktion RemoveFinishedOrAbortedJob.
//
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 25.3.2013
//  Änderungen....: Aus dem Namensraum DMS in den Namensraum mko.BatchProcessing verschoben
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

        // Wenn Verarbeitungsstation für Job frei ist, dann wird busy auf true gesetzt,
        // sonst ist busy false
        bool busy = false;

        // Wenn true, dann wird die Jobverarbeitung angehalten
        bool _pause = false;

        // Konstruktoren
        public BatchProcessor(mko.Log.LogServer log, TWorker worker)
        {
            this.log = log;
            this.worker = worker;

            // Delegate für Asynchronen Start von DoIt initialisieren
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

        // Wurde ein Job asynchron durchgeführt vom Worker, dann wird diese Methode zurückgerufen
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
        /// In vorausgegangenen Versionen wurde der Fall nicht berücksichtigt, das ein auf die
        /// Verarbeitung wartender Job bereits abgebrochen werden kann. Durch eine Schleife,
        /// die alle bereits abgebrochenen Jobs überspringt, wurde dies jetzt korrigiert
        /// </summary>
        private void ProcessNextJob()
        {
            // Sind weitere Jobs in der Job-Queue, dann wird die Arbeit mit dem nächsten Job
            // aus der Queue unmittelbar fortgesetzt
            if (JobQueue.Count > 0 && !_pause)
            {
                // Alle abgebrochenen Jobs überspringen
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
                    // 2011.05.09, mko: Abgebrochene Jobs werden hier nicht mehr gelöscht !
                    if (currentJob.JobState != Job.JobStates.aborted)
                    {
                        currentJob.SetFinished();
                        Trace.WriteLineIf(ts.TraceInfo, string.Format("Job id= {0:N0} fertiggestellt", currentJob.JobId));
                    }

                    // OneWay Jobs automatisch löschen. Nicht OneWay Jobs verbleiben
                    // bis zur Auslieferung im JobStorage
                    // 2011.05.09, mko: Unabhängig vom Zustand werden (z.B. Finished od. aborted)
                    // werden One- Way Jobs gelöscht (vorher nur Finished Jobs. Aborted wurden immer 
                    // gelöscht)
                    if (currentJob.OneWay)
                    {
                        JobStorage.Remove(currentJob.JobId);
                    }

                    busy = false;

                    // Signalisieren an WaitUntilJobFinished, das ein Job fertiggestellt wurde
                    JobFertigEvent.Set();

                    // Nächten arbeitsbereiten Job zur Ausführung bringen
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

                        // Warten, bis der nächste Job beendet wurde, und nachschauen, ob 
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
                        // Befindet sich der Job noch in der Warteaschlange, dann wird er zum Löschen 
                        // markiert. Sonst wird der Job sofort gelöscht
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
                        // Job wird gelöscht                        
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
                    // Sind weitere Jobs in der Job-Queue, dann wird die Arbeit mit dem nächsten Job
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
            // Wiederherstellen des Delegaten für den Workerprozess
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
