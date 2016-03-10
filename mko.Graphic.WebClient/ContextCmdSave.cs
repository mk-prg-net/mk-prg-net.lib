using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Graphic.WebClient
{
    public class ContextCmdSave : mko.Graphic.ContextCmdSave
    {
        public override bool exec(IPlotter plotter)
        {
            var pl = plotter as mko.Graphic.WebClient.CanvasPlotter;
            pl.Context.Add(pl.scriptBld.save());
            return true;
        }
    }
}
