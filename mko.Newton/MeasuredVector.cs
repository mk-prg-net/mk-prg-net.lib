using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;

using System.Diagnostics;

namespace mko.Newton
{
    public abstract class MeasuredVector : SIBaseType
    {
        public class MeasuredVectorException : Exception
        {
            public MeasuredVectorException(string msg) : base(msg) { }
            public MeasuredVectorException(string msg, Exception InnerException) : base(msg, InnerException) { }
        }


        public abstract MeasuredVector Create(params double[] coordinates);

        public abstract MeasuredVector Create(MeasuredVector Value);

        protected virtual void CreateVector(int Dimension)
        {
            _vector = new E.Vector(Dimension);
        }

        protected virtual void CreateVector(params double[] Coordinates)
        {
            _vector = new E.Vector(Coordinates);
        }

        public MeasuredVector() {}

        public MeasuredVector(int Dimension)
        {
            CreateVector(Dimension);
        }

        public MeasuredVector(params double[] Coordinates)
        {
            CreateVector(Coordinates);
        }

        /// <summary>
        /// Konvertierungskonstruktor: Erzeugt aus den Daten einer Klasse, die zur gleichen physikalischen Größe gehört wie z.B. Länge
        /// eine neue Instanz dieser Klasse.
        /// Hinweis: In den Ableitungen dieser abstrakten Basisklasse werden z.B. ToBaseUnitConversionFactor und OrderOfMagnitude
        /// festgelegt. Z.B. gibt es eine Klasse für Centimeter und eine zweite für Kilometer. 
        /// </summary>
        /// <param name="mVector"></param>
        public MeasuredVector(MeasuredVector mVector)
        {
            try
            {
                if (ToBaseUnitConversionFactor == mVector.ToBaseUnitConversionFactor)
                {
                    // Fall: Einheiten gleich
                    if (OrderOfMagnitude == mVector.OrderOfMagnitude)
                        // Fall: Größenordnungen gleich
                        CreateVector(mVector.Vector.coordinates);
                    else
                        // Fall: Größenordnungen verschieden
                        CreateVector(Mag.FromTo(mVector.Vector, mVector.OrderOfMagnitude, OrderOfMagnitude).coordinates);
                }
                else
                {
                    // Fall: Einheiten verschieden                
                    CreateVector(Mag.FromTo(mVector.VectorInBaseUnit * (1 / ToBaseUnitConversionFactor), Mag.OrderOfMagnitudeEnum.One, OrderOfMagnitude).coordinates);
                }
            }
            catch (Exception ex)
            {
                throw new MeasuredVectorException("Konstruktor MeasuredVector(MeasuredVector mVector)", ex);
            }
        }


        public int Dimension
        {
            get
            {
                return Vector.Dimensions;
            }
        }

        public virtual E.Vector Vector
        {
            get
            {
                Debug.Assert(!Object.ReferenceEquals(_vector,null));
                return _vector;
            }
            set
            {
                _vector = value;
            }
        }
        E.Vector _vector;

        public virtual E.Vector VectorInOrderOfMagnitudeOne
        {
            get
            {
                return Mag.ToOne(Vector, OrderOfMagnitude);
            }
        }


        public virtual E.Vector VectorInBaseUnit
        {
            get
            {
                return VectorInOrderOfMagnitudeOne * ToBaseUnitConversionFactor;
            }
        }

        public override string ToString()
        {
            try
            {
                if (Dimension == 1)
                    return Vector[0].ToString() + " " + UnitSymbol;
                else
                    return Vector.ToString() + " " + UnitSymbol;
            }
            catch (Exception ex)
            {
                throw new MeasuredVectorException("ToString()", ex);
            }
        }

        public virtual string ToString(string FormatString)
        {
            try
            {

                if (Dimension == 1)
                    return Vector[0].ToString(FormatString) + " " + UnitSymbol;
                else
                    return Vector.ToString(FormatString) + " " + UnitSymbol;
            }
            catch (Exception ex)
            {
                throw new MeasuredVectorException("TorString(" + FormatString + ")", ex);
            }
        }

        public virtual string ToString(string FormatString, System.Globalization.NumberFormatInfo nif)
        {
            try
            {

                if (Dimension == 1)
                    return Vector[0].ToString(FormatString, nif) + " " + UnitSymbol;
                else
                    return Vector.ToString(FormatString) + " " + UnitSymbol;
            }
            catch (Exception ex)
            {
                throw new MeasuredVectorException("TorString(" + FormatString + ")", ex);
            }
        }

        //------------------------------------------------------------------------------------------------
        // Generische Operatoren, die zur Implementierung von Operatoren in abgeleiteten Klassen diesen

        /// <summary>
        /// Prüft auf gleichheit bezüglich Typ und Wert. Z.B. sind bezüglich EQUAL zwei Längenmaße
        /// gleich, wenn sie den gleichen Wert in der gleichen Einheit mit der gleichen Größenordnung 
        /// angeben (z.B. 1 dm).
        /// </summary>
        /// <typeparam name="TVector"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EQUAL<TVector>(TVector a, TVector b)
            where TVector : MeasuredVector
        {
            // Der Typvergleich zur Laufzeit ist notwendig, da TVector vom Typ einer Basisklasse
            // (z.B. Length) sein kann.             
            return a.GetType() == b.GetType() && a.Vector == b.Vector;
        }

        /// <summary>
        /// Prüft auf gleichheit bezüglich Wert. Z.B. sind das Längenmaß 100cm und 1m nach 
        /// VALUE_EQUAL gleich
        /// </summary>
        /// <typeparam name="TVector"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool VALUE_EQUAL<TVectorA, TVectorB, TBaseType>(TVectorA a, TVectorB b)
            where TBaseType : MeasuredVector
            where TVectorA : TBaseType
            where TVectorB : TBaseType
        {
            return a.VectorInBaseUnit == b.VectorInBaseUnit;
        }

        /// <summary>
        /// Addition von zwei Vektoren gleichen Typs. 
        /// </summary>
        /// <typeparam name="TVector"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>a+b</returns>
        public static TVector ADD<TVector>(TVector a, TVector b)
            where TVector : MeasuredVector
        {
            // Der Typvergleich zur Laufzeit ist notwendig, da TVector vom Typ einer Basisklasse
            // (z.B. Length) sein kann.
            ASSERT_ARGUMETNS_OF_SAME_TYPE(a, b);
            return (TVector)a.Create((a.Vector + b.Vector).coordinates);
        }


        /// <summary>
        /// Subtraktion zweier Vektoren gleichen Typs. 
        /// </summary>
        /// <typeparam name="TVector"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static TVector SUB<TVector>(TVector a, TVector b)
            where TVector : MeasuredVector
        {
            ASSERT_ARGUMETNS_OF_SAME_TYPE(a, b);
            return (TVector)a.Create((a.Vector - b.Vector).coordinates);
        }

        /// <summary>
        /// Skalierung eines Vektors
        /// </summary>
        /// <typeparam name="TVector"></typeparam>
        /// <param name="factor"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static TVector SCALE<TVector>(double factor, TVector a)
            where TVector : MeasuredVector
        {
            return (TVector)a.Create((a.Vector * factor).coordinates);
        }

    }
}
