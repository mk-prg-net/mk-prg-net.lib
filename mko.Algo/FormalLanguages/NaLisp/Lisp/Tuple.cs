//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: List.cs
//  Aufgabe/Fkt...: Tupel ist ein Fragment einer Parameterliste. 
//                  Enthält ein NaLisp ein Tupel als Element, dann wird das Tupel
//                  beim Evaluieren expandiert. Dabei werden alle Elemente des Tupels
//                  evaluiert und die Ergebnisse eingefügt in die Liste der Ergebnisse,
//                  welche an die Eval- Methode des NaLisp ausdrucks übergeben werden:
//
//                  (Name a (Tupel b c d) e) -> (Name a b c d e)
//
//                  Listenoperationen:
//                  ------------------
//
//                  Listenoperationen operieren auf ihren evaluierten Elementen.
//                  Skip und Take liefern Tupel zurück, die ihrerseits wieder eingebettet werden
//                  können in übergeordneten NaLisp Aufrufen:
//
//                  (ADD (Take (int 2) (int 1) (int 2) (int 3) (int 4))) -> 
//                  (ADD (Tupel (int 1) (int 2))) -> 
//                  (ADD (int 1) (int 2))
//
//                  Concat gibt es nicht als explizite Listenoperation, da sie durch Einsatz zweier Tupel
//                  in einer Elementliste eines NaLisp bereits realisert wird
//
//                  (Tupel (Tupel (int 1) (int 2)) (Tupel (int 3) (int 4))) ->
//                  (Tupel (int 1) (int 2) (int 3) (int 4))
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

namespace mko.Algo.FormalLanguages.NaLisp.Lisp
{
    public class Tuple : Core.NaLispNonTerminal
    {
        public Tuple(Core.NaLisp[] Elements)
            : base((int)Core.NameDir.Names.Tupel)
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(ElemValidationResult), typeof(Lisp.Tuple), "");
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(new Tuple(EvaluatedElements.Select(e => e.ResultTerm).ToArray()), new Core.Inspector.ProtocolEntry(this, true, true, typeof(Tuple), ""));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            return new Tuple(Elements.Select(e => e.Clone()).ToArray());
        }
    }
}
