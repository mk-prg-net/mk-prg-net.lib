using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class LengthInInch : Length
    {
        public override MeasuredVector Create(params double[] coordinates)
        {
            return new LengthInInch(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector mVector)
        {
            return new LengthInInch((Length)mVector);
        }


        public LengthInInch(int Dimension) : base(Dimension) { }
        public LengthInInch(params double[] coordinates) : base(coordinates) { } 

        /// <summary>
        /// Konstruktoren für die Konvertierung
        /// </summary>
        /// <param name="Value"></param>
        public LengthInInch(Length Value) : base(Value) {}

        public override double ToBaseUnitConversionFactor
        {
            get
            {
                return 0.0254;
            }
        }

        public override string UnitSymbol
        {
            get { return "in"; }
        }

        // Operatoren

        public static bool operator ==(LengthInInch a, LengthInInch b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(LengthInInch a, LengthInInch b)
        {
            return !EQUAL(a, b);
        }

        public static LengthInInch operator *(LengthInInch a, double factor)
        {
            return SCALE(factor, a);
        }

        public static LengthInInch operator *(double factor, LengthInInch a)
        {
            return SCALE(factor, a);
        }

        public static LengthInInch operator +(LengthInInch a, LengthInInch b)
        {
            return ADD(a, b);
        }

        public static LengthInInch operator -(LengthInInch a, LengthInInch b)
        {
            return SUB(a, b);
        }


    }
}
