//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.4.2017
//
//  Projekt.......: KeplerBI.Parser.RPN
//  Name..........: Composer.cs
//  Aufgabe/Fkt...: Erzeugt RPN Ausdrücke auf Basis von IFunctionNames
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
//  Datum.........: 7.5.2017
//  Änderungen....: Int, Bool, Dbl virtuell gemacht. Ergänzt um rInt, rBool und rDbl
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
    public class Composer
    {

        //protected readonly Tfn fn;
        protected readonly IFormatProvider pfmt;

        /// <summary>
        /// Namensliste der RPN Basisfunktionen
        /// </summary>
        public IFunctionNames fn { get; }

        public Composer(IFunctionNames fn) //(Tfn fn)
        {
            this.fn = fn;
            pfmt = BasicTokenizer.rpnCult;
        }


        public virtual string Bool(bool value)
        {
            return pn(fn.constBool, value.ToString());
        }


        public virtual string rBool(bool value)
        {
            return rpn(fn.constBool, value.ToString());
        }


        public virtual string Int(int value)
        {
            return pn(fn.constInt, value.ToString());
        }

        public virtual string rInt(int value)
        {
            return rpn(fn.constInt, value.ToString());
        }


        public virtual string Dbl(double value, int accuracy = -1)
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

        public virtual string rDbl(double value, int accuracy = -1)
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


        public virtual string Str(string value)
        {
            return pn(fn.constStr, value);
        }

        public virtual string rStr(string value)
        {
            return rpn(fn.constStr, value);
        }


        // Unäre Funktionen erzeugen
        protected const string pnUnFmt = "{0:s} {1:s}";
        protected const string rpnUnFmt = "{1:s} {0:s}";

        /// <summary>
        /// Erzeugt einen unäre Funktion in polnischer Notation
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public string pn(string fName, string p1)
        {
            if (string.IsNullOrEmpty(fName))
                return p1;
            else 
                return string.Format(pnUnFmt, fName, p1);
        }

        /// <summary>
        /// Erzeugt eine unäre Funktion in revers polnischer Notation
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public string rpn(string fName, string p1)
        {
            if (string.IsNullOrEmpty(fName))
                return p1;
            else
                return string.Format(rpnUnFmt, fName, p1);
        }

        // binäre Funktionen erzeugen
        protected const string pnBinFmt = "{0:s} {1:s} {2:s}";
        protected const string rpnBinFmt = "{2:s} {1:s} {0:s}";

        /// <summary>
        /// Erzeugt einen binäre Funktion in polnischer Notation
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public string pn(string fName, string p1, string p2)
        {
            return string.Format(pnBinFmt, fName, p1, p2);
        }

        /// <summary>
        /// Erzeugt eine binäre Funktion in revers polnischer Notation
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public string rpn(string fName, string p1, string p2)
        {
            return string.Format(rpnBinFmt, fName, p1, p2);
        }

        // ternäre Funktionen erzeugen
        protected const string pnTernaryFmt = "{0:s} {1:s} {2:s} {3:s}";
        protected const string rpnTernaryFmt = "{3:s} {2:s} {1:s} {0:s}";

        /// <summary>
        /// Erzeugt einen ternäre Funktion in polnischer Notation
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public string pn(string fName, string p1, string p2, string p3)
        {
            return string.Format(pnTernaryFmt, fName, p1, p2, p3);
        }

        /// <summary>
        /// Erzeugt eine ternäre Funktion in revers polnischer Notation
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public string rpn(string fName, string p1, string p2, string p3)
        {
            return string.Format(rpnTernaryFmt, fName, p1, p2, p3);
        }


        // quaternäre Funktionen erzeugen
        protected const string pnQuaternanaryFmt = "{0:s} {1:s} {2:s} {3:s} {4:s}";
        protected const string rpnQuaternaryFmt = "{4:s} {3:s} {2:s} {1:s} {0:s}";

        /// <summary>
        /// Erzeugt einen quaternäre Funktion in polnischer Notation
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public string pn(string fName, string p1, string p2, string p3, string p4)
        {
            return string.Format(pnQuaternanaryFmt, fName, p1, p2, p3, p4);
        }

        /// <summary>
        /// Erzeugt eine quaternäre Funktion in revers polnischer Notation
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public string rpn(string fName, string p1, string p2, string p3, string p4)
        {
            return string.Format(rpnQuaternaryFmt, fName, p1, p2, p3, p4);
        }

        /// <summary>
        /// Erzeugt eine vsriadische Funktion (n Parameter, n belibig).
        /// Die Parameterliste wird durch ein Listeende- Symbol begrenzt.
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public string pnL(string fName, params string[] p)
        {
            var bld = new StringBuilder();
            bld.Append(fName);

            for (int i = 0, c = p.Length; i < c; i++)
            {
                bld.Append(" ");
                bld.Append(p[i]);
            }

            bld.Append(" ");
            bld.Append(fn.ListEnd);

            return bld.ToString();

        }


        /// <summary>
        /// Erzeugt eine vsriadische Funktion (n Parameter, n belibig).
        /// Die Parameterliste wird durch ein Listeende- Symbol begrenzt.
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public string rpnL(string fName, params string[] p)
        {
            var bld = new StringBuilder();

            bld.Append(fn.ListEnd);


            for (int i = p.Length - 1; i >= 0; i--)
            {
                bld.Append(" ");
                bld.Append(p[i]);
            }

            bld.Append(" ");
            bld.Append(fName);


            return bld.ToString();

        }

        /// <summary>
        /// Kombiniert mehrere Ausdrücke in polnischer Notation in einer Zeile
        /// </summary>
        /// <param name="pns"></param>
        /// <returns></returns>
        public string combinePn(params string[] pns)
        {
            var bld = new StringBuilder();
            for (int i = 0, len = pns.Length; i < len; i++)
            {
                bld.Append(" ");
                bld.Append(pns[i].Trim());
            }

            return bld.ToString().Trim();
        }

        public string combineRpn(params string[] pns)
        {
            var bld = new StringBuilder();
            for (int i = pns.Length - 1; i >= 0; i--)
            {
                bld.Append(" ");
                bld.Append(pns[i].Trim());
            }

            return bld.ToString().Trim();
        }



    }
}
