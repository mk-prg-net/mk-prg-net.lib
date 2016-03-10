using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class PowerInHorsePower : Power
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public PowerInHorsePower(double Value) : base(Value) { }
        public PowerInHorsePower(Power P) : base(P) { }


        public override MeasuredValue Create(double Value)
        {
            return new PowerInHorsePower(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new PowerInHorsePower((Power)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 745.7; }
        }

        public override string UnitSymbol
        {
            get { return "hp"; }
        }

        // Operator

        public static bool operator ==(PowerInHorsePower a, PowerInHorsePower b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(PowerInHorsePower a, PowerInHorsePower b)
        {
            return !EQUAL(a, b);
        }


        public static PowerInHorsePower operator *(PowerInHorsePower a, double factor)
        {
            return SCALE(factor, a);
        }

        public static PowerInHorsePower operator *(double factor, PowerInHorsePower a)
        {
            return SCALE(factor, a);

        }

        public static PowerInHorsePower operator /(PowerInHorsePower a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static PowerInHorsePower operator +(PowerInHorsePower a, PowerInHorsePower b)
        {
            return ADD(a, b);
        }

        public static PowerInHorsePower operator -(PowerInHorsePower a, PowerInHorsePower b)
        {
            return SUB(a, b);
        }


    }
}
