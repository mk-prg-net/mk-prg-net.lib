using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 20.12.2017
    /// Serializable variant of RCV2
    /// </summary>
    [Serializable]
    public class RCV2Ser : IRCV2
    {
        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV2Ser Ok(string User = "*", string Message = "", RCV2Ser inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2Ser(true, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
        }

        internal RCV2Ser(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, RCV2Ser inner)
        {
            Succeeded = succeeded;
            this.User = User;
            AssemblyName = Assembly;
            this.TypeName = TypeName;
            this.FunctionName = FunctionName;
            this.Message = Message;
            LogDate = dat;
            InnerRCV2Ser = inner;
        }


        /// <summary>
        /// mko, 20.12.2017
        /// Only for deserialization purpose !
        /// </summary>
        public RCV2Ser()
        {

        }


        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RCV2Ser Failed(string User = "*", string ErrorDescription = "", RCV2Ser inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2Ser(false, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
        }

        public IDocuEntity ToPlx()
        {
            throw new NotImplementedException();
        }

        public string Message { get; set; }

        [XmlIgnore]
        public IRCV2 InnerRCV2 => InnerRCV2Ser;        

        public RCV2Ser InnerRCV2Ser { get; set; } 

        public bool Succeeded { get; set; }

        public DateTime LogDate { get; set; }

        public string User { get; set; }

        public string AssemblyName { get; set; }

        public string TypeName { get; set; }

        public string FunctionName { get; set; }

        public IDocuEntity MessageEntity => throw new NotImplementedException();
    }

    /// <summary>
    /// mko, 20.12.2017
    /// Reimplementation of RCV2 for serialization purposes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RCV2Ser<T> : RCV2Ser, IRCV2<T>
    {
        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV2Ser<T> Ok(T value, string User = "*", string Message = "", RCV2Ser inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2Ser<T>(true, value, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RCV2Ser<T> Failed(T value, string User = "*", string ErrorDescription = "", RCV2Ser inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV2Ser<T>(false, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
        }

        internal RCV2Ser(bool succeeded, T value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, RCV2Ser inner)
            : base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
        {
            Value = value;
        }

        /// <summary>
        /// mko, 20.12.2017
        /// Only for deserialization purpose !
        /// </summary>
        public RCV2Ser()
        {

        }

        public T Value { get; set; }
    }
}
