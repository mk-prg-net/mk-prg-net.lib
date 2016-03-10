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
//                  Siehe http://www.xml-cml.org/unit/si/
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

using E = mko.Euklid;
using System.Diagnostics;
using Mag = mko.Newton.OrderOfMagnitude;


namespace mko.Newton
{
    
    public abstract class SIBaseType
    {
        public SIBaseType()
        {
        }

        /// <summary>
        /// Liefert den Umrechnungsfaktor in die Basieinheit. 
        /// Wird in Abgeleiteten Klassen, die Messwerte einer ausgewählten Einheit darstellen (z.B. Energie in KWh)
        /// überschrieben, um den Umrechnigsfaktor in die Basiseinheit zu liefern (z.B. Joule)
        /// </summary>
        /// <returns></returns>
        public abstract Double ToBaseUnitConversionFactor
        {
            get;
        }

        public abstract string SiBaseUnitId {
            get;
        }

        public abstract string SiBaseUnitDefinition
        {
            get;
        }

        public abstract string UnitSymbol
        {
            get;
        }


        /// <summary>
        /// Klasse der Fehlerobjekte für den Fall, dass eine unbekannte Einheit eingesetzt wurde.
        /// </summary>
        public class UndefUnit : Exception
        {
            public UndefUnit() : base("undefined Unit") { }
        }

        public class NotAllArgumentsOfSameType : ArgumentException
        {
            public NotAllArgumentsOfSameType() : base("Not all arguments of same type") { }
        }

        public class NotAllArgumentsOfSameBaseType : ArgumentException
        {
            public NotAllArgumentsOfSameBaseType() : base("Not all arguments types are derived from same base class") { }
        }

        protected static void ASSERT_ARGUMETNS_OF_SAME_TYPE(SIBaseType a, SIBaseType b)
        {
            if(a.GetType() != b.GetType())
                throw new NotAllArgumentsOfSameType();
        }

        protected static void ASSERT_ARGUMETNS_OF_SAME_BASE_TYPE<TBaseType>(SIBaseType a, SIBaseType b)
        {
            if (!(a is TBaseType && b is TBaseType))
                throw new NotAllArgumentsOfSameBaseType();
        }



        /// <summary>
        /// Größenordnung/Vielfaches der Einheit, in die die Messwertskale geteilt ist. Z.B. Centi = 0.01 Einheiten 
        /// pro Teilstrich oder Kilo = 1000 Einheiten pro Teilstrich
        /// </summary>
        public virtual Mag.OrderOfMagnitudeEnum OrderOfMagnitude
        {
            get
            {
                return Mag.OrderOfMagnitudeEnum.One;
            }
        }
    }
}
