using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Data
{
    public interface IConstValComp<T> : IConstValue<T>, IComparable<T>
        where T : IComparable<T>
    {
    }
}
