//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.10.2015
//
//  Projekt.......: mko.Woc.WocStore
//  Name..........: LowerThen.cs
//  Aufgabe/Fkt...: Implementiert den < Operator auf vergleichbaren Typen
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

namespace mko.NaLisp.ComparsionOps
{
    public class LowerThen<T> : OpBase<T>
        where T: IComparable<T>
    {
        public LowerThen(params Core.INaLisp[] Elements)
            : base(Elements) { }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new LowerThen<T>(Elements);
        }

        protected override string GetOpName()
        {
            return "LT";
        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(this, EvaluatedElements, StackInstance, DebugOn, (left, right) => left.CompareTo(right) < 0);
        }

    }
}
