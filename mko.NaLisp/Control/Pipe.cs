//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: mko.NaLisp
//  Name..........: Pipe.cs
//  Aufgabe/Fkt...: Hintereinanderschaltung von Funktionen. Vereinfachte Syntax.
//                  Pipe(f1(...), f2(....)) -> f2(f1(....)....)
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

namespace mko.NaLisp.Control
{
    /// <summary>
    /// Hintereinanderschalten von Funktionen: f(a) | b | c => c(b(f(a)))
    /// </summary>
    public class Pipe : Core.NaLispNonTerminal
    {
        public Pipe(params Core.INaLisp[] Elements)
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (Stack.ParentIs(typeof(Pipe)))
            {
                if (Elements.Length < 1)
                    return new Core.Inspector.ProtocolEntry(this, false, SubTreeValid(ElemValidationResult), null, "Pipe- Operator innerhalb eines Pipe- Operators  muss mindestens 1 Argument haben");
                if (!(Elements.All(e => e is Core.NaLispNonTerminal)))
                    return new Core.Inspector.ProtocolEntry(this, false, SubTreeValid(ElemValidationResult), ElemValidationResult.Last().TypeOfEvaluated, "Pipe- Operator innerhalb eines Pipe- Operators darf nur aus NaLispNonTerminal Elementes bestehen.");
            }
            else
            {
                if (Elements.Length < 2)
                    return new Core.Inspector.ProtocolEntry(this, false, SubTreeValid(ElemValidationResult), null, "Pipe- Operator  muss mindestens 2 Argumente haben");
                if (!(Elements.Skip(1).All(e => e is Core.NaLispNonTerminal)))
                    return new Core.Inspector.ProtocolEntry(this, false, SubTreeValid(ElemValidationResult), ElemValidationResult.Last().TypeOfEvaluated, "Pipe- Operator darf bis auf das Erste nur aus NaLispNonTerminal Elemente bestehen.");
            }
            return new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(ElemValidationResult), ElemValidationResult.Last().TypeOfEvaluated, ToString());
        }

        /// <summary>
        /// Wandelt einen Pipe- Term f(a) | g um in g(f(a), ...)
        /// </summary>
        /// <returns></returns>
        public Core.NaLispNonTerminal Transform()
        {
            var f = Elements[0];
            foreach (Core.NaLispNonTerminal g in Elements.Skip(1))
            {
                // f(a) | g -> g(f(a), ...)                                        

                // Nur Kopfinformation clonen
                var gf = (Core.NaLispNonTerminal)g.Clone(false);

                // Elementliste aus f und der Elementliste von g zusammensetzen 
                gf.Elements = (new Core.INaLisp[] { f }).Concat(g.Elements).ToArray();

                f = gf;
            }

            return (Core.NaLispNonTerminal)f;
        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            throw new NotImplementedException();
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new Pipe(Elements);
        }

        //public override Core.NaLisp Clone(bool deep)
        //{
        //    if (deep)
        //        return new Pipe(Elements.Select(r => r.Clone()).ToArray());
        //    else
        //        return new Pipe(Elements);
        //}
    }
}
