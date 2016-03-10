using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp
{
    public class ComparsionOpsFactory<T> : ComparsionOps.IFactory<T>
        where T : IComparable<T>
    {        

        public ComparsionOps.Equal<T> EQ(Core.NaLisp Left, Core.NaLisp Right)            
        {
            return new ComparsionOps.Equal<T>(new Core.NaLisp[] {Left, Right});
        }

        public ComparsionOps.GreaterThen<T> GT(Core.NaLisp Left, Core.NaLisp Right)
        {
            return new ComparsionOps.GreaterThen<T>(new Core.NaLisp[] { Left, Right });
        }

        public ComparsionOps.GreaterEqualThen<T> GE(Core.NaLisp Left, Core.NaLisp Right)            
        {
            return new ComparsionOps.GreaterEqualThen<T>(new Core.NaLisp[] { Left, Right });
        }

        public ComparsionOps.LowerThen<T> LT(Core.NaLisp Left, Core.NaLisp Right)
        {
            return new ComparsionOps.LowerThen<T>(new Core.NaLisp[] { Left, Right });
        }

        public ComparsionOps.LowerEqualThen<T> LE(Core.NaLisp Left, Core.NaLisp Right)
        {
            return new ComparsionOps.LowerEqualThen<T>(new Core.NaLisp[] { Left, Right });
        }

    }
}
