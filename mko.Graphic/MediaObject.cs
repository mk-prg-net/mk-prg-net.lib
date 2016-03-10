using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Graphic
{
    public abstract class MediaObject
    {
        //public abstract void Implements(object graphicContext);
        public abstract bool Set(IPlotter plotter);
    }
        
}
