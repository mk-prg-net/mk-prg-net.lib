//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.11.2011
//
//  Projekt.......: Algorithmen
//  Name..........: Interpreter.cs
//  Aufgabe/Fkt...: Eine Liste aus Funktoren, genannt Symbole, wird eingelesen 
//                  und zu einem geklammerten Ausdruck komponiert. Am Ende wird der 
//                  Ausdruck ausgewertet.
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

using System.Text.RegularExpressions;

namespace mko.Algo.fml
{
    public class Interpreter<T>
    {
        public T Eval(List<FunctorBase<T>> symbole)
        {
            var stack = new Stack<FunctorBase<T>>();
            int akt_pos = 0;

            do
            {
                // Einkellern bis zur schließenden Klammer
                while (symbole[akt_pos] is FunctorConst<T> && akt_pos < symbole.Count)
                {
                    stack.Push(symbole[akt_pos]);
                    akt_pos++;
                }

                if (symbole.Count > 1 && akt_pos == symbole.Count)
                    throw new Exception("Syntax Error");

                if (symbole[akt_pos] is FunctorUnary<T>)
                {
                    var A = stack.Pop();
                    var F = (FunctorUnary<T>)symbole[akt_pos];
                    F.A = A;
                    stack.Push(F);
                }
                else if (symbole[akt_pos] is FunctorBinary<T>)
                {
                    var A = stack.Pop();
                    var B = stack.Pop();
                    var F = (FunctorBinary<T>)symbole[akt_pos];
                    F.A = A;
                    F.B = B;
                    stack.Push(F);
                }

                akt_pos++;

            } while (akt_pos < symbole.Count);

            return stack.Peek().map();
        }
    }
}
