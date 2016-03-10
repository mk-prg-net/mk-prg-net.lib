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

namespace mko.NaLisp.Core
{
    public abstract class NaLispNonTerminal : NaLisp, INonTerminal
    {

        //public override bool IsTerminal
        //{
        //    get { return false; }
        //}


        public INaLisp[] Elements { get; set; }

        /// <summary>
        /// mko, 9.11.2015
        /// Erzeugt alle Elemente der Paramaterliste
        /// </summary>
        /// <param name="Elements"></param>
        protected void CreateElements(params INaLisp[] Elements)
        {
            this.Elements = Elements;
        }


        public abstract Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult);

        /// <summary>
        /// 04.11.2015, mko
        /// Aufbau der Parameterliste nicht variadischer Funktionen prüfen
        /// </summary>
        /// <param name="ElemValidationResult"></param>
        /// <param name="ExpectedParamType"></param>
        /// <returns></returns>
        protected bool ParameterTypesAsExpected(Inspector.ProtocolEntry[] ElemValidationResult, params Type[] ExpectedParamType)
        {
            bool ok;
            if (ElemValidationResult.Length == ExpectedParamType.Length)
            {
                // Anzahl der Parameter ok
                ok = true;

                for (int i = 0; i < ExpectedParamType.Length; i++)
                {
                    // Typ eines einzelnen Parameters ok
                    ok &= ElemValidationResult[i].TypeOfEvaluated == ExpectedParamType[i];
                }
            }
            else
            {
                ok = false;
            }
            return ok;
        }

        /// <summary>
        /// 04.11.2015, mko
        /// Fehlerhaften Aufbau von Parameterliste von nicht variadischen Funktionen beschreiben.
        /// </summary>
        /// <param name="ElemValidationResult"></param>
        /// <param name="ExpectedParamType"></param>
        /// <returns></returns>
        protected string InvalidParameterListDescription(Inspector.ProtocolEntry[] ElemValidationResult, params Type[] ExpectedParamType)
        {
            var descr = new StringBuilder("");
            if (ElemValidationResult.Length == ExpectedParamType.Length)
            {
                // Anzahl der Parameter ok

                for (int i = 0; i < ExpectedParamType.Length; i++)
                {
                    // Typ eines einzelnen Parameters ok
                    if (ElemValidationResult[i].TypeOfEvaluated != ExpectedParamType[i])
                    {
                        descr.Append("Typ von Parameter[" + i + "]: ");
                        descr.Append(ElemValidationResult[i].TypeOfEvaluated.Name);
                        descr.Append(", erwartet: ");
                        descr.Append(ExpectedParamType[i].Name);
                    }

                    if (!ElemValidationResult[i].IsCurrentValid)
                    {
                        descr.Append(" Parameter[" + i + "] ist eine Funktion, deren Validierung fehlschlug");
                    }
                }
            }
            else
            {
                descr.Append("Anzahl Parameter ungültig. Erwartet: ");
                descr.Append(ExpectedParamType.Length);
                descr.Append(", erhalten");
                descr.Append(ElemValidationResult.Length);

            }
            return descr.ToString();
        }


        /// <summary>
        /// 23.2.2016
        /// Fehlerhaften Aufbau der Parameterlisten variadischer Funktionen erläutern.
        /// </summary>
        /// <param name="ElemValidationResult"></param>
        /// <param name="ExpectedParamType"></param>
        /// <returns></returns>
        protected string InvalidParameterListDescription(Inspector.ProtocolEntry[] ElemValidationResult, Type ExpectedParamType, int minParamCount)
        {
            var descr = new StringBuilder("");

            // Anzahl der Parameter ok
            if (ElemValidationResult.Length < minParamCount)
            {
                descr.Append("Es werden mindestens " + minParamCount + " Parameter erwartet. Die Funktion hat jedoch nur " + ElemValidationResult.Length + " Parameter");
            }

            for (int i = 0; i < ElemValidationResult.Length; i++)
            {
                // Typ eines einzelnen Parameters ok
                if (ElemValidationResult[i].TypeOfEvaluated != ExpectedParamType)
                {
                    descr.Append("Typ von Parameter[" + i + "]: ");
                    descr.Append(ElemValidationResult[i].TypeOfEvaluated.Name);
                    descr.Append(", erwartet: ");
                    descr.Append(ExpectedParamType.Name);
                }

                if (!ElemValidationResult[i].IsCurrentValid)
                {
                    descr.Append(" Parameter[" + i + "] ist eine Funktion, deren Validierung fehlschlug");
                }
            }

            return descr.ToString();

        }



        protected Inspector.ProtocolEntry ValidateAndDescribeResults(NaLispStack Stack, Type ReturnType, Inspector.ProtocolEntry[] ElemValidationResult, params Type[] ExpectedParamType)
        {
            if (Stack.ParentIs(typeof(Control.Pipe)))
            {
                // Der erste erwartete Parameter wird von der übergeordneten Pipe geliefert- seine Prüfung kann hier nicht erfolgen.
                return new Inspector.ProtocolEntry(Current: this,
                                                    IsCurrentValid: ParameterTypesAsExpected(ElemValidationResult, ExpectedParamType.Skip(1).ToArray()),
                                                    IsTreeValid: SubTreeValid(ElemValidationResult),
                                                    TypeOfEvaluated: ReturnType,
                                                    Description: GetOpName() + ": " + InvalidParameterListDescription(ElemValidationResult, ExpectedParamType.Skip(1).ToArray()),
                                                    ChildProtocolEntries: ElemValidationResult);
            }
            else
            {
                return new Inspector.ProtocolEntry(Current: this,
                                                    IsCurrentValid: ParameterTypesAsExpected(ElemValidationResult, ExpectedParamType),
                                                    IsTreeValid: SubTreeValid(ElemValidationResult),
                                                    TypeOfEvaluated: ReturnType,
                                                    Description: GetOpName() + ": " + InvalidParameterListDescription(ElemValidationResult, ExpectedParamType),
                                                    ChildProtocolEntries: ElemValidationResult);
            }
        }


        /// <summary>
        /// 23.2.2016
        /// Variadische Funktionen validieren
        /// </summary>
        /// <param name="Stack"></param>
        /// <param name="ReturnType"></param>
        /// <param name="ElemValidationResult"></param>
        /// <param name="ExpectedParamType"></param>
        /// <returns></returns>
        protected Inspector.ProtocolEntry ValidateAndDescribeResults(NaLispStack Stack, Type ReturnType, Inspector.ProtocolEntry[] ElemValidationResult, Type ExpectedParamType, int minParamCount = 1)
        {

            return new Inspector.ProtocolEntry(Current: this,
                                                IsCurrentValid: (ElemValidationResult.Length >= (minParamCount -  (Stack.ParentIs(typeof(Control.Pipe)) ?1:0))) &&  (!ElemValidationResult.Any() || ElemValidationResult.All(p => p.IsCurrentValid && p.TypeOfEvaluated == ExpectedParamType)),
                                                IsTreeValid: SubTreeValid(ElemValidationResult),
                                                TypeOfEvaluated: ReturnType,
                                                Description: GetOpName() + ": " + InvalidParameterListDescription(ElemValidationResult, ExpectedParamType),
                                                ChildProtocolEntries: ElemValidationResult);

        }


        /// <summary>
        /// Hilfsfunktion zum Validieren von Teilbäumen
        /// </summary>
        /// <param name="ElemValidationResult"></param>
        /// <returns></returns>
        protected bool SubTreeValid(IEnumerable<Core.Inspector.ProtocolEntry> ElemValidationResult)
        {
            return ElemValidationResult.Any() && ElemValidationResult.All(e => e.IsCurrentValid && e.IsTreeValid);
        }


        public abstract INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn);

        ///// <summary>
        ///// Hilfsfunktion zum Prüfen der Teilergebnisse in Eval
        ///// </summary>
        ///// <param name="EvaluatedElement"></param>
        ///// <returns></returns>
        //protected bool EvalSubTreeSuccesful(IEnumerable<NaLisp> EvaluatedElement, Func<Evaluator.Result, bool> Test)
        //{
        //    return EvaluatedElement.Any() && EvaluatedElement.All(e => e.ResultProtocolEntry.IsCurrentValid && e.ResultProtocolEntry.IsTreeValid && Test(e));
        //}

        public override string ToString()
        {
            var bld = new StringBuilder();
            bld.Append("(" + Name + " ");
            foreach (var e in Elements)
            {
                bld.Append(e.ToString());
                bld.Append(" ");
            }

            bld.Remove(bld.Length - 1, 1);
            bld.Append(")");
            return bld.ToString();
        }

        /// <summary>
        /// Klassenfabrik, die zur Implementierung von Clone dient
        /// </summary>
        /// <param name="Elements"></param>
        /// <returns></returns>
        protected abstract INaLisp Create(mko.NaLisp.Core.INaLisp[] Elements);

        public override Core.INaLisp Clone(bool deep = true)
        {
            if (deep)
                return Create(Elements.Select(r => r.Clone()).ToArray());
            else
                return Create(Elements);

        }

    }
}
