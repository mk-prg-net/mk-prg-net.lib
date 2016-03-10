using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;

namespace mko.Newton
{
    public class VelocityInMilesPerHour : Velocity
    {
        protected override void CreateVector(int Dimension)
        {
            _s = new LengthInMiles(Dimension);
        }

        protected override void CreateVector(params double[] Coordinates)
        {
            _s = new LengthInMiles(Coordinates);
        }

        public VelocityInMilesPerHour(int Dimension) : base(Dimension) { }
        public VelocityInMilesPerHour(params double[] coordinates) : base(coordinates) { }
        public VelocityInMilesPerHour(E.Vector V) : base(V.coordinates) { }
        public VelocityInMilesPerHour(Length S, Time t) : base(S,t) { }

        public VelocityInMilesPerHour(Velocity V) : base(V) { }

        //public override Velocity Create(Length S, Time T)
        //{
        //    return new VelocityInMilesPerHour(S, T);
        //}

        public override Length S
        {
            get { return _s; }
        }
        LengthInMiles _s;

        public override Length ConvertInS(Length S)
        {
            return Length.Miles(S);            
        }

        public override Time T
        {
            get { return Time.Hours(1.0); }
        }

        public override Time ConvertInT(Time t)
        {
            return Time.Hours(t);
        }

        public override MeasuredVector Create(params double[] coordinates)
        {
            return new VelocityInMilesPerHour(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new VelocityInMilesPerHour((Velocity) Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 0.44704; }
        }

        // Operator

        public static bool operator ==(VelocityInMilesPerHour a, VelocityInMilesPerHour b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(VelocityInMilesPerHour a, VelocityInMilesPerHour b)
        {
            return !EQUAL(a, b);
        }


        public static VelocityInMilesPerHour operator *(VelocityInMilesPerHour a, double factor)
        {
            return SCALE(factor, a);
        }

        public static VelocityInMilesPerHour operator *(double factor, VelocityInMilesPerHour a)
        {
            return SCALE(factor, a);

        }

        public static VelocityInMilesPerHour operator /(VelocityInMilesPerHour a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static VelocityInMilesPerHour operator +(VelocityInMilesPerHour a, VelocityInMilesPerHour b)
        {
            return ADD(a, b);
        }

        public static VelocityInMilesPerHour operator -(VelocityInMilesPerHour a, VelocityInMilesPerHour b)
        {
            return SUB(a, b);
        }




        
    }
}
