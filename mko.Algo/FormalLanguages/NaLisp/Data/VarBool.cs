using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class VarBool : VariableOf<bool>
    {
        public VarBool(int ID)
            : base(Core.NameDir.Names.VarBool, ID) { }

        public VarBool(int ID, bool Value)
            : base(Core.NameDir.Names.VarBool, ID) { this.Value = Value; }

        public override void SetValue(bool newValue)
        {
            Value = newValue;
        }

        public override void SetValue(DateTime newValue)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(double newValue)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(int newValue)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(string newValue)
        {
            throw new NotImplementedException();
        }

        protected override Type GetResultConstType()
        {
            return typeof(ConstBool);
        }

        public override Core.Evaluator.Result Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(new ConstBool(Value), new Core.Inspector.ProtocolEntry(this, true, true, GetResultConstType(), ""));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            return new VarBool(ID, Value);
        }
    }
}
