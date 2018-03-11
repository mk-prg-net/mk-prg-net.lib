//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 13.11.2017
//
//  Projekt.......: mko
//  Name..........: RCBuilder.cs
//  Aufgabe/Fkt...: This Builder is nessesary for deserialization of a RC.
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

namespace mko.Logging
{
    /// <summary>
    /// mko, 13.11.2017
    /// Builder of RC for deserialization purpose.
    /// </summary>
    public class RCBuilder : ISucceeded, ITraceInfo
    {
        public string AssemblyName
        {
            get;
            set;
        }
        public string FunctionName
        {
            get;
            set;
        }

        public DateTime LogDate
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public bool Succeeded
        {
            get;
            set;
        }

        public string TypeName
        {
            get;
            set;
        }

        public string User
        {
            get;
            set;
        }

        public RCBuilder InnerRCV2 { get; set; }

        public RC CreateRC()
        {
            return new Logging.RC(Succeeded, LogDate, User, AssemblyName, TypeName, FunctionName, Message, InnerRCV2?.CreateRC());
        }
    }

    /// <summary>
    /// mko, 13.11.2017
    /// Builder of RC for deserialization purpose.
    /// </summary>
    public class RCBuilder<T> : RCBuilder, ISucceeded, ITraceInfo
    {
        public T Value
        {
            get;
            set;
        }

        public new  RC<T> CreateRC()
        {
            return new RC<T>(Succeeded, Value, LogDate, User, AssemblyName, TypeName, FunctionName, Message, InnerRCV2?.CreateRC());
        }
    }
}
