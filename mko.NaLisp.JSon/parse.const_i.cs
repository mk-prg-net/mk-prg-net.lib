using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.NaLisp;

namespace mko.NaLisp.JSon
{
    public class const_i : ParseUnary<long>
    {
        public const string Name = "const_i";

        protected override Base.Core.INaLisp CreateNaLisp(long Parameter)
        {
            return Base.Factories.Int._.Create((int)Parameter);
        }

        public override string FunctionName
        {
            get { return Name; }
        }
    }
}
