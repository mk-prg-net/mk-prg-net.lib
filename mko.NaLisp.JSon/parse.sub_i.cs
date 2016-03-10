using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.NaLisp;

namespace mko.NaLisp.JSon
{
    public class sub_i : ParseParameters
    {
        public const string Name = "sub_i";

        public override string FunctionName
        {
            get { return "sub_i"; }
        }

        protected override Base.Core.NaLisp CreateNaLisp(Base.Core.INaLisp[] parameters)
        {
            return Base.Factories.Int._.SUB(parameters);
        }
    }
}
