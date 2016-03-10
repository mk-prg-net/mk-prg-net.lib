using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Control
{
    /// <summary>
    /// Hintereinanderschalten von Funktionen: f(a) | b | c => c(b(f(a)))
    /// </summary>
    public class Pipe : Core.NaLispNonTerminal
    {
        public Pipe(params Core.NaLisp[] Elements)
            : base((int)Core.NameDir.Names.Pipe)
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (Stack.ParentIs((int)Core.NameDir.Names.Pipe))
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
                gf.Elements = (new Core.NaLisp[] { f }).Concat(g.Elements).ToArray();

                f = gf;
            }

            return (Core.NaLispNonTerminal)f;
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            throw new NotImplementedException();
        }

        public override Core.NaLisp Clone(bool deep)
        {
            if (deep)
                return new Pipe(Elements);
            else
                return new Pipe(null);
        }
    }
}
