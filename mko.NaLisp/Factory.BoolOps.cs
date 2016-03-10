using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp
{
    public class BoolOpsFactory : BoolOps.IFactory
    {
        public static Factory Instance = new BoolOpsFactory() {
            ConstBoolValueFactory = 
        }

        public BoolOps.AND AND(params Core.INaLisp[] Elements)
        {
            return new BoolOps.AND(Elements);
        }

        public BoolOps.OR OR(params Core.INaLisp[] Elements)
        {
            return new BoolOps.OR(Elements);
        }

        public BoolOps.NOT NOT(Core.INaLisp Element)
        {
            return new BoolOps.NOT(new Core.INaLisp[]{ Element});
        }

        public Data.IConstValueFactory<bool> ConstBoolValueFactory
        {
            get;
            set;            
        }
    }
}
