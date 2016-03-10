using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class TimeInSec<TOrderOfMagnitude> : Time
        where TOrderOfMagnitude : mko.Newton.OrderOfMagnitudeBase
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public override MeasuredValue Create(double Value)
        {
            return new TimeInSec<TOrderOfMagnitude>(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new TimeInSec<TOrderOfMagnitude>((Time)Value);
        }

        public TimeInSec() { }
        public TimeInSec(double Value) : base(Value) { }
        public TimeInSec(Time Value) : base(Value) { }

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

        public override string UnitSymbol
        {
            get { return Mag.OrderOfMagnitudeId[OrderOfMagnitude] + "s"; }
        }

        // Operatoren

        public static bool operator ==(TimeInSec<TOrderOfMagnitude> a, TimeInSec<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(TimeInSec<TOrderOfMagnitude> a, TimeInSec<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static TimeInSec<TOrderOfMagnitude> operator +(TimeInSec<TOrderOfMagnitude> a, TimeInSec<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static TimeInSec<TOrderOfMagnitude> operator -(TimeInSec<TOrderOfMagnitude> a, TimeInSec<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }

        public static TimeInSec<TOrderOfMagnitude> operator *(double a, TimeInSec<TOrderOfMagnitude> b)
        {
            return SCALE(a, b);
        }

        public static TimeInSec<TOrderOfMagnitude> operator *(TimeInSec<TOrderOfMagnitude> a, double b)
        {
            return SCALE(b, a);
        }
    }
}
