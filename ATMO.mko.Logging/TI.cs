using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ATMOLog = ATMO.mko.Logging;
using pnL = ATMO.mko.Logging.PNDocuTerms.Composer;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 26.9.2017
    /// Implementiert Klassenfabriken, die ITraceInfo- Objekte erzeugen
    /// </summary>
    public class TI : ITraceInfo
    {
        DateTime _dat;
        string _User;
        string _Assembly;
        string _TypeName;
        string _FunctionName;
        string _Message;

        internal TI(DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message)
        {
            _User = User;
            _Assembly = Assembly;
            _TypeName = TypeName;
            _FunctionName = FunctionName;
            _Message = Message;
            _dat = dat;
        }

        /// <summary>
        /// Erzeugt eine neue Traceinfo. Die Name der aktuell aufgerufenen Funktion, der sie biinhaltenden 
        /// Klasse uns Assembly werden in dieser Funktion automatisch bestimmt und in der Traceinfo 
        /// aufgezeichnet.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static TI CreateTI(string User, string Message)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new TI(DateTime.Now, User, assembly, cls, mth.Name, Message);
        }

        public string User => _User;

        public string TypeName => _TypeName;

        public string FunctionName => _FunctionName;

        public string AssemblyName => _Assembly;

        public string Message => _Message;

        public DateTime LogDate => _dat;

        public override string ToString()
        {
            string msg = pnL.i("TI",
                                pnL.p("t", StartTimeSingleton.TimeDifferenceToStartTimeInMs(LogDate).ToString("D9")),
                                pnL.p("User", User),
                                pnL.m($"{AssemblyName}.{TypeName}.{FunctionName}"),
                                pnL.eInfo(Message));                            
            return msg;
        }

    }
}
