//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: VariableOf.cs
//  Aufgabe/Fkt...: Variablen als NaLisp
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
//  Datum.........: 27.10.2015
//  Änderungen....: In eine generische Klasse umgewandelt
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Data
{
    public class VarOf<T> : Core.NaLispTerminal, IVarOf<T>
    {

        public VarOf(string VarName)
        { 
            _VarName = VarName; 
        }

        public VarOf(string VarName, T Value)
        {
            _VarName = VarName;
            this.Value = Value;
        }


        public string VarName { get { return _VarName; } }
        string _VarName;


        public T Value
        {
            get;
            set;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack)
        {
            return new Core.Inspector.ProtocolEntry(this, true, true, typeof(ConstVal<T>), "");
        }        


        public override Core.INaLisp Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new ConstVal<T>(Value);
        }

        public override Core.INaLisp Clone(bool deep = true)
        {
            return new VarOf<T>(VarName);
        }

        public override string ToString()
        {
            return "(" + Name + " " + VarName + " " + Value.ToString() + ")";
        }
    }
}
