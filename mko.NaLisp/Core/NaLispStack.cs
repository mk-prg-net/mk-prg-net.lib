//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: NaLispStack
//  Aufgabe/Fkt...: Stapelspeicher für die Analyse und Auswertung von NaLisp
//                  Ausdrücken
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 1.4.2014
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

namespace mko.NaLisp.Core
{
    public class NaLispStack : Stack<INaLisp>
    {
        /// <summary>
        /// Prüft, ob der Rufer Kindelement eines Tasp mit Namen Name ist.
        /// </summary>
        /// <param name="sym"></param>
        /// <returns></returns>
        public bool ParentIs(Type type)
        {
            if (Count >= 2)
            {
                INaLisp BackUp = Pop();
                try
                {
                    if (Peek().GetType() == type)
                        return true;
                }
                finally
                {
                    Push(BackUp);
                }
            }
            return false;
        }

    }
}
