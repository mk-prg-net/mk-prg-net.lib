//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
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

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace mko
{
    public class Utils
    {
        /// <summary>
        /// Clonalgorithmus. Funktioniert für serialisierbare Objekte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CloneSerializableObject(object obj)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone)); 
                binaryFormatter.Serialize(memStream, obj); 
                memStream.Seek(0, SeekOrigin.Begin); 
                return binaryFormatter.Deserialize(memStream);
            }
        }

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
        public static TProp GetPropertyOrDefault<TObj, TProp>(TObj obj, Func<TObj, TProp> PropertySelector, TProp DefaultValue)
        {
            if (obj != null)
                return PropertySelector(obj);
            else
                return DefaultValue;
        }


        /// <summary>
        /// Gesicherter Zugriff oauf eine Zeichenkette. Ist die Zeichenkette leer oder existiert sie nicht,
        /// dann wird der Defaultwert zurückgegeben
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static string GetValueOrDefault(string Value, string Default)
        {
            if (string.IsNullOrWhiteSpace(Value))
                return Default;
            else
                return Value;
        }

    }


}
