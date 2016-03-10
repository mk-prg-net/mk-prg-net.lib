using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;


namespace mko.Newton
{
    public class LengthInAU : Length
    {
        public override MeasuredVector Create(params double[] coordinates)
        {
            return new LengthInAU(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector mVector)
        {
            return new LengthInAU((Length)mVector);
        }


        public LengthInAU(int Dimension) : base(Dimension) { }
        public LengthInAU(params double[] coordinates) : base(coordinates) { }

        public LengthInAU(Length Value) : base(Value) { }

        public override double ToBaseUnitConversionFactor
        {
            get
            {
                return 1.495978707e11;
            }
        }

        public override string UnitSymbol
        {
            get { return "AU"; }
        }

        // Operatoren

        public static bool operator ==(LengthInAU a, LengthInAU b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(LengthInAU a, LengthInAU b)
        {
            return !EQUAL(a, b);
        }

        public static LengthInAU operator *(LengthInAU a, double factor)
        {
            return SCALE(factor, a);
        }

        public static LengthInAU operator *(double factor, LengthInAU a)
        {
            return SCALE(factor, a);
        }

        public static LengthInAU operator +(LengthInAU a, LengthInAU b)
        {
            return ADD(a, b);
        }

        public static LengthInAU operator -(LengthInAU a, LengthInAU b)
        {
            return SUB(a, b);
        }

    }    
}
