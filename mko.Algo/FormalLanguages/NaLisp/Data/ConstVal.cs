//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: ConstVal.cs
//  Aufgabe/Fkt...: Basisklasse von Konstanten
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public abstract class ConstVal<T> : Core.NaLispTerminal
    {
        protected ConstVal(int Name, T Value) : base(Name)
        {
            _Value = Value;
        }

        T _Value;
        public T Value { get { return _Value; } }

        protected abstract Type GetType();


        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack)
        {
            return new Core.Inspector.ProtocolEntry(this, true, true, GetType(), "");
        }

        public override Core.Evaluator.Result Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            if (DebugOn)
                return new Core.Evaluator.Result(this, new Core.Inspector.ProtocolEntry(this, true, true, GetType(), Value.ToString()));
            else
                return new Core.Evaluator.Result(this, new Core.Inspector.ProtocolEntry(this, true, true, GetType(), ""));
        }

        public override string ToString()
        {
            return Core.NameDir.Get(this).Name + "(" + Value.ToString() + ")";
        }
    }
}
