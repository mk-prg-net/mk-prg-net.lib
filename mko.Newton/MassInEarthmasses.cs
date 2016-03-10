using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Newton
{
    public class MassInEarthmasses : Mass
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public MassInEarthmasses() { }
        public MassInEarthmasses(double Value) : base(Value){ }
        public MassInEarthmasses(Mass Value) : base(Value) { }

        public override double ToBaseUnitConversionFactor
        {
            get { return Mass.Gram(Mass.MassOfEarth).Value; }
        }

        public override MeasuredValue Create(double Value)
        {
            return new MassInEarthmasses(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new MassInEarthmasses((Mass)Value);
        }

        public override string UnitSymbol
        {
            get { return "xEarthMasses"; }
        }

        public static bool operator ==(MassInEarthmasses a, MassInEarthmasses b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(MassInEarthmasses a, MassInEarthmasses b)
        {
            return !EQUAL(a, b);
        }


        public static MassInEarthmasses operator *(MassInEarthmasses a, double factor)
        {
            return SCALE(factor, a);
        }

        public static MassInEarthmasses operator *(double factor, MassInEarthmasses a)
        {
            return SCALE(factor, a);

        }

        public static MassInEarthmasses operator /(MassInEarthmasses a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static MassInEarthmasses operator +(MassInEarthmasses a, MassInEarthmasses b)
        {
            return ADD(a, b);
        }

        public static MassInEarthmasses operator -(MassInEarthmasses a, MassInEarthmasses b)
        {
            return SUB(a, b);
        }



    }
}
