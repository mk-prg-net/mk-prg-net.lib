using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp
{
    partial class Factory
    {
        public static ComparsionOps.Equal EQ(Core.NaLisp Left, Core.NaLisp Right)
        {
            return new ComparsionOps.Equal(new Core.NaLisp[] {Left, Right});
        }

        public static ComparsionOps.GreaterThen GT(Core.NaLisp Left, Core.NaLisp Right)
        {
            return new ComparsionOps.GreaterThen(new Core.NaLisp[] { Left, Right });
        }

        public static ComparsionOps.GreaterEqualThen GE(Core.NaLisp Left, Core.NaLisp Right)
        {
            return new ComparsionOps.GreaterEqualThen(new Core.NaLisp[] { Left, Right });
        }

        public static ComparsionOps.LowerThen LT(Core.NaLisp Left, Core.NaLisp Right)
        {
            return new ComparsionOps.LowerThen(new Core.NaLisp[] { Left, Right });
        }

        public static ComparsionOps.LowerEqualThen LE(Core.NaLisp Left, Core.NaLisp Right)
        {
            return new ComparsionOps.LowerEqualThen(new Core.NaLisp[] { Left, Right });
        }

    }
}
