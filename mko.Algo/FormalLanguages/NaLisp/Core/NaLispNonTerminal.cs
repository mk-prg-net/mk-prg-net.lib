//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: NaLispNonTerminal.cs
//  Aufgabe/Fkt...: Von NaLisp abgeleitete Klasse. Objekte sind Ausdrücke,
//                  deren Elemente ausschließlich NaLisp sind (hier immer in
//                  runde Klammern eingeschlossene Listen).
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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

namespace mko.Algo.FormalLanguages.NaLisp.Core
{
    public abstract class NaLispNonTerminal : NaLisp, INonTerminal
    {
        protected NaLispNonTerminal(int Name) 
            : base(Name) 
        { }

        //public override bool IsTerminal
        //{
        //    get { return false; }
        //}


        public NaLisp[] Elements { get; set; }


        public abstract Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult);

        /// <summary>
        /// Hilfsfunktion zum Validieren von Teilbäumen
        /// </summary>
        /// <param name="ElemValidationResult"></param>
        /// <returns></returns>
        protected bool SubTreeValid(IEnumerable<Core.Inspector.ProtocolEntry> ElemValidationResult)
        {
            return ElemValidationResult.Any() && ElemValidationResult.All(e => e.IsCurrentValid && e.IsTreeValid);
        }


        public abstract Evaluator.Result Eval(Evaluator.Result[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn);

        /// <summary>
        /// Hilfsfunktion zum Prüfen der Teilergebnisse in Eval
        /// </summary>
        /// <param name="EvaluatedElement"></param>
        /// <returns></returns>
        protected bool EvalSubTreeSuccesful(IEnumerable<Evaluator.Result> EvaluatedElement, Func<Evaluator.Result, bool> Test)
        {
            return EvaluatedElement.Any() && EvaluatedElement.All(e => e.ResultProtocolEntry.IsCurrentValid && e.ResultProtocolEntry.IsTreeValid && Test(e));
        }

        public override string ToString()
        {
            var bld = new StringBuilder();
            bld.Append(NameDir.Get(this).Name + "(");
            foreach (var e in Elements)
            {
                bld.Append(e.ToString());
                bld.Append(", ");
            }

            bld.Remove(bld.Length - 2, 2);
            bld.Append(")");
            return bld.ToString();
        }

    }
}
