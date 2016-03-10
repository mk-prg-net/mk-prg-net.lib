using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class LengthInMiles : Length
    {
        public LengthInMiles(int Dimension) : base(Dimension) { }
        public LengthInMiles(params double[] coordinates) : base(coordinates) { }
        public LengthInMiles(Length mVector) : base(mVector) { }

        public override MeasuredVector Create(params double[] coordinates)
        {
            return new LengthInMiles(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new LengthInMiles((Length)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 1609.344; }
        }

        public override string UnitSymbol
        {
            get { return "mi"; }
        }

        // Operatoren

        public static bool operator ==(LengthInMiles a, LengthInMiles b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(LengthInMiles a, LengthInMiles b)
        {
            return !EQUAL(a, b);
        }

        public static LengthInMiles operator *(LengthInMiles a, double factor)
        {
            return SCALE(factor, a);
        }

        public static LengthInMiles operator *(double factor, LengthInMiles a)
        {
            return SCALE(factor, a);
        }

        public static LengthInMiles operator +(LengthInMiles a, LengthInMiles b)
        {
            return ADD(a, b);
        }

        public static LengthInMiles operator -(LengthInMiles a, LengthInMiles b)
        {
            return SUB(a, b);
        }

    }
}
