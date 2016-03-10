using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class EnergyInJoule<TOrderOfMagnitude> : Energy
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {

        protected override void InitValue()
        {
            Value = 0.0;
        }

        public EnergyInJoule(double Value) : base(Value) { }
        public EnergyInJoule(Energy E) : base(E) { }

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


        public override MeasuredValue Create(double Value)
        {
            return new EnergyInJoule<TOrderOfMagnitude>(Value); 
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new EnergyInJoule<TOrderOfMagnitude>((Energy)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 1.0; }
        }

        public override string UnitSymbol
        {
            get { return OofMServer.OrderOfMagnitudeId + "J"; }
        }

        // Operator

        public static bool operator ==(EnergyInJoule<TOrderOfMagnitude> a, EnergyInJoule<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(EnergyInJoule<TOrderOfMagnitude> a, EnergyInJoule<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static EnergyInJoule<TOrderOfMagnitude> operator *(EnergyInJoule<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static EnergyInJoule<TOrderOfMagnitude> operator *(double factor, EnergyInJoule<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static EnergyInJoule<TOrderOfMagnitude> operator /(EnergyInJoule<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static EnergyInJoule<TOrderOfMagnitude> operator +(EnergyInJoule<TOrderOfMagnitude> a, EnergyInJoule<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static EnergyInJoule<TOrderOfMagnitude> operator -(EnergyInJoule<TOrderOfMagnitude> a, EnergyInJoule<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }


    }
}
