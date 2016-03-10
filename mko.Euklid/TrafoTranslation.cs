using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Euklid.Transformations
{
    public class Translation : Transformation
    {
        Vector _displacement;

        public double this[int axis]
        {
            get
            {
                return _displacement[axis];
            }
        }

        public Translation(Vector displacement)
            : base(displacement.Dimensions)
        {
            _displacement = new Vector(displacement);
        }

        public Translation(params double[] displacemets)
            : base(displacemets.Length)
        {
            _displacement = new Vector(displacemets);
        }


        ///// <summary>
        ///// Fügt zur allgemeinen Transformationsmatrix (affine Abbildung) einen Verschiebung hinzu
        ///// -              -  -            -
        ///// | m11 m12 | t1 |  | 1  0 | t1n |
        ///// | m21 m22 | t2 |  | 0  1 | t2n |
        ///// | --------+--- | *| -----+-----|
        ///// |   0   0 |  1 |  | 0  0 |  1  |
        ///// -              -  -            -
        ///// </summary>
        ///// <param name="combinedTransformation"></param>
        ///// <returns></returns>
        //public override Matrix apply(Matrix combinedTransformation)
        //{
        //    Debug.Assert(combinedTransformation.Dimensions == _displacement.Dimensions + 1);

        //    var trans = Matrix.CreateIdentityMatrix(Dimensions + 1);

        //    for (int i = 0; i < _displacement.Dimensions; i++)
        //    {
        //        trans[i, _displacement.Dimensions + 1] = _displacement[i];
        //    }

        //    return combinedTransformation * trans;
            
        //}

        public override Vector apply(Vector v)
        {
             return new Vector(v) + _displacement;

        }
    }
}
