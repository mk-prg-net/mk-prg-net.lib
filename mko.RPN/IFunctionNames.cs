//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.4.2017
//
//  Projekt.......: mko.RPN
//  Name..........: IFunctionName.cs
//  Aufgabe/Fkt...: Grundlegende Struktur einer Funktionsnamen- Tabelle. 
//                  Definiert die Funktionsnamen für elementare Token wie int, double und bool
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 3.5.2017
//  Änderungen....: ListEnd - Symbol, IsSemanticDescriptor hinzugefügt
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    public interface IFunctionNames
    {
        /// <summary>
        /// Funktionsname für boolsche Konstante
        /// </summary>
        string constBool{ get; }

        /// <summary>
        /// Funktionsname für Integer- Konstante
        /// </summary>
        string constInt { get; }

        /// <summary>
        /// Funktionsname für Double. Konstante
        /// </summary>
        string constDbl { get; }


        /// <summary>
        /// Funktionsname für String Konstante
        /// </summary>
        string constStr { get; }

        /// <summary>
        /// Funktionsname für Listenende- Symbol
        /// </summary>
        string ListEnd { get; }


        /// <summary>
        /// Allgemeines Namensprefix. Kann auch leer sein. Für Einsatz von RPN in URL- Querystrings
        /// kann auch ein Punkt "." eingesetzt werden
        /// </summary>
        string NamePrefix { get; }

        /// <summary>
        /// Allgemeines Namenspräfix für Markup Funktionen, die Funktionsparameter benennen
        /// Für Einsatz von RPN in URL- Querystrings kann auch ein Doppelpunkt ".." eingesetzt werden
        /// </summary>
        string ParamNamePrefix { get; }


        /// <summary>
        /// True, wenn die Funktion ihren Parameter, der ein Subtree ist, einer Bedeutung zuweist.
        /// Z.B. .rng.Mass .EM 2 .EM 10 filtert alle Himmelskörper mit mindestens zwei und maximal 
        /// 10 Erdmassen Masse heraus. Die Parameter könnten durch die semantischen 
        /// Descriptorfunktionen ..min und ..max wie folgt dokumentiert werden:
        /// .rng.Mass ..min .EM 2 ..max .EM 10
        /// Semantische Descriptoren erleichtern die Entwicklung von dynamischen Views für solche Ausdrücke.
        /// Semantische Deskriptoren sind immer unär
        /// </summary>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        bool IsSemanticDescriptor(string FunctionName);

        /// <summary>
        /// Präfix für FunctionNameTokens, die Ergebnisse aus Auswertungen vorausgegangener Token darstellen.
        /// </summary>
        string DerivedTokenPrefix { get; }

    }
}
