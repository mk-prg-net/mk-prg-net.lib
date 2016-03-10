using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public class VelocityInMeterPerSec<TOrderOfMagnitude> : Velocity
        where TOrderOfMagnitude : OrderOfMagnitudeBase, new()
    {
        // Konstruktionsprozess

        protected override void CreateVector(int Dimension)
        {
            _s = new LengthInMeter<TOrderOfMagnitude>(Dimension);
        }

        protected override void CreateVector(params double[] Coordinates)
        {
            _s = new LengthInMeter<TOrderOfMagnitude>(Coordinates);
        }

        public VelocityInMeterPerSec(int Dimension) : base(Dimension) { }
        public VelocityInMeterPerSec(params double[] coordinates) : base(coordinates) { }
        public VelocityInMeterPerSec(E.Vector V) : base(V.coordinates) { }
        public VelocityInMeterPerSec(Length S, Time t) : base(S, t) {}
        
        // Konvertierungskonstruktor
        public VelocityInMeterPerSec(Velocity V) : base(V) {}
        

        //public override Velocity Create(Length S, Time T)
        //{
        //    return new VelocityInMeterPerSec<TOrderOfMagnitude>(new LengthInMeter<TOrderOfMagnitude>(S), Time.Sec(T));
        //}

        public override MeasuredVector Create(params double[] coordinates)
        {
            return new VelocityInMeterPerSec<TOrderOfMagnitude>(coordinates);
        }

        public override MeasuredVector Create(MeasuredVector Value)
        {
            return new VelocityInMeterPerSec<TOrderOfMagnitude>((Velocity)Value);
        }


        // Zugriff auf die Komponenten einer Geschwindigkeit
        /// <summary>
        /// Zurückgelegter Weg pro Zeiteinheit
        /// </summary>
        public override Length S
        {
            get { return _s; }
        }
        LengthInMeter<TOrderOfMagnitude> _s;

        public override Length ConvertInS(Length S)
        {
            return new LengthInMeter<TOrderOfMagnitude>(S);
        }

        /// <summary>
        /// Zeiteinheit
        /// </summary>
        public override Time T
        {
            get { return Time.Sec(1.0); }
        }

        public override Time ConvertInT(Time t)
        {
            return Time.Sec(t);
        }


        ///// <summary>
        ///// Geschwindigkeitsvektor
        ///// </summary>
        //public override Euklid.Vector Vector
        //{
        //    get
        //    {                
        //        return S.Vector;
        //    }
        //    set
        //    {
        //        S.Vector = value;
        //    }
        //}

        // Umrechnung in Basieinheiten

        public override double ToBaseUnitConversionFactor
        {
            get { return 1.0; }
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

        public static bool operator ==(VelocityInMeterPerSec<TOrderOfMagnitude> a, VelocityInMeterPerSec<TOrderOfMagnitude> b)
        {
            return EQUAL(a, b);
        }

        public static bool operator !=(VelocityInMeterPerSec<TOrderOfMagnitude> a, VelocityInMeterPerSec<TOrderOfMagnitude> b)
        {
            return !EQUAL(a, b);
        }


        public static VelocityInMeterPerSec<TOrderOfMagnitude> operator *(VelocityInMeterPerSec<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(factor, a);
        }

        public static VelocityInMeterPerSec<TOrderOfMagnitude> operator *(double factor, VelocityInMeterPerSec<TOrderOfMagnitude> a)
        {
            return SCALE(factor, a);

        }

        public static VelocityInMeterPerSec<TOrderOfMagnitude> operator /(VelocityInMeterPerSec<TOrderOfMagnitude> a, double factor)
        {
            return SCALE(1 / factor, a);
        }


        public static VelocityInMeterPerSec<TOrderOfMagnitude> operator +(VelocityInMeterPerSec<TOrderOfMagnitude> a, VelocityInMeterPerSec<TOrderOfMagnitude> b)
        {
            return ADD(a, b);
        }

        public static VelocityInMeterPerSec<TOrderOfMagnitude> operator -(VelocityInMeterPerSec<TOrderOfMagnitude> a, VelocityInMeterPerSec<TOrderOfMagnitude> b)
        {
            return SUB(a, b);
        }




    }
}
