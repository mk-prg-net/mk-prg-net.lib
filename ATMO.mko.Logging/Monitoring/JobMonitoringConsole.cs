using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;

using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.ComposerSubTrees;

namespace ATMO.mko.Logging.Monitoring
{
    /// <summary>
    /// Implementierung  einer einfachen Jobverwaltung
    /// </summary>
    public class JobMonitoringConsole : IJobMonitoring, IJobMonitoringConsole
    {
        public JobMonitoringConsole(PNDocuTerms.DocuEntities.IComposer pnL)
        {
            this.pnL = pnL;
        }

        PNDocuTerms.DocuEntities.IComposer pnL;
        long _nextJobId = 1;
        ConcurrentQueue<Job> _newJobQueue = new ConcurrentQueue<Job>();
        ConcurrentDictionary<long, Job> _Jobs = new ConcurrentDictionary<long, Job>();
        public RCV3sV<IEnumerable<IJob>> Jobs => RCV3sV<IEnumerable<IJob>>.Ok(_Jobs.Select(r => r.Value));

        public RCV3 abortJob(long JobId)
        {
            var ret = RCV3.Failed(pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3.Failed(JobIdNotFound(JobId));

            }
            else
            {
                _Jobs[JobId].State = JobState.aborted;
                ret = RCV3.Ok();
            }

            return ret;
        }


        public RCV3sV<JobState> continueJob(long JobId)
        {

            var ret = RCV3sV<JobState>.Failed(JobState.none, pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted,JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.running;
                ret = RCV3sV<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;
        }

        private PNDocuTerms.DocuEntities.IDocuEntity JobIdNotFound(long JobId)
        {
            return pnL.i("JobList",
                            pnL.m("get",
                                    pnL.p("JobId", pnL.txt(JobId.ToString())),
                                    pnL.ret(pnL.eFails(pnL.txt("NotFound")))));
        }

        public RCV3sV<JobState> deregisterJob(long JobId)
        {
            var ret = RCV3sV<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobIdNotFound(JobId));

            }            
            if(_Jobs[JobId].State == JobState.running)
            {
                ret = RCV3sV<JobState>.Failed(value: _Jobs[JobId].State,
                    pnL.ReturnValidatePreconditionFailed(
                        pnL.m(TechTerms.RelationalOperators.mNotEq,
                            pnL.p(TechTerms.MetaData.Arg, TechTerms.StateMachine.State),
                            pnL.p(TechTerms.MetaData.Val, JobState.running.ToString()))));
            }
            else
            {
                _Jobs.TryRemove(JobId, out Job job);
                job.State = JobState.completed;
                ret = RCV3sV<JobState>.Ok(job.State);
            }

            return ret;
        }

        public RCV3sV<long> registerJob(string title, long estimatedEffort)
        {
            var job = new Job();
            job.JobId = System.Threading.Interlocked.Increment(ref _nextJobId);
            job.EstimatedEffort = estimatedEffort;
            job.Title = title;
            job.Created = DateTime.Now;

            _Jobs[job.JobId] = job;

            return RCV3sV<long>.Ok(job.JobId);
        }

        public RCV3sV<JobState> reportProgess(long JobId, long progress)
        {
            var ret = RCV3sV<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobAbortedMsg(JobId));
            }
            else
            {
                var job = _Jobs[JobId];
                job.CurrentProgress += progress;
                ret = RCV3sV<JobState>.Ok(job.State);
            }

            return ret;

        }

        public RCV3sV<JobState> stopJob(long JobId)
        {
            var ret = RCV3sV<JobState>.Failed(JobState.none, pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.none, JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RCV3sV<JobState>.Failed(_Jobs[JobId].State, JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.stopped;
                ret = RCV3sV<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;

        }

        private PNDocuTerms.DocuEntities.IDocuEntity JobAbortedMsg(long JobId)
        {
            return pnL.i("Job", pnL.p("JobId", pnL.txt(JobId.ToString())), pnL.p("State", pnL.txt("aborted")));
        }

        public RCV3sV<JobState> completeJob(long JobId)
        {
            var ret = RCV3sV<JobState>.Failed(value: JobState.none, pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.none, JobIdNotFound(JobId));
            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RCV3sV<JobState>.Failed(_Jobs[JobId].State, JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.completed;
                ret = RCV3sV<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;

        }
    }
}
