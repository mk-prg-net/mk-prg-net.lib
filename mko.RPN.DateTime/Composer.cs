//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.5.2017
//
//  Projekt.......: mko.RPN.DateTime
//  Name..........: Composer.R.cs
//  Aufgabe/Fkt...: Erzeugt Datumsausdrücke in polnischer Notation
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
    public partial class Composer : mko.RPN.Composer        
    {
        public Composer(mko.RPN.IFunctionNames fnBase, IFunctionNamesDateTime fn) : base(fnBase)
        {
            this.fnDateTime = fn;
        }

        /// <summary>
        /// Namensliste der Zeitfunktionen
        /// </summary>
        public IFunctionNamesDateTime fnDateTime { get; }

        // Datumsfunktionen
        public string Year(string val)
        {
            return pn(fnDateTime.Year, val);
        }

        public string Year(int val)
        {
            return pn(fnDateTime.Year, Int(val));
        }


        public string Month(string val)
        {
            return pn(fnDateTime.Month, val);
        }

        public string Month(int val)
        {
            return pn(fnDateTime.Month, Int(val));
        }


        public string Day(string val)
        {
            return pn(fnDateTime.Day, val);
        }

        public string Day(int val)
        {
            return pn(fnDateTime.Day, Int(val));
        }


        public string DateTime(string val)
        {
            return pn(fnDateTime.DateTime, val);
        }

        public string DateTime(int year)
        {
            return pn(fnDateTime.DateTime, Year(year));
        }

        public string Date(int year, int month)
        {
            return pn(fnDateTime.DateTime, Year(year), Month(month));
        }


        public string Date(int year, int month, int day)
        {
            return pn(fnDateTime.DateTime, Year(year), Month(month), Day(day));
        }

        public string Date(System.DateTime val)
        {
            return pn(fnDateTime.DateTime, Year(val.Year), Month(val.Month), Day(val.Day));
        }

        


    }
}
