using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.Algo.FormalLanguages.NaLisp;

namespace mko.NaLisp.JSon
{
    public class sub_i : ParseParameters
    {
        public const string Name = "sub_i";

        public override string FunctionName
        {
            get { return "sub_i"; }
        }

        protected override Base.Core.NaLisp CreateNaLisp(Base.Core.NaLisp[] parameters)
        {
            return Base.Factory.SUB_to_Int(parameters);
        }
    }
}
