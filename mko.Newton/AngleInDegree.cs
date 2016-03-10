using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class AngleInDegree : Angle
    {
        public override MeasuredVector Create(params double[] coordinates)
        {
            return new AngleInDegree(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new AngleInDegree((Angle)Value);
        }

        public AngleInDegree() { }
        public AngleInDegree(params double[] coordinates) : base(coordinates) { }
        public AngleInDegree(Angle mVector) : base(mVector) { }

        public override double ToBaseUnitConversionFactor
        {
            get
            {
                return Math.PI/180.0;
            }
        }

        public override string UnitSymbol
        {
            get { return "deg"; }
        }


        // Operatoren

        public static bool operator ==(AngleInDegree a, AngleInDegree b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(AngleInDegree a, AngleInDegree b)
        {
            return !EQUAL(a, b);
        }


        public static AngleInDegree operator *(AngleInDegree a, double factor)
        {
            return SCALE(factor, a);
        }

        public static AngleInDegree operator *(double factor, AngleInDegree a)
        {
            return SCALE(factor, a);
        }

        public static AngleInDegree operator /(AngleInDegree a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static AngleInDegree operator +(AngleInDegree a, AngleInDegree b)
        {
            return ADD(a, b);
        }

        public static AngleInDegree operator -(AngleInDegree a, AngleInDegree b)
        {
            return SUB(a, b);
        }


    }
}
