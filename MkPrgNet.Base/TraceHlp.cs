//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.1.2013
//
//  Projekt.......: mko
//  Name..........: TraceHlp.cs
//  Aufgabe/Fkt...: Prüfen von Vor- und Nachbedingungen, Formatieren von Fehler- und 
//                  Statusmeldungen.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 15.3.2017
//  Änderungen....: Throw...IfNot Methoden implementiert
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 28.3.2017
//  Änderungen....: Throw...If Methoden implementiert
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 05.6.2017
//  Änderungen....: Throw...If Methoden einheitlich um innerException erweitert
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MkPrgNet.Base
{
    public class TraceHlp
    {
        /// <summary>
        /// Wirft eine Argumentexception, wenn die Bedingung nictht erfüllt ist. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition">Bedingung, die erfüllt sein muß</param>
        /// <param name="msg">Fehlermeldung, falls Bedingung nicht erfüllt ist</param>
        /// <param name="callerName">Name der aufrufenden Funktion, siehe https://msdn.microsoft.com/en-us/library/mt653988.aspx </param>
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static void ThrowArgExIfNot(bool condition, string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            if (!condition)
            {
                throw new ArgumentException(TraceHlp.FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// Wirft eine Argumentexception, wenn die Bedingung erfüllt ist. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="callerName"></param>
        public static void ThrowArgExIf(bool condition, string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            if (condition)
            {
                throw new ArgumentException(TraceHlp.FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }


        /// <summary>
        /// Wirft eine Argumentexception. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition">Bedingung, die erfüllt sein muß</param>
        /// <param name="msg">Fehlermeldung, falls Bedingung nicht erfüllt ist</param>
        /// <param name="callerName">Name der aufrufenden Funktion, siehe https://msdn.microsoft.com/en-us/library/mt653988.aspx </param>
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static void ThrowArgEx(string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            throw new ArgumentException(TraceHlp.FormatErrMsg(assembly, cls, callerName, msg), innerException);
        }

        /// <summary>
        /// Wirft eine Exception, wenn die Bedingung nictht erfüllt ist. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="callerName"></param>
        public static void ThrowExIfNot(bool condition, string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (!condition)
            {
                throw new Exception(TraceHlp.FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// Wirft eine Exception, wenn die Bedingung erfüllt ist. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="callerName"></param>
        public static void ThrowExIf(bool condition, string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (condition)
            {
                throw new Exception(TraceHlp.FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }


        /// <summary>
        /// Wirft eine Exception. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition">Bedingung, die erfüllt sein muß</param>
        /// <param name="msg">Fehlermeldung, falls Bedingung nicht erfüllt ist</param>
        /// <param name="callerName">Name der aufrufenden Funktion, siehe https://msdn.microsoft.com/en-us/library/mt653988.aspx </param>
        public static void ThrowEx(string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if(innerException == null)
            {
                throw new Exception(TraceHlp.FormatErrMsg(assembly, cls, callerName, msg));
            } else
            {
                throw new Exception(TraceHlp.FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
            

        }


        public static string MsgsToString(string[] msgs)
        {
            string res = "";
            foreach (string msg in msgs)
            {
                res += "\"" + msg + "\" ";
            }
            return res;
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(object Obj, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " ERR!  " + Obj.GetType().FullName + "." + MethodName + " " + MsgsToString(msgs);
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(string ClassName, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " ERR!  " + ClassName + "." + MethodName + " " + MsgsToString(msgs);
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(string Assembly, string ClassName, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " ERR!  " + Assembly + "." + ClassName + "." + MethodName + " " + MsgsToString(msgs);
        }



        /// <summary>
        /// Erzeugt eine Warnmeldung mit allen notwendigen Informationen wie Ort, Zeit, Beschreibung der Warnung                
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(object Obj, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " WARN! " + Obj.GetType().FullName + "." + MethodName + " " + MsgsToString(msgs);
        }


        /// <summary>
        /// Erzeugt eine Warnmeldung mit allen notwendigen Informationen wie Ort, Zeit, Beschreibung der Warnung                
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(string ClassName, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " WARN! " + ClassName + "." + MethodName + " " + MsgsToString(msgs);
        }


        /// <summary>
        /// Erzeugt eine Infomeldung mit allen notwendigen Informationen wie Ort, Zeit, Information
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(object Obj, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " INFO  " + Obj.GetType().FullName + "." + MethodName + " " + MsgsToString(msgs);
        }

        /// <summary>
        /// Erzeugt eine Infomeldung mit allen notwendigen Informationen wie Ort, Zeit, Information
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(string ClassName, string MethodName, params string[] msgs)
        {
            return DateTime.Now.ToShortTimeString() + " INFO  " + ClassName + "." + MethodName + " " + MsgsToString(msgs);
        }
    }

}
