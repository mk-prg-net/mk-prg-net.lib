//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: InputBase.cs
//  Aufgabe/Fkt...: Basisimplementierung der IInuput- Schnitsttelle
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
    /// Basic implementation of IInput
    /// </summary>
    public class InputBase
    {
        public InputBase(int prio)
        {
            Priority = prio;
        }

        public bool On
        {
            get
            {
                return _on;
            }
        }
        protected bool _on = false;

        public int Priority
        {
            get;
        }

        public void Reset()
        {
            _on = false;
        }

    }
}
