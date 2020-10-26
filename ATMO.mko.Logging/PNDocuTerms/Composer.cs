using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms
{
    /// <summary>
    /// mko, 1.3.2018
    /// Structured documentation with terms in Polish notation.
    /// Log or trace messages often describe successful or failed operations on objects.
    /// The effective postprocessing of these messages requires a strict structuring of these messages.
    /// This lib exports functions to format operations on objects as terms in reverse polish notation.
    /// </summary>
    public class Composer
    {
        public static Fn fn = new Fn();

        /// <summary>
        /// Defines the instance/object, on wich operations/actions are executed
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static string i(string name, params string[] pn)
        {
            return $"{fn.Instance} {name} " + (pn.Any() ? String.Join(" ", pn) : "") + " ";
        }

        /// <summary>
        /// Defines a vrsion number of an object or method
        /// </summary>
        /// <param name="versionStr"></param>
        /// <returns></returns>
        public static string ver(string versionStr)
        {
            return $"{fn.Version} {versionStr} ";
        }

        /// <summary>
        /// Decribes a method/action call
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static string m(string name, params string[] pn)
        {
            return $"{fn.Method} {name} " + (pn.Any() ? String.Join(" ", pn) : "") + " ";
        }

        /// <summary>
        /// Describes a function call. 
        /// </summary>
        /// <param name="name">Name of function</param>
        /// <param name="ret">Value, that function call returns</param>
        /// <param name="pn">Parameters of function call</param>
        /// <returns></returns>
        public static string f(string name, string ret, params string[] pn)
        {
            return $"{fn.Function} {name} {fn.Return} {ret}"+ (pn.Any() ? $" {fn.List} {String.Join(" ", pn)}" : "") + " ";
        }

        /// <summary>
        /// Reports the value of a property 
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static string p(string Name, string Value)
        {
            return $"{fn.Property} {Name} {Value} ";
        }

        /// <summary>
        /// Reports the value, property was set
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static string pSet(string Name, string Value)
        {
            return $"{fn.PropertySet} {Name} {Value} ";
        }

        /// <summary>
        /// Defines a fired event with parameters
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static string e(string name, params string[] pn)
        {
            return $"{fn.Event} {name} " + (pn.Any() ? String.Join(" ", pn) : "") + " ";
        }

        /// <summary>
        /// Describes an event, that fires, when a process starts
        /// </summary>        
        /// <param name="pn"></param>
        /// <returns></returns>
        public static string eStart(params string[] pn)
        {
            return e("start", pn);            
        }

        /// <summary>
        /// Describes an event, that fires, when a process ends
        /// </summary>
        /// <param name="nameOfProcess"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static string eEnd(params string[] pn)
        {
            return e("end", pn);            
        }

        /// <summary>
        /// Describes an event, that fires, when a process fails
        /// </summary>
        /// <param name="nameOfProcess"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static string eFails(params string[] pn)
        {
            return e("fails", pn);            
        }

        public static string eWarn(params string[] pn)
        {
            return e("warn", pn);            
        }

        public static string eInfo(params string[] pn)
        {
            return e("info", pn);            
        }


        /// <summary>
        /// Defines a textvalue
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string txt(string text)
        {
            return $"{fn.Txt} {text} {fn.ListEnd}";
        }


        /// <summary>
        /// Defines a date constant
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public static string date(DateTime dat)
        {
            return $"#date {dat.Day}.{dat.Month}.{dat.Year} ";
        }

        /// <summary>
        /// Defines a time constant
        /// </summary>
        /// <param name="dat"></param>
        /// <param name="showMilliseconds"></param>
        /// <returns></returns>
        public static string time(DateTime dat, bool showMilliseconds = false)
        {
            return $"#time {dat.Hour}:{dat.Minute}:{dat.Second}" + (showMilliseconds ? $".{dat.Millisecond} " : " ");
        }

        /// <summary>
        /// timespan descriptor
        /// </summary>
        public struct TS
        {
            /// <summary>
            /// Units of timespan
            /// </summary>
            public enum _unit {
                ms,
                sec,
                min,
                hour
            }

            public _unit Unit { get; }

            public int Value { get; }

        }

        /// <summary>
        /// Describes a Timespan
        /// </summary>
        /// <param name="timeSpanDescriptor"></param>
        /// <returns></returns>
        public static string timeSpan(TS timeSpanDescriptor)
        {
            return $"#ts {timeSpanDescriptor.Unit} {timeSpanDescriptor.Value} ";
        }

    }
}
