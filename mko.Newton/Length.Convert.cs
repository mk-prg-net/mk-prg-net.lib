//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, November 2012 
//
//  Projekt.......: mko.Newton
//  Name..........: Distance.Convert
//  Aufgabe/Fkt...: Convertierungsfunktionen, die einen Distance- Wert in einen Zielformat wandeln
//                  unter berücksichtigung der Umrechnigsfaktoren und Magnituden
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

namespace mko.Newton
{
    public partial class Length
    {
        public static mko.Newton.LengthInMeter<Mag.Atto> Attometer(Length value)
        {
            return new LengthInMeter<Mag.Atto>(value);
        }

        public static mko.Newton.LengthInMeter<Mag.Femto> Femtometer(Length value)
        {
            return new LengthInMeter<Mag.Femto>(value);
        }

        public static mko.Newton.LengthInMeter<Mag.Pico> Picometer(Length value)
        {
            return new LengthInMeter<Mag.Pico>(value);
        }

        public static mko.Newton.LengthInMeter<Mag.Nano> Nanometer(Length value)
        {
            return new LengthInMeter<Mag.Nano>(value);
        }

        public static mko.Newton.LengthInMeter<Mag.Micro> Micrometer(Length value)
        {
            return new LengthInMeter<Mag.Micro>(value);
        }

        public static mko.Newton.LengthInMeter<Mag.Milli> Millimeter(Length value)
        {
            return new LengthInMeter<Mag.Milli>(value);
        }

        public static mko.Newton.LengthInMeter<Mag.Centi> Centimeter(Length value)
        {
            return new LengthInMeter<Mag.Centi>(value);
        }

        // Decimeter
        public static mko.Newton.LengthInMeter<Mag.Deci> Decimeter(Length mVector)
        {
            return new LengthInMeter<Mag.Deci>(mVector);
        }


        // Meter
        public static mko.Newton.LengthInMeter<Mag.One> Meter(Length mVector)
        {
            return new LengthInMeter<Mag.One>(mVector);
        }


        // km
        public static mko.Newton.LengthInMeter<Mag.Kilo> Kilometer(Length mVector)
        {
            return new LengthInMeter<Mag.Kilo>(mVector);
        }

        // Point
        public static mko.Newton.LengthInPoint Point(Length mVector)
        {
            return new LengthInPoint(mVector);
        }

        // Inch
        public static mko.Newton.LengthInInch Inch(Length mVector)
        {
            return new LengthInInch(mVector);
        }

        // Miles
        public static mko.Newton.LengthInMiles Miles(Length mVector)
        {
            return new LengthInMiles(mVector);
        }

        // Nautical Miles
        public static mko.Newton.LengthInNauticalMiles NauticalMiles(Length mVector)
        {
            return new LengthInNauticalMiles(mVector);
        }

        // AE
        public static mko.Newton.LengthInAU AU(Length mVector)
        {
            return new LengthInAU(mVector);
        }


        // Lichtjahre
        public static mko.Newton.LengthInLightYear Lightyear(Length mVector)
        {
            return new LengthInLightYear(mVector);
        }

    }
}
