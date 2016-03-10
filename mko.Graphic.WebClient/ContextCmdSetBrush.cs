using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Graphic.WebClient
{
    public class ContextCmdSetBrush : mko.Graphic.ContextCmdSetBrush
    {
        public override bool exec(IPlotter plotter)
        {
            return Brush.Set(plotter);      
        }
    }
}
