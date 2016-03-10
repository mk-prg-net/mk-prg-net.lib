using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public class Int64Comp : ComparsionOps.IFactory<long>
    {
        public Int64Comp _ = new Int64Comp();

        public ComparsionOps.Equal<long> EQ(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.Equal<long>(Left, Right);
        }

        public ComparsionOps.GreaterThen<long> GT(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.GreaterThen<long>(Left, Right);
        }

        public ComparsionOps.GreaterEqualThen<long> GE(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.GreaterEqualThen<long>(Left, Right);
        }

        public ComparsionOps.LowerThen<long> LT(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.LowerThen<long>(Left, Right);
        }

        public ComparsionOps.LowerEqualThen<long> LE(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.LowerEqualThen<long>(Left, Right);
        }
    }
}
