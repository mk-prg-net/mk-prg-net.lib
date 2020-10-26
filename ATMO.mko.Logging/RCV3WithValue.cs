using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using mko.Logging;
using mko.RPN;

using System.Runtime.Serialization;

using System.Runtime.CompilerServices;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 25.7.2018
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInner"></typeparam>
    [DataContract]
    public class RCV3WithValue<TInner, TValue> : RCV3<TInner>, IRCV2, IValue<TValue>
        where TInner : class, IRCV2
        
    {
        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV3WithValue<TInner, TValue> Ok(TValue value, string Message = "", string User = "*", TInner inner = null, [CallerMemberName] string caller = "")            
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3WithValue<TInner, TValue>(true, value, DateTime.Now, User, assembly, cls, caller, pnL.txt(Message), inner);
        }

        public static RCV3WithValue<TInner, TValue> Ok(TValue value, IDocuEntity Message, string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3WithValue<TInner, TValue>(true, value, DateTime.Now, User, assembly, cls, caller, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RCV3WithValue<TInner, TValue> Failed(TValue value, string ErrorDescription = "", string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3WithValue<TInner, TValue>(false, value, DateTime.Now, User, assembly, cls, caller, pnL.txt(ErrorDescription), inner);
        }

        /// <summary>
        /// mko, 04.05.2018
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV3WithValue<TInner, TValue> Failed(TValue value, Exception ex, string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            var fmt = new PNDocuTerms.DocuEntities.PNFormater();

            return new RCV3WithValue<TInner, TValue>(false, value, DateTime.Now, User, assembly, cls, caller, TraceHlp.FlattenExceptionMessagesPN(ex), inner);
        }


        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RCV3WithValue<TInner, TValue> Failed(TValue value, IDocuEntity ErrorDescription, string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3WithValue<TInner, TValue>(false, value, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }


        /// <summary>
        /// mko, 25.7.2018
        /// Konstruktor only for deserialization purpose.
        /// </summary>
        /// <param name="succeeded"></param>
        /// <param name="value"></param>
        /// <param name="dat"></param>
        /// <param name="User"></param>
        /// <param name="Assembly"></param>
        /// <param name="TypeName"></param>
        /// <param name="FunctionName"></param>
        /// <param name="Message"></param>
        /// <param name="inner"></param>
        //[Newtonsoft.Json.JsonConstructor]
        //public RCV3WithValue(bool succeeded, TValue value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, TInner inner)
        //    : base(succeeded, dat, User, Assembly, TypeName, FunctionName, pnL.txt(Message), inner)
        //{
        //    _value = value;
        //}

        
        public RCV3WithValue(bool succeeded, TValue value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity Message, TInner inner)
            : base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
        {
            _value = value;
        }


        public new RCV3WithValue<TInner, TValue> Clone()
        {
            return new RCV3WithValue<TInner, TValue>(Succeeded, Value, LogDate, User, AssemblyName, TypeName, FunctionName, pnL.txt(Message), _InnerRC);
        }


        public RCV3WithValue()
        {
        }

        [DataMember(Name ="Value")]
        protected TValue _value;

        [Newtonsoft.Json.JsonIgnore]
        public TValue Value => _value;

        public override string ToString()
        {
            return ToPlx().ToString();
        }

        public override IDocuEntity ToPlx()
        {
            PNDocuTerms.DocuEntities.IDocuEntity de = null;

            // To leave the IRCV2 interface untouched type check for _InnerIRCv2 is needed 
            // Otherwise IRCV2 needs to be extended with IRCV2 InnerIRCv2 { get; } Property
            de = pnL.i($"{AssemblyName}.{TypeName}.{FunctionName}",
                                Succeeded ? pnL.eSucceeded() : pnL.eFails(),
                                pnL.KillIf(MessageEntity == null, () => pnL.p("msg", MessageEntity)),
                                pnL.KillIf(Value == null, () => pnL.p("value", pnL.txt(Value?.ToString().Replace("#", "").Replace("'", "")))),
                                pnL.KillIf(_InnerRC == null, () => pnL.p("inner", _InnerRC.ToPlx())));

            return de;
        }
    }
}
