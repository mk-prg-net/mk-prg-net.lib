//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 22.2.2016
//
//  Projekt.......: mko.NaLisp
//  Name..........: IFactory.cs
//  Aufgabe/Fkt...: Klassenfabriken für Vergleichsoperationen
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
    public interface IFactory<T>
        where T : IComparable<T>
    {
        ComparsionOps.Equal<T> EQ(Core.NaLisp Left, Core.NaLisp Right);

        ComparsionOps.GreaterThen<T> GT(Core.NaLisp Left, Core.NaLisp Right);

        ComparsionOps.GreaterEqualThen<T> GE(Core.NaLisp Left, Core.NaLisp Right);

        ComparsionOps.LowerThen<T> LT(Core.NaLisp Left, Core.NaLisp Right);

        ComparsionOps.LowerEqualThen<T> LE(Core.NaLisp Left, Core.NaLisp Right);

    }
}
