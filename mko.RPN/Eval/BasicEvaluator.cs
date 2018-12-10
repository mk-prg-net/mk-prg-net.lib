//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.5.2016
//
//  Projekt.......: mko.RPN
//  Name..........: BasicEvaluator.cs
//  Aufgabe/Fkt...: Basisimplementierung für einen Evaluator.
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
    public abstract class BasicEvaluator : IEval
    {
        int ParameterCount = 0;

        /// <summary>
        /// Konstruktor für Basisimplementierung eines Funktions- Evaluators
        /// </summary>
        /// <param name="ParameterCount">Anzahl der Funktionsparameter</param>
        public BasicEvaluator(int ParameterCount)
        {
            this.ParameterCount = ParameterCount;
        }

        /// <summary>
        /// Basisimplementierung von IEval.Succesful
        /// </summary>
        public bool Succesful
        {
            get { return _successful; }
        }
        bool _successful;

        /// <summary>
        /// Basisimplementierung von IEval.EvalException
        /// </summary>
        public Exception EvalException
        {
            get { return _ex; }
        }
        Exception _ex;

        /// <summary>
        /// Helper zur Entnahme eines nummerischen Tokens (int oder double) vom Stapel
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public static Tuple<double, IToken> PopNummeric(Stack<IToken> stack)
        {
            if (stack.Peek().IsNummeric)
            {
                if (stack.Peek().IsInteger)
                {
                    var token = stack.Pop() as IntToken;
                    return Tuple.Create((double)token, (IToken)token);
                }
                else
                {
                    var token = stack.Pop() as DoubleToken;
                    return Tuple.Create((double)token, (IToken)token);
                }
            }
            else
            {
                throw new ArgumentException(string.Format(Properties.Resources.nummeric_expected, stack.Peek().Value));
            }
        }

        /// <summary>
        /// Hier sind, die Parameter der Funktion vom Stapel zu entnehmen und die Funktion
        /// zu evaluieren.
        /// Es kann davon ausgegangen werden, das alle benötigten Argumente auf dem Stapel abgelegt sind.
        /// </summary>
        /// <param name="stack">Stapelspeicher</param>
        public abstract void ReadParametersAndEvaluate(Stack<IToken> stack);        

        /// <summary>
        /// Basisimplementierung von IEval.Eval. Die Detailarbeit bei der Implementierung einer 
        /// Funktion wird an ReadParametersAndEvaluate(stack) deligiert.
        /// </summary>
        /// <param name="stack"></param>
        public void Eval(Stack<IToken> stack)
        {
            if (stack.Count >= ParameterCount)
            {
                try
                {
                    ReadParametersAndEvaluate(stack);
                    _successful = true;
                }
                catch (Exception ex)
                {
                    _successful = false;
                    _ex = new ArgumentException(mko.TraceHlp.FormatErrMsg(this, "Eval", "ParameterCount", ParameterCount.ToString(), "Stack.Count:", stack.Count.ToString()), ex);
                }
            }
            else
            {
                _successful = false;
                _ex = new ArgumentException(mko.TraceHlp.FormatErrMsg(this, "Eval", "ParameterCount", ParameterCount.ToString(), "Stack.Count:", stack.Count.ToString()));
            }
        }
    }
}
