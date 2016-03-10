using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class LengthInLightYear : Length
    {
        public override MeasuredVector Create(params double[] coordinates)
        {
            return new LengthInPoint(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector mVector)
        {
            return new LengthInLightYear((Length)mVector);
        }

        public LengthInLightYear(int Dimension) : base(Dimension) { }
        public LengthInLightYear(params double[] coordinates) : base(coordinates) { }

        public LengthInLightYear(Length Value) : base(Value) { }

        public override double ToBaseUnitConversionFactor
        {
            get
            {
                return 9.4607304725808e15;
            }
        }

        public override string UnitSymbol
        {
            get { return "ly"; }
        }

        // Operatoren

        public static bool operator ==(LengthInLightYear a, LengthInLightYear b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(LengthInLightYear a, LengthInLightYear b)
        {
            return !EQUAL(a, b);
        }

        public static LengthInLightYear operator *(LengthInLightYear a, double factor)
        {
            return SCALE(factor, a);
        }

        public static LengthInLightYear operator *(double factor, LengthInLightYear a)
        {
            return SCALE(factor, a);
        }

        public static LengthInLightYear operator +(LengthInLightYear a, LengthInLightYear b)
        {
            return ADD(a, b);
        }

        public static LengthInLightYear operator -(LengthInLightYear a, LengthInLightYear b)
        {
            return SUB(a, b);
        }


    }
}
