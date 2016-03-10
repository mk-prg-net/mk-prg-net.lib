//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den Nov. 2006
//
//  Projekt.......: Stapelverarbeitung
//  Name..........: Job.cs
//  Aufgabe/Fkt...: Ein Job ist ein Arbeitsauftrag für die Stapelverarbeitung.
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
//  Datum.........: 29.11.2011
//  Änderungen....: Zustand Pause hinzugefügt. Dieser zeigt an, das die 
//                  Verarbeitung eines Jobs temporär unterbrochen wurde.
// 
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 25.3.2013
//  Änderungen....: Aus dem Namensraum DMS in den Namensraum mko.BatchProcessing verschoben
//
//</unit_history>
//</unit_header>        

using System;
using System.Xml.Serialization;

namespace mko.BatchProcessing
{
    [Serializable]
    [XmlRoot(Namespace = "DMS", ElementName = "Job")]
    public class Job
    {
        //------------------------------------------------------------------------------------
        //Zustände eines Jobs

        public enum JobStates
        {
            undefined   = 0x1,  // Job wurde erstellt, aber es ist noch keine JobId definiert worden
            defined     = 0x2,  // Job wurde neu erstellt und JopId ist definiert
            waiting     = 0x4,  // Job befindet sich in der Warteschlange vor der Bearbeitungsstation
            processing  = 0x8,  // Job wird bearbeitet
            pause     = 0x9,    // Bearbeitung des Jobs pausiert/wurde kurz unterbrochen
            finished    = 0x10, //Job wurde fertiggestellt    
            aborted     = 0x20, //Bearbeitung des Jobs wurde abgebrochen    
        }

        JobStates mJobState;
        public JobStates JobState
        {
            get
            {
                return mJobState;
            }
            set
            {
            }
        }

        public virtual void SetWaiting()
        {
            mJobState = JobStates.waiting;
        }

        public virtual void SetProcessing()
        {
            mJobState = JobStates.processing;
        }

        public virtual void SetPause()
        {
            mJobState = JobStates.pause;
        }

        public virtual void SetFinished()
        {
            mJobState = JobStates.finished;
        }

        //Bricht die Bearbeitung eines Jobs ab
        public virtual void SetAborted()
        {
            mJobState = JobStates.aborted;
        }

        //------------------------------------------------------------------------------
        // Priorität eines Jobs
        public enum JobPriorities
        {
            High,
            Normal,
            Low
        }

        JobPriorities mJobPriority;
        public JobPriorities JobPriority
        {
            get
            {
                return mJobPriority;
            }
            set
            {
            }
        }

        //-------------------------------------------------------------------------------
        // Eindeutige Id eines Jobs innerhalb einer AppDomäne
        int mJobId;
        bool jobIdDefined = false;
        public int JobId
        {

            get
            {
                return mJobId;
            }

            set
            {
                if (mJobState != JobStates.undefined)
                    throw new Exception("JobId des Jobs wurde bereits festgelegt, und kann nicht überschrieben werden");

                mJobState = JobStates.defined;
                mJobId = value;
            }
        }

        //---------------------------------------------------------------------------------
        // Wenn der Client nach fertigstellung des Jobs auf diesen wieder zugreifen muss
        // dann ist OneWay auf true zu setzen, sonst false

        public bool OneWay = false;

        //---------------------------------------------------------------------------------
        // Konstruktoren

        // Der Parameterlose Konstruktor ist Vorraussetzung für die XML- Serialisierung
        public Job()
        {
            mJobState = JobStates.undefined;
            mJobPriority = JobPriorities.Normal;

        }

        public Job(int JobId)
        {
            mJobId = JobId;
            mJobState = JobStates.defined;
            mJobPriority = JobPriorities.Normal;
        }

        public Job(Job job)
        {
            mJobId = job.JobId;
            mJobState = job.JobState;
            mJobPriority = job.JobPriority;
        }

    } //End Class
}