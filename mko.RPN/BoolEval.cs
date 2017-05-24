﻿//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: BoolEval.cs
//  Aufgabe/Fkt...: Implementierung der konstanten Funktion .bool x
//                  
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

namespace mko.RPN
{
    public class BoolEval : BasicEvaluator
    {
        public BoolEval() : base(1) { }

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            mko.TraceHlp.ThrowArgExIfNot(
                stack.Peek().IsBoolean
                || (StringToken.Test(stack.Peek())
                    && (stack.Peek().Value == "true" || stack.Peek().Value == "false")),
                string.Format(Properties.Resources.nummeric_expected, stack.Peek().Value));
            var tok = stack.Pop();

            if (StringToken.Test(tok))
            {
                stack.Push(new BoolToken((bool)(StringToken)tok, tok.CountOfEvaluatedTokens + 1));
            }
            else
            {
                if (tok.IsInteger)
                {
                    stack.Push(new IntToken((IntToken)tok, tok.CountOfEvaluatedTokens + 1));
                }
                else
                {
                    stack.Push(new IntToken((int)(DoubleToken)tok, tok.CountOfEvaluatedTokens + 1));
                }
            }
        }
    }
}
