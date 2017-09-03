//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.5.2017
//
//  Projekt.......: mko.RPN.DateTime
//  Name..........: DateTimeEval.cs
//  Aufgabe/Fkt...: Evaluierung eines Datums
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

namespace mko.RPN.DateTime.Eval
{
    public class DateEval : EvalBase
    {
        public DateEval(IFunctionNamesDateTime fn)
        {
            this.fn = fn;
        }

        IFunctionNamesDateTime fn;

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            mko.TraceHlp.ThrowArgExIfNot(stack.Count >= 1, "");

            var set = new List<Type>();
            set.Add(typeof(Day));
            set.Add(typeof(Month));
            set.Add(typeof(Year));

            var date = new System.DateTime();

            while (stack.Count >= 1 && set.Contains(stack.Peek().GetType()))
            {
                var part = stack.Pop();

                // Datumskomponente konfigurieren
                date = ((BaseDateComponentToken)part).Config(date);

                // Datumskomponeten (Tag, Monat oder Jahr) als verarbeitet kennzeichnen
                set.Remove(part.GetType());

            }

            mko.TraceHlp.ThrowArgExIf(set.Contains(typeof(Year)), "");
            mko.TraceHlp.ThrowArgExIf(set.Count == 1 && set.Contains(typeof(Month)), "");

            stack.Push(new Date(fn, date));
        }
    }
}
