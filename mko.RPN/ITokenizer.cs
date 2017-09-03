//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2016
//
//  Projekt.......: mko.RPN
//  Name..........: ITokenizer.cs
//  Aufgabe/Fkt...: Struktur von Klassen, deren Objekte Typ 2 Sprachtexte, die in 
//                  der Reverse Polish Notation (RPN) verfasst sind, in ihre Grundbausteine
//                  wie Konstanten und Funktionsnamen auflösen. Die Tokens werden
//                  als IToken- Objekte zurückgegeben.
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
//  Datum.........: 6.4.2017
//  Änderungen....: Neue Konvention für Funktionsnamen: 
//                  - Groß/Kleinschreibung ist irrelevant
//                  - jeder Funktionsname beginnt mit einem Punkt: .
//                  - Funktionsnamen können mehrteilig sein. Ihre Bestandteile werden durch 
//                    Punkt (.) getrennt
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    public interface ITokenizer
    {
        /// <summary>
        /// Liest das nächte Token ein
        /// </summary>
        void Read();

        /// <summary>
        /// Liefert true, wenn kein weiteres Token mehr eingelesen werden kann
        /// </summary>
        bool EOF { get; }


        /// <summary>
        /// Liefert das zuletzt eingelesene Token
        /// </summary>
        IToken Token { get; }
    }
}
