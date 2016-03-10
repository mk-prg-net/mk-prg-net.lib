using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public class DateTime : Data.IConstValueFactory<System.DateTime>
    {
        public static DateTime _ = new DateTime();

        public Data.IConstValue<System.DateTime> Create()
        {
            return new Data.ConstVal<System.DateTime>(System.DateTime.Now);
        }

        public Data.IConstValue<System.DateTime> Create(System.DateTime initVal)
        {
            return new Data.ConstVal<System.DateTime>(initVal);
        }
    }
}
