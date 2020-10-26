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
    /// Base class of Returncodes. With innerRC of type base class. - Leave node.
    /// 
    /// mko, 13.9.2018
    /// In Zukunft nur noch diese oder RCV3WithValue einsetzen. Anstatt InnerRCV3 eventuelle Fehler
    /// über ausführlicher PLX- Messages zurückmelden.
    /// 
    /// mko, 18.9.2018
    /// RCV3 kann jetzt aus der in PLX serialisierten Form wiederhergestellt werden. 
    /// Sinnvoll, um RCV3 über alte Webdienste zu empfangen (die string zurückgeben).
    /// </summary>
    [DataContract]
    public class RCV3 : IRCV2
    {
        public static PNDocuTerms.DocuEntities.Composer pnL = new PNDocuTerms.DocuEntities.Composer();
        public static PNFormater fmtPN = new PNFormater();
        public static HTMLFormater fmtHTML = new HTMLFormater();

        [DataMember(Name = "Succeeded")]
        bool _Succeeded;

        [DataMember(Name = "User")]
        string _User;

        [DataMember(Name = "AssemblyName")]
        string _AssemblyName;

        [DataMember(Name = "TypeName")]
        string _TypeName;

        [DataMember(Name = "FunctionName")]
        string _FunctionName;

        IDocuEntity _MessageEntity = null;

        [DataMember(Name = "LogDate")]
        DateTime _LogDate;

        /// <summary>
        /// If true, then function call was successful
        /// </summary>        
        public bool Succeeded => _Succeeded;

        /// <summary>
        /// Date when function call ended.
        /// </summary>

        public DateTime LogDate => _LogDate;

        /// <summary>
        /// User, who calls the function
        /// </summary>        
        public string User => _User;

        /// <summary>
        /// Assembly were function is defined
        /// </summary>        
        public string AssemblyName => _AssemblyName;

        /// <summary>
        /// Type were function is definied
        /// </summary>        
        public string TypeName => _TypeName;

        /// <summary>
        /// Name of function
        /// </summary>        
        public string FunctionName => _FunctionName;

        [Newtonsoft.Json.JsonIgnore]
        public IDocuEntity MessageEntity => _MessageEntity;

        [Newtonsoft.Json.JsonIgnore]
        public virtual bool HasInnerRC => _InnerRC != null;

        /// <summary>
        /// for serialization purpose _InnerRC cant be of type IRCV2, because interface won't be deserialized
        /// </summary>
        [DataMember(Name = "InnerRC")]
        RCV3 _InnerRC;

        /// <summary>
        /// mko, 25.7.2018
        /// Messages are basically stored as IDocuEntity items. This properties serializes them 
        /// as property expression lists. This can be used for serialization/deserialiation purposes.
        /// The setter is implemented especially for deserialization.
        /// 
        /// </summary>
        [DataMember]
        public string Message
        {
            get
            {
                return MessageEntity != null ? fmtPN.Print(MessageEntity) : fmtPN.Print(pnL.txt(""));
            }

            // mko, 25.7.2018
            // setter for deserialization purpose
            set
            {
                // Null or whitespace tests makes it more robust during deserialization 
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var ret = PNDocuTerms.Parser.Parser.Parse(value, PNDocuTerms.Fn._);

                    if (ret.Succeeded)
                    {
                        _MessageEntity = ret.Value;
                    } else
                    {
                        _MessageEntity = pnL.txt(value.Replace("#.", "").Replace("#$", "").Replace("#", ""));
                    }
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual IRCV2 InnerRCV2 => _InnerRC;

        public static RCV3 Ok(string Message = "", string User = "*", IRCV2 innerRC = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //return new RCV3(true, DateTime.Now, User, assembly, cls, mth.Name, pnL.txt(Message), innerRC);
            return new RCV3(true, DateTime.Now, User, assembly, cls, caller, pnL.txt(Message), innerRC);
        }

        public static RCV3 Ok(IDocuEntity docuEntity, string User = "*", IRCV2 innerRC = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3(true, DateTime.Now, User, assembly, cls, caller, docuEntity, innerRC);
        }

        /// <summary>
        /// mko, 23.4.2018
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RCV3 Failed(string ErrorDescription = "", string User = "*", IRCV2 innerRC = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3(false, DateTime.Now, User, assembly, cls, caller, pnL.txt(ErrorDescription), innerRC);

        }

        /// <summary>
        /// mko, 25.7.2018
        /// </summary>
        /// <param name="ErrorDescription"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV3 Failed(IDocuEntity ErrorDescription, string User = "*", IRCV2 innerRC = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3(false, DateTime.Now, User, assembly, cls, caller, ErrorDescription, innerRC);
        }

        /// <summary>
        /// mko, 04.05.2018
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV3 Failed(Exception ex, string User = "*", [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            var fmt = new PNDocuTerms.DocuEntities.PNFormater();

            return new RCV3(false, DateTime.Now, User, assembly, cls, caller, TraceHlp.FlattenExceptionMessagesPN(ex));
        }

        /// <summary>
        /// mko, 22.10.2018
        /// Erstellt einen RCV3 parametrisch.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Succedeed"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="User"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RCV3 Create(bool Succedeed = true, IDocuEntity ErrorDescription = null, string User = "*", RCV3 inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3(Succedeed, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }



        //internal RCV3(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, IRCV2 innerRC = null)            
        //{
        //    _Succeeded = succeeded;
        //    _User = User;
        //    _AssemblyName = Assembly;
        //    _TypeName = TypeName;
        //    _FunctionName = FunctionName;
        //    this.Message = Message;
        //    _LogDate = dat;
        //    _InnerRC = TranformToRCV3(innerRC);
        //}

        internal RCV3(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity Message, IRCV2 innerRC = null)
        {
            _Succeeded = succeeded;
            _User = User;
            _AssemblyName = Assembly;
            _TypeName = TypeName;
            _FunctionName = FunctionName;
            _MessageEntity = Message;
            _LogDate = dat;
            _InnerRC = TranformToRCV3(innerRC);
        }

        public RCV3(RCV3 ori)
            : this(ori.Succeeded, ori.LogDate, ori.User, ori.AssemblyName, ori.TypeName, ori.FunctionName, ori.MessageEntity)
        {
        }

        public RCV3 Clone()
        {
            return new RCV3(Succeeded, LogDate, User, AssemblyName, TypeName, FunctionName, pnL.txt(Message));
        }


        /// <summary>
        /// mko, 25.7.2018
        /// Transforms objects with IRCV2 Values in RCV3- values recursivly. 
        /// Because _InnerRC ist of type RCV3, this "reconstruction" of RCV3 object with data from IRCV2 
        /// objects is necessary.
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public static RCV3 TranformToRCV3(IRCV2 rc)
        {
            if (rc != null)
            {
                return new RCV3(
                    rc.Succeeded,
                    rc.LogDate,
                    rc.User,
                    rc.AssemblyName,
                    rc.TypeName,
                    rc.FunctionName,
                    pnL.txt(rc.Message),
                    rc.InnerRCV2 != null ? TranformToRCV3(rc.InnerRCV2) : null);
            }
            else
            {
                return null;
            }
        }

        public static RCV3 TranformToRCV3(RC<ParserV2.Result> rc)
        {
            if (rc != null)
            {
                IRCV2 inner = null;
                if (rc.InnerRCV2 != null)
                {
                    inner = new RCV3(true, rc.InnerRCV2.LogDate, rc.InnerRCV2.User, rc.InnerRCV2.AssemblyName, rc.InnerRCV2.TypeName, rc.InnerRCV2.FunctionName, pnL.txt(rc.InnerRCV2.Message));
                }

                return new RCV3(
                    rc.Succeeded,
                    rc.LogDate,
                    rc.User,
                    rc.AssemblyName,
                    rc.TypeName,
                    rc.FunctionName,
                    pnL.txt(rc.Message),
                    inner);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// mko, 15.11.2018
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public static RCV3 TranformToRCV3(RC<IToken[]> rc)
        {
            if (rc != null)
            {
                IRCV2 inner = null;
                if (rc.InnerRCV2 != null)
                {
                    inner = new RCV3(true, rc.InnerRCV2.LogDate, rc.InnerRCV2.User, rc.InnerRCV2.AssemblyName, rc.InnerRCV2.TypeName, rc.InnerRCV2.FunctionName, pnL.txt(rc.InnerRCV2.Message));
                }

                return new RCV3(
                    rc.Succeeded,
                    rc.LogDate,
                    rc.User,
                    rc.AssemblyName,
                    rc.TypeName,
                    rc.FunctionName,
                    pnL.txt(rc.Message),
                    inner);
            }
            else
            {
                return null;
            }
        }


        public RCV3()
        {
        }

        public virtual IDocuEntity ToPlx()
        {
            PNDocuTerms.DocuEntities.IDocuEntity de = null;

            // To leave the IRCV2 interface untouched type check for _InnerIRCv2 is needed 
            // Otherwise IRCV2 needs to be extended with IRCV2 InnerIRCv2 { get; } Property
            de = pnL.i($"{AssemblyName}.{TypeName}.{FunctionName}",
                                Succeeded ? pnL.eSucceeded() : pnL.eFails(),
                                pnL.p("logDate", pnL.txt(LogDate.ToString("s"))),
                                pnL.KillIf(string.IsNullOrWhiteSpace(User), () => pnL.p("user", pnL.txt(User))),
                                pnL.KillIf(MessageEntity == null, () => pnL.p("msg", MessageEntity)),
                                pnL.KillIf(_InnerRC == null, () => pnL.p("inner", _InnerRC.ToPlx())));

            return de;
        }

        /// <summary>
        /// mko, 18.9.2018
        /// Creates from plx
        /// </summary>
        /// <param name="plx"></param>
        public static RCV3 Parse(IDocuEntity plx)
        {
            var rc = new RCV3();

            TraceHlp.ThrowArgExIfNot(plx.EntityType == DocuEntityTypes.Instance, "plx is not a instantce");
            TraceHlp.ThrowArgExIfNot(System.Text.RegularExpressions.Regex.IsMatch(plx.Name(), @"[\w\.\<\>]+\.[\w\<\>]+\.[\w\<\>]+$"), "plx instance name do not contains assembly.typename.functionname");
            {
                var parts = plx.Name().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                TraceHlp.ThrowArgExIfNot(parts.Length >= 3, "plx instance name is incomplete");                
                rc._TypeName = parts[parts.Length-2];
                rc._FunctionName = parts[parts.Length-1];

                rc._AssemblyName = string.Join(".", parts.Take(parts.Length - 2));
            }

            TraceHlp.ThrowArgExIfNot(plx.HasValue(), "plx of RCV3 do not contains content");
            {
                rc._Succeeded = null != plx.FindNamedEntity(DocuEntityTypes.Event, "succeeded", 2);

                var dat = plx.FindNamedEntity(DocuEntityTypes.Property, "logDate", 2);
                if(null != dat)
                {
                    rc._LogDate = DateTime.Parse(dat.EntityValue().GetText());
                }

                var user = plx.FindNamedEntity(DocuEntityTypes.Property, "user", 2);
                if (null != user)
                {
                    rc._User = user.EntityValue().GetText();
                }

                var msg = plx.FindNamedEntity(DocuEntityTypes.Property, "msg", 2);
                if (null != msg)
                {
                    rc._MessageEntity = msg.EntityValue();
                }

                var inner = plx.FindNamedEntity(DocuEntityTypes.Property, "inner", 2);
                if(inner != null)
                {
                    rc._InnerRC = Parse(inner.EntityValue());
                }                
            }

            return rc;
        }

        public override string ToString()
        {
            return ToPlx().ToString();
        }
    }

}
