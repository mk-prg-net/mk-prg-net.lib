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
    /// <typeparam name="TInner"></typeparam>
    [DataContract]
    public class RCV3<TInner> : RCV3, IRCV2
        where TInner : class, IRCV2
    {
        [Newtonsoft.Json.JsonIgnore]
        public override IRCV2 InnerRCV2 => _InnerRC;

        [Newtonsoft.Json.JsonIgnore]
        public override bool HasInnerRC => InnerRCV2 != null;

        /// <summary>
        /// Strong typed Version of InnerRC
        /// </summary>         
        [Newtonsoft.Json.JsonIgnore]
        public TInner InnerRC_T => _InnerRC;


        [DataMember(Name ="InnerRC")]
        protected TInner _InnerRC;

        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV3<TInner> Ok(string Message = "", string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3<TInner>(true, DateTime.Now, User, assembly, cls, caller, pnL.txt(Message), inner);
        }

        public static RCV3<TInner> Ok(IDocuEntity docuEntity, string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3<TInner>(true, DateTime.Now, User, assembly, cls, caller, docuEntity, inner);
        }

        /// <summary>
        /// mko, 23.4.2018
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RCV3<TInner> Failed(string ErrorDescription = "", string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3<TInner>(false, DateTime.Now, User, assembly, cls, caller, pnL.txt(ErrorDescription), inner);
        }

        public static RCV3<TInner> Failed(IDocuEntity ErrorDescription, string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3<TInner>(false, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }


        /// <summary>
        /// mko, 04.05.2018
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public new static RCV3<TInner> Failed(Exception ex, string User = "*", [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            var fmt = new PNDocuTerms.DocuEntities.PNFormater();

            return new RCV3<TInner>(false, DateTime.Now, User, assembly, cls, caller, TraceHlp.FlattenExceptionMessagesPN(ex), null);
        }


        //internal RCV3(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, TInner inner)
        //    : base(succeeded, dat, User, Assembly, TypeName, FunctionName, pnL.txt(Message))
        //{
        //    _InnerRC = inner;
        //}

        internal RCV3(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity MessageEntity, TInner inner)
            : base(succeeded, dat, User, Assembly, TypeName, FunctionName, MessageEntity, inner)
        {

            _InnerRC = inner;
        }


        public new RCV3<TInner> Clone()
        {
            return new RCV3<TInner>(Succeeded, LogDate, User, AssemblyName, TypeName, FunctionName, pnL.txt(Message), _InnerRC);
        }

        public RCV3()
        {
        }

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
                                pnL.KillIf(_InnerRC == null, () => pnL.p("inner", _InnerRC.ToPlx())));

            return de;
        }
    }
}
