//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, Nov. 2006
//
//  Projekt.......: Stapelverarbeitung
//  Name..........: JobProgressInfo.cs
//  Aufgabe/Fkt...: Liefert Informationen über den aktuellen Bearbeitungszustand eines Jobs
//                  
//
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
//  Datum.........: 
//  Änderungen....: 
//
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 25.3.2013
//  Änderungen....: Aus dem Namensraum DMS in den Namensraum mko.BatchProcessing verschoben
//
//</unit_history>
//</unit_header>        
        
using System;

namespace mko.BatchProcessing
{
    [Serializable()]
    public class JobProgressInfo : ProgressInfo
    {
        public JobProgressInfo(int jobId, Job.JobStates jobState)
        {
            this.mJobId = jobId;
            this.mJobState = jobState;
        }       

        int mJobId;
        public int JobId
        {
            get
            {
                return mJobId;
            }
        }


        Job.JobStates mJobState;
        public Job.JobStates JobState
        {
            get
            {
                return mJobState;
            }
        }

    } // End Class
}