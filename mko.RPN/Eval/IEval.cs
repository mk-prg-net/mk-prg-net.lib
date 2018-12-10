//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2016
//
//  Projekt.......: mko.RPN
//  Name..........: IEval.cs
//  Aufgabe/Fkt...: Struktur  eines Evaluators für Funktionen.
//                  RPN- Ausdrücke können als Syntaxbäume dargestellt 
//                  werden (https://en.wikipedia.org/wiki/Parse_tree).
//                  Ein Evaluator Brechnet den Wurzelknoten eines neues 
//                  Syntaxbaumes aus n Teilsyntaxbäumen.
//                  Beispiel: 
//                              2.3 4.7 ADD = Teilbaum1
//                              5.2 1.2 SUB = Teilbaum2
//                              Teilbaum1  Teilbaum2 MUL = 2.3 4.7 ADD 5.2 1.2 SUB MUL = Baum
//                              Eval(Baum) = 28
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
    public interface IEval
    {
        void Eval(Stack<IToken> stack);

        /// <summary>
        /// true, wenn evaluierung einer Funktion erfolgreich verlief
        /// </summary>
        bool Succesful { get; }
        
        /// <summary>
        /// Falls die Evaluierung nicht erfolgreich war, kann hier die Exception abgerufen werden,
        /// welche auf die Gründe des Misserfolges hinweist
        /// </summary>
        Exception EvalException { get; }
    }
}
