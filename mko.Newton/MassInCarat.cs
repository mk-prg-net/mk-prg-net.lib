using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class MassInCarat : Mass
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public MassInCarat() { }
        public MassInCarat(double Value) : base(Value) { }
        public MassInCarat(Mass Value) : base(Value) { }

        public override double ToBaseUnitConversionFactor
        {
            get { return 0.2; }
        }

        public override MeasuredValue Create(double Value)
        {
            return new MassInCarat(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new MassInCarat((Mass) Value);
        }

        public override string UnitSymbol
        {
            get { return "CD"; }
        }

        // Operator

        public static bool operator ==(MassInCarat a, MassInCarat b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(MassInCarat a, MassInCarat b)
        {
            return !EQUAL(a, b);
        }


        public static MassInCarat operator *(MassInCarat a, double factor)
        {
            return SCALE(factor, a);
        }

        public static MassInCarat operator *(double factor, MassInCarat a)
        {
            return SCALE(factor, a);

        }

        public static MassInCarat operator /(MassInCarat a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static MassInCarat operator +(MassInCarat a, MassInCarat b)
        {
            return ADD(a, b);
        }

        public static MassInCarat operator -(MassInCarat a, MassInCarat b)
        {
            return SUB(a, b);
        }


    }
}
