//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.6.2016
//
//  Projekt.......: mko.RPN
//  Name..........: NofunctionToken.cs
//  Aufgabe/Fkt...: Basisklasse für Tokens, die keine Funktionen sind
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

namespace mko.RPN
{
    public abstract class NoFunctionToken : IToken
    {
        public NoFunctionToken(int CountOfEvaluatedTokens = 1)
        {            
            _CountOfEvaluatedTokens = CountOfEvaluatedTokens;
        }


        public bool IsFunctionName
        {
            get { return false; }
        }

        public abstract bool IsInteger
        {
            get;
        }

        public abstract bool IsBoolean
        {
            get;
        }

        public abstract bool IsNummeric
        {
            get;
        }

        public int CountOfEvaluatedTokens
        {
            get { return _CountOfEvaluatedTokens; }
        }
        int _CountOfEvaluatedTokens;

        public string Value
        {
            get { return ValueToString; }
        }

        /// <summary>
        /// Liefert den Wert in eine Stringdarstellung. Muss in der abgeleiteten Klasse implementiert werden
        /// </summary>
        protected abstract string ValueToString { get; }


        /// <summary>
        /// Erstellt eine Kopie des Tokens
        /// </summary>
        /// <returns></returns>
        public abstract IToken Copy();
    }
}
