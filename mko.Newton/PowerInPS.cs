using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class PowerInPS : Power
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public PowerInPS(double Value) : base(Value) { }
        public PowerInPS(Power P) : base(P) { }

        public override MeasuredValue Create(double Value)
        {
            return new PowerInPS(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new PowerInPS((Power)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 735.49875; }
        }

        public override string UnitSymbol
        {
            get { return "PS"; }
        }

        // Operator

        public static bool operator ==(PowerInPS a, PowerInPS b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(PowerInPS a, PowerInPS b)
        {
            return !EQUAL(a, b);
        }


        public static PowerInPS operator *(PowerInPS a, double factor)
        {
            return SCALE(factor, a);
        }

        public static PowerInPS operator *(double factor, PowerInPS a)
        {
            return SCALE(factor, a);

        }

        public static PowerInPS operator /(PowerInPS a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static PowerInPS operator +(PowerInPS a, PowerInPS b)
        {
            return ADD(a, b);
        }

        public static PowerInPS operator -(PowerInPS a, PowerInPS b)
        {
            return SUB(a, b);
        }


    }
}
