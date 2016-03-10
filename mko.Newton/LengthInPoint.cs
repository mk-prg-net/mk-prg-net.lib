using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class LengthInPoint : Length
    {
        public override MeasuredVector Create(params double[] coordinates)
        {
            return new LengthInPoint(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector mVector)
        {
            return new LengthInPoint((Length)mVector);
        }


        public LengthInPoint(int Dimension) : base(Dimension) { }
        public LengthInPoint(params double[] coordinates) : base(coordinates) { }

        /// <summary>
        /// Konstruktor für die Konvertierung
        /// </summary>
        /// <param name="Value"></param>
        public LengthInPoint(Length Value) : base(Value) {}


        public override Mag.OrderOfMagnitudeEnum OrderOfMagnitude
        {
            get { return Mag.OrderOfMagnitudeEnum.One; }
        }

        public override string UnitSymbol
        {
            get { return "pt"; }
        }        

        public override double ToBaseUnitConversionFactor
        {
            get
            {
                return 0.000353;
            }
        }

        // Operatoren

        public static bool operator ==(LengthInPoint a, LengthInPoint b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(LengthInPoint a, LengthInPoint b)
        {
            return !EQUAL(a, b);
        }

        public static LengthInPoint operator *(LengthInPoint a, double factor)
        {
            return SCALE(factor, a);
        }

        public static LengthInPoint operator *(double factor, LengthInPoint a)
        {
            return SCALE(factor, a);
        }

        public static LengthInPoint operator +(LengthInPoint a, LengthInPoint b)
        {
            return ADD(a, b);
        }

        public static LengthInPoint operator -(LengthInPoint a, LengthInPoint b)
        {
            return SUB(a, b);
        }


    }
}
