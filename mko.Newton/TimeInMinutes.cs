using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class TimeInMinutes : Time
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public override MeasuredValue Create(double Value)
        {
            return new TimeInMinutes(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new TimeInMinutes((Time)Value);
        }

        public TimeInMinutes() { }
        public TimeInMinutes(double Value) : base(Value) { }
        public TimeInMinutes(Time Value) : base(Value) { }

        public override double ToBaseUnitConversionFactor
        {
            get { return 60.0; }
        }

        public override string UnitSymbol
        {
            get { return "Min"; }
        }

        // Operator

        public static bool operator ==(TimeInMinutes a, TimeInMinutes b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(TimeInMinutes a, TimeInMinutes b)
        {
            return !EQUAL(a, b);
        }


        public static TimeInMinutes operator *(TimeInMinutes a, double factor)
        {
            return SCALE(factor, a);
        }

        public static TimeInMinutes operator *(double factor, TimeInMinutes a)
        {
            return SCALE(factor, a);

        }

        public static TimeInMinutes operator /(TimeInMinutes a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static TimeInMinutes operator +(TimeInMinutes a, TimeInMinutes b)
        {
            return ADD(a, b);
        }

        public static TimeInMinutes operator -(TimeInMinutes a, TimeInMinutes b)
        {
            return SUB(a, b);
        }




    }
}
