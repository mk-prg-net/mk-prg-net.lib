using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class TimeInHours : Time
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public override MeasuredValue Create(double Value)
        {
            return new TimeInHours(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new TimeInHours((Time)Value);
        }

        public TimeInHours () { }
        public TimeInHours(double Value) : base(Value) { }
        public TimeInHours(Time Value) : base(Value) { }

        public override double ToBaseUnitConversionFactor
        {
            get { return 3600.0; }
        }

        public override string UnitSymbol
        {
            get { return "h"; }
        }

        // Operatoren

        public static bool operator ==(TimeInHours a, TimeInHours b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(TimeInHours a, TimeInHours b)
        {
            return !EQUAL(a, b);
        }


        public static TimeInHours operator *(TimeInHours a, double factor)
        {
            return SCALE(factor, a);
        }

        public static TimeInHours operator *(double factor, TimeInHours a)
        {
            return SCALE(factor, a);

        }

        public static TimeInHours operator /(TimeInHours a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static TimeInHours operator +(TimeInHours a, TimeInHours b)
        {
            return ADD(a, b);
        }

        public static TimeInHours operator -(TimeInHours a, TimeInHours b)
        {
            return SUB(a, b);
        }


    }
}
