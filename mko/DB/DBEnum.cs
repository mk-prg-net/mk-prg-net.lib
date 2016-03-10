//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 19.10.2010
//
//  Projekt.......: mkoItAsp
//  Name..........: DBEnum.cs
//  Aufgabe/Fkt...: Aufzählungstypen, die in einer Datenbank als Tabelle definiert
//                  sind, werden in ein Dictionary geladen. Dabei wird der 
//                  Bezeichner als Schlüssel, und der Wert als Wert gespeichert.
//                  Objekte vom Typ DBEnum sind Singletons.
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

namespace mko.Bo
{
    public abstract class DBEnum
    {
        Dictionary<string, int> _mapNameToValue = new Dictionary<string, int>();

        public DBEnum()
        {
            loadFromDB();
        }

        /// <summary>
        /// Löschen aller Einträge in der Enumtabelle.
        /// Nur für die Implementierung eines DBEnum.
        /// </summary>
        protected void Clear()
        {
            _mapNameToValue.Clear();
        }

        protected void Add(string EnumMemberName, int EnumMemberValue)
        {
            Debug.Assert(!_mapNameToValue.ContainsKey(EnumMemberName),
                        "DBEnum.Add: Der Member " +
                        EnumMemberName +
                        " ist bereits enthalten");
            _mapNameToValue.Add(EnumMemberName, EnumMemberValue);
        }

        /// <summary>
        /// Laden der Enum- Tabelle aus der Datenbank
        /// </summary>
        protected abstract void loadFromDB();

        public int resolve(string ValueName)
        {
            Debug.Assert(_mapNameToValue != null);
            Debug.Assert(_mapNameToValue.ContainsKey(ValueName),
                    "DBEnum.resolve: Der Enum "
                    + ValueName
                    + " ist nicht in der Datenbank definiert");

            return _mapNameToValue[ValueName];
        }

        public string inversResolve(int value)
        {
            foreach (KeyValuePair<string, int> pair in _mapNameToValue)
            {
                if (pair.Value == value)
                    return pair.Key;
            }

            throw new Exception("Der Wert " + value + " ist im DBEnum nicht enthalten");
        }
    }
}
