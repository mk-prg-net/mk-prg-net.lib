using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.ImplHelper
{
    public class ConstVal<TNaLispConst, TConstValue>
        where TNaLispConst : Core.NaLisp
    {
        public ConstVal(TNaLispConst NaLispConst, TConstValue ConstValue)
        {
            _Value = ConstValue;
            _NaLispConst = NaLispConst;
        }

        TConstValue _Value;
        public TConstValue ConstValue { get { return _Value; } }

        TNaLispConst _NaLispConst;


        public Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack)
        {
            return new Core.Inspector.ProtocolEntry(Current: _NaLispConst, IsCurrentValid: true, IsTreeValid: true, TypeOfEvaluated: typeof(TNaLispConst));
        }

        public Core.Evaluator.Result Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {

            return new Core.Evaluator.Result(
                ResultTerm: _NaLispConst, 
                ProtocolEntry:  new Core.Inspector.ProtocolEntry(_NaLispConst, true, true, typeof(TConstValue), DebugOn ? ConstValue.ToString() : ""));
            
        }

        public string ToString()
        {
            return Core.NameDir.Get(_NaLispConst).Name + "(" + ConstValue.ToString() + ")";
        }
    }
}
