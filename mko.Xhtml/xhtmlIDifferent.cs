using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    interface IDifferent
    {
        /// <summary>
        /// True, wenn die beiden Eigenschaften bezüglich ihres Inhaltes verschieden sind.
        /// Sonst False.
        /// </summary>
        /// <param name="propertyA"></param>
        /// <param name="propertyB"></param>
        /// <returns></returns>
        bool IsDifferent(object propertyB);
    }

}
