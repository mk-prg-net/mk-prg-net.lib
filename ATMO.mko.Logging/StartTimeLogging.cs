﻿//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 02.11.2017
//
//  Projekt.......: mko
//  Name..........: LaunchTimeSingleton.cs
//  Aufgabe/Fkt...: Records the start time of a application.
//                  Useful for calculating time differences.
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

namespace ATMO.mko.Logging
{

    /// <summary>
    /// mko, 2.11.2017
    /// Records the start time of a application.
    /// </summary>
    public class StartTimeSingleton
    {
        public static StartTimeSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StartTimeSingleton();
                }
                return _instance;
            }
        }
        static StartTimeSingleton _instance;


        public static long TimeDifferenceToStartTimeInMs(DateTime now)
        {

            var t = Math.Round(new TimeSpan(now.Ticks - StartTimeSingleton.Instance.StartTime.Ticks).TotalMilliseconds, 0);
            return (long)t;
        }

        public static long ElapsedTimeSinceStartInMs
        {
            get
            {
                return TimeDifferenceToStartTimeInMs(DateTime.Now);
            }
        }

        internal StartTimeSingleton()
        {
            StartTime = DateTime.Now;
        }

        public DateTime StartTime { get; }
    }
}

