using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Trafo = mko.Euklid.Transformations;

namespace mko.Graphic
{
    public interface IPlotter
    {
        /// <summary>
        /// Beginnt einen neuen Pfad mit Zeichenoperationen
        /// </summary>
        /// <returns></returns>
        IPath BeginPath(Brush brush, Pen pen);

    }
}
