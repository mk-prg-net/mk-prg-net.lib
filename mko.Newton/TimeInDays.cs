using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class TimeInDays : Time
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public override MeasuredValue Create(double Value)
        {
            return new TimeInDays(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new TimeInDays((Time)Value);
        }

        public TimeInDays () { }
        public TimeInDays(double Value) : base(Value) { }
        public TimeInDays(Time Value) : base(Value) { }

        public override double ToBaseUnitConversionFactor
        {
            get { return 24.0*3600.0; }
        }

        public override string UnitSymbol
        {
            get { return "d"; }
        }

        // Operator

        public static bool operator ==(TimeInDays a, TimeInDays b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(TimeInDays a, TimeInDays b)
        {
            return !EQUAL(a, b);
        }


        public static TimeInDays operator *(TimeInDays a, double factor)
        {
            return SCALE(factor, a);
        }

        public static TimeInDays operator *(double factor, TimeInDays a)
        {
            return SCALE(factor, a);

        }

        public static TimeInDays operator /(TimeInDays a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static TimeInDays operator +(TimeInDays a, TimeInDays b)
        {
            return ADD(a, b);
        }

        public static TimeInDays operator -(TimeInDays a, TimeInDays b)
        {
            return SUB(a, b);
        }


    }
}
