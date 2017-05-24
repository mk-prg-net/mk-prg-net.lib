//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.6.2016
//
//  Projekt.......: mko.RPN.Arithmetik
//  Name..........: BasicOperators.cs
//  Aufgabe/Fkt...: Implementierung der Grundrechenarten
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 6.4.2017
//  Änderungen....: Tokenbuffer entfernt
//
//</unit_history>
//</unit_header>        


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.Arithmetik
{
    public class Add : NumBinOp
    {
        protected override double Func(double a, double b)
        {
            return a + b;
        }
    }

    public class Sub : NumBinOp
    {
        protected override double Func(double a, double b)
        {
            // Reihenfolge der Operanden vertauscht, da RPN die Operanden "rückwärts" einliest
            return a- b;
        }
    }

    public class Mul : NumBinOp
    {
        protected override double Func(double a, double b)
        {
            return a * b;
        }
    }

    public class Div : NumBinOp
    {
        protected override double Func(double a, double b)
        {
            // Reihenfolge der Operanden vertauscht, da RPN die Operanden "rückwärts" einliest
            return a / b;
        }
    }
}
