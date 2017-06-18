//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: Timeline.cs
//  Aufgabe/Fkt...: Simple Implementierung eies Timeline- Repositories
//                  
//
//
//
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

namespace mko.Timeline
{
    public partial class Timeline : ITimeline, IJson
    {

        public Timeline()
        {
            md5Gen = System.Security.Cryptography.MD5.Create();
        }


        System.Security.Cryptography.MD5 md5Gen;

        /// <summary>
        /// Konstriktor für die Deserialisierung aus einem JSon- String
        /// </summary>
        /// <param name="jsonDict"></param>
        public Timeline(string jsonDict)
        {
            // Json Deserialisierung.
            // Achtung. Appointments sind nur lesbar. Nur ein AppointmentBuilder kann zum erzeugen eines neuen Termins dienen.
            // In diesem ist ein spezieller Konstruktor mittels [JsonConstructor] Attribut gekennzeichnet
            var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.Dictionary<string, AppointmentBuilder>>(jsonDict);

            // Erzeugen der Termin- Dictionary aus den Buildern
            foreach (var kv in dict)
            {
                if (kv.Value.IsValid)
                {
                    _timelineDict[kv.Key] = kv.Value.Create();
                } else
                {
                    throw new Exception("Ungültige Daten für Termine entdeckt");
                }
            }
        }

        public static DateTime ToDateTime(IDate date, ITime time)
        {
            var t = time.ToDateTime();
            var d = date.ToDateTime();

            return new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);
        }

        public ITimelineFSSBld CreateFSSBld()
        {
            return new FSSBld(_timelineDict);
        }

        public IAppointment Get(string Owner, DateTime begin, DateTime end)
        {
            return _timelineDict[GetKey(Owner, begin, end)];
        }

        // Implementierungsdetails

        string GetKey(IAppointment appointment)
        {
            return GetKey(appointment.Owner, ToDateTime(appointment.BeginDate, appointment.BeginTime), ToDateTime(appointment.EndDate, appointment.EndTime));
        }

        string GetKey(string Owner, DateTime begin, DateTime end)
        {
            string txt = Owner
                        + begin.ToShortDateString()
                        + end.ToShortDateString();

            byte[] bytes = Encoding.Unicode.GetBytes(txt);
            byte[] md5 = md5Gen.ComputeHash(bytes);

            return String.Join("", md5.Select(b => b.ToString("x2")).ToArray());
        }

        public bool Exists(string Owner, DateTime begin, DateTime end)
        {
            return _timelineDict.ContainsKey(GetKey(Owner, begin, end));
        }

        public IAppointmentBuilder Create()
        {
            var bld = new AppointmentBuilder();
            CreateJobQueue.Enqueue(bld);
            return bld;

        }

        public void Delete(string Owner, DateTime begin, DateTime end)
        {
            DeleteJobQueue.Enqueue(GetKey(Owner, begin, end));
        }

        public void Delete(IAppointment appointment)
        {
            DeleteJobQueue.Enqueue(GetKey(appointment.Owner, ToDateTime(appointment.BeginDate, appointment.BeginTime), ToDateTime(appointment.EndDate, appointment.EndTime)));
        }


        public void SaveChanges()
        {
            // Alle Jobs, die Termine erstellen, realisieren
            while (CreateJobQueue.Count > 0)
            {
                var job = CreateJobQueue.Dequeue();

                mko.TraceHlp.ThrowArgExIfNot(job.IsValid, "ungültige Termine");

                var appointment = job.Create();
                _timelineDict[GetKey(appointment.Owner,
                    ToDateTime(appointment.BeginDate, appointment.BeginTime),
                    ToDateTime(appointment.EndDate, appointment.EndTime))] = appointment;

            }

            // Alle Jobs, die Termine löschen, realisieren
            while (DeleteJobQueue.Count > 0)
            {
                var key = DeleteJobQueue.Dequeue();

                mko.TraceHlp.ThrowArgExIfNot(_timelineDict.ContainsKey(key), "Nicht existierender Termin " + key + " sollte gelöscht werden");

                _timelineDict.Remove(key);
            }
        }


        public void Rollback()
        {
            CreateJobQueue.Clear();
            DeleteJobQueue.Clear();
        }

        /// <summary>
        /// Serialisiert Kalendar in einen JSon- String
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
#if(DEBUG)
            return Newtonsoft.Json.JsonConvert.SerializeObject(_timelineDict, Newtonsoft.Json.Formatting.Indented);
#else
              return Newtonsoft.Json.JsonConvert.SerializeObject(_timelineDict);
#endif
        }

        Queue<AppointmentBuilder> CreateJobQueue = new Queue<AppointmentBuilder>();

        Queue<string> DeleteJobQueue = new Queue<string>();

        /// <summary>
        /// Speicher der Daten primär in einem Dictionary
        /// </summary>
        public Dictionary<string, IAppointment> _timelineDict = new Dictionary<string, IAppointment>();

        public int Count
        {
            get
            {
                return _timelineDict.Count;
            }
        }
    }
}
