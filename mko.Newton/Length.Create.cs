
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: mko.Newton
//  Name..........: Distance.Create
//  Aufgabe/Fkt...: Klassenfabriken zum Erzeugen von Distanzwerten
//                  
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
        // Bezgülich der Einheit streng typisierte Messwerte

        public static LengthInMeter<Mag.Atto> Attometer(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Atto>(coordinates);
        }

        public static LengthInMeter<Mag.Femto> Femtometer(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Femto>(coordinates);
        }

        public static LengthInMeter<Mag.Pico> Picometer(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Pico>(coordinates);
        }

        public static LengthInMeter<Mag.Nano> Nanometer(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Nano>(coordinates);
        }

        public static LengthInMeter<Mag.Micro> Micrometer(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Micro>(coordinates);
        }

        public static LengthInMeter<Mag.Milli> Millimeter(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Milli>(coordinates);
        }

        public static LengthInMeter<Mag.Centi> Centimeter(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Centi>(coordinates);
        }

        // Decimeter
        public static LengthInMeter<Mag.Deci> Decimeter(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Deci>(coordinates);
        }     


        // Meter
        public static LengthInMeter<Mag.One> Meter(params double[] coordinates)
        {
            return new LengthInMeter<Mag.One>(coordinates);
        }

        public static LengthInMeter<Mag.One> Meter(Velocity v, Time t)
        {
            return (LengthInMeter<Mag.One>)SCALE(Time.Sec(t).Value, Velocity.MeterPerSec(v).S);
        }

        // km
        public static LengthInMeter<Mag.Kilo> Kilometer(params double[] coordinates)
        {
            return new LengthInMeter<Mag.Kilo>(coordinates);
        }

        // Point
        public static LengthInPoint Point(params double[] coordinates)
        {
            return new LengthInPoint(coordinates);
        }        

        // Inch
        public static LengthInInch Inch(params double[] coordinates)
        {
            return new LengthInInch(coordinates);
        }

        // Miles
        public static LengthInMiles Miles(params double[] coordinates)
        {
            return new LengthInMiles(coordinates);
        }       

        // Nautical Miles
        public static LengthInNauticalMiles NauticalMiles(params double[] coordinates)
        {
            return new LengthInNauticalMiles(coordinates);
        }

        // AE
        public static LengthInAU AU(params double[] coordinates)
        {
            return new LengthInAU(coordinates);
        }       


        // Lichtjahre
        public static LengthInLightYear Lightyear(params double[] coordinates)
        {
            return new LengthInLightYear(coordinates);
        }        

    }
}
