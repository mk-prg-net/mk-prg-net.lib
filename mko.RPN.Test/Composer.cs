//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 3.5.2017
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
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

namespace mko.RPN.Test
{
    public class Composer : mko.RPN.Composer
    {

        public Composer(mko.RPN.IFunctionNames fn)
            : base(fn) { }

        public string Trio(int a, int b, int c)
        {
            return pn(".trio", Int(a), Int(b), Int(c));
        }

        public string Quattro(int a, int b, int c, int d)
        {
            return pn(".quattro", Int(a), Int(b), Int(c), Int(d));
        }

        public string IntList(params int[] elems)
        {
            return pnL(".int.list", elems.Select(el => Int(el)).ToArray());
        }

        public string VectorProdukt(string v1, string v2)
        {
            return pn(".vec.prod", v1, v2);
        }


        public string rTrio(int a, int b, int c)
        {
            return rpn(".trio", rInt(a), rInt(b), rInt(c));
        }

        public string rQuattro(int a, int b, int c, int d)
        {
            return rpn(".quattro", rInt(a), rInt(b), rInt(c), rInt(d));
        }

        public string rIntList(params int[] elems)
        {
            return rpnL(".int.list", elems.Select(el => rInt(el)).ToArray());
        }

        public string rVectorProdukt(string v1, string v2)
        {
            return rpn(".vec.prod", v1, v2);
        }


    }

}
