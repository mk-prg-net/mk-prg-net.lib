using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class EnergyInWs<TOrderOfMagnitude> : Energy
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public EnergyInWs(double Value) : base(Value) { }
        public EnergyInWs(Energy E) : base(E) { }

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
            get
            {
                // 1 J = 1 Nm = 1 Ws
                return 1.0;
            }
        }

        public override MeasuredValue Create(double Value)
        {
            return new EnergyInWh<TOrderOfMagnitude>(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new EnergyInWs<TOrderOfMagnitude>((Energy)Value);
        }

        public override string UnitSymbol
        {
            get { return OofMServer.OrderOfMagnitudeId + "Ws"; }
        }

        // Operator

        public static bool operator ==(EnergyInWs<TOrderOfMagnitude> a, EnergyInWs<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(EnergyInWs<TOrderOfMagnitude> a, EnergyInWs<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static EnergyInWs<TOrderOfMagnitude> operator *(EnergyInWs<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static EnergyInWs<TOrderOfMagnitude> operator *(double factor, EnergyInWs<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static EnergyInWs<TOrderOfMagnitude> operator /(EnergyInWs<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static EnergyInWs<TOrderOfMagnitude> operator +(EnergyInWs<TOrderOfMagnitude> a, EnergyInWs<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static EnergyInWs<TOrderOfMagnitude> operator -(EnergyInWs<TOrderOfMagnitude> a, EnergyInWs<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }


    }
}
