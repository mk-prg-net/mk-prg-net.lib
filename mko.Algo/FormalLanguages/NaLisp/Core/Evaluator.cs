//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: Evaluator.cs
//  Aufgabe/Fkt...: Wertet NaLisp- Ausdrücke aus
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

using mko.Algo.Listprocessing;

namespace mko.Algo.FormalLanguages.NaLisp.Core
{
    public partial class Evaluator
    {
        public NaLispStack StackInstance = new NaLispStack();

        public bool DebugOn { get; set; }

        /// <summary>
        /// Berechnet einen TASP- Term. Das Ergebnis ist wiederum ein TASP
        /// </summary>
        /// <param name="SubTerm"></param>
        /// <returns></returns>
        public Result Eval(NaLisp RootTerm)
        {

            StackInstance.Clear();
            return EvalR(RootTerm);

        }

        Result EvalR(NaLisp SubTerm)
        {
            try
            {
                StackInstance.Push(SubTerm);

                if (SubTerm is ITerminal)
                {
                    //var SubTermT = (NaLispTerminal)SubTerm;
                    var ITerm = (Core.ITerminal)SubTerm;
                    return ITerm.Eval(StackInstance, DebugOn);
                }
                else
                {
                    //var SubTermNT = (NaLispNonTerminal)SubTerm;
                    //var SubTermNT = SubTerm;
                    var INonTerm = (Core.INonTerminal)SubTerm;

                    if (SubTerm is Control.IfThen)
                    {
                        // Eval von IfThen: 
                        // Bei IfThen wird nur der Zweig ausgewertet, der lt. Bedingung zu durchlaufenn ist
                        var IfThen = (Control.IfThen)SubTerm;

                        var resCond = EvalR(IfThen.Condition);
                        if (resCond.Valid)
                        {
                            var resCondBool = (Data.ConstBool)resCond.ResultTerm;
                            if (resCondBool.Value)
                                return EvalR(IfThen.IfTrue);
                            else
                                return EvalR(IfThen.IfFalse);
                        }
                        else
                            return new Result(SubTerm, new Inspector.ProtocolEntry(SubTerm, false, false, SubTerm.GetType(), "ifThen Bedingung konnte nicht ausgewertet werden"));
                    }
                    else if (SubTerm is Control.Pipe)
                    {
                        // Eval von Pipe: 
                        var Pipe = (Control.Pipe)SubTerm;

                        try
                        {

                            // 1. Form f(a) | g | ... umformen in ...(...g(f(a)) ...)
                            var f = Pipe.Transform();

                            // 2. Auswerten des umgeformten Terms
                            return EvalR(f);

                        }
                        catch (Exception ex)
                        {
                            return new Core.Evaluator.Result(Pipe, new Core.Inspector.ProtocolEntry(Pipe, false, true, null, mko.TraceHlp.FormatErrMsg(this, "Pipe", mko.ExceptionHelper.FlattenExceptionMessages(ex))));
                        }
                    }
                    else
                    {

                        // Elemente vom Subterm auswerten
                        //Core.Evaluator.Result[] pList = SubTermNT.Elements.Select(e => EvalR(e)).ToArray();
                        Core.Evaluator.Result[] pList = INonTerm.Elements.Select(e => EvalR(e)).ToArray();

                        //if (pList.Any(e => e.ResultTerm.Name == (int)Core.NameDir.Names.Tupel))
                        if (pList.Any(e => e.ResultTerm is Lisp.Tuple))
                        {
                            // Ergebnisse von ausgewerteten Elementen, die Tupel sind, expandieren, wenn der SubTerm das zulässt
                            // a, (b,c,d), e -> a, b, c, d, e

                            IEnumerable<Core.Evaluator.Result> newPList =  Fn.L<Core.Evaluator.Result>();
                            foreach (var res in pList)
                            {
                                //if (res.ResultTerm.Name == (int)Core.NameDir.Names.Tupel)
                                if (res.ResultTerm is Lisp.Tuple)
                                {
                                    // Anwenden der Funktion auf die Elemente der Liste
                                    var ElemsAsResults = ((Core.NaLispNonTerminal)res.ResultTerm).Elements.Select(e => new Core.Evaluator.Result(e, new Inspector.ProtocolEntry(res.ResultProtocolEntry))).ToArray();
                                    //newPList = newPList.Concat(Fn.L(SubTermNT.Eval(ElemsAsResults, StackInstance, DebugOn)));                                    
                                    newPList = newPList.Concat(ElemsAsResults);                                    
                                }
                                else
                                    newPList = newPList.Concat(Fn.L(res));
                            }

                            // Subterm auswerten
                            return INonTerm.Eval(newPList.ToArray(), StackInstance, DebugOn);
                        }
                        else
                        {
                            // Subterm auswerten, ohne expansion von Listen
                            return INonTerm.Eval(pList, StackInstance, DebugOn);
                        }                        
                    }
                }
            }
            finally
            {
                StackInstance.Pop();
            }
        }

    }
}
