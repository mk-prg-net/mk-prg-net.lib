//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 22.2.2016
//
//  Projekt.......: mko.NaLisp
//  Name..........: IConstValue.cs
//  Aufgabe/Fkt...: Schnittstelle eines Na- Lisp- Terms, der zu einer Konstante evaluiert wird
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

namespace mko.NaLisp.Data
{
    public interface IConstValue<T> : Core.ITerminal
    {
        /// <summary>
        /// Konstanter Wert, zu dem dieser Term evaluiert wird
        /// </summary>
        T Value { get; }
    }


    public interface IConstValueFactory<T>
    {
        IConstValue<T> Create();
        IConstValue<T> Create(T initVal);
    }
}
