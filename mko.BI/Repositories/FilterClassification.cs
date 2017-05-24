//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.4.2017
//
//  Projekt.......: mko.BI
//  Name..........: FilterClassification.cs
//  Aufgabe/Fkt...: Klassifikation von Filtern.
//                  Ein allgemeine  Einteilung von Filteralgorithmen auf Mengen.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
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

namespace mko.BI.Repositories
{
    public enum FilterClassification
    {
        /// <summary>
        /// Bereichsfilter (engl. range). Schränken auf Elemente ein, 
        /// die bezüglich einer Ordnung sich innerhalb eines definierten Intervalls, Bereiches 
        /// befinden. Z.B. Alle, die zwischen 1.1.2017 und 31.3.2017 hergestellt wurden
        /// </summary>
        rng,

        /// <summary>
        /// Definiert eine Reihenfolge (Sortierkriterium). Z.B. Order by price
        /// </summary>
        sort,

        /// <summary>
        /// Auswahl von einem oderer mehreren aus einer endlichen Menge geringer 
        /// Mächtigkeit. Z.B. Menge der Himmelskörpertypen (Stern, Planet, Mond, ...), 
        /// Menge der Werke etc.
        /// </summary>
        set,

        /// <summary>
        /// Auswahl eines Elementes über einen Schlüssel. In einer Gui wird hier in der 
        /// Regel ein Autocomplete- Feld angeboten (z.B. Auswahl über Lieferscheiunnummer, 
        /// Himmelskörpername, ...)
        /// </summary>
        key,

        /// <summary>
        /// Auswähl über Änlichkeit zu einem Muster (z.B. Alle Himmelskörper, deren Name mit "M"
        /// beginnt
        /// </summary>
        like,
        
        /// <summary>
        /// Menge wird bezüglich eines logischen Prädikates (wie z.B. IstPrimzahl) in zwei Teilmengen
        /// aufgeteilt: Teilmenge derer, die das Prädikat erfüllen, und kmomplementärer Teilmenge jener,
        /// die es nicht erfüllen.
        /// </summary>
        predicate
    }
}
