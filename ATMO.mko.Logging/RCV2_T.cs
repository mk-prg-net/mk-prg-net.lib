using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using mko.Logging;
using mko.RPN;

using System.Runtime.Serialization;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 2.11.2017
    /// Common structured return value for functions. In addition to RCV2 is returns a Result in the 
    /// generic value property.
    /// mko, 04.06.2018
    /// Class- hirarchiy of RCV2/RCV2(T) deleted. Implementation of RCV2 and RCV2(T) now are completly independent.
    /// </summary>
    /// <typeparam name="T"></typeparam>   
    [DataContract]
    public class RCV2<T> : IRCV2, IRCV2<T>
    {
        internal static PNDocuTerms.DocuEntities.Composer pnL = new PNDocuTerms.DocuEntities.Composer();
        internal static PNDocuTerms.DocuEntities.PNFormater fmt = new PNDocuTerms.DocuEntities.PNFormater();

        bool _Succeeded;
        string _User;
        string _AssemblyName;
        string _TypeName;
        string _FunctionName;
        string _Message;
        internal PNDocuTerms.DocuEntities.IDocuEntity _MessageEntity = null;
        DateTime _LogDate;

        /// <summary>
        /// If true, then function call was successful
        /// </summary>
        [DataMember]
        public bool Succeeded => _Succeeded;

        /// <summary>
        /// Date when function call ended.
        /// </summary>
        [DataMember]
        public DateTime LogDate => _LogDate;

        /// <summary>
        /// User, who calls the function
        /// </summary>
        [DataMember]
        public string User => _User;

        /// <summary>
        /// Assembly were function is defined
        /// </summary>
        [DataMember]
        public string AssemblyName => _AssemblyName;

        /// <summary>
        /// Type were function is definied
        /// </summary>
        [DataMember]
        public string TypeName => _TypeName;

        /// <summary>
        /// Name of function
        /// </summary>
        [DataMember]
        public string FunctionName => _FunctionName;

        [DataMember]
        public string Message => _Message;

        [DataMember]
        public IRCV2 InnerRCV2 => _InnerIRCV2;
        IRCV2 _InnerIRCV2;



        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV2<Ty> Ok<Ty>(Ty value, string User = "*", string Message = "", IRCV2 inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2<Ty>(true, value, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RCV2<T> Failed(T value, string User = "*", string ErrorDescription = "", IRCV2 inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2<T>(false, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
        }


        /// <summary>
        /// mko, 23.4.2018
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RCV2<T> Failed(string User = "*", string ErrorDescription = "", IRCV2 inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2<T>(false, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);

        }

        /// <summary>
        /// mko, 04.05.2018
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV2<T> Failed(Exception ex, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            var fmt = new PNDocuTerms.DocuEntities.PNFormater();

            return new RCV2<T>(false, DateTime.Now, User, assembly, cls, mth.Name, TraceHlp.FlattenExceptionMessages(ex), null);
        }


        internal RCV2(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, IRCV2 inner)
        {
            _Succeeded = succeeded;
            _User = User;
            _AssemblyName = Assembly;
            _TypeName = TypeName;
            _FunctionName = FunctionName;
            _Message = Message;
            _LogDate = dat;
            _InnerIRCV2 = inner;
        }




        [Newtonsoft.Json.JsonConstructor]
        public RCV2(bool succeeded, T value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, IRCV2 inner)
            : this(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
        {
            _value = value;
        }



        public RCV2(mko.Logging.IRCV2<T> mkoRC)
            : this(mkoRC.Succeeded, mkoRC.LogDate, mkoRC.User, mkoRC.AssemblyName, mkoRC.TypeName, mkoRC.FunctionName, mkoRC.Message, null)
        { }

        public RCV2(RC<ParserV2.Result> mkoRC)
            : this(mkoRC.Succeeded, mkoRC.LogDate, mkoRC.User, mkoRC.AssemblyName, mkoRC.TypeName, mkoRC.FunctionName, mkoRC.Message, null)
        { }

        public static RCV2<T> CreateFrom<Tin>(RCV2<Tin> rcv2In)
            where Tin : T
        {
            return new RCV2<T>(rcv2In.Succeeded, rcv2In.Value, rcv2In.LogDate, rcv2In.User, rcv2In.AssemblyName, rcv2In.TypeName, rcv2In.FunctionName, rcv2In.Message, rcv2In.InnerRCV2);
        }

        T _value;

        [DataMember]
        public T Value => _value;

        [DataMember]
        public IDocuEntity MessageEntity => _MessageEntity;

        public override string ToString()
        {
            return ToPlx().ToString();
        }

        public PNDocuTerms.DocuEntities.IDocuEntity ToPlx()
        {
            PNDocuTerms.DocuEntities.IDocuEntity de = null;

            // To leave the IRCV2 interface untouched type check for _InnerIRCv2 is needed 
            // Otherwise IRCV2 needs to be extended with IRCV2 InnerIRCv2 { get; } Property
            de = pnL.i($"{AssemblyName}.{TypeName}.{FunctionName}",
                                Succeeded ? pnL.eSucceeded() : pnL.eFails(),
                                pnL.KillIfNot(!string.IsNullOrWhiteSpace(Message), () => pnL.p("msg", pnL.txt(Message.Replace("#", "")))),
                                pnL.KillIfNot(Value != null, () => pnL.p("value", pnL.txt(Value?.ToString().Replace("#", "").Replace("'", "")))),
                                pnL.KillIfNot(InnerRCV2 != null && (InnerRCV2 is IRCV2), () => pnL.p("inner", InnerRCV2.ToPlx())));

            return de;
        }
    }
}
