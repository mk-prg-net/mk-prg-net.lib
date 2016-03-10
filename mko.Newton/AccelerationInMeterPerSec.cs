using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class AccelerationInMeterPerSec<TOrderOfMagnitude> : Acceleration
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {
        protected override void CreateVector(int Dimension)
        {
            _v = new VelocityInMeterPerSec<TOrderOfMagnitude>(Dimension);
        }

        protected override void CreateVector(params double[] Coordinates)
        {
            _v = new VelocityInMeterPerSec<TOrderOfMagnitude>(Coordinates);
        }

        public AccelerationInMeterPerSec(int Dimension) : base(Dimension) { }
        public AccelerationInMeterPerSec(params double[] coordinates) : base(coordinates) { }
        public AccelerationInMeterPerSec(E.Vector V) : base(V) { }
        public AccelerationInMeterPerSec(Acceleration A) : base(A) { }



        public override Velocity V
        {
            get { return _v; }
        }
        VelocityInMeterPerSec<TOrderOfMagnitude> _v;

        public override Velocity ConvertInV(Velocity V)
        {
            return new VelocityInMeterPerSec<TOrderOfMagnitude>(V);
        }

        public override Time T
        {
            get { return Time.Sec(1.0); }
        }

        public override Time ConvertInT(Time T)
        {
            return Time.Sec(T);
        }

        public override MeasuredVector Create(params double[] coordinates)
        {
            return new AccelerationInMeterPerSec<TOrderOfMagnitude>(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new AccelerationInMeterPerSec<TOrderOfMagnitude>((Acceleration)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 1.0; }
        }

        TOrderOfMagnitude OofMServer
        {
            get
            {
                return (TOrderOfMagnitude)OrderOfMagnitudeBase.Instance[typeof(TOrderOfMagnitude)];
            }
        }

        public override Mag.OrderOfMagnitudeEnum OrderOfMagnitude
        {
            get { return OofMServer.OrderOfMagnitude; }
        }

        // Operator

        public static bool operator ==(AccelerationInMeterPerSec<TOrderOfMagnitude> a, AccelerationInMeterPerSec<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(AccelerationInMeterPerSec<TOrderOfMagnitude> a, AccelerationInMeterPerSec<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static AccelerationInMeterPerSec<TOrderOfMagnitude> operator *(AccelerationInMeterPerSec<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static AccelerationInMeterPerSec<TOrderOfMagnitude> operator *(double factor, AccelerationInMeterPerSec<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static AccelerationInMeterPerSec<TOrderOfMagnitude> operator /(AccelerationInMeterPerSec<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static AccelerationInMeterPerSec<TOrderOfMagnitude> operator +(AccelerationInMeterPerSec<TOrderOfMagnitude> a, AccelerationInMeterPerSec<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static AccelerationInMeterPerSec<TOrderOfMagnitude> operator -(AccelerationInMeterPerSec<TOrderOfMagnitude> a, AccelerationInMeterPerSec<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }

    }
}
