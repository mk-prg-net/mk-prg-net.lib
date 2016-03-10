using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class ForceInNewton<TOrderOfMagnitude> : Force
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {
        protected override void CreateVector(int Dimension)
        {
            _a = new AccelerationInMeterPerSec<TOrderOfMagnitude>(Dimension);
        }

        protected override void CreateVector(params double[] Coordinates)
        {
            _a = new AccelerationInMeterPerSec<TOrderOfMagnitude>(Coordinates);
        }

        //public override Force Create(Mass m, Acceleration a)
        //{
        //    return new ForceInNewton<TOrderOfMagnitude>(a, m);   
        //}

        public ForceInNewton(int Dimension) : base(Dimension) { }
        public ForceInNewton(params double[] coordinates) : base(coordinates) { }
        public ForceInNewton(E.Vector V) : base(V) { }
        public ForceInNewton(Force F) : base(F) { }

        public override string UnitSymbol
        {
            get { return OofMServer.OrderOfMagnitudeId + "N"; }
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

        public override Mass M
        {
            get { return Mass.Kilogram(1.0); }
        }

        public override Mass ConvertInM(Mass M)
        {
            return Mass.Kilogram(M);
        }

        public override Acceleration A
        {
            get { return _a; }
        }
        AccelerationInMeterPerSec<TOrderOfMagnitude> _a;

        public override Acceleration ConvertInA(Acceleration A)
        {
            return new AccelerationInMeterPerSec<TOrderOfMagnitude>(A);
        }

        public override MeasuredVector Create(params double[] coordinates)
        {
            return new ForceInNewton<TOrderOfMagnitude>(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new ForceInNewton<TOrderOfMagnitude>((Force)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 1.0; }
        }

        // Operator

        public static bool operator ==(ForceInNewton<TOrderOfMagnitude> a, ForceInNewton<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(ForceInNewton<TOrderOfMagnitude> a, ForceInNewton<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static ForceInNewton<TOrderOfMagnitude> operator *(ForceInNewton<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static ForceInNewton<TOrderOfMagnitude> operator *(double factor, ForceInNewton<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static ForceInNewton<TOrderOfMagnitude> operator /(ForceInNewton<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static ForceInNewton<TOrderOfMagnitude> operator +(ForceInNewton<TOrderOfMagnitude> a, ForceInNewton<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static ForceInNewton<TOrderOfMagnitude> operator -(ForceInNewton<TOrderOfMagnitude> a, ForceInNewton<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }


    }
}
