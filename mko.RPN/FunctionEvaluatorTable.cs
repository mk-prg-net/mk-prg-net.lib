//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: FunctionEvaluatorTable.cs
//  Aufgabe/Fkt...: Allgemeine Implementierung einer IFunctionEvaluatorTable.
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    public class FunctionEvaluatorTable : IFunctionEvaluatorTable        
    {
        /// <summary>
        /// Konstruktor der Function Evaluator Tabelle
        /// </summary>
        /// <param name="FnameEvalMappers">Liste von Delegates, die in der Tabelle der Name-Evaluator Zuordnungen neue Einträge anlegen</param>
        public FunctionEvaluatorTable(params IFnameEvalMapper[] FnameEvalMappers)
        {
            var dict = new Dictionary<string, mko.RPN.IEval>();

            foreach(var femap in FnameEvalMappers)
            {
                femap.MapFnameToEvalIn(dict);
            }

            _NameEvalTab = new ReadOnlyDictionary<string, IEval>(dict);

        }

        ReadOnlyDictionary<string, mko.RPN.IEval> _NameEvalTab;

        /// <summary>
        /// Zurodnungstabelle (Funktionsname, Evaluator)
        /// </summary>
        public IReadOnlyDictionary<string, IEval> FuncEvaluators
        {
            get
            {
                return _NameEvalTab;
            }
        }
    }
}
