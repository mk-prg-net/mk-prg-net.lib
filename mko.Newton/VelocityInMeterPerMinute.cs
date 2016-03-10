using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;        
using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class VelocityInMeterPerMinute<TOrderOfMagnitude> : Velocity
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {
        protected override void CreateVector(int Dimension)
        {
            _s = new LengthInMeter<TOrderOfMagnitude>(Dimension);
        }

        protected override void CreateVector(params double[] Coordinates)
        {
            _s = new LengthInMeter<TOrderOfMagnitude>(Coordinates);
        }

        public VelocityInMeterPerMinute(int Dimension) : base(Dimension) { }
        public VelocityInMeterPerMinute(params double[] coordinates) : base(coordinates) { }
        public VelocityInMeterPerMinute(E.Vector V) : base(V.coordinates) { }
        public VelocityInMeterPerMinute(Length S, Time t) : base(S, t) { }
        public VelocityInMeterPerMinute(Velocity V) : base(V) { }

        //public override Velocity Create(Length S, Time T)
        //{
        //    return new VelocityInMeterPerMinute<TOrderOfMagnitude>(S, T);
        //}

        public override Length S
        {
            get { return _s; }
        }
        LengthInMeter<TOrderOfMagnitude> _s;

        public override Length ConvertInS(Length S)
        {
            return new LengthInMeter<TOrderOfMagnitude>(S);
        }

        public override Time T
        {
            get { return Time.Minutes(1.0); }
        }

        public override Time ConvertInT(Time t)
        {
            return Time.Minutes(t);
        }

        public override MeasuredVector Create(params double[] coordinates)
        {
            return new VelocityInMeterPerMinute<TOrderOfMagnitude>(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new VelocityInMeterPerMinute<TOrderOfMagnitude>((Velocity)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 1.0/60.0; }
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

        public static bool operator ==(VelocityInMeterPerMinute<TOrderOfMagnitude> a, VelocityInMeterPerMinute<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(VelocityInMeterPerMinute<TOrderOfMagnitude> a, VelocityInMeterPerMinute<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static VelocityInMeterPerMinute<TOrderOfMagnitude> operator *(VelocityInMeterPerMinute<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static VelocityInMeterPerMinute<TOrderOfMagnitude> operator *(double factor, VelocityInMeterPerMinute<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static VelocityInMeterPerMinute<TOrderOfMagnitude> operator /(VelocityInMeterPerMinute<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static VelocityInMeterPerMinute<TOrderOfMagnitude> operator +(VelocityInMeterPerMinute<TOrderOfMagnitude> a, VelocityInMeterPerMinute<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static VelocityInMeterPerMinute<TOrderOfMagnitude> operator -(VelocityInMeterPerMinute<TOrderOfMagnitude> a, VelocityInMeterPerMinute<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }





    }
}
