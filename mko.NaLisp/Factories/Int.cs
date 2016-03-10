using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public partial class Int : Data.IConstValueFactory<int>
    {
        public Data.IConstValue<int> Create()
        {
            return new Data.ConstValComp<int>(0);
        }

        public Data.IConstValue<int> Create(int initVal)
        {
            return new Data.ConstValComp<int>(initVal);
        }

    }
}
