using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Euklid.Transformations
{
    public class RotationInCylindricalCoordinates : Transformation
    {
        double _rotAngleInRad;
        int _ixCartesianXAxis;
        int _ixCartesianYAxis;

        public double AngleInRad
        {
            get
            {
                return _rotAngleInRad;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensions">Dimension des Vektorraumes, für den die Roatation definiert wrid</param>
        /// <param name="rotAngleInRad">Drehwinkel</param>
        /// <param name="ixCartesianXAxis">X- Achse der Drehebene</param>
        /// <param name="ixCartesianYAxis">Y- Achse der Drehebene</param>
        public RotationInCylindricalCoordinates(int dimensions, double rotAngleInRad, int ixCartesianXAxis, int ixCartesianYAxis)
            : base(dimensions)
        {
            // Rotationsebene definieren
            _ixCartesianXAxis = ixCartesianXAxis;
            _ixCartesianYAxis = ixCartesianYAxis;
            _rotAngleInRad = rotAngleInRad;
        }

        public override Vector apply(Vector v)
        {
            Debug.Assert(v.Dimensions > _ixCartesianXAxis);
            Debug.Assert(v.Dimensions > _ixCartesianYAxis);

            // Umrechnen in Zylinderkoordinaten um die _ixCartesianAxis

            double phi = v.PhiCylindrical(_ixCartesianXAxis, _ixCartesianYAxis);

            // Drehwinkel hinzuaddieren
            phi += _rotAngleInRad;

            // Zurücktransformieren in kartesische Koordinaten

            Vector vv = new Vector(v);
            Vector vxy = new Vector(2, v[_ixCartesianXAxis], v[_ixCartesianYAxis]);
            double rxy = vxy.Length;


            vv[_ixCartesianXAxis] = rxy * Math.Cos(phi);
            vv[_ixCartesianYAxis] = rxy * Math.Sin(phi);

            return vv;

        }
    }
}
