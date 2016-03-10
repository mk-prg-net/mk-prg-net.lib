using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.MathOps
{
    public interface IFactory<T>
        where T: IComparable<T>
    {
        MathOps.ADD<T> ADD(params Core.INaLisp[] Elements);
        
        MathOps.SUB<T> SUB(params Core.INaLisp[] Elements);
        
        MathOps.MUL<T> MUL(params Core.INaLisp[] Elements);
        
        MathOps.DIV<T> DIV(params Core.INaLisp[] Elements);        

    }
}
