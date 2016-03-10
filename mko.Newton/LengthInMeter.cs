using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class LengthInMeter<TOrderOfMagnitude> : Length
        where TOrderOfMagnitude : mko.Newton.OrderOfMagnitudeBase, new()
    {
        public override MeasuredVector Create(params double[] coordinates)
        {
            return new LengthInMeter<TOrderOfMagnitude>(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector mVector)
        {
            return new LengthInMeter<TOrderOfMagnitude>((Length)mVector);
        }

        public LengthInMeter(int Dimension) : base(Dimension) { }
        public LengthInMeter(params double[] coordinates) : base(coordinates) { }

        /// <summary>
        /// Konvertierungs- Konstruktor
        /// </summary>
        /// <param name="mVector"></param>
        public LengthInMeter(Length mVector) : base(mVector) { }

        public override double ToBaseUnitConversionFactor
        {
            get
            {
                return 1.0;
            }
        }


        public override string UnitSymbol
        {
            get { return Mag.OrderOfMagnitudeId[OrderOfMagnitude] + "m"; }
        }

        TOrderOfMagnitude OofMServer
        {
            get
            {
                return (TOrderOfMagnitude) OrderOfMagnitudeBase.Instance[typeof(TOrderOfMagnitude)];
            }
        }

        public override Mag.OrderOfMagnitudeEnum OrderOfMagnitude
        {
            get { return OofMServer.OrderOfMagnitude; }
        }

        // Operatoren

        public static bool operator ==(LengthInMeter<TOrderOfMagnitude> a, LengthInMeter<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(LengthInMeter<TOrderOfMagnitude> a, LengthInMeter<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }

        public static LengthInMeter<TOrderOfMagnitude> operator *(LengthInMeter<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static LengthInMeter<TOrderOfMagnitude> operator *(double factor, LengthInMeter<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);
        }

        public static LengthInMeter<TOrderOfMagnitude> operator +(LengthInMeter<TOrderOfMagnitude> a, LengthInMeter<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static LengthInMeter<TOrderOfMagnitude> operator -(LengthInMeter<TOrderOfMagnitude> a, LengthInMeter<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }



    }
}
