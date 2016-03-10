using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public class Var
    {
        public static Var _ = new Var();

        public static Data.VarOfComp<int> VarInt(string ID)
        {
            return new Data.VarOfComp<int>(ID);
        }

        public static Data.VarOfComp<long> VarInt64(string ID)
        {
            return new Data.VarOfComp<long>(ID);
        }

        public static Data.VarOfComp<double> VarDbl(string ID)
        {
            return new Data.VarOfComp<double>(ID);
        }

        public static Data.VarOf<bool> VarBool(string ID)
        {
            return new Data.VarOf<bool>(ID);
        }

        public static Data.VarOfComp<string> VarString(string ID)
        {
            return new Data.VarOfComp<string>(ID);
        }

        public static Data.VarOfComp<System.DateTime> VarDateTime(string ID)
        {
            return new Data.VarOfComp<System.DateTime>(ID);
        }


        //-------------------------------------------------------------------------------------------------

        public static Data.VarOfComp<int> VarInt(string ID, int Value)
        {
            return new Data.VarOfComp<int>(ID, Value);
        }
        
        public static Data.VarOfComp<long> VarInt64(string ID, long Value)
        {
            return new Data.VarOfComp<long>(ID, Value);
        }

        public static Data.VarOfComp<double> VarDbl(string ID, double Value)
        {
            return new Data.VarOfComp<double>(ID, Value);
        }

        public static Data.VarOf<bool> VarBool(string ID, bool Value)
        {
            return new Data.VarOf<bool>(ID, Value);
        }

        public static Data.VarOfComp<string> VarString(string ID, string Value)
        {
            return new Data.VarOfComp<string>(ID, Value);
        }

        public static Data.VarOfComp<System.DateTime> VarDateTime(string ID, System.DateTime dat)
        {
            return new Data.VarOfComp<System.DateTime>(ID, dat);
        }

    }
}
