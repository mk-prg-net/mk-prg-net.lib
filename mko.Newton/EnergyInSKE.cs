using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class EnergyInSKE : Energy
    {
        protected override void InitValue()
        {
            Value = 0;
        }

        public EnergyInSKE(double Value) : base(Value) { }
        public EnergyInSKE(Energy Value) : base(Value) { }

        public override MeasuredValue Create(double Value)
        {
            return new EnergyInSKE(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new EnergyInSKE((Energy)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 20.3076e6; }
        }

        public override string UnitSymbol
        {
            get { return "SKE"; }
        }

        // Operatoren

        public static bool operator ==(EnergyInSKE a, EnergyInSKE b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(EnergyInSKE a, EnergyInSKE b)
        {
            return !EQUAL(a, b);
        }


        public static EnergyInSKE operator *(EnergyInSKE a, double factor)
        {
            return SCALE(factor, a);
        }

        public static EnergyInSKE operator *(double factor, EnergyInSKE a)
        {
            return SCALE(factor, a);

        }

        public static EnergyInSKE operator /(EnergyInSKE a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static EnergyInSKE operator +(EnergyInSKE a, EnergyInSKE b)
        {
            return ADD(a, b);
        }

        public static EnergyInSKE operator -(EnergyInSKE a, EnergyInSKE b)
        {
            return SUB(a, b);
        }

    }
}
