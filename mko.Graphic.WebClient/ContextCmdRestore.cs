using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Graphic.WebClient
{
    public class ContextCmdRestore : mko.Graphic.ContextCmdRestore
    {
        public override bool exec(IPlotter plotter)
        {
            var pl = plotter as mko.Graphic.WebClient.CanvasPlotter;
            pl.Context.Add(pl.scriptBld.restore());
            return true;
        }
    }
}
