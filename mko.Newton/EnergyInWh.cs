using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class EnergyInWh<TOrderOfMagnitude> : Energy
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public EnergyInWh(double Value) : base(Value) { }
        public EnergyInWh(Energy E) : base(E) { }

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
                return 3600.0;
            }
        }

        public override MeasuredValue Create(double Value)
        {
            return new EnergyInWh<TOrderOfMagnitude>(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new EnergyInWh<TOrderOfMagnitude>((Energy) Value);
        }

        public override string UnitSymbol
        {
            get { return OofMServer.OrderOfMagnitudeId + "Wh"; }
        }

        // Operator

        public static bool operator ==(EnergyInWh<TOrderOfMagnitude> a, EnergyInWh<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(EnergyInWh<TOrderOfMagnitude> a, EnergyInWh<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static EnergyInWh<TOrderOfMagnitude> operator *(EnergyInWh<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static EnergyInWh<TOrderOfMagnitude> operator *(double factor, EnergyInWh<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static EnergyInWh<TOrderOfMagnitude> operator /(EnergyInWh<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static EnergyInWh<TOrderOfMagnitude> operator +(EnergyInWh<TOrderOfMagnitude> a, EnergyInWh<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static EnergyInWh<TOrderOfMagnitude> operator -(EnergyInWh<TOrderOfMagnitude> a, EnergyInWh<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }


    }
}
