using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace mko.db
{
    public class ReturnCodeSqlException<TEnumStatus, TEnumError> : mko.db.SPAdapterTemplate<TEnumStatus, TEnumError>.ReturnCode
        where TEnumError: struct
        where TEnumStatus: struct
    {
        public int Schweregrad;
        public int Fehlernummer;
        public string msg;

        public ReturnCodeSqlException(SqlException sex, TEnumError ErrorCodeConnection)
            : base(ErrorCodeConnection)
        {
            Schweregrad = sex.Class;
            Fehlernummer = sex.Number;
            msg = sex.Message;
        }

        public override string Description
        {
            get
            {
                if (Success)
                    return base.Description;
                else
                {
                    return string.Format("{0}\nDetails: Schweregrad= {1:D}, Fehlernummer= {2:D}, Beschreibung= {3}", base.Description, Schweregrad, Fehlernummer, msg);
                }
            }
        }
    }
}
