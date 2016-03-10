using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.NaLisp;

namespace mko.NaLisp.JSon
{
    public class mul_i : ParseParameters
    {
        public const string Name = "mul_i";

        public override string FunctionName
        {
            get { return Name; }
        }

        protected override Base.Core.NaLisp CreateNaLisp(Base.Core.INaLisp[] parameters)
        {
            return Base.Factories.Int._.MUL(parameters);
        }
    }
}
