using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using mko.Log;

using mko.BatchProcessing;


namespace mko.BatchProcessing.Test
{
    [TestClass]
    public class BatchProcessingTests
    {

        LogServer log = new LogServer();

        const string BatchProcessingBackupfile = "BatchProcessingBackup.bin";

        [TestMethod]
        public void SerializationTest()
        {
            Serialisierung();
            Deserialisieren();
        }

        private void Serialisierung()
        {
            // Protokollierung einrichten
            var dbgLogHnd = new mko.Log.DebugLogHandler("SerializationTest");
            log.registerLogHnd(dbgLogHnd);

            // Stapelverarbeitungsanlage aufbauen

            TestWorker worker = null;
            BatchProcessor<TestWorker> bp = null;
            IBatchProcessing ibp = null;
            IFormatter binform = new BinaryFormatter();
            //IFormatter binform = new SoapFormatter();

            // Fall: Aufbauen einer neuen Anlage
            worker = new TestWorker(log);
            bp = new BatchProcessor<TestWorker>(log, worker);
            ibp = bp;

            // Jobs anlegen
            int j1Id;
            TestJob j1 = TestJob.Create(ibp, 1000, out j1Id);

            int j2Id;
            TestJob j2 = TestJob.Create(ibp, 5000, out j2Id);

            int j3Id;
            TestJob j3 = TestJob.Create(ibp, 2000, out j3Id);

            // Jobs starten
            ibp.pushJob(j1);
            ibp.pushJob(j2);
            ibp.pushJob(j3);

            System.Threading.Thread.Sleep(500);

            // Anhalten und Serialisieren des BatchProzessors
            ibp.Pause();

            FileStream stream = new FileStream(BatchProcessingBackupfile, FileMode.Create);
            binform.Serialize(stream, worker);
            binform.Serialize(stream, bp);
            stream.Flush();
            stream.Close();
        }

        private void Deserialisieren()
        {
            // Protokollierung einrichten
            var dbgLogHnd = new mko.Log.DebugLogHandler("DeSerializationTest");
            log.registerLogHnd(dbgLogHnd);


            TestWorker worker = null;
            BatchProcessor<TestWorker> bp = null;
            IBatchProcessing ibp = null;
            IFormatter binform = new BinaryFormatter();
            //IFormatter binform = new SoapFormatter();

            // Fall: Wiederherstellen einer Anlage                                
            if (File.Exists(BatchProcessingBackupfile))
            {
                FileStream stream = new FileStream(BatchProcessingBackupfile, FileMode.Open);
                worker = (TestWorker)binform.Deserialize(stream);
                worker.InitAfterDeserialization(log);
                bp = (BatchProcessor<TestWorker>)binform.Deserialize(stream);
                bp.InitAfterDeserialization(log, worker);
                ibp = bp;
            }
            else
            {
                throw new Exception("Die Datei {0} mit den serialisierten Daten existiert nicht !");
            }

            // Verarbeitung fortsetzen
            ibp.Resume();

            // Stapelverarbitung überwachen
            Console.WriteLine("{3, 8}: {0,3} {1, 10} {2,6}", "JobId", "Zustand", "Verarbeitungszeit", "Zeit");

            while (!ibp.Idle())
            {
                ZustandeAllerJobsAusgeben(log, ibp);
                System.Threading.Thread.Sleep(1000);
            }
            ZustandeAllerJobsAusgeben(log, ibp);
        }

        private void ZustandeAllerJobsAusgeben(LogServer log, IBatchProcessing ibp)
        {
            int[] AllJobIds = ibp.AllJobs();
            foreach (int JobId in AllJobIds)
            {
                ZustandStapelverarbeitungAusgeben(log, ibp, JobId);
            }
            log.Log(mko.Log.RC.CreateStatus("------------------------------------------------------------------"));

        }

        private void ZustandStapelverarbeitungAusgeben(LogServer log, IBatchProcessing ibp, int JobId)
        {
            TestJobProgressInfo pi = (TestJobProgressInfo)ibp.GetProgressInfo(JobId);
            if (pi == null)
                throw new Exception(string.Format("Zur JobId {0:D} kann keine ProgressInfo abgerufen werden", JobId));
            string msg = string.Format("{3, 8:T}: {0,3:D} {1, 10:G} {2,6:D}", pi.JobId, pi.JobState, pi.elapsedProcessTime, pi.timestamp);
            log.Log(mko.Log.RC.CreateStatus(msg));
        }

    }
}
