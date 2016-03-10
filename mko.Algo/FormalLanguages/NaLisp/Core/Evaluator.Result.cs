//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: Evaluatror.Result
//  Aufgabe/Fkt...: Klasse von Objekten, die Ergebnisse der Auswertung von NaLisp- 
//                  Ausdrücken enthalten.
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

namespace mko.Algo.FormalLanguages.NaLisp.Core
{
    public partial class Evaluator
    {
        public class Result
        {
            public Result(NaLisp ResultTerm, Inspector.ProtocolEntry ProtocolEntry)
            {
                _ResultTerm = ResultTerm;
                _ResultProtocolEntry = ProtocolEntry;
            }

            public bool Valid
            {
                get
                {
                    if (ResultTerm != null && ResultProtocolEntry != null)
                        return ResultProtocolEntry.IsCurrentValid;
                    else
                        return false;
                }
            }

            public NaLisp ResultTerm { get { return _ResultTerm; } }
            NaLisp _ResultTerm;

            public Inspector.ProtocolEntry ResultProtocolEntry { get { return _ResultProtocolEntry; } }
            Inspector.ProtocolEntry _ResultProtocolEntry;
        }
    }
}
