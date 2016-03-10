using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public partial class Ctrl
    {
        public static Ctrl _ = new Ctrl();

        public Control.IfThen IfThen(Core.INaLisp Condition, Core.INaLisp EvalIfTrue, Core.INaLisp EvalIfFalse)
        {
            return new Control.IfThen(new Core.INaLisp[] { Condition, EvalIfTrue, EvalIfFalse });
        }

        public Control.Pipe Pipe(params Core.INaLisp[] Elements)
        {
            return new Control.Pipe(Elements);
        }


    }
}
