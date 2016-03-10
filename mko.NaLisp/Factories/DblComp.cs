using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public class DblComp : ComparsionOps.IFactory<double>
    {

        public ComparsionOps.Equal<double> EQ(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.Equal<double>(Left, Right);
        }

        public ComparsionOps.GreaterThen<double> GT(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.GreaterThen<double>(Left, Right);
        }

        public ComparsionOps.GreaterEqualThen<double> GE(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.GreaterEqualThen<double>(Left, Right);
        }

        public ComparsionOps.LowerThen<double> LT(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.LowerThen<double>(Left, Right);
        }

        public ComparsionOps.LowerEqualThen<double> LE(Core.INaLisp Left, Core.INaLisp Right)
        {
            return new ComparsionOps.LowerEqualThen<double>(Left, Right);
        }
    }
}
