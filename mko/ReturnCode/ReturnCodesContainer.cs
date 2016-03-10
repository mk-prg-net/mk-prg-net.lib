using System;
using System.Collections.Generic;
using System.Text;

namespace mko.db
{
    public abstract class ReturnCodesContainer<TEnumStatus, TEnumError> : mko.db.SPAdapterTemplate<TEnumStatus, TEnumError>.ReturnCode
        where TEnumError : struct
        where TEnumStatus : struct
    {
        public ReturnCodesContainer(TEnumError ErrContainer)
            : base(ErrContainer)
        {
        }

        public List<object> ReturnCodesList = new List<object>();
        public bool HasReturnCodes
        {
            get
            {
                if (ReturnCodesList.Count > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
