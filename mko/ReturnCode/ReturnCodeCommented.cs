using System;
using System.Collections.Generic;
using System.Text;

namespace mko.db
{
    public class ReturnCodeCommented<TEnumStatus, TEnumError> : SPAdapterTemplate<TEnumStatus, TEnumError>.ReturnCode
        where TEnumError : struct
        where TEnumStatus : struct
    {

        string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
        }

        public ReturnCodeCommented(TEnumError errorReturnCode, string comment)
            : base(errorReturnCode)
        {
            _comment = comment;
        }

        public ReturnCodeCommented(TEnumStatus statusReturnCode, string comment)
            : base(statusReturnCode)
        {
            _comment = comment;
        }

        public override string Description
        {
            get
            {
                if (Success)
                    return base.Description;
                else
                {
                    return string.Format("{0}\nDetail: {1}", base.Description, _comment);
                }
            }
        }
    }
}
