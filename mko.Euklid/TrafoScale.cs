using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Euklid.Transformations
{
    public class Scale : Transformation
    {
        double[] scaleFactors;

        public double this[int axis]
        {
            get
            {
                return scaleFactors[axis];
            }
        }

        public Scale(params double[] scaleFactors)
            : base(scaleFactors.Length)
        {
            this.scaleFactors = new double[scaleFactors.Length];
            Array.Copy(scaleFactors, this.scaleFactors, scaleFactors.Length);
        }

        public override Vector apply(Vector v)
        {
            var scaledVector = new Vector(v);
            for (int i = 0; i < scaledVector.Dimensions; i++)
                scaledVector[i] *= scaleFactors[i];

            return scaledVector;
        }
    }
}
