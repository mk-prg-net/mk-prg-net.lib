using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag= mko.Newton.OrderOfMagnitude;


namespace mko.Newton
{
    public class MassInGram<TOrderOfMagnitude> : Mass
        where TOrderOfMagnitude : mko.Newton.OrderOfMagnitudeBase, new()
    {
        protected override void InitValue()
        {
            Value = 0.0;
        }

        public override MeasuredValue Create(double Value)
        {
            return new MassInGram<TOrderOfMagnitude>(Value);
        }

        public override MeasuredValue Create(MeasuredValue Value)
        {
            return new MassInGram<TOrderOfMagnitude>((Mass)Value);
        }

        public MassInGram() { }
        public MassInGram(double Value) : base(Value) { }
        public MassInGram(Mass Value) : base(Value) { }


        public override double ToBaseUnitConversionFactor
        {
            get
            {
                return 1.0;
            }
        }

        public override string UnitSymbol
        {
            get { return Mag.OrderOfMagnitudeId[OrderOfMagnitude] + "g"; }
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

        // Operator

        public static bool operator ==(MassInGram<TOrderOfMagnitude> a, MassInGram<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(MassInGram<TOrderOfMagnitude> a, MassInGram<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static MassInGram<TOrderOfMagnitude> operator *(MassInGram<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static MassInGram<TOrderOfMagnitude> operator *(double factor, MassInGram<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static MassInGram<TOrderOfMagnitude> operator /(MassInGram<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static MassInGram<TOrderOfMagnitude> operator +(MassInGram<TOrderOfMagnitude> a, MassInGram<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static MassInGram<TOrderOfMagnitude> operator -(MassInGram<TOrderOfMagnitude> a, MassInGram<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }



    }
}
