//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.5.2017
//
//  Projekt.......: mko.RPN.DateTime
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
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
    /// <summary>
    /// Basiklasse der Datumskomponenten
    /// </summary>
    public abstract class BaseDateComponentToken : FunctionNameToken
    {
        public BaseDateComponentToken(string Name, int val, int CountOfEvaluated) : base(Name, CountOfEvaluated)
        {
            this.Val = val;
        }

        public int Val { get; }

        /// <summary>
        /// Konfiguriert die Datuskomponente mit dem Wert im übergebenen Datum
        /// </summary>
        /// <param name="d"></param>
        public abstract System.DateTime Config(System.DateTime d);
    }

    /// <summary>
    /// Definiert einen Tag im Monat
    /// </summary>
    public class Day: BaseDateComponentToken
    {
        public Day(IFunctionNamesDateTime fn, int day, int CountOfEvaluated = 1): base(fn.Day, day, CountOfEvaluated)
        {}

        public override System.DateTime Config(System.DateTime d)
        {
            return new System.DateTime(d.Year, d.Month, Val);
        }
    }

    /// <summary>
    /// Definiert einen Monat im Jahr
    /// </summary>
    public class Month : BaseDateComponentToken
    {
        public Month(IFunctionNamesDateTime fn, int month, int CountOfEvaluated = 1) : base(fn.Day, month, CountOfEvaluated) { }

        public override System.DateTime Config(System.DateTime d)
        {
            return new System.DateTime(d.Year, Val, d.Day);
        }
    }

    /// <summary>
    /// Definiert das Jahr
    /// </summary>
    public class Year : BaseDateComponentToken
    {
        public Year(IFunctionNamesDateTime fn, int year, int CountOfEvaluated = 1) : base(fn.Day, year, CountOfEvaluated) { }

        public override System.DateTime Config(System.DateTime d)
        {
            return new System.DateTime(Val, d.Month, d.Day);
        }
    }


}
