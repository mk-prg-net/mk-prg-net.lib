//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: ComposerRPNstrongTyped.cs
//  Aufgabe/Fkt...: Erzeugt streng typisierte Ausdrücke in polnischer Notation.
//                  Streng typisiert bedeutet dabei, dass Integer- und Double- Konstanten
//                  explizit als konstante Funktionen der Art .int 99 oder .dbl 30.1    
//                  definiert werden.
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

namespace mko.RPN
{
    public class ComposerRPNstrongTyped<Tfn> : Composer
        where Tfn : IFunctionNames
    {
        public ComposerRPNstrongTyped(Tfn fn) : base(fn.ListEnd)
        {
            this.fn = fn;
        }

        Tfn fn;

        public override string Bool(bool value)
        {
            return pn(fn.constBool, value.ToString());
        }


        public override string rBool(bool value)
        {
            return rpn(fn.constBool, value.ToString());
        }


        public override string Int(int value)
        {
            return pn(fn.constInt, value.ToString());
        }

        public override string rInt(int value)
        {
            return rpn(fn.constInt, value.ToString());
        }


        public override string Dbl(double value, int accuracy = -1)
        {
            if (accuracy >= 0)
            {
                return pn(fn.constDbl, value.ToString("N" + accuracy, pfmt));
            }
            else
            {
                return pn(fn.constDbl, value.ToString(pfmt));
            }
        }

        public override string rDbl(double value, int accuracy = -1)
        {
            if (accuracy >= 0)
            {
                return rpn(fn.constDbl, value.ToString("N" + accuracy, pfmt));
            }
            else
            {
                return rpn(fn.constDbl, value.ToString(pfmt));
            }
        }
    }
}
