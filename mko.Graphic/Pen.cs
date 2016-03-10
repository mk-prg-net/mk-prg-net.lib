using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Css = mkoIt.Xhtml.Css;
using Nwt = mko.Newton;

namespace mko.Graphic
{
   public abstract class Pen : MediaObject
    {   
        public Css.BorderStyle DashStyle { get; set; }

        public Css.LengthPixel Width { get; set; }
    }
}
