using System;
using System.Collections.Generic;
using System.Text;

namespace mko.db
{
    public class ReturnCodeAllgemeinerFehler<TEnumStatus, TEnumError> : mko.db.SPAdapterTemplate<TEnumStatus, TEnumError>.ReturnCode
        where TEnumError : struct
        where TEnumStatus : struct
    {
        public string msg;

        public ReturnCodeAllgemeinerFehler(Exception ex, TEnumError ErrorCodeAllgemein)
            : base(ErrorCodeAllgemein)
        {
            msg = ex.Message;
        }

        public override string Description
        {
            get
            {
                if (Success)
                    return base.Description;
                else
                {
                    return string.Format("{0}\nBeschreibung: {1}", base.Description, msg);
                }

            }
        }
    }
}
