using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public partial class Bool : Data.IConstValueFactory<bool>
    {
        public Data.IConstValue<bool> Create()
        {
            return new Data.ConstVal<bool>(false);
        }

        public Data.IConstValue<bool> Create(bool initVal)
        {
            return new Data.ConstVal<bool>(initVal);
        }
    }
}
