using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Graphic.WebClient
{
    public class ContextCmdSetPen : mko.Graphic.ContextCmdSetPen
    {
        public override bool exec(IPlotter plotter)
        {
            return Pen.Set(plotter);
        }
    }
}
