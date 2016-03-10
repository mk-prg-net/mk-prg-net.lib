//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.5.2012
//
//  Projekt.......: mko.Newton
//  Name..........: LispMeasuredValuesArray
//  Aufgabe/Fkt...: Listen aus Messwerten. Z.B. können hiermit vektorielle Messdaten wie Geschwindigkeiten
//                  und Kräfte effizient dargestellt werden. Die Implementierung erfolgt nach dem
//                  Flyweight - Pattern.
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
using System.Diagnostics;

using E = mko.Euklid;

using Mag = mko.Newton.Magnitude;

namespace mko.Newton.Vector
{
    public abstract class MeasuredValuesVector<TMeasuredValue, TUnit>
        where TUnit : struct
        where TMeasuredValue : mko.Newton.MeasuredValue<TUnit>, new()
    {

        // Der Master- Value 
        TMeasuredValue MasterValue;

        //TUnit _unit;
        ///// <summary>
        ///// Einheit, die für alle Messwerte der Liste gilt
        ///// </summary>
        //public TUnit Unit {
        //    get {
        //        return _unit;
        //    }
        //}

        //public TUnit BaseUnit
        //{
        //    get
        //    {
        //        return new TMeasuredValue().BaseUnit;
        //    }
        //}


        E.Vector _vector;

        /// <summary>
        /// Euklid.Vektor, der durch diesen Dekorator verwaltet wird
        /// </summary>
        public E.Vector Vector
        {
            get
            {
                return _vector;
            }
        }

        public E.Vector VectorInBaseUnit
        {
            get
            {                
                return new E.Vector(Vector.coordinates.Select(c => c * MasterValue.ToBaseUnitConversionFactor()).ToArray());
            }
        }

        public E.Vector VectorInUnit
        {
            get
            {
                return new E.Vector(Vector.coordinates.Select(c => c * Mag.MagnitudeFactor[MasterValue.Magnitude]).ToArray());
            }
        }


        public int Dimensions
        {
            get
            {
                return _vector.Dimensions;
            }
        }

        public E.Vector VectorInUnit(TUnit unit)
        {            
            return new E.Vector(Vector.coordinates.Select(c => MasterValue.ToBaseUnitConversionFactor m.ConvertTo(unit)).ToArray());
        }
    
        /// <summary>
        /// Konstruktor, der einen 0- Vektor mit Coordinatenangaben in der Basiseinheit erzeugt
        /// </summary>
        /// <param name="dimensions"></param>
        protected MeasuredValuesVector(int dimensions)            
        {
            _unit = (new TMeasuredValue()).BaseUnit;
            _vector = new E.Vector(dimensions);
        }

        //public abstract MeasuredValuesVector<TMeasuredValue, TUnit> Create();
        //public abstract MeasuredValuesVector<TMeasuredValue, TUnit> Create(int dimensions);
        //public abstract MeasuredValuesVector<TMeasuredValue, TUnit> Create(TUnit unit, int dimensions);
        

        /// <summary>
        /// Copy- Konstruktor
        /// </summary>
        /// <param name="v"></param>
        protected MeasuredValuesVector(MeasuredValuesVector<TMeasuredValue, TUnit> v)            
        {
            _unit = v.Unit;
            _vector = new E.Vector(v.Vector);
        }



        /// <summary>
        /// Konstruktoren, der einen 0- Vektor erzeugt.
        /// </summary>
        /// <param name="unit">Maßeinheit, auf die sich die numerischen Koordinaten beziehen</param>
        /// <param name="dimensions">Anzahl der Dimensionen</param>
        protected  MeasuredValuesVector(TUnit unit, int dimensions)            
        {
            _unit = unit;
            _vector = new E.Vector(dimensions);
        }


        /// <summary>
        /// Konstruktor, der für eine übergebenen Liste von Werten und der übergebenen Einheit 
        /// einen Vektor erzeugt
        /// </summary>
        /// <param name="unit">Maßeinheit, auf die sich die numerischen Koordinaten beziehen</param>
        /// <param name="values">Liste der Koordinaten</param>
        protected MeasuredValuesVector(TMeasuredValue MasterValue, params double[] values)            
        {
            Debug.Assert(values.Length > 0);
            this.MasterValue = MasterValue;
            _vector = new E.Vector(values);
        }


        /// <summary>
        /// Konstruktor, mit dem ein Existierender Vector mit einer Unit dekoriert wird
        /// </summary>
        /// <param name="unit">Maßeinheit, auf die sich die numerischen Koordinaten beziehen, </param>
        /// <param name="Vector">Vektor mit Coordinaten</param>
        protected MeasuredValuesVector(TUnit unit, E.Vector Vector)
        {
            _unit = unit;
            _vector = Vector;
        }

        //public abstract MeasuredValuesVector<TMeasuredValue, TUnit> Create(TUnit unit, params double[] values);



        /// <summary>
        /// Konstruktor, der eine übergebene Liste von Messwerten in die im ersten Parameter 
        /// übergebene Einheit umrechnet und als Koordinaten eines Vektors speichert.
        /// </summary>
        /// <param name="unit">Maßeinheit, auf die sich die numerischen Koordinaten beziehen</param>
        /// <param name="values">Liste der Koordinaten als Messwerte in individuellen Einheiten</param>
        protected MeasuredValuesVector(TUnit unit, params mko.Newton.MeasuredValue<TUnit>[] values)            
        {
            Debug.Assert(values.Length > 0);
            _unit = unit;
            _vector = new E.Vector(values.Select(m => m.ConvertTo(unit)).ToArray());
        }
        //public abstract MeasuredValuesVector<TMeasuredValue, TUnit> Create(TUnit unit, params mko.Newton.MeasuredValue<TUnit>[] values);


        public TMeasuredValue this[int ix]
        {
            get
            {
                return new TMeasuredValue() { Value = Vector[ix], Unit = this.Unit };
            }

            set {
                // Zugewiesener Wert wird in die Einheit des Feldes gewandelt
                Vector[ix] = value.ConvertTo(_unit);
            }
        }

        public override string ToString()
        {
            return Vector.ToString() + " " + Unit.ToString();
        }


        //protected static MeasuredValuesVector<TMeasuredValue, TUnit> Add(MeasuredValuesVector<TMeasuredValue, TUnit> a, MeasuredValuesVector<TMeasuredValue, TUnit> b)            
        //{   
        //    if (a.Unit.Equals(b.Unit))
        //    {
        //        var sum = a.Create(a.Unit, a.Dimensions);                
        //        (a + b).CopyTo(sum);
        //        return sum;
        //    }
        //    else
        //    {
        //        var m = new TMeasuredValue();
        //        var sum = a.Create(m.BaseUnit, a.Dimensions);                                
        //        var aa = a.Create(m.BaseUnit, a.coordinates.Select(c => m.ConvertTo(m.BaseUnit)).ToArray());
        //        var bb = b.Create(m.BaseUnit, b.coordinates.Select(c => m.ConvertTo(m.BaseUnit)).ToArray());

        //        (aa + bb).CopyTo(sum);

        //        return sum;

        //    }
             
        //}

        
    }
}
