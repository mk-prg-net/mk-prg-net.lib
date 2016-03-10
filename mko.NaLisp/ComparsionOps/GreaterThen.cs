//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: mko.NaLisp
//  Name..........: GreaterThen.cs
//  Aufgabe/Fkt...: Größer als Operator
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
//  Datum.........: 27.10.2015
//  Änderungen....: In einen generischen Typ verallgemeinert.
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.ComparsionOps
{
    public class GreaterThen<T> : OpBase<T>
        where T : IComparable<T>
    {
        public GreaterThen(params Core.INaLisp[] Elements)
            : base(Elements) { }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new GreaterThen<T>(Elements);            
        }

        protected override string GetOpName()
        {
            return "GT";
        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(this, EvaluatedElements, StackInstance, DebugOn, (left, right) => left.CompareTo(right) > 0);
        }

    }
}
