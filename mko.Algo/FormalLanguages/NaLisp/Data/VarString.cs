using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class VarString : VariableOf<string>
    {
        public VarString(int ID)
            : base(Core.NameDir.Names.VarBool, ID) { }

        public VarString(int ID, string Value)
            : base(Core.NameDir.Names.VarBool, ID) { this.Value = Value; }


        public override void SetValue(bool newValue)
        {
            throw new NotImplementedException();
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
            Value = newValue;
        }

        protected override Type GetResultConstType()
        {
            return typeof(ConstString);
        }

        public override Core.Evaluator.Result Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(new ConstString(Value), new Core.Inspector.ProtocolEntry(this, true, true, GetResultConstType(), ""));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            return new VarString(ID, Value);
        }
    }
}
