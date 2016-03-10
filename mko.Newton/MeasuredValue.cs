//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart 2011,2012
//
//  Projekt.......: mko.Newton
//  Name..........: MeasuredValue.cs
//  Aufgabe/Fkt...: Basisklasse für alle Messwerte. 
//                  Die Messwerte werden durch Objekte einer streng typisierten Klassenbibliothek dargestellt.
//                  Für jede Art von Messwert (z.B. Abstand oder Energie) muss eine Klasse abgeleitet werden. 
//                  Von dieser Klasse wiederum muss für jede Einheit eine Klasse abgeleitet werden. 
//                  So wird z.B. eine Abstandsmessung in Metern durch 
//                  die Klasse DistanceInMeter : Distance : MeasuredValue abgeleitet.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mag = mko.Newton.OrderOfMagnitude;
using System.Diagnostics;

namespace mko.Newton
{
    public abstract class MeasuredValue : SIBaseType
    {
        protected abstract void InitValue();

        public abstract MeasuredValue Create(double Value);

        public abstract MeasuredValue Create(MeasuredValue Value);

        public MeasuredValue()
        {
            InitValue();
        }

        public MeasuredValue(double Value)
        {
            InitValue();
            this.Value = Value;
        }

        public MeasuredValue(MeasuredValue mValue)
        {
            InitValue();
            if (ToBaseUnitConversionFactor == mValue.ToBaseUnitConversionFactor)
            {
                // Fall: Einheiten gleich
                if (OrderOfMagnitude == mValue.OrderOfMagnitude)
                    // Fall: Größenordnungen gleich
                    Value = mValue.Value;
                else
                    // Fall: Größenordnungen verschieden
                    Value = Mag.FromTo(mValue.Value, mValue.OrderOfMagnitude, OrderOfMagnitude);
            }
            else
            {
                // Fall: Einheiten verschieden                
                Value = Mag.FromTo(mValue.ValueInBaseUnit * (1 / ToBaseUnitConversionFactor), Mag.OrderOfMagnitudeEnum.One, OrderOfMagnitude);
            }
        }



        public double Value
        {
            get;
            set;
        }

        public double ValueInOrderOfMagnitudeOne
        {
            get
            {
                return Mag.ToOne(Value, OrderOfMagnitude);
            }
        }

        /// <summary>
        /// Implementiert in den abgeleiteten Klassen, die für bestimmte physikalische Einheiten
        /// stehen, die Umrechnung in die SI- Basiseinheit
        /// </summary>
        /// <returns></returns>
        public double ValueInBaseUnit
        {
            get
            {
                return ValueInOrderOfMagnitudeOne * ToBaseUnitConversionFactor; 
            }
        }

        public override string ToString()
        {
            return string.Format("{0:N} {1}", Value, UnitSymbol);            
        }

        // Operatoren

        public static bool EQUAL<TMeasuredValue>(TMeasuredValue a, TMeasuredValue b)
            where TMeasuredValue : MeasuredValue
        {
            ASSERT_ARGUMETNS_OF_SAME_TYPE(a, b);
            return a.Value == b.Value;
        }

        public static bool VALUE_EQUAL<TValueA, TValueB, TBaseType>(TValueA a, TValueB b)
            where TBaseType : MeasuredValue
            where TValueA : TBaseType
            where TValueB : TBaseType
        {            
            return a.ValueInBaseUnit == b.ValueInBaseUnit;
        }

        public static TValue ADD<TValue>(TValue a, TValue b)
            where TValue : MeasuredValue
        {
            ASSERT_ARGUMETNS_OF_SAME_TYPE(a, b);
            return (TValue)a.Create(a.Value + b.Value);
        }

        public static TValue SUB<TValue>(TValue a, TValue b)
           where TValue : MeasuredValue
        {
            ASSERT_ARGUMETNS_OF_SAME_TYPE(a, b);
            return (TValue)a.Create(a.Value - b.Value);
        }

        public static TValue SCALE<TValue>(double fact, TValue a)
           where TValue : MeasuredValue
        {            
            return (TValue)a.Create(fact * a.Value);
        }

    }

}
