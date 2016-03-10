using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp
{
    public partial class Factory
    {
        // Konstanten erzeugen
        public static Data.ConstValComp<int> Int(int Value)
        {
            return new Data.ConstValComp<int>(Value);
        }

        public static Data.ConstValComp<long> Int64(int Value)
        {
            return new Data.ConstValComp<long>(Value);
        }

        public static  Data.ConstValComp<double> Dbl(double Value)
        {
            return new Data.ConstValComp<double>(Value);
        }

        public static Data.ConstVal<bool> Bool(bool Value)
        {
            return new Data.ConstVal<bool>(Value);
        }

        public static Data.ConstVal<string> Txt(string Value)
        {
            return new Data.ConstVal<string>(Value);
        }

        public static Data.ConstVal<DateTime> DateTime(DateTime dat)
        {
            return new Data.ConstVal<DateTime>(dat);
        }



    }
}
