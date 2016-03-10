using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class VelocityInKilometerPerHour : Velocity
    {
        protected override void CreateVector(int Dimension)
        {
            _s = new LengthInMeter<Mag.Kilo>(Dimension);
        }

        protected override void CreateVector(params double[] Coordinates)
        {
            _s = new LengthInMeter<Mag.Kilo>(Coordinates);
        }

        public VelocityInKilometerPerHour(int Dimension) : base(Dimension) { }
        public VelocityInKilometerPerHour(params double[] coordinates) : base(coordinates) { }
        public VelocityInKilometerPerHour(E.Vector V) : base(V.coordinates) { }
        public VelocityInKilometerPerHour(Length S, Time t) : base(S, t) { }
        public VelocityInKilometerPerHour(Velocity V) : base(V) { }


        //public override Velocity Create(Length S, Time T)
        //{
        //    return new VelocityInKilometerPerHour(S, T);
        //}

        public override Length S
        {
            get { return _s; }
        }
        LengthInMeter<Mag.Kilo> _s;

        public override Length ConvertInS(Length S)
        {
            return Length.Kilometer(S);
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
            return new VelocityInKilometerPerHour(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new VelocityInMilesPerHour((Velocity)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 3.6; }
        }

        // Operatoren

        // Operator

        public static bool operator ==(VelocityInKilometerPerHour a, VelocityInKilometerPerHour b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(VelocityInKilometerPerHour a, VelocityInKilometerPerHour b)
        {
            return !EQUAL(a, b);
        }


        public static VelocityInKilometerPerHour operator *(VelocityInKilometerPerHour a, double factor)
        {
            return SCALE(factor, a);
        }

        public static VelocityInKilometerPerHour operator *(double factor, VelocityInKilometerPerHour a)
        {
            return SCALE(factor, a);

        }

        public static VelocityInKilometerPerHour operator /(VelocityInKilometerPerHour a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static VelocityInKilometerPerHour operator +(VelocityInKilometerPerHour a, VelocityInKilometerPerHour b)
        {
            return ADD(a, b);
        }

        public static VelocityInKilometerPerHour operator -(VelocityInKilometerPerHour a, VelocityInKilometerPerHour b)
        {
            return SUB(a, b);
        }



    }
}
