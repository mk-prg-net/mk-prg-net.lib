using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trafo = mko.Euklid.Transformations;

namespace mko.Graphic.WebClient
{
    public class ContextCmdTrafoRotateSpherical : mko.Graphic.ContextCmdTrafoRotateSpherical
    {
        public ContextCmdTrafoRotateSpherical() : base() { }
        public ContextCmdTrafoRotateSpherical(Trafo.RotationInSphericalCoordinates trafo) { Trafo = trafo; }

        public override bool exec(IPlotter plotter)
        {
            return ((mko.Graphic.WebClient.CanvasPlotter)plotter).SetTrafo(Trafo);

        }
    }

    public class ContextCmdTrafoRotateCylindrical : mko.Graphic.ContextCmdTrafoRotateCylindrical
    {
        public ContextCmdTrafoRotateCylindrical() : base() { }
        public ContextCmdTrafoRotateCylindrical(Trafo.RotationInCylindricalCoordinates trafo) { Trafo = trafo; }


        public override bool exec(IPlotter plotter)
        {
            return ((mko.Graphic.WebClient.CanvasPlotter)plotter).SetTrafo(Trafo);
        }
    }

    public class ContextCmdTrafoTranslate : mko.Graphic.ContextCmdTrafoTranslate
    {
        public ContextCmdTrafoTranslate() : base() { }
        public ContextCmdTrafoTranslate(Trafo.Translation trafo) { Trafo = trafo; }

        public override bool exec(IPlotter plotter)
        {
            return ((mko.Graphic.WebClient.CanvasPlotter)plotter).SetTrafo(Trafo);
        }
    }

    public class ContextCmdScale : mko.Graphic.ContextCmdScale
    {
        public ContextCmdScale() : base() { }
        public ContextCmdScale(Trafo.Scale trafo) { Trafo = trafo; }


        public override bool exec(IPlotter plotter)
        {
            return ((mko.Graphic.WebClient.CanvasPlotter)plotter).SetTrafo(Trafo);
        }
    }


}
