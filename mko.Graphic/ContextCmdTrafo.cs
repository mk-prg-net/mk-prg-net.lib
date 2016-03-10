using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trafo = mko.Euklid.Transformations;

namespace mko.Graphic
{
    public abstract class ContextCmdTrafoRotateSpherical : ContextCmd
    {
        public ContextCmdTrafoRotateSpherical() : base() { }
        public ContextCmdTrafoRotateSpherical(Trafo.RotationInSphericalCoordinates trafo) { Trafo = trafo; }

        public Trafo.RotationInSphericalCoordinates Trafo { get; set; }
    }

    public abstract class ContextCmdTrafoRotateCylindrical : ContextCmd
    {
        public ContextCmdTrafoRotateCylindrical() : base() { }
        public ContextCmdTrafoRotateCylindrical(Trafo.RotationInCylindricalCoordinates trafo) { Trafo = trafo; }

        public Trafo.RotationInCylindricalCoordinates Trafo { get; set; }
    }

    public abstract class ContextCmdTrafoTranslate : ContextCmd
    {
        public ContextCmdTrafoTranslate() : base() { }
        public ContextCmdTrafoTranslate(Trafo.Translation trafo) { Trafo = trafo; }

        public Trafo.Translation Trafo { get; set; }

    }

    public abstract class ContextCmdScale : ContextCmd
    {
        public ContextCmdScale() : base() { }
        public ContextCmdScale(Trafo.Scale trafo) { Trafo = trafo; }
        public Trafo.Scale Trafo { get; set; }

    }
}
