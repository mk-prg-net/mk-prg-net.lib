using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp
{
    public partial class Factory
    {
        // Konstanten erzeugen
        public static Data.ConstInt Int(int Value)
        {
            return new Data.ConstInt(Value);
        }

        public static  Data.ConstDbl Dbl(double Value)
        {
            return new Data.ConstDbl(Value);
        }

        public static Data.ConstBool Bool(bool Value)
        {
            return new Data.ConstBool(Value);
        }

        public static Data.ConstString Txt(string Value)
        {
            return new Data.ConstString(Value);
        }

        public static Data.ConstDateTime DateTime(DateTime dat)
        {
            return new Data.ConstDateTime(dat);
        }



    }
}
