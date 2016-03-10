using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko
{
    public class ExceptionHelper
    {
        public static string FlattenExceptionMessages(Exception ex)
        {
            if (ex != null)
            {
                string msg = ex.Message;
                Exception inner = ex.InnerException;
                while (inner != null)
                {
                    msg += "\n" + inner.GetType().Name + ": " + inner.Message;
                    inner = inner.InnerException;
                }

                return msg;
            } return "";
        }
    }
}
