//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.10.2015
//
//  Projekt.......: mko.NaLisp
//  Name..........: SUB.cs
//  Aufgabe/Fkt...: Arithmetische Subtraktion als NaLisp
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

namespace mko.NaLisp.MathOps
{
    public class SUB<T> : OpBase<T>
        where T : IComparable<T>
    {
        public SUB(IArithmetikOps<T> AOps, params T[] Operanden)
            : base(AOps, Operanden)
        { }

        public SUB(IArithmetikOps<T> AOps, params Core.INaLisp[] Operanden)
            : base(AOps, Operanden)
        { }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(this, EvaluatedElements, StackInstance, DebugOn, AOps.SUB());
        }

        public override Core.INaLisp Clone(bool deep = true)
        {
            if (deep)
                return new SUB<T>(AOps, Elements.Select(r => r.Clone()).ToArray());
            else
                return new SUB<T>(AOps, Elements);
        }
    }
}
