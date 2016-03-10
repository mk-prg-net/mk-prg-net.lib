﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class VarDbl : VariableOf<double>
    {
        public VarDbl(int ID)
            : base(Core.NameDir.Names.VarDbl, ID) { }

        public VarDbl(int ID, double Value)
            : base(Core.NameDir.Names.VarDbl, ID) { this.Value = Value; }

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
            Value = newValue;
        }

        public override void SetValue(int newValue)
        {
            Value = (double)newValue;
        }


        public override void SetValue(string newValue)
        {
            throw new NotImplementedException();
        }

        protected override Type GetResultConstType()
        {
            return typeof(ConstDbl);
        }

        public override Core.Evaluator.Result Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(new ConstDbl(Value), new Core.Inspector.ProtocolEntry(this, true, true, GetResultConstType(), ""));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            return new VarDbl(ID, Value);
        }
    }
}
