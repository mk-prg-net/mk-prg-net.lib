//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine
//  Name..........: InputBase.cs
//  Aufgabe/Fkt...: Standard- Implementierung einer Input- Function
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

namespace MkPrgNet.Pattern.StateMachine
{
    /// <summary>
    /// Base class with default implementation of Name property
    /// </summary>
    public abstract class InputBase : IInput
    {
        public string Name
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// true, if the Input was set
        /// </summary>
        public abstract bool On
        {
            get;
        }

        /// <summary>
        /// Reset the input
        /// </summary>
        public abstract void Reset();
    }
}
