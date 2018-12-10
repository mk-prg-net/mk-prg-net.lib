//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2016
//
//  Projekt.......: mko.RPN
//  Name..........: IToken.cs
//  Aufgabe/Fkt...: Grundstruktur von Tokens, die der Tokenizer beim 
//                  Einlesen von Typ 2 Sprachtermen in der RPN (Reverse Polish Notation)
//                  erzeugt. 
//                  Elemente des Stapelspeichers, auf dem der Parser die Terme 
//                  evaluiert sind ebenfalls vom Typ IToken.
//                  Die Ergebnisse der Evaluierung landen wieder auf dem Stapelspeicher
//                  unsd sind damit auch IToken- Strukturen.
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

namespace mko.RPN
{
    public interface IToken
    {
        /// <summary>
        /// Liefert true, wenn das zuletzt eingelese Token eine Funktion ist
        /// </summary>
        bool IsFunctionName { get; }

        /// <summary>
        /// True, wenn das Token einen reinen Integerwert darstellt
        /// </summary>
        bool IsInteger { get; }

        /// <summary>
        /// True, wenn das Token einen Boolean darstellt
        /// </summary>
        bool IsBoolean { get; }

        /// <summary>
        /// True, wenn das Token einen allgemeinen nummerischen Wert (Festkomma oder Gleitkomma)
        /// darstellt.
        /// </summary>
        bool IsNummeric { get; }

        /// <summary>
        /// Das Token als Textfragment
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Anzahl der Tokens, die von einem Evaluator verarbeitet wurden, um dieses Token zu berechnen
        /// = Anzahl der Knoten im konbstituierenden Syntaxbaum, dessen Spitze die Phrase darstellt,
        /// welches diese Token repräsensiert.
        /// Z.B. RPN Term: 2.3 4.7 ADD 2 MUL -> CountOfEvaluatedTokens für MUL= 4 (2.3, 4.7, ADD, 2)
        /// </summary>
        int CountOfEvaluatedTokens { get; }

        /// <summary>
        /// Erzeugt vom Token eine Kopie
        /// </summary>
        /// <returns></returns>
        IToken Copy();

    }
}
