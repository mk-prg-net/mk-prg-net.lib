using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public partial class Dbl : Data.IConstValueFactory<double>
    {
        public Data.IConstValue<double> Create()
        {
            return new Data.ConstValComp<double>(0);
        }

        public Data.IConstValue<double> Create(double initVal)
        {
            return new Data.ConstValComp<double>(initVal);
        }
    }
}
