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
//  Datum.........: 27.10.15
//  Änderungen....: Eval gibt jetzt kein Result sondern ein NaLisp zurück
//
//</unit_history>
//</unit_header>        

using System;
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: Projektkontext
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
//  Datum.........: 27.10.2015
//  Änderungen....: Rückgabewert von Evaluator- Result auf NaLisp umgestellt.
//                  Fehler beim Evaluieren werden jetzt durch Exceptions signalisiert.
//</unit_history>
//</unit_header>        

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.Algo.Listprocessing;

namespace mko.NaLisp.Core
{
    public partial class Evaluator
    {
        public static Evaluator _ = new Evaluator();

        public class EvalException : Exception
        {
            public EvalException( INaLisp CurrentNode, string Message)
                : base(Message) 
            {
                _CurrentNode = CurrentNode;
            }

            public EvalException(INaLisp CurrentNode, string Message, Exception InnerException)
                : base(Message)
            {
                _CurrentNode = CurrentNode;
            }


            public INaLisp CurrentNode
            {
                get
                {
                    return _CurrentNode;
                }
            }
            INaLisp _CurrentNode;

        }

        public NaLispStack StackInstance = new NaLispStack();

        public bool DebugOn { get; set; }

        /// <summary>
        /// Berechnet einen TASP- Term. Das Ergebnis ist wiederum ein TASP
        /// </summary>
        /// <param name="SubTerm"></param>
        /// <returns></returns>
        public INaLisp Eval(INaLisp RootTerm)
        {

            StackInstance.Clear();
            return EvalR(RootTerm);

        }

        /// <summary>
        /// mko, 10.11.2015
        /// Eval als asynchronem Methode
        /// </summary>
        /// <param name="RootTerm"></param>
        /// <returns></returns>
        public async Task<INaLisp> EvalAsync(INaLisp RootTerm)
        {
            INaLisp res = null;
            await Task.Run(() => res = Eval(RootTerm));
            return res;
        }

        INaLisp EvalR(INaLisp SubTerm)
        {
            try
            {
                StackInstance.Push(SubTerm);

                if (SubTerm is ITerminal)
                {
                    // Wert einer konstanten Funktion berechnen
                    var ITerm = (Core.ITerminal)SubTerm;
                    return ITerm.Eval(StackInstance, DebugOn);
                }
                else
                {
                    // Wert einer Funktion mit n Parametern berechnen. 

                    var INonTerm = (Core.INonTerminal)SubTerm;

                    // Evaluierung von Funktionen, die den Kontrollfluss verändern erfolgt hier direkt.
                    // Die Eval- Methode der Funktionen selbst ist nicht implementiert
                    if (SubTerm is Control.IfThen)
                    {
                        // Eval von IfThen: 
                        // Bei IfThen wird nur der Zweig ausgewertet, der lt. Bedingung zu durchlaufenn ist
                        var IfThen = (Control.IfThen)SubTerm;

                        var resCond = EvalR(IfThen.Condition);
                        var resCondBool = (Data.ConstVal<bool>)resCond;
                        if (resCondBool.Value)
                            return EvalR(IfThen.IfTrue);
                        else
                            return EvalR(IfThen.IfFalse);
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
                            throw new  EvalException(Pipe, mko.TraceHlp.FormatErrMsg(this, "Pipe", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
                        }
                    }
                    else
                    {

                        // Funktionen in der Parameterlist evaluieren
                        INaLisp[] pList = INonTerm.Elements.Select(e => EvalR(e)).ToArray();

                        if (pList.Any(e => e is Lisp.Tuple))
                        {
                            // Ergebnisse von ausgewerteten Elementen, die Tupel sind, expandieren, wenn der SubTerm das zulässt
                            // a, (b,c,d), e -> a, b, c, d, e

                            IEnumerable<INaLisp> newPList = Fn.L<INaLisp>();
                            foreach (var res in pList)
                            {
                                //if (res.ResultTerm.Name == (int)Core.NameDir.Names.Tupel)
                                if (res is Lisp.Tuple)
                                {
                                    // Anwenden der Funktion auf die Elemente der Liste
                                    var Elems = ((Core.NaLispNonTerminal)res).Elements.ToArray();
                                    //newPList = newPList.Concat(Fn.L(SubTermNT.Eval(ElemsAsResults, StackInstance, DebugOn)));                                    
                                    newPList = newPList.Concat(Elems);
                                }
                                else
                                    newPList = newPList.Concat(Fn.L(res));
                            }

                            // Subterm auswerten
                            return INonTerm.Eval(newPList.ToArray(), StackInstance, DebugOn);
                        }
                        else
                        {
                            // Subterm auswerten, ohne Expansion von Listen
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
