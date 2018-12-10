//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: EvalBase.cs
//  Aufgabe/Fkt...: Basisklasse aller Evaluatoren
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
    public abstract class EvalBase : mko.RPN.IEval
    {

        public EvalBase()
        {
        }

        public Exception EvalException
        {
            get
            {
                return _ex;
            }
        }
        protected Exception _ex;

        public bool Succesful
        {
            get
            {
                return _succ;
            }
        }

        protected bool _succ;

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
        public virtual void Eval(Stack<IToken> stack)
        {
            try
            {
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
