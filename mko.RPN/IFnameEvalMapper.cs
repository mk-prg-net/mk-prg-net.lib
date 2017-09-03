//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 9.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: IFnameEvalMapper.cs
//  Aufgabe/Fkt...: Definieren für einen Funktionsnamen einen Evaluator in einer 
//                  Zurodnungstabelle fname -> Eval
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

namespace mko.RPN
{
    public interface IFnameEvalMapper
    {
        /// <summary>
        /// Erzeugt in einer Dictionary eine Zuordnung eines Funktionsnamens zu einem
        /// Evaluator
        /// </summary>
        /// <param name="dict"></param>
        void MapFnameToEvalIn(Dictionary<string, IEval> dict);
    }
}
