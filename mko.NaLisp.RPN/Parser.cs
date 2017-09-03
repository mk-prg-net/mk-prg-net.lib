//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2016
//
//  Projekt.......: mko.NaLisp.RPN
//  Name..........: Parser.cs
//  Aufgabe/Fkt...: Ein String mit einem Ausdruck in der RPN- Notation 
//                  wie 2 3 ADD 5 MUL <=> (2 + 3) * 4 
//                  wird in einen NaLisp gewandelt
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base = mko.NaLisp;

namespace mko.NaLisp.RPN
{
    public class Parser
    {

        Stack<IToken> stack = new Stack<IToken>();

        public bool TryParse(ITokenizer RpnTokenizer, Dictionary<string, IFuncParser> FuncParsers, out Base.Core.INaLisp NaLispExpression)
        {

            stack.Clear();

            NaLispExpression = null;

            RpnTokenizer.Read();
            while (!RpnTokenizer.EOF)
            {
                IToken token = RpnTokenizer.Token;
                if (token.IsFunctionName)
                {

                    var funcname = token.Value;

                    if (FuncParsers.ContainsKey(funcname))
                    {
                        return FuncParsers[funcname].TryParse(RpnTokenizer, FuncParsers, out NaLispExpression);
                    }
                    else
                    {
                        Debug.WriteLine("unbekannte Funktion " + funcname);
                        return false;
                    }
                }
                else
                {
                    stack.Push(token);
                }
                
            }
            return true;
        }

    }
}
