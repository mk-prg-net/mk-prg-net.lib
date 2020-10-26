using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using mko.Logging;
using mko.RPN;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 2.11.2017
    /// Common strutured return value for functions.
    /// </summary>
    [DataContract]
    public class RCV2 : IRCV2
    {

        internal static PNDocuTerms.DocuEntities.Composer pnL = new PNDocuTerms.DocuEntities.Composer();
        internal static PNDocuTerms.DocuEntities.PNFormater fmt = new PNDocuTerms.DocuEntities.PNFormater();

        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV2 Ok(string User = "*", string Message = "", IRCV2 inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2(true, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docuEntity"></param>
        /// <param name="User"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RCV2 Ok(PNDocuTerms.DocuEntities.IDocuEntity docuEntity, string User = "*", IRCV2 inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2(true, DateTime.Now, User, assembly, cls, mth.Name, docuEntity, inner);
        }


        ///// <summary>
        ///// mko, 4.6.2018
        ///// Creates a strong typed return code with a value. This class factory is syntatic sugar.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <param name="User"></param>
        ///// <returns></returns>
        //public static RCV2<T> Ok<T>(T value, string User = "*")
        //{
        //    var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
        //    var cls = mth.ReflectedType.Name;
        //    var assembly = mth.ReflectedType.Assembly.GetName().Name;

        //    return RCV2<T>.Ok(value, User);
        //}

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RCV2 Failed(string User = "*", string ErrorDescription = "", IRCV2 inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2(false, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
        }

        public static RCV2 Failed(PNDocuTerms.DocuEntities.IDocuEntity docuEntity, string User = "*", IRCV2 inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2(false, DateTime.Now, User, assembly, cls, mth.Name, docuEntity, inner);
        }

        public static RCV2 Failed(Exception ex, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            var fmt = new PNDocuTerms.DocuEntities.PNFormater();

            return new RCV2(false, DateTime.Now, User, assembly, cls, mth.Name, TraceHlp.FlattenExceptionMessages(ex), null);
        }




        bool _Succeeded;
        string _User;
        string _AssemblyName;
        string _TypeName;
        string _FunctionName;
        string _Message;
        internal PNDocuTerms.DocuEntities.IDocuEntity _MessageEntity = null;
        DateTime _LogDate;

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


        internal RCV2(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, PNDocuTerms.DocuEntities.IDocuEntity entity, IRCV2 inner)
        {
            _Succeeded = succeeded;
            _User = User;
            _AssemblyName = Assembly;
            _TypeName = TypeName;
            _FunctionName = FunctionName;
            _Message = "";
            _MessageEntity = entity;
            _LogDate = dat;
            _InnerIRCV2 = inner;
        }


        public RCV2(mko.Logging.RCV2 mkoRCV2)
        {
            _Succeeded = mkoRCV2._Succeeded;
            _User = mkoRCV2._User;
            _AssemblyName = mkoRCV2._AssemblyName;
            _TypeName = mkoRCV2._TypeName;
            _FunctionName = mkoRCV2._FunctionName;
            _Message = mkoRCV2.Message;
            _LogDate = mkoRCV2._LogDate;
        }

        public RCV2(mko.Logging.RC mkoRC)
        {
            _Succeeded = mkoRC.LogType != EnumLogType.Error;
            _User = "-";
            _Message = mkoRC.Message;
            _LogDate = mkoRC.LogDate;
        }



        /// <summary>
        /// Konstruktor für Deserialisierung
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>


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

        [DataMember]
        public IDocuEntity MessageEntity => _MessageEntity;

        IRCV2 _InnerIRCV2;


        public virtual PNDocuTerms.DocuEntities.IDocuEntity ToPlx()
        {
            return ToPlx(this);
        }


        private PNDocuTerms.DocuEntities.IDocuEntity ToPlx(IRCV2 rc)
        {
            PNDocuTerms.DocuEntities.IDocuEntity de = null;

            // To leave the IRCV2 interface untouched type check for _InnerIRCv2 is needed 
            // Otherwise IRCV2 needs to be extended with IRCV2 InnerIRCv2 { get; } Property
            // mko, 12.11.2018
            // Prüfung auf  !(rc.InnerRCV2 is IRCV2) offensichtlich unnötig
            if (rc.InnerRCV2 == null) // || !(rc.InnerRCV2 is IRCV2))
            {
                if (string.IsNullOrWhiteSpace(rc.Message))
                {
                    if (rc.MessageEntity != null)
                    {
                        de = pnL.i($"{rc.AssemblyName}.{rc.TypeName}.{rc.FunctionName}",
                                rc.Succeeded ? pnL.eSucceeded(pnL.p("msg", rc.MessageEntity)) : pnL.eFails(pnL.p("msg", rc.MessageEntity)));

                    }
                    else
                    {
                        de = pnL.i($"{rc.AssemblyName}.{rc.TypeName}.{rc.FunctionName}",
                                        rc.Succeeded ? pnL.eSucceeded() : pnL.eFails());
                    }
                }
                else
                {
                    de = pnL.i($"{rc.AssemblyName}.{rc.TypeName}.{rc.FunctionName}",
                                        rc.Succeeded ? pnL.eSucceeded(pnL.p("msg", pnL.txt(rc.Message))) : pnL.eFails(pnL.p("msg", pnL.txt(rc.Message))));
                }
            }
            // mko, 12.11.2018
            // Prüfung auf (rc.InnerRCV2 is IRCV2) offensichtlich unnötig
            else //if (rc.InnerRCV2 is IRCV2)
            {
                var inner = rc.InnerRCV2;

                var pInner = pnL.p("inner", inner.ToPlx());

                if (string.IsNullOrWhiteSpace(rc.Message))
                {
                    if (rc.MessageEntity != null)
                    {
                        de = pnL.i($"{rc.AssemblyName}.{rc.TypeName}.{rc.FunctionName}",
                                rc.Succeeded ? pnL.eSucceeded(pnL.p("msg", rc.MessageEntity)) : pnL.eFails(pnL.p("msg", rc.MessageEntity)), pInner);

                    }
                    else
                    {
                        de = pnL.i($"{rc.AssemblyName}.{rc.TypeName}.{rc.FunctionName}",
                                        rc.Succeeded ? pnL.eSucceeded() : pnL.eFails(), pInner);
                    }
                }
                else
                {
                    de = pnL.i($"{rc.AssemblyName}.{rc.TypeName}.{rc.FunctionName}",
                                        rc.Succeeded ? pnL.eSucceeded(pnL.p("msg", pnL.txt(rc.Message))) : pnL.eFails(pnL.p("msg", pnL.txt(rc.Message))), pInner);
                }
            }
            //else
            //{
            //    de = pnL.i($"{rc.AssemblyName}.{rc.TypeName}.{rc.FunctionName}",
            //                        rc.Succeeded ? pnL.eSucceeded() : pnL.eFails(),                                    
            //                        pnL.p("msg", pnL.txt(rc.Message)));
            //}

            return de;
        }

        public override string ToString()
        {

            //return (_InnerIRCV2 != null ? $"{_InnerIRCV2.ToString()}\n" : "")
            //      + $"{StartTimeSingleton.TimeDifferenceToStartTimeInMs(LogDate).ToString("D9")} "
            //      + $"{AssemblyName}.{TypeName}.{FunctionName} {(Succeeded ? "-> ok" : "-> failed!")} "
            //      + (!string.IsNullOrWhiteSpace(Message) ? $": {Message}" : "");


            //var str = fmt.Print(ToPlx());
            return ToPlx().ToString();
        }

    }

}
