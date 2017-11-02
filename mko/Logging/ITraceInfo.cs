using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Logging
{
    /// <summary>
    /// mko, 26.9.2017
    /// Aufzeichnung von Laufzeitinformationen für Fehlermeldungen und 
    /// </summary>
    public interface ITraceInfo
    {
        
        /// <summary>
        /// Datum der Aufzeichnung
        /// </summary>
        DateTime LogDate
        {
            get;
        }


        /// <summary>
        /// Name des Benutzers, der die Funktion ausführte, für die Aufzeichnungen erfolgen
        /// </summary>
        string User { get; }

        /// <summary>
        /// Name der Assembly, welche den Typenthält
        /// </summary>
        string AssemblyName
        {
            get;
        }

        /// <summary>
        /// Name der Klasse, Struktur, welcher die Funktion zugeordnet ist
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Name der Aufgezeichneten Funktion
        /// </summary>
        string FunctionName { get; }

        /// <summary>
        /// Zusätzliche Informationen über die Aufzeichnung eines Funktionsaufrufes
        /// </summary>
        string Message { get; }

    }
}
