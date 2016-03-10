//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: Inspector.cs
//  Aufgabe/Fkt...: Validiert NaLisp- Ausdrücke.
//                  Ein Inspector prüft einen NaLisp bezüglich seines korrekten statischen Aufbaus. 
//                  Inspector: {NaLisp} → {true, false}
//                  Für jeden Namen kann eine Validierungsfunktion registriert werden. Diese wird vom Inspector
//                  aufgerufen, wenn ein NaLisp zu überprüfen ist.
//                  Nur validierte NaLisp dürfen evaluiert werden.
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
    public partial class Inspector
    {

        /// <summary>
        /// Protokolliert die Interpretation eines TASP
        /// </summary>
        NaLispStack StackInstance = new NaLispStack();        

        /// <summary>
        /// Untersucht einen TASP- Term auf Fehlerfreiheit. Alle IsValid- Eigenschaften im TASP- Term werdengesetzt
        /// </summary>
        /// <param name="SubTerm"></param>
        /// <returns></returns>
        public ProtocolEntry Validate(NaLisp RootTerm)
        {
            StackInstance.Clear();            
            return ValidateR(RootTerm);
        }


        ProtocolEntry ValidateR(NaLisp SubTerm)
        {
            try
            {
                StackInstance.Push(SubTerm);

                if (SubTerm is ITerminal)
                {
                    return ((ITerminal)SubTerm).Validate(StackInstance);                    
                }
                else
                {
                    var NonTerm = (INonTerminal)SubTerm;
                    if (NonTerm.Elements.Any())
                    {
                        // Validieren der Listenelemente (Kinder)
                        ProtocolEntry[] ChildProtocolEntries = new ProtocolEntry[NonTerm.Elements.Length];
                        
                        for (int i = 0; i < ChildProtocolEntries.Length; i++)
                        {
                            ChildProtocolEntries[i] = ValidateR(NonTerm.Elements[i]);
                        }

                        // Validieren der Liste insgesamt
                        var res = NonTerm.Validate(StackInstance, ChildProtocolEntries);
                        res.ChildProtocolEntries = ChildProtocolEntries;
                        return res;
                    }
                    else
                        return new ProtocolEntry(SubTerm, false, false, null, "NonTerminal NaLisp enthält keine Kindelemente");
                }
            }
            finally
            {
                StackInstance.Pop();
            }
        }

    }
}
