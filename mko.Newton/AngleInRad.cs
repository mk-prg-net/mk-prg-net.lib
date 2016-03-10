using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    public class AngleInRad : Angle
    {
        public override MeasuredVector Create(params double[] coordinates)
        {
            return new AngleInRad(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new AngleInRad((Angle)Value);
        }

        public AngleInRad() { }
        public AngleInRad(params double[] coordinates) : base(coordinates) { }
        public AngleInRad(Angle mVector) : base(mVector) { }

        public override double ToBaseUnitConversionFactor
        {
            get
            {
                return 1.0;
            }
        }

        public override string UnitSymbol
        {
            get { return "rad"; }
        }


        // Operatoren

        public static bool operator ==(AngleInRad a, AngleInRad b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(AngleInRad a, AngleInRad b)
        {
            return !EQUAL(a, b);
        }


        public static AngleInRad operator *(AngleInRad a, double factor)
        {
            return SCALE(factor, a);
        }

        public static AngleInRad operator *(double factor, AngleInRad a)
        {
            return SCALE(factor, a);
        }

        public static AngleInRad operator /(AngleInRad a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static AngleInRad operator +(AngleInRad a, AngleInRad b)
        {
            return ADD(a, b);
        }

        public static AngleInRad operator -(AngleInRad a, AngleInRad b)
        {
            return SUB(a, b);
        }


    }
}
