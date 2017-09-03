//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.4.2017
//
//  Projekt.......: mko.RPN.Filter
//  Name..........: IFilterFunctionNames.cs
//  Aufgabe/Fkt...: Für die Implementierung von RPN- Sprachen zur Definition
//                  von Filter auf Mengen wird ein Namensschema vorgegeben, 
//                  welches die Klassifizierung von Filtern aus Mengen aus 
//                  mko.BI.Repositories wiederspiegelt.
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

namespace mko.RPN.Filter
{

    /// <summary>
    /// Für die Implementierung von RPN- Sprachen zur Definition
    /// von Filter auf Mengen wird ein Namensschema vorgegeben, 
    /// welches die Klassifizierung von Filtern aus Mengen aus 
    /// mko.BI.Repositories wiederspiegelt.
    /// </summary>
    public interface IFilterFunctionNameFactories
    {
        /// <summary>
        /// Erzeugt einen normierten Bereichsfilternamen. Z.B. 
        /// "Price" -> rng.Price
        /// </summary>
        /// <param name="FunctionName">einfache Funktionsbezeichnung</param>
        /// <returns></returns>
        string createRngFltName(string FunctionName);

        /// <summary>
        /// Schneidet das rng- Flt PRefix vom Funktionsnamen ab
        /// </summary>
        /// <param name="rngFltName"></param>
        /// <returns></returns>
        string reduceRngFilterName(string rngFltName);

        /// <summary>
        /// True, wenn der Funkmtionsname ein Bereichsfilter bezeichnet
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsRngFilter(string name);
        
        // Sortiern

        /// <summary>
        /// Erzeugt einen normierten Sortierfilternamen. Z.B. 
        /// "Price" -> rng.Price
        /// </summary>
        /// <param name="FunctionName">einfache Funktionsbezeichnung</param>
        /// <returns></returns>
        string createSortFltName(string FunctionName);

        /// <summary>
        /// Schneidet das sort- Flt PRefix vom Funktionsnamen ab
        /// </summary>
        /// <param name="FltName"></param>
        /// <returns></returns>
        string reduceSortFilterName(string FltName);

        /// <summary>
        /// True, wenn der Funktionsname ein Sortierfilter bezeichnet
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsSortFilter(string name);


        // Set filter

        /// <summary>
        /// Erzeugt einen normierten Setfilternamen. Z.B. 
        /// "Price" -> rng.Price
        /// </summary>
        /// <param name="FunctionName">einfache Funktionsbezeichnung</param>
        /// <returns></returns>
        string createSetFltName(string FunctionName);

        /// <summary>
        /// Schneidet das Set- Flt Präfix vom Funktionsnamen ab
        /// </summary>
        /// <param name="FltName"></param>
        /// <returns></returns>
        string reduceSetFilterName(string FltName);

        /// <summary>
        /// True, wenn der Funktionsname ein Setierfilter bezeichnet
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsSetFilter(string name);


        // Key Filter

        /// <summary>
        /// Erzeugt einen normierten Keyfilternamen. Z.B. 
        /// "Price" -> rng.Price
        /// </summary>
        /// <param name="FunctionName">einfache Funktionsbezeichnung</param>
        /// <returns></returns>
        string createKeyFltName(string FunctionName);

        /// <summary>
        /// Schneidet das Key- Flt Präfix vom Funktionsnamen ab
        /// </summary>
        /// <param name="FltName"></param>
        /// <returns></returns>
        string reduceKeyFilterName(string FltName);

        /// <summary>
        /// True, wenn der Funktionsname ein Keyfilter bezeichnet
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsKeyFilter(string name);


        // Like filter

        /// <summary>
        /// Erzeugt einen normierten Likefilternamen. Z.B. 
        /// "Price" -> rng.Price
        /// </summary>
        /// <param name="FunctionName">einfache Funktionsbezeichnung</param>
        /// <returns></returns>
        string createLikeFltName(string FunctionName);

        /// <summary>
        /// Schneidet das Like- Flt Präfix vom Funktionsnamen ab
        /// </summary>
        /// <param name="FltName"></param>
        /// <returns></returns>
        string reduceLikeFilterName(string FltName);

        /// <summary>
        /// True, wenn der Funktionsname ein Likefilter bezeichnet
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsLikeFilter(string name);
        
        // Prädikats- Filter

        /// <summary>
        /// Namensprefix für Filter,  die Mengen bezüglich eines logischen Prädikates
        /// in zwei Teilmengen aufteilen.
        /// </summary>
        /// <param name="FltName"></param>
        /// <returns></returns>
        string createPredFltName(string FltName);

        /// <summary>
        /// Schneidet das Pred- Flt Präfix vom Funktionsnamen ab
        /// </summary>
        /// <param name="FltName"></param>
        /// <returns></returns>
        string reducePredFilterName(string FltName);

        /// <summary>
        /// True, wenn der Funktionsname ein Prädikatsfilter bezeichnet
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsPredFilter(string name);

    }
}
