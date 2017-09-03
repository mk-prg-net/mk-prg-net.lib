//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: Error.cs
//  Aufgabe/Fkt...: Diese Eingabe dokumentiert Fehler
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

namespace MkPrgNet.Pattern.Automaton
{
    /// <summary>
    /// Input, that decribes errors
    /// </summary>
    public class Error : InputBase, IInput
    {

        /// <summary>
        /// per default Error has the highest priority
        /// </summary>
        public Error(): base(int.MaxValue) { }


        /// <summary>
        /// Constructor of Inputtype Error
        /// </summary>
        /// <param ="priority">Priority of input</param>
        public Error(int priority) : base(priority) { }

        public string Description
        {
            get
            {
                return _descr;
            }
        }
        string _descr;

        public void describeError(string descr)
        {
            _descr = descr;
        }
    }
}
