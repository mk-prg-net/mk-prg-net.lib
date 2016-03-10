using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Euklid.Transformations
{
    public abstract class Transformation 
    {
        int _dimensions;

        public Transformation(int dimensions)
        {
            _dimensions = dimensions;
        }

        public int Dimensions {
            get
            {
                return _dimensions;
            }
        }

        /// <summary>
        /// Transformiert den übergebenen Vektor in den Zielraum
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public abstract Vector apply(Vector v);

        ///// <summary>
        ///// Wendet die Transformation auf einen Kontext an
        ///// </summary>
        //public virtual void apply()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
