using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Euklid.Transformations
{
    public class RotationInSphericalCoordinates : Transformation
    {
        double[] _rotPhiInRad;

        public RotationInSphericalCoordinates(params double[] rotAngleSphericalInRad)
            : base(rotAngleSphericalInRad.Length + 1)
        {
            _rotPhiInRad = new double[rotAngleSphericalInRad.Length];
            Array.Copy(rotAngleSphericalInRad, _rotPhiInRad, rotAngleSphericalInRad.Length);
        }


        ///// <summary>
        ///// Fügt zur allgemeinen Transformationsmatrix (affine Abbildung) einen Verschiebung hinzu
        ///// -              -  -            -
        ///// | m11 m12 | t1 |  |  cos phi  sin phi | 0 |
        ///// | m21 m22 | t2 |  | -sin phi  cos phi | 0 |
        ///// | --------+--- | *| ------------------+---|
        ///// |   0   0 |  1 |  |        0        0 | 1 |
        ///// -              -  -                       -
        ///// </summary>
        ///// <param name="combinedTransformation"></param>
        ///// <returns></returns>

        //public override Matrix apply(Matrix combinedTransformation)
        //{
        //    throw new NotImplementedException();
        //}



        public override Vector apply(Vector v)
        {
            Debug.Assert(v.Dimensions == Dimensions);
            Debug.Assert(v.Dimensions > 1);

             // Vector in Polarkoordinaten umwandeln 
            double[] vPhi = new double[v.Dimensions - 1];           

            // Rotation durchführen
            for (int i = 1; i < v.Dimensions; i++)
                vPhi[i-1] = v.Phi(i) + _rotPhiInRad[i-1];

            double vLength = v.Length;

            Vector vv = new Vector(v.Dimensions);

            for (int i = 0; i < v.Dimensions; i++)
            {
                vv[i] = vLength;
                if (i == 0)
                {
                    for (int k = 0; k < vPhi.Length; k++)
                        vv[i] *= Math.Sin(vPhi[k]);
                }
                else
                {
                    vv[i] *= Math.Cos(vPhi[i - 1]);
                    for (int k = i; k < vPhi.Length; k++)
                    {
                        vv[i] *= Math.Sin(vPhi[k]);
                    }
                }
            }

            return vv;

        }
    }
}
