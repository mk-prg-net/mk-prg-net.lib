using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


namespace mko.Bo
{
    public class StaticEnvironment
    {
        // Verwaltung der Verbindungszeichenfolge für die Geschäftsobjekte
        static string _boConnectionString;
        public static void SetBoConnectionString(string ConnectionString)
        {
            // Verbindungszeichenfolge prüfen und zuweisen
            SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder(ConnectionString);
            _boConnectionString = cb.ConnectionString;
        }

        public static string boConnectionString
        {
            get
            {
                return _boConnectionString;
            }
        }

        public static SqlConnectionStringBuilder boConnectionStringBuilder
        {
            get
            {
                return new SqlConnectionStringBuilder(_boConnectionString);
            }
        }


        // Verwalten des Log- Systems
        static mko.Log.LogServer _log;
        public static void SetBoLog(mko.Log.LogServer log)
        {
            _log = log;
        }

        public static mko.Log.LogServer log
        {
            get
            {
                return _log;
            }
        }

    }
}
