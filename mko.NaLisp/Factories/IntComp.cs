using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public class IntComp : ComparsionOps.IFactory<int>
    {
        public IntComp _ = new IntComp();

        public ComparsionOps.Equal<int> EQ(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.Equal<int>(Left, Right);
        }

        public ComparsionOps.GreaterThen<int> GT(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.GreaterThen<int>(Left, Right);
        }

        public ComparsionOps.GreaterEqualThen<int> GE(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.GreaterEqualThen<int>(Left, Right);
        }

        public ComparsionOps.LowerThen<int> LT(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.LowerThen<int>(Left, Right);
        }

        public ComparsionOps.LowerEqualThen<int> LE(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.LowerEqualThen<int>(Left, Right);
        }
    }
}
