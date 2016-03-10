using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class PowerInWatt<TOrderOfMagnitude> : Power
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public PowerInWatt(double Value) : base(Value) { }
        public PowerInWatt(Power P) : base(P) { }        


        public override MeasuredValue Create(double Value)
        {
            return new PowerInWatt<TOrderOfMagnitude>(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new PowerInWatt<TOrderOfMagnitude>((Power)Value);
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


        public override double ToBaseUnitConversionFactor
        {
            get { return 1.0; }
        }

        public override string UnitSymbol
        {
            get { return OofMServer.OrderOfMagnitudeId + "W"; }
        }

        // Operator

        public static bool operator ==(PowerInWatt<TOrderOfMagnitude> a, PowerInWatt<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(PowerInWatt<TOrderOfMagnitude> a, PowerInWatt<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static PowerInWatt<TOrderOfMagnitude> operator *(PowerInWatt<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static PowerInWatt<TOrderOfMagnitude> operator *(double factor, PowerInWatt<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static PowerInWatt<TOrderOfMagnitude> operator /(PowerInWatt<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static PowerInWatt<TOrderOfMagnitude> operator +(PowerInWatt<TOrderOfMagnitude> a, PowerInWatt<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static PowerInWatt<TOrderOfMagnitude> operator -(PowerInWatt<TOrderOfMagnitude> a, PowerInWatt<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }


    }
}
