//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.10.2015
//
//  Projekt.......: mko.NaLisp
//  Name..........: ConstValComp.cs
//  Aufgabe/Fkt...: NaLisp- Konstante, die einen bezüglich <, > und = vergleichbaren Wert speichert.
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
    public class ConstValComp<T> : ConstVal<T>, IConstValComp<T>
        where T : IComparable<T>
    {

        public ConstValComp(T Value)
            : base(Value)
        {}

        public int CompareTo(object obj)
        {
           var other = (ConstValComp<T>)obj;
            return this.Value.CompareTo(other.Value);
        }

        public int CompareTo(T other)
        {
            return this.Value.CompareTo(other);
        }
    }
}
