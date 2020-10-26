using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 22.11.2018
    /// Bezeichner für häufig verwendete Dokumentationsstrukturen.
    /// </summary>
    public partial class Composer
    {
        /// <summary>
        /// Liste allgemeiner Fachbegriffe für die Dokumentation von Rückgabewerten/Ergebnissen von 
        /// Funktionsaufrufen
        /// </summary>
        public static class TechTerms
        {

            /// <summary>
            /// Auftrag wurde erfolgreich fertig gestellt
            /// </summary>
            public const string eSucceeded = "succeeded";

            /// <summary>
            /// Auftrag wurde nicht fertig gestellt
            /// </summary>
            public const string eNotCompleted = "notCompleted";

            public const string eFails = "fails";

            public const string eInfo = "info";

            public const string eStart = "start";

            public const string eEnd = "end";

            public const string eWarn = "warn";

            /// <summary>
            /// mko, 22.11.2018
            /// Beschreibt den von einer Prozedur/Funktion am Ende erreichten Zustand. Wenn die 
            /// Funktion nicht die erwarteten Ergebnisse liefern konnte, werden hier z.B. die
            /// Ursachen beschrieben.
            /// </summary>
            public const string FinStateDescr = "FinStateDescr";

            //----------------------------------------------------------------------------------
            // Beschreibung der Resultate von Datenbankabfragen
            // Die Beschreibungen haben folgende Struktur:
            // #i query 
            //     #m exec #_ #ret #_ "eFails|eSucceeds" #. #. 
            //     #i result #_ #p count "Anzahl Datensätze" #.                                      

            /// <summary>
            /// mko, 22.11.2018
            /// Bezeichner der Instanz, welche Details zum ergebnis von Datebankabfragen enthält 
            /// </summary>        
            public const string iQuery = "query";

            /// <summary>
            /// Definiert innerhalb einer iQuery- Instanz den Bereich, über den Erfolg der Ausführung einer 
            /// Abfrage an sich informiert
            /// </summary>
            public const string mQueryExec = "exec";

            /// <summary>
            /// Definiert innerhalb eine 
            /// </summary>
            public const string iQueryResult = "result";
            public const string pQueryResultCount = "count";


            public class Development
            {
                public const string Version = "Ver";
            }


            /// <summary>
            /// Zahlen
            /// </summary>
            public class Numbers
            {
                /// <summary>
                /// Vorzeichen einer Zahl
                /// </summary>                
                public const string Signum = "sng";

                /// <summary>
                /// Definiert den Datentyp double
                /// </summary>
                public const string Dbl = "dbl";

                public const string Mantissa = "m";


                /// <summary>
                /// Exponent
                /// </summary>
                public const string Exp = "exp";


            }


            /// <summary>
            /// Metadaten zum Klassifizieren von Methodenaufrufen
            /// </summary>
            public class MetaData
            {
                /// <summary>
                /// Name einer Eigenschalft, welche z.B als  Methodenparameter den semantischen Kontext des Methodenaufrufes.
                /// </summary>

                public const string pSemCtx = "semCtx";

                /// <summary>
                /// Allgemeiner Bezeichner für eine Nachricht
                /// </summary>
                public const string pMsg = "Msg";

                /// <summary>
                /// Zusatzinformationen, allgemein verständlich
                /// </summary>
                public const string pInfoHR = "InfoHR";

                /// <summary>
                /// Allgemeiner bezeichner für ein Argument
                /// </summary>
                public const string Arg = "Arg";

                /// <summary>
                /// Allgemeiner Bezeichner für einen Wert
                /// </summary>
                public const string Val = "Val";

                /// <summary>
                /// Definiert einen anonymen Block von Eigenschaften und Methoden
                /// </summary>
                public const string Block = "block";

                /// <summary>
                /// Allg. Anzeige von Detail- Informationen zu einem 
                /// Ergebnis/Fehler
                /// </summary>
                public const string Details = "Details";

            }

            /// <summary>
            /// mko, 7.3.2019
            /// Allgemeine Statusbeschreibungen
            /// </summary>
            public class StateDescription
            {
                public const string Details = "Details";
                public const string WhatsUp = "whatsUp";
                public const string Why = "why";
            }


            public class StateMachine
            {
                public const string State = "State";
                public const string mStateTransistion = "StateTrans";
                public const string FromState = "fromState";
                public const string ToState = "toState";
                public const string Input = "Input";
                public const string Output = "Output";

            }
            /// <summary>
            /// mko, 22.2.2019
            /// Alle Bezeichner zum Thema Suche/Abfrage
            /// </summary>
            public class Search
            {
                /// <summary>
                /// mko, 22.11.2018
                /// Bezeichner der Instanz, welche Details zum ergebnis von Datebankabfragen enthält 
                /// </summary>        
                public const string mSearch = "search";

                /// <summary>
                /// filterausdrücke werden als instanz modeliert, 
                /// </summary>
                public const string iFilter = "SearchFilter";

                /// <summary>
                /// Wird nach einer id gefiltert, dann definiert dieser Parameter die ID
                /// </summary>
                public const string pId = "id";
            }


            public class PatternMatching
            {
                /// <summary>
                /// Definiert eine Methode, die prüft, ob ein Muster auf einen gegeben Text passt
                /// </summary>
                public const string mTestIfMatch = "testIfMatch";

                /// <summary>
                /// Definiert eine Wortmuster, wie es in SQL gebräuchlich ist.
                /// </summary>
                public const string pSqlLikeExpression = "SqlLike";

                /// <summary>
                /// Definiert ein Wortmuster als regulären Ausdruck
                /// </summary>
                public const string pRegularExpression = "RegEx";

            }

            public class RelationalOperators
            {
                public const string mEq = "eq";

                /// <summary>
                /// Lower or equal
                /// </summary>
                public const string mLtEq = "le";

                /// <summary>
                /// Lower than
                /// </summary>
                public const string mLt = "lt";


                /// <summary>
                /// Greater or equal
                /// </summary>
                public const string mGtEq = "ge";

                /// <summary>
                /// Greater then
                /// </summary>
                public const string mGt = "gt";


                /// <summary>
                /// not equal
                /// </summary>
                public const string mNotEq = "notEq";
            }

            public class BooleanOps
            {
                public const string valTrue = "True";
                public const string valFalse = "False";

                public const string mAnd = "and";
                public const string mOr = "or";
                public const string mNot = "not";
            }

            /// <summary>
            /// Allgemeine Mengenoperationen
            /// </summary>
            public class SetOparators
            {
                public const string mUnion = "union";
                public const string mIntersect = "intersect";
                public const string mComplement = "complement";
                public const string mDifference = "difference";
            }

            /// <summary>
            /// Allgemeine Mengen
            /// </summary>
            public class Sets
            {
                public const string semCtx = "Sets";

                public const string EmptySet = "EmptySet";
                public const string Power = "PowerSet";
                public const string CartesianProduct = "CartesianSet";

                /// <summary>
                /// Bereich, von...bis (implementierung einer Menge)
                /// </summary>
                public class Range
                {
                    public const string semCtx = "Range";
                    public const string Interval = "Interval";

                    public const string Begin = "Beg";
                    public const string End = "End";
                }
            }

            /// <summary>
            /// Prozesse und Entitäten, die an der Authentifizierung beteiligt sind
            /// </summary>
            public class Authentication
            {
                public const string semCtx = "Authentication";


                public const string pLoginStep = "LoginStep";
                public const string LoginStepAuthenticateUser = "IdentifyUserAsCustomerOrCoworker";
                public const string LoginStepGetCustomerGroups = "GetCustomerGroups";
                public const string LoginStepGetListOfCustomerGroups = "GetListOfAllCustomerGroups";

                public const string pUserClass = "UserClass";
                public const string UserClassCustomer = "Customer";
                public const string UserClassCoWorker = "CoWorker";

                public const string UserId = "UserId";

                public const string Login = "login";
                public const string Logout = "logout";
                public const string pPassword = "Password";

                public const string Group = "Group";
                public const string Role = "Role";

                public const string mGetRolesUserIsMemeber = "GetRolesWhereUserIsMember";
                public const string mIsMemberOfRole = "IsMemberOfRole";
            }

            public class Authorzation
            {
                public const string semCtx = "Authorization";

                public const string allowed = "allowed";
                public const string denied = "denied";
            }


            /// <summary>
            /// Kommandos zum Steuern der Ausführung von Programmen und Prozessen
            /// </summary>
            public class Execution
            {
                public const string semCtx = "Execution";

                public const string mExecute = "exec";

                public const string mStart = "start";
                public const string mBreak = "break";
                public const string mContinue = "continue";
                public const string mStop = "stop";
                public const string mAbort = "abort";
                public const string mEnd = "end";

                /// <summary>
                /// Starten eines Unterprogrammes
                /// </summary>
                public const string mCallSub = "callSub";

            }

            public class Validation
            {
                public const string semCtx = "Validation";

                public const string mValidate = "validate";

                /// <summary>
                /// Vorbedingungen, die für die erfolgreiche Ausführung einer Funktion erfüllt sein 
                /// </summary>
                public const string iPreCondition = "PreCondition";

                /// <summary>
                /// Nachbedingungen, die das Ergebnis/der Rückgabewert einer Fuktion
                /// erfüllen müssen
                /// </summary>
                public const string iPostCondition = "PostCondition";

                /// <summary>
                /// Prüft, ob ein Wert sich innerhalb eines Definitionsbereiches befindet
                /// </summary>
                public const string mCheckIfValueInRange = "checkIfValueInRange";
            }

            public class Access
            {
                public const string semCtx = "Access";

                //------------------------------------------------------------
                // Bezeichnung von Datenquellen/Verbindungen

                public const string DataSource = "Src";


                //---------------------------------------------------------------
                // allgemeine Bezeichnung von  Zugriffsoperationen (spezielle Methodennamen)

                /// <summary>
                /// Starten einer Zugriffstransaktion auf einem Objekt
                /// </summary>
                public const string open = "open";

                /// <summary>
                /// Beenden einer Zugriffstransaktion auf einem Objket
                /// </summary>
                public const string close = "close";

                /// <summary>
                /// Löschen eines Objektes/Datensatzes
                /// </summary>
                public const string delete = "del";

                /// <summary>
                /// Verschieben eines Datensatzes/Objektes von einer Datenquelle in eine andere
                /// </summary>
                public const string move = "mov";

                /// <summary>
                /// Lesen von Daten aus den Feldern eines Datensatzes/den eigenschaften eines Objektes
                /// </summary>
                public const string read = "read";

                /// <summary>
                /// Operation schreiben/erzeugen eines neuen Datensatzes/Objketes
                /// </summary>
                public const string write = "write";

                /// <summary>
                /// Operation Objekt holen aus einem Speicher/Quelle
                /// </summary>
                public const string fetch = "fetch";

                /// <summary>
                /// Statusänderung auf einem Datensatz/Objekt
                /// </summary>
                public const string StatusChange = "StatChg";

                /// <summary>
                /// Detail einer Statusänderung: ursprünglicher Status
                /// </summary>
                public const string FromStatus = "FromState";

                /// <summary>
                /// Detail einer Statusänderung: neuer Status
                /// </summary>
                public const string ToState = "ToState";
            }

            /// <summary>
            /// Prozesse und Objekte im Kontext von Speichermedien.
            /// </summary>
            public class Storage
            {
                public const string semCtx = "Storage";

                //---------------------------------------------------------------
                // Allgemeine Speicherobjekte

                public const string File = "File";
                public const string Folder = "Folder";
                public const string DataRow = "DataRow";
                public const string DataField = "DataField";

                /// <summary>
                /// Bitte nicht mehr verwenden ! Stattdessen Set.EmptySet.
                /// </summary>
                public const string EmptySet = "empty";
            }

            public class Network
            {
                public const string semCtx = "Network";

                public const string Url = "Url";
                public const string IP4Adr = "IP4Adr";
                public const string IP6Adr = "IP6Adr";

                public const string TCPPort = "TCPPort";
                public const string UDPPort = "UDPPort";
            }

            public class ActiveDirectory
            {
                public const string semCtx = "ActiveDirectory";

                public const string Domain = "Domain";
                public const string DNSDomainName = "DomainFQN";

                /// <summary>
                /// GC= global catalog
                /// </summary>
                public const string GC = "GC";

                public const string ComputerName = "Computer";
                public const string UserId = "UserId";
                public const string UserIdFQN = "UserIdFQN";


                /// <summary>
                /// Universal ressource name of a file share
                /// </summary>
                public const string FileShareURN = "FileShare";
            }


            /// <summary>
            /// Allgemeine Begriffe der Organisation ATMO
            /// </summary>
            public class ATMO
            {
                /// <summary>
                /// ATMO- Standort
                /// </summary>
                public const string Site = "Site";

            }

            public class Dfc
            {
                public const string semCtx = "DFC";

                /// <summary>
                /// SAP- Klassifizierung von Dokumente
                /// </summary>
                public const string SAPDocType = "DocType";

                public const string DFC2Name = "DFC2";

                //---------------------------------------------------------------
                // Fachbegriffe zu Stücklisten und Stücklistenstrukturen

                /// <summary>
                /// Bill of material
                /// </summary>
                public const string BOM = "BOM";

                /// <summary>
                /// Projekt- Stationsnummer
                /// </summary>
                public const string PSPNo = "PSPNo";

                /// <summary>
                /// Materialnummer
                /// </summary>
                public const string MatNo = "MatNo";

                /// <summary>
                /// DFC- Interne eindeutige ID für ein Dokument.
                /// </summary>
                public const string DocId = "DocId";

                /// <summary>
                /// Baugruppe
                /// </summary>
                public const string Assembly = "Assembly";

                /// <summary>
                /// Einzelteil
                /// </summary>
                public const string SingelPart = "SinglePart";

                /// <summary>
                /// Atmo- Projekt
                /// </summary>
                public const string Project = "Project";

                /// <summary>
                ///  Station
                /// </summary>
                public const string Station = "Station";


                /// <summary>
                /// Document- Knoten
                /// </summary>
                public const string Document = "Doc";

                /// <summary>
                /// Sicherheitsmerkmale eines Dokumentes, die bei der Entscheidung über den Zugriff auf ein Dokument herangezogen werden
                /// </summary>
                public const string DocSecurityFeatures = "DocSecFeatures";

                /// <summary>
                /// Bezeichnet den DFC Dokumententyp aus Sicht der Zugriffsberechtigungen (DFCSecurity).
                /// Ein SAP Typ wie ATD kann auf Ebene der Zugriffssteuerung zu einem Typen wie ATD_pub umgewandelt werden.
                /// </summary>
                public const string DfcSecurityDocType = "DfcSecurityDocType";

                /// <summary>
                /// 
                /// </summary>
                public const string ZAT = "ZAT";



                public class DokuCheck
                {
                    public const string MissingDrawing = "missing drawing";
                }

            }

            public class Runtime
            {
                public const string Session = "Session";
                public const string SessionId = "SessionId";
                public const string Environment = "RuntimeEnvironment";
            }
        }
    }
}
