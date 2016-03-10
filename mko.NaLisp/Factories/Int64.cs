using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public partial class Int64 : Data.IConstValueFactory<long>
    {
        public Data.IConstValue<long> Create()
        {
            return new Data.ConstValComp<long>(0);

        }

        public Data.IConstValue<long> Create(long initVal)
        {
            return new Data.ConstValComp<long>(initVal);

        }
    }
}
