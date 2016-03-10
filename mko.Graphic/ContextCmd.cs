using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Graphic
{
    public abstract class ContextCmd
    {
        public abstract class Exception : System.Exception
        {
            public Exception() { }
            public Exception(string msg) : base(msg) { }
        }

        public abstract bool exec(IPlotter plotter);
    }
}
