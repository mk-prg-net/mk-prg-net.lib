//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 6.5.017
//
//  Projekt.......: mko.RPN
//  Name..........: SetPropertyBatch.cs
//  Aufgabe/Fkt...: Speichert einen Konfigurationsbefehl für eine Eigenschaft eines Objektes vom
//                  Typ T. Der Konfigurationsbefehl wird vom Evaluator durch vorausgegangene 
//                  Auswertung von RPN- BEfehlen ermittelt.
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
//  Datum.........: 20.5.2017
//  Änderungen....: In direkt von FunctionNameToken abgeleitete Klasse umgewandelt
//                  (vorher: DerivedToken)
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    public class SetPropertyBatchCmd<T> : FunctionNameToken        
    {
        public SetPropertyBatchCmd(string PropName, Action<T> SetAction, int CountOfEvaluatedTokens = 1)
            : base(string.Format("SetProp.{0:s}.{1:s}", typeof(T).GetType().Name, PropName), CountOfEvaluatedTokens)
        {
            this.SetAction = SetAction;
        }

        public Action<T> SetAction { get; }
    }
}
