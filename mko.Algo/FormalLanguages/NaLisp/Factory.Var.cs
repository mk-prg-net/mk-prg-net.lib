using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp
{
    public partial class Factory
    {
        public static Data.VarInt VarInt(int ID)
        {
            return new Data.VarInt(ID);
        }

        public static Data.VarDbl VarDbl(int ID)
        {
            return new Data.VarDbl(ID);
        }

        public static Data.VarBool VarBool(int ID)
        {
            return new Data.VarBool(ID);
        }

        public static Data.VarString VarString(int ID)
        {
            return new Data.VarString(ID);
        }

        public static Data.VarDateTime VarDateTime(int ID)
        {
            return new Data.VarDateTime(ID);
        }


        //-------------------------------------------------------------------------------------------------

        public static Data.VarInt VarInt(int ID, int Value)
        {
            return new Data.VarInt(ID, Value);
        }

        public static Data.VarDbl VarDbl(int ID, double Value)
        {
            return new Data.VarDbl(ID, Value);
        }

        public static Data.VarBool VarBool(int ID, bool Value)
        {
            return new Data.VarBool(ID, Value);
        }

        public static Data.VarString VarBool(int ID, string Value)
        {
            return new Data.VarString(ID, Value);
        }

        public static Data.VarDateTime VarDateTime(int ID, DateTime dat)
        {
            return new Data.VarDateTime(ID, dat);
        }

    }
}
