//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: VariableBase.cs
//  Aufgabe/Fkt...: Basisklasse von terminalen NaLisp- Ausdrücken, die Variablen darstellen
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
//  Version 1.0...: 1.4.2014
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
    public abstract class VariableBase : Core.NaLispTerminal
    {
        public VariableBase(int Name, int ID)
            : base(Name)
        { _ID = ID; }

        public int ID { get { return _ID; } }
        int _ID;

        /// <summary>
        ///  Polymorphe Set- Funktionen zum setzen neuer Werte. Einsetzen in Fällen, wo die Variablen in 
        ///  Listen vom Typ VariableBase verwaltet werden.
        /// </summary>
        /// <param name="newValue"></param>
        public abstract void SetValue(bool newValue);
        public abstract void SetValue(int newValue);
        public abstract void SetValue(double newValue);
        public abstract void SetValue(string newValue);
        public abstract void SetValue(DateTime newValue);


        protected abstract Type GetResultConstType();

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack)
        {
            return new Core.Inspector.ProtocolEntry(this, true, true, GetResultConstType(), "");
        }        

    }
}
