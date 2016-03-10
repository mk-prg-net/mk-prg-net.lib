using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.Algo.FormalLanguages.NaLisp;

namespace mko.NaLisp.JSon
{
    public class const_i : ParseUnary<long>
    {
        public const string Name = "const_i";

        protected override Base.Core.NaLisp CreateNaLisp(long Parameter)
        {
            return Base.Factory.Int((int)Parameter);
        }

        public override string FunctionName
        {
            get { return Name; }
        }
    }
}
