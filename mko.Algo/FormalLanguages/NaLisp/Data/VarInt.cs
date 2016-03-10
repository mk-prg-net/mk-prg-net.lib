using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class VarInt : VariableOf<int>
    {
        public VarInt(int ID)
            : base(Core.NameDir.Names.VarInt, ID) { }

        public VarInt(int ID, int Value)
            : base(Core.NameDir.Names.VarInt, ID) { this.Value = Value; }

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
            Value = (int)newValue;
        }

        public override void SetValue(int newValue)
        {
            Value = newValue;
        }

        public override void SetValue(string newValue)
        {
            throw new NotImplementedException();
        }

        protected override Type GetResultConstType()
        {
            return typeof(ConstInt);
        }

        public override Core.Evaluator.Result Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(new ConstInt(Value), new Core.Inspector.ProtocolEntry(this, true, true, GetResultConstType(), ""));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            return new VarInt(ID, Value);
        }
    }
}
