//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: VariableOf.cs
//  Aufgabe/Fkt...: Template für Basisklasse von Variablen
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
    public abstract class VariableOf<T> : VariableBase
    {
        protected VariableOf(Core.NameDir.Names Name, int ID)
            :base((int) Name, ID)
        { }

        public T Value
        {
            get;
            set;
        }

    }
}
