//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: Inspector.Protocol.cs
//  Aufgabe/Fkt...: Klasse von Objekten, die Ergebnisse der Validierung von NaLisp- 
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
    public partial class Inspector
    {
        public class ProtocolEntry
        {
            public ProtocolEntry(ProtocolEntry pe)
            {
                _NaLispTreeNode = pe._NaLispTreeNode;
                _TypeOfEvaluated = pe.TypeOfEvaluated;
                _IsCurrentValid = pe.IsCurrentValid;
                _IsTreeValid = pe.IsTreeValid;
                _Description = pe.Description;
            }

            public ProtocolEntry(NaLisp Current, bool IsCurrentValid, bool IsTreeValid, Type TypeOfEvaluated)
            {
                _NaLispTreeNode = Current;
                _TypeOfEvaluated = TypeOfEvaluated;
                _IsCurrentValid = IsCurrentValid;
                _IsTreeValid = IsTreeValid;
                _Description = "";
            }

            public ProtocolEntry(NaLisp Current, bool IsCurrentValid, bool IsTreeValid, Type TypeOfEvaluated, string Description)
            {
                _NaLispTreeNode = Current;
                _TypeOfEvaluated = TypeOfEvaluated;
                _IsCurrentValid = IsCurrentValid;
                _IsTreeValid = IsTreeValid;
                _Description = Description;
            }

            public ProtocolEntry(NaLisp Current, bool IsCurrentValid, bool IsTreeValid, Type TypeOfEvaluated, string Description, IEnumerable<ProtocolEntry> ChildProtocolEntries)
            {
                _NaLispTreeNode = Current;
                _TypeOfEvaluated = TypeOfEvaluated;
                _IsCurrentValid = IsCurrentValid;
                _IsTreeValid = IsTreeValid;
                _Description = Description;

                this.ChildProtocolEntries = ChildProtocolEntries.ToArray();
            }


            public NaLisp NaLispTreeNode { get { return _NaLispTreeNode; } }
            NaLisp _NaLispTreeNode;

            public Type TypeOfEvaluated { get { return _TypeOfEvaluated; } }
            Type _TypeOfEvaluated;

            /// <summary>
            /// True, wenn das aktuelle Element gültig ist
            /// </summary>
            public bool IsCurrentValid { get { return _IsCurrentValid;} }
            bool _IsCurrentValid = false;

            /// <summary>
            /// True, wenn gesamter Baum unterhalb des aktuellen Elements gültig ist
            /// </summary>
            public bool IsTreeValid { get { return _IsTreeValid; } }
            bool _IsTreeValid = false;

            public string Description {
                get { return _Description;}
                set { _Description = value; }
            }
            string _Description;


            public ProtocolEntry[] ChildProtocolEntries;
        }
    }
}
