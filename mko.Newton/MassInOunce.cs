using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class MassInOunce : Mass
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public override MeasuredValue Create(double Value)
        {
            return new MassInOunce(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new MassInOunce((Mass)Value);
        }

        public MassInOunce() { }
        public MassInOunce(double Value) : base(Value) { }
        public MassInOunce(Mass Value) : base(Value) { }

        public override double ToBaseUnitConversionFactor
        {
            get { return 28.3495231; }
        }

        public override string UnitSymbol
        {
            get { return "oz"; }
        }

        // Operator

        public static bool operator ==(MassInOunce a, MassInOunce b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(MassInOunce a, MassInOunce b)
        {
            return !EQUAL(a, b);
        }


        public static MassInOunce operator *(MassInOunce a, double factor)
        {
            return SCALE(factor, a);
        }

        public static MassInOunce operator *(double factor, MassInOunce a)
        {
            return SCALE(factor, a);

        }

        public static MassInOunce operator /(MassInOunce a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static MassInOunce operator +(MassInOunce a, MassInOunce b)
        {
            return ADD(a, b);
        }

        public static MassInOunce operator -(MassInOunce a, MassInOunce b)
        {
            return SUB(a, b);
        }


    }
}
