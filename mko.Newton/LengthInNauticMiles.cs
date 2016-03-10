using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class LengthInNauticalMiles : Length
    {

        public LengthInNauticalMiles(int Dimension) : base(Dimension) { }
        public LengthInNauticalMiles(params double[] coordinates) : base(coordinates) { }
        public LengthInNauticalMiles(Length mVector) : base(mVector) { }

        public override MeasuredVector Create(params double[] coordinates)
        {
            return new LengthInNauticalMiles(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new LengthInNauticalMiles((Length)Value);
        }

        public override double ToBaseUnitConversionFactor
        {
            get { return 1852.0; }
        }

        public override string UnitSymbol
        {
            get { return "nmi"; }
        }

        // Operatoren

        public static bool operator ==(LengthInNauticalMiles a, LengthInNauticalMiles b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(LengthInNauticalMiles a, LengthInNauticalMiles b)
        {
            return !EQUAL(a, b);
        }

        public static LengthInNauticalMiles operator *(LengthInNauticalMiles a, double factor)
        {
            return SCALE(factor, a);
        }

        public static LengthInNauticalMiles operator *(double factor, LengthInNauticalMiles a)
        {
            return SCALE(factor, a);
        }

        public static LengthInNauticalMiles operator +(LengthInNauticalMiles a, LengthInNauticalMiles b)
        {
            return ADD(a, b);
        }

        public static LengthInNauticalMiles operator -(LengthInNauticalMiles a, LengthInNauticalMiles b)
        {
            return SUB(a, b);
        }

    }
}
