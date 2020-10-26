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
    /// 
    /// mko, 22.10.2018
    /// Inheritance changed. Previously RCV3sV inherited from RCV3. Now it 
    /// Inherits from RCV3WithValue
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInner"></typeparam>
    [DataContract]
    // public class RCV3sV<TValue> : RCV3<RCV3>, IRCV2, IValue<TValue>
    public class RCV3sV<TValue> : RCV3WithValue<RCV3, TValue>, IRCV2, IValue<TValue>
    {
        /// <summary>
        /// Indicates a successful function call.
        /// 
        /// mko, 14.2.2019
        /// Added Parameter caller. It determines the caller more save in async environments.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static new RCV3sV<TValue> Ok(TValue value, IDocuEntity Message = null, string User = "*", RCV3 inner = null, [CallerMemberName] string caller="")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //return new RCV3sV<TValue>(true, value, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
            return new RCV3sV<TValue>(true, value, DateTime.Now, User, assembly, cls, caller, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static new RCV3sV<TValue> Failed(TValue value, IDocuEntity ErrorDescription, string User = "*", RCV3 inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //return new RCV3sV<TValue>(false, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
            return new RCV3sV<TValue>(false, value, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }

        /// <summary>
        /// mko, 22.10.2018
        /// Erstellt einen RCV3sV parametrisch.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Succedeed"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="User"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RCV3sV<TValue> Create(TValue value, bool Succedeed = true, IDocuEntity ErrorDescription= null, string User = "*", RCV3 inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //return new RCV3sV<TValue>(Succedeed, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
            return new RCV3sV<TValue>(Succedeed, value, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }



        /// <summary>
        /// Constructor only for deserialization purpose.
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
        public RCV3sV(bool succeeded, TValue value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity Message, RCV3 inner)
            //: base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
            : base(succeeded, value, dat, User, Assembly, TypeName, FunctionName, Message, inner)
        {
            _value = value;
        }

        public RCV3sV(RCV3WithValue<RCV3, TValue> rc)
            //: base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
            : base(rc.Succeeded, rc.Value, rc.LogDate, rc.User, rc.AssemblyName, rc.TypeName, rc.FunctionName, rc.MessageEntity, rc.InnerRC_T)
        {            
        }

        public new RCV3sV<TValue> Clone()
        {
            return new RCV3sV<TValue>(Succeeded, Value, LogDate, User, AssemblyName, TypeName, FunctionName, pnL.txt(Message), _InnerRC);
        }

        public RCV3sV()
        {
        }

        //[DataMember(Name = "Value")]
        //TValue _value;

        //[Newtonsoft.Json.JsonIgnore]
        //public TValue Value => _value;

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
                                pnL.KillIf(Value == null, () => pnL.p("value", pnL.txt(Value?.ToString().Replace("#.", "").Replace("#", "").Replace("'", "")))),
                                pnL.KillIf(_InnerRC == null, () => pnL.p("inner", _InnerRC.ToPlx())));

            return de;
        }
    }
}
