using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp
{
    partial class Factory
    {
        public static Control.IfThen IfThen(Core.NaLisp Condition, Core.NaLisp EvalIfTrue, Core.NaLisp EvalIfFalse)
        {
            return new Control.IfThen(new Core.NaLisp[] { Condition, EvalIfTrue, EvalIfFalse });
        }

        public static Control.Pipe Pipe(params Core.NaLisp[] Elements)
        {
            return new Control.Pipe(Elements);
        }


    }
}
