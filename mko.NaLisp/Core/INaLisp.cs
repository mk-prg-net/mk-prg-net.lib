using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Core
{
    public interface INaLisp        
    {
        string Name { get; }

        INaLisp Clone(bool deep = true);
    }
}
