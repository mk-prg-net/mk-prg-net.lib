using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class VarDateTime : VariableOf<DateTime>
    {
        public VarDateTime(int ID)
            : base(Core.NameDir.Names.VarDateTime, ID) { }

        public VarDateTime(int ID, DateTime Value)
            : base(Core.NameDir.Names.VarDateTime, ID) { this.Value = Value; }

        public override void SetValue(bool newValue)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(DateTime newValue)
        {
            Value = newValue;
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
            return typeof(ConstDateTime);
        }

        public override Core.Evaluator.Result Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(new ConstDateTime(Value), new Core.Inspector.ProtocolEntry(this, true, true, GetResultConstType(), ""));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            return new VarDateTime(ID, Value);
        }
    }
}
