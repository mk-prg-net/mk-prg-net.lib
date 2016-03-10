using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public class Txt : Data.IConstValueFactory<string>
    {
        public static Txt _ = new Txt();

        public Data.IConstValue<string> Create()
        {
            return new Data.ConstVal<string>("");
        }

        public Data.IConstValue<string> Create(string initVal)
        {
            return new Data.ConstVal<string>(initVal);
        }
    }
}
