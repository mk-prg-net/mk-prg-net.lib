using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class EnergyInNm<TOrderOfMagnitude> : Energy
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public EnergyInNm(double Value) : base(Value) { }
        public EnergyInNm(Force F, Length s) : base((new ForceInNewton<TOrderOfMagnitude>(F)).Vector * Length.Meter(s).Vector) { }
        public EnergyInNm(Energy E) : base(E) { }        

        public override string UnitSymbol
        {
            get { return OofMServer.OrderOfMagnitudeId + "Nm"; }
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
            get
            {
                // 1 J == 1 Nm
                return 1.0;
            }
        }


        public override MeasuredValue Create(double Value)
        {
            return new EnergyInNm<TOrderOfMagnitude>(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new EnergyInNm<TOrderOfMagnitude>((Energy)Value);
        }

        // Operator

        public static bool operator ==(EnergyInNm<TOrderOfMagnitude> a, EnergyInNm<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(EnergyInNm<TOrderOfMagnitude> a, EnergyInNm<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static EnergyInNm<TOrderOfMagnitude> operator *(EnergyInNm<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static EnergyInNm<TOrderOfMagnitude> operator *(double factor, EnergyInNm<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static EnergyInNm<TOrderOfMagnitude> operator /(EnergyInNm<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }

        public static EnergyInNm<TOrderOfMagnitude> operator +(EnergyInNm<TOrderOfMagnitude> a, EnergyInNm<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static EnergyInNm<TOrderOfMagnitude> operator -(EnergyInNm<TOrderOfMagnitude> a, EnergyInNm<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }



    }
}
