//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: EvalFunctionWithFixedParamCount.cs
//  Aufgabe/Fkt...: Basisklasse aller Evaluatoren für Funktionen mit einer festen Anzahl von Parametern
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
    /// <summary>
    /// Basisklasse aller Evaluatoren für Funktionen mit einer festen Anzahl von Parametern
    /// </summary>
    public abstract class EvalFunctionWithFixedParamCount : EvalBase
    {
        public EvalFunctionWithFixedParamCount(string FunctionName, int ParameterCount)
        {
            this.ParameterCount = ParameterCount;
            this.FunctionName = FunctionName;
        }

        /// <summary>
        /// Name der zu evaluierenden Funktion
        /// </summary>
        protected string FunctionName;

        /// <summary>
        /// Anzahl der Funktionsparameter der zu evaluierenden Funktion
        /// </summary>
        protected int ParameterCount;
        

        public override void Eval(Stack<IToken> stack)
        {
            try
            {
                mko.TraceHlp.ThrowArgExIfNot(ParameterCount > 0 && stack.Count - ParameterCount >= 0, string.Format(Properties.Resources.EvalErr_InvalidParameterCount, FunctionName));

                ReadParametersAndEvaluate(stack);
                _succ = true;
            }
            catch (Exception ex)
            {
                _succ = false;
                _ex = ex;
            }
        }
    }
}
