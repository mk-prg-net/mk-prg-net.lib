using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;

namespace mko.Newton
{
    public class VelocityInKnots : Velocity
    {
        protected override void CreateVector(int Dimension)
        {
            _s = new LengthInNauticalMiles(Dimension);
        }

        protected override void CreateVector(params double[] Coordinates)
        {
            _s = new LengthInNauticalMiles(Coordinates);
        }

        public VelocityInKnots(int Dimension) : base(Dimension) { }
        public VelocityInKnots(params double[] coordinates) : base(coordinates) { }
        public VelocityInKnots(E.Vector V) : base(V.coordinates) { }
        //public VelocityInKnots(Length S, Time t) : base(S, t) { }
        public VelocityInKnots(Velocity V) : base(V) { }
        

        //public override Velocity Create(Length S, Time T)
        //{
        //    return new VelocityInKnots(S, T);
        //}


        public override Length S
        {
            get { return _s; }
        }
        LengthInNauticalMiles _s;

        public override Length ConvertInS(Length S)
        {
            return Length.NauticalMiles(S);
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
            return new VelocityInKnots(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new VelocityInKnots((Velocity)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 0.514444; }
        }

        // Operator

        public static bool operator ==(VelocityInKnots a, VelocityInKnots b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(VelocityInKnots a, VelocityInKnots b)
        {
            return !EQUAL(a, b);
        }


        public static VelocityInKnots operator *(VelocityInKnots a, double factor)
        {
            return SCALE(factor, a);
        }

        public static VelocityInKnots operator *(double factor, VelocityInKnots a)
        {
            return SCALE(factor, a);

        }

        public static VelocityInKnots operator /(VelocityInKnots a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static VelocityInKnots operator +(VelocityInKnots a, VelocityInKnots b)
        {
            return ADD(a, b);
        }

        public static VelocityInKnots operator -(VelocityInKnots a, VelocityInKnots b)
        {
            return SUB(a, b);
        }



    }
}
