using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.DateTime
{
    public class FunctionNameDateTimeBasic : IFunctionNamesDateTime
    {

        public FunctionNameDateTimeBasic(mko.RPN.IFunctionNames fn)
        {
            this.fnBase = fn;
        }

        public mko.RPN.IFunctionNames fnBase { get; }

        public string DateTime
        {
            get
            {
                return fnBase.NamePrefix + "dat";
            }
        }

        public string Year
        {
            get
            {
                return fnBase.NamePrefix + "y";
            }
        }

        public string Month
        {
            get
            {
                return fnBase.NamePrefix + "mon";
            }
        }

        public string Day
        {
            get
            {
                return fnBase.NamePrefix + "day";
            }
        }       

    }

}
