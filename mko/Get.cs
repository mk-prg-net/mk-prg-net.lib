//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2.5.2014
//
//  Projekt.......: mko
//  Name..........: Get.cs
//  Aufgabe/Fkt...: Hilfsfunktionen für den sicheren Zugriff auf Objekte, Eigenschaften und Strings
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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
using System.Threading.Tasks;

namespace mko
{
    public class Get
    {

        /// <summary>
        /// Inteligenter Accessor für Eigenschaften. Wenn das Objekt existiert, dann wird die gewünschte Eigenschaft mit dem
        /// Selektor ausgelesen. Wenn nicht, dann wird ein Default- Wert zurückgegeben.
        /// </summary>
        /// <typeparam name="TObj"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="obj"></param>
        /// <param name="PropertySelector"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static TProp PropertyOrDefault<TObj, TProp>(TObj obj, Func<TObj, TProp> PropertySelector, TProp DefaultValue)
        {
            if (obj != null)
                return PropertySelector(obj);
            else
                return DefaultValue;
        }

        /// <summary>
        /// Gesicherter Zugriff auf ein Objekt. Wenn das Objekt nicht existiert
        /// dann wird ein Defaultobjekt zurückgegeben
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static T ValueOrDefault<T>(T Value, T Default)
        {
            if (Value == null)
                return Default;
            else
                return Value;
        }


        /// <summary>
        /// Gesicherter Zugriff oauf eine Zeichenkette. Ist die Zeichenkette leer oder existiert sie nicht,
        /// dann wird der Defaultwert zurückgegeben
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static string ValueOrDefault(string Value, string Default)
        {
            if (string.IsNullOrWhiteSpace(Value))
                return Default;
            else
                return Value;
        }

    }
}
