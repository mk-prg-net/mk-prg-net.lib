using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko
{
    public class TraceHlp
    {
        static string MsgsToString(string[] msgs)
        {
            string res = "";
            foreach (string msg in msgs)
            {
                res += "\"" + msg + "\" ";
            }
            return res;
        }

        public static string FormatErrMsg(object Obj, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " ERR!  " + Obj.GetType().FullName + "." + MethodName + " " + MsgsToString(msgs);
        }

        public static string FormatErrMsg(string ClassName, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " ERR!  " + ClassName + "." + MethodName + " " + MsgsToString(msgs);
        }


        public static string FormatWarningMsg(object Obj, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " WARN! " + Obj.GetType().FullName + "." + MethodName + " " + MsgsToString(msgs);
        }

        public static string FormatWarningMsg(string ClassName, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " WARN! " + ClassName + "." + MethodName + " " + MsgsToString(msgs);
        }


        public static string FormatInfoMsg(object Obj, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " INFO  " + Obj.GetType().FullName + "." + MethodName + " " + MsgsToString(msgs);
        }

        public static string FormatInfoMsg(string ClassName, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " INFO  " + ClassName + "." + MethodName + " " + MsgsToString(msgs);
        }

        


    }

}
