//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.7.2008
//
//  Projekt.......: babaros6Db
//  Name..........: SPAdapterTemplate
//  Aufgabe/Fkt...: Vorlage für Adapterklassen, mit denen gepsicherte Prozeduren
//                  auf einem SQL- Server aufgerufen werden können.
//                  Der grundlegende Ablauf des Aufrufes ist in der Methode execute   
//                  gekapselt. In abgeleiteten Klassen von SPAdapterTemplate kann die 
//                  Konfiguration des Aufrufes durch überschreiben der abstrakten Methode
//                  initSP(..) definiert werden. Der Aufruf selbst kann in der abgeleiteten Klasse
//                  durch Überschreiben von callSP(..) definiert werden.
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
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
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace mko.db
{
    public abstract class SPAdapterTemplate<TEnumStatus, TEnumError> 
        where TEnumStatus : struct
        where TEnumError : struct
    {
        public class ReturnCode : mko.ReturnCode<TEnumStatus, TEnumError>
        {
            public ReturnCode(TEnumStatus stat) : base(stat) { }
            public ReturnCode(TEnumError err) : base(err) { }
        }

        //---------------------------------------------------------------------------
        // Spezielle Statuscodes

        protected abstract TEnumStatus GetStatusCodeOk();
        protected ReturnCode CreateStatusOk()
        {
            _lastReturnCode = new ReturnCode(GetStatusCodeOk());
            return _lastReturnCode;
        }

        //---------------------------------------------------------------------------
        // Spezielle Fehlerklassen

        // 1) Fehler beim Verbindungsaufbau

        public class ReturnCodeErrConnection : ReturnCode
        {
            public int Schweregrad;
            public int Fehlernummer;
            public string msg;


            // Private Konstruktoren, denen in den Create- Classfactories die definierte
            // Fehlernummer für Verbindungsfehler übergeben werden
            internal ReturnCodeErrConnection(TEnumError ErrCodeConnection, string msg)
                : base(ErrCodeConnection)
            {
                Schweregrad = -1;
                Fehlernummer = -1;
                this.msg = msg;
            }

            internal ReturnCodeErrConnection(TEnumError ErrCodeConnection, SqlException sex)
                : base(ErrCodeConnection)
            {
                Schweregrad = sex.Class;
                Fehlernummer = sex.Number; 
                msg = sex.Message;
            }
        }

        // Gibt die in der abgeleiteten Klasse definierte definierte Fehlernummer 
        // für Verbindungsfehler zurück.
        protected abstract TEnumError GetErrCodeConnection();

        protected ReturnCodeErrConnection CreateReturnCodeErrConnection(string msg)
        {
            _lastReturnCode =  new ReturnCodeErrConnection(GetErrCodeConnection(), msg);
            return (ReturnCodeErrConnection)_lastReturnCode;
        }

        // 2) Allgemeiner Fehler

        public class ReturnCodeErrCommon : mko.db.ReturnCodeAllgemeinerFehler<TEnumStatus, TEnumError>
        {
            public ReturnCodeErrCommon(Exception ex, TEnumError errCode)
                : base(ex, errCode)
            {
            }
        }

        // Gibt die in der abgeleiteten Klasse definierte definierte Fehlernummer 
        // für Verbindungsfehler zurück.
        protected abstract TEnumError GetErrCodeCommon();

        protected ReturnCodeErrCommon CreateReturnCodeErrCommon(Exception ex)
        {
            _lastReturnCode = new ReturnCodeErrCommon(ex, GetErrCodeCommon());
            return (ReturnCodeErrCommon)_lastReturnCode;
        }

        // 3) Sql Fehler

        public class ReturnCodeErrSql : mko.db.ReturnCodeSqlException<TEnumStatus, TEnumError>
        {
            public ReturnCodeErrSql(SqlException sex, TEnumError errCode)
                : base(sex, errCode)
            {
            }
        }

        // Gibt die in der abgeleiteten Klasse definierte definierte Fehlernummer 
        // für Verbindungsfehler zurück.
        protected abstract TEnumError GetErrCodeSql();

        protected ReturnCodeErrSql CreateReturnCodeErrSql(SqlException sex)
        {
            _lastReturnCode= new ReturnCodeErrSql(sex, GetErrCodeSql());
            return (ReturnCodeErrSql)_lastReturnCode;
        }

        // 4) Xml Fehler

        public class ReturnCodeErrXml : mko.db.ReturnCodeXmlException<TEnumStatus, TEnumError>
        {
            public ReturnCodeErrXml(System.Xml.XmlException xex, TEnumError errCode)
                : base(xex, errCode)
            {
            }
        }

        // Gibt die in der abgeleiteten Klasse definierte definierte Fehlernummer 
        // für Verbindungsfehler zurück.
        protected abstract TEnumError GetErrCodeXml();

        protected ReturnCodeErrXml CreateReturnCodeErrXml(System.Xml.XmlException xex)
        {
            _lastReturnCode= new ReturnCodeErrXml(xex, GetErrCodeXml());
            return (ReturnCodeErrXml)_lastReturnCode;
        }       

        // Arbeitsschritte beim Ausführen einer StoredProcedure
        // 1. Initiaslisierung. Es ist nicht sichergestellt, daß die Verbindung geöffnet ist
        protected virtual ReturnCode initSP(SqlConnection conn)
        {
            return CreateStatusOk();
        }
        // 2. Aufruf der SP. Es ist sichergestellt, daß eine Verbindung geöffnet ist
        protected abstract ReturnCode callSP(SqlConnection conn);


        ReturnCode _lastReturnCode;
        public ReturnCode LastReturnCode
        {
            get
            {
                return _lastReturnCode;
            }
        }

        // Ausführung der StoredProcedure. Dabei werden die Arbeisschritte wie oben abgearbeitet 
        // In der abgeleiteten Klasse sollte ein für die SP- Parameter typisierte exec- Methode bereit-
        // gestellt werden, die diese exec Methode schließlich aufruft.
        protected ReturnCode exec(SqlConnection conn)
        {
            // Initialisierung
            _lastReturnCode = initSP(conn);
            if (!_lastReturnCode.Success)
                return _lastReturnCode;

            bool require_close_connection = false;
            try
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        require_close_connection = true;
                        conn.Open();
                    }
                }
                catch (SqlException sex)
                {
                    return CreateReturnCodeErrConnection(sex.Message);
                }
                catch (Exception ex)
                {
                    return CreateReturnCodeErrConnection(ex.Message);
                }

                // Ausführung
                _lastReturnCode= callSP(conn);
                return _lastReturnCode;
            }
            finally
            {
                if (require_close_connection)
                    conn.Close();
            }
        }
    }
}
