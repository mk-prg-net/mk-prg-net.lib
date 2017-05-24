//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.5.2017
//
//  Projekt.......: mko.RPN.DateTime
//  Name..........: Composer.R.cs
//  Aufgabe/Fkt...: Erzeugt Datumsausdrücke in umgekehrt polnischer Notation
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

namespace mko.RPN.DateTime
{
    partial class Composer { 

        // Datumsfunktionen
        public string rYear(string val)
        {
            return rpn(fnDateTime.Year, val);
        }

        public string rYear(int val)
        {
            return rpn(fnDateTime.Year, rInt(val));
        }


        public string rMonth(string val)
        {
            return rpn(fnDateTime.Month, val);
        }

        public string rMonth(int val)
        {
            return rpn(fnDateTime.Month, rInt(val));
        }


        public string rDay(string val)
        {
            return rpn(fnDateTime.Day, val);
        }

        public string rDay(int val)
        {
            return rpn(fnDateTime.Day, rInt(val));
        }


        public string rDateTime(string val)
        {
            return pn(fnDateTime.DateTime, val);
        }

        public string rDateTime(int year)
        {
            return rpn(fnDateTime.DateTime, rYear(year));
        }

        public string rDate(int year, int month)
        {
            return rpn(fnDateTime.DateTime, rYear(year), rMonth(month));
        }


        public string rDate(int year, int month, int day)
        {
            return rpn(fnDateTime.DateTime, rYear(year), rMonth(month), rDay(day));
        }

        public string rDate(System.DateTime val)
        {
            return rpn(fnDateTime.DateTime, rYear(val.Year), rMonth(val.Month), rDay(val.Day));
        }       


    }
}
