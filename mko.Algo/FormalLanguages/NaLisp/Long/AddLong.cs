using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Long
{
    public class Add : Long, Core.INonTerminal
    {
        public Add(params Long[] Operands)
            : base((int)Core.NameDir.Names.AddLong)
        {
            
        }

        //public override bool IsTerminal
        //{
        //    get { return false; }
        //}

        //public override Core.INonTerminal GetNonTerminalInterface()
        //{
        //    return this;
        //}


        Long[] _Operands;
        public Core.NaLisp[] Elements
        {
            get { return _Operands; }
        }

        public Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            throw new NotImplementedException();
        }

        public Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            long akku = 0;
            foreach (var el in EvaluatedElements)
            {
                akku += ((ConstLong)el.ResultTerm).Value;
            }

            return new Core.Evaluator.Result(new ConstLong(akku),  new Core.Inspector.ProtocolEntry(this, true, true, typeof(ConstLong), ToString()));
        }
        
        public override Core.NaLisp Clone(bool deep = true)
        {
            throw new NotImplementedException();
        }
    }
}
