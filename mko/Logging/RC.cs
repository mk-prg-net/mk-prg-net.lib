using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Logging
{
    /// <summary>
    /// mko, 2.11.2017
    /// Enhanced RC
    /// </summary>
    public class RC : ISucceeded, ITraceInfo
    {
        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RC Ok(string User = "*", string Message = "", RC inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(true, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RC Failed(string User = "*", string ErrorDescription = "", RC inner= null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(false, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
        }


        internal RC(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, RC inner)
        {
            _succeeded = succeeded;
            _User = User;
            _Assembly = Assembly;
            _TypeName = TypeName;
            _FunctionName = FunctionName;
            _Message = Message;
            _dat = dat;
            _innerRCV2 = inner;
        }


        bool _succeeded = false;
        DateTime _dat;
        string _User;
        string _Assembly;
        string _TypeName;
        string _FunctionName;
        string _Message;

        /// <summary>
        /// If true, then function call was successful
        /// </summary>
        public bool Succeeded => _succeeded;

        /// <summary>
        /// Date when function call ended.
        /// </summary>
        public DateTime LogDate => _dat;

        /// <summary>
        /// User, who calls the function
        /// </summary>
        public string User => _User;

        /// <summary>
        /// Assembly were function is defined
        /// </summary>
        public string AssemblyName => _Assembly;

        /// <summary>
        /// Type were function is definied
        /// </summary>
        public string TypeName => _TypeName;

        /// <summary>
        /// Name of function
        /// </summary>
        public string FunctionName => _FunctionName;

        public string Message => _Message;

        RC _innerRCV2 = null;
        public RC InnerRCV2 => _innerRCV2;


        public override string ToString()
        {
            return $"{StartTimeSingleton.TimeDifferenceToStartTimeInMs(LogDate).ToString("D9")} "
                  + $"{AssemblyName}.{TypeName}.{FunctionName} {(Succeeded ? "-> ok": "-> failed!")} "
                  + (!string.IsNullOrWhiteSpace(Message) ? $": {Message}" : "");
        }
    }

    public class RC<T> : RC, ISucceeded, ITraceInfo
    {
        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RC<T> Ok(T value, string User = "*", string Message = "", RC inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC<T>(true, value, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RC<T> Failed(T value, string User = "*", string ErrorDescription = "", RC inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC<T>(false, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
        }


        internal RC(bool succeeded, T value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, RC inner)
            : base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
        {
            _value = value;
        }

        T _value;

        public T Value => _value;

        public override string ToString()
        {
            return $"{StartTimeSingleton.TimeDifferenceToStartTimeInMs(LogDate).ToString("D9")} "
                  + $"{AssemblyName}.{TypeName}.{FunctionName} " + (Succeeded ? $"-> {Value}" : "-> failed!")
                  + (!string.IsNullOrWhiteSpace(Message) ? $": {Message}" : "");
        }
    }
}
