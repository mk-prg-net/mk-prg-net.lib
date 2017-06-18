//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: Appointment.cs
//  Aufgabe/Fkt...: Implementierung eines Kalendereintrages
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
using mko.BI.Bo.Addresses;

using System.ComponentModel.DataAnnotations;

namespace mko.Timeline
{
    public class Appointment : IAppointment, IJson
    {
        public Appointment(
            IDate BeginDate, 
            ITime BeginTime, 
            IDate EndDate, 
            ITime EndTime, 
            string Owner,
            ILocation Location,
            AppointmentCategory category,
            string details)
        {
            this.BeginDate = BeginDate;
            this.BeginTime = BeginTime;
            this.EndDate = EndDate;
            this.EndTime = EndTime;
            this.Owner = Owner;
            this.Location = Location;
            this.Category = category;
            this.Details = details;
        }

        /// <summary>
        /// Mittels UIHint kann z.B. in ASP.NET MVC dieser Eigenschaft eine partielle View zum Rendern zugeorndet werden
        /// </summary>
        [UIHint("Date")]
        //[Newtonsoft.Json.JsonConverter(typeof(Date))]
        public IDate BeginDate
        {
            get;
        }

        [UIHint("Time")]
        //[Newtonsoft.Json.JsonConverter(typeof(Time))]
        public ITime BeginTime
        {
            get;
        }

        public AppointmentCategory Category
        {
            get;
        }

        public string Details
        {
            get;
        }

        [UIHint("Date")]
        //[Newtonsoft.Json.JsonConverter(typeof(Date))]
        public IDate EndDate
        {
            get;
        }

        [UIHint("Time")]
        //[Newtonsoft.Json.JsonConverter(typeof(Time))]
        public ITime EndTime
        {
            get;
        }

        [UIHint("Location")]
        //[Newtonsoft.Json.JsonConverter(typeof(mko.BI.Bo.Addresses.Location))]
        public ILocation Location
        {
            get;
        }

        public string Owner
        {
            get;
        }

        public string ToJson()
        {
#if (DEBUG)
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
#else
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
#endif
        }

        public override string ToString()
        {
            return string.Format(string.Format("Begin: {0}, End: {1}, Owner: {2}, {3}",
                Timeline.ToDateTime(BeginDate, BeginTime).ToString("s"),
                Timeline.ToDateTime(EndDate, EndTime).ToString("s"),
                Owner, Details));
        }
    }
}
