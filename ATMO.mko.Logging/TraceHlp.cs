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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 21.9.2017
//  Änderungen....: Integration in ATMO.mko.Logging
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 22.9.2017
//  Änderungen....: FlattenExceptionMessages aus mko.mkoExceptionMessagesFlat.cs
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 26.9.2017
//  Änderungen....: In FlattenExceptionMessages werden Teile der Meldung jetzt durch ;
//                  getrennt.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 2.3.2018
//  Änderungen....: Format der Textmeldungen umgestellt auf polnische Notation
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 27.3.2018
//  Änderungen....: Exakte, parsertaugliche polnische Notation
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.9.2018
//  Änderungen....: ThrowArgEx... nehmen jetzt als Fehlerbeschreibung auch IDokuEnties an.
//  Version.......: 2.1.61
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using pnL = ATMO.mko.Logging.PNDocuTerms.Composer;

namespace ATMO.mko.Logging
{
    public class TraceHlp
    {

        static PNDocuTerms.DocuEntities.Composer pnL
        {
            get
            {
                if (_pnL == null)
                {
                    _pnL = new PNDocuTerms.DocuEntities.Composer();
                }
                return _pnL;
            }
        }

        static PNDocuTerms.DocuEntities.Composer _pnL;


        static PNDocuTerms.DocuEntities.PNFormater fmt
        {
            get
            {
                if (_fmt == null)
                {
                    _fmt = new PNDocuTerms.DocuEntities.PNFormater();
                }
                return _fmt;
            }
        }

        static PNDocuTerms.DocuEntities.PNFormater _fmt;


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
                throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static void ThrowArgExIfNot(bool condition, PNDocuTerms.DocuEntities.IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            if (!condition)
            {
                throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
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
                throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowArgExIf(bool condition, PNDocuTerms.DocuEntities.IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            if (condition)
            {
                throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
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

            throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static void ThrowArgEx(PNDocuTerms.DocuEntities.IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
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
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowExIfNot(bool condition, PNDocuTerms.DocuEntities.IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (!condition)
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
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
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowExIf(bool condition, PNDocuTerms.DocuEntities.IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (condition)
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
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

            if (innerException == null)
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg));
            }
            else
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        public static void ThrowEx(PNDocuTerms.DocuEntities.IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (innerException == null)
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg));
            }
            else
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 19.2.2019
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowIndexOutOfRangeException(
            PNDocuTerms.DocuEntities.IDocuEntity msg,
            Exception innerException = null,
            [System.Runtime.CompilerServices.CallerMemberName]
            string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (innerException == null)
            {
                throw new IndexOutOfRangeException(FormatErrMsg(assembly, cls, callerName, msg));

            }
            else
            {
                throw new IndexOutOfRangeException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }


        /// <summary>
        /// mko, 19.2.2019
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowIndexOutOfRangeExceptionIf(
            bool cond,
            PNDocuTerms.DocuEntities.IDocuEntity msg,
            Exception innerException = null,
            [System.Runtime.CompilerServices.CallerMemberName]
                    string callerName = "")
        {
            if (cond)
            {

                var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                var cls = mth.ReflectedType.Name;
                var assembly = mth.ReflectedType.Assembly.FullName;

                if (innerException == null)
                {
                    throw new IndexOutOfRangeException(FormatErrMsg(assembly, cls, callerName, msg));

                }
                else
                {
                    throw new IndexOutOfRangeException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
                }
            }
        }

        /// <summary>
        /// mko, 19.2.2019
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowIndexOutOfRangeExceptionIfNot(
            bool cond,
            PNDocuTerms.DocuEntities.IDocuEntity msg,
            Exception innerException = null,
            [System.Runtime.CompilerServices.CallerMemberName]
                    string callerName = "")
        {
            if (!cond)
            {

                var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                var cls = mth.ReflectedType.Name;
                var assembly = mth.ReflectedType.Assembly.FullName;

                if (innerException == null)
                {
                    throw new IndexOutOfRangeException(FormatErrMsg(assembly, cls, callerName, msg));

                }
                else
                {
                    throw new IndexOutOfRangeException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
                }
            }
        }





        public static string MsgsToString(string[] msgs)
        {
            var res = new StringBuilder();
            foreach (string msg in msgs)
            {
                res.Append($" {msg}");
            }
            return res.ToString();
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatErrMsg(object Obj, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i($"{Obj.GetType().FullName}.{MethodName}", pnL.date(now), pnL.eFails(msg)));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatErrMsg(string ClassName, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i($"{ClassName}.{MethodName}", pnL.date(now), pnL.eFails(msg)));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatErrMsg(string Assembly, string ClassName, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i($"{Assembly}.{ClassName}.{MethodName}", pnL.date(now), pnL.eFails(msg)));
        }

        // Warnings

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(object Obj, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i($"{Obj.GetType().FullName}.{MethodName}", pnL.date(now), pnL.eWarn(msg)));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(string ClassName, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i($"{ClassName}.{MethodName}", pnL.date(now), pnL.eWarn(msg)));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(string Assembly, string ClassName, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i($"{Assembly}.{ClassName}.{MethodName}", pnL.date(now), pnL.eWarn(msg)));
        }

        // Infos
        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(object Obj, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i(Obj.GetType().FullName, pnL.m(MethodName, pnL.date(now), pnL.ret(msg))));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(string ClassName, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i(ClassName, pnL.m(MethodName, pnL.date(now), pnL.ret(msg))));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(string Assembly, string ClassName, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        {
            var now = DateTime.Now;
            return fmt.Print(pnL.i($"{Assembly}.{ClassName}", pnL.m(MethodName, pnL.date(now), pnL.ret(msg))));
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(object Obj, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;
            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{Obj.GetType().FullName}.{MethodName}", pnL.date(now), pnL.eFails(pnL.txt(msgs[0])));
                }
            }
            else
            {
                pn = pnL.i($"{Obj.GetType().FullName}.{MethodName}", pnL.date(now), pnL.eFails(pnL.txt(String.Join(" ", msgs))));
            }

            var err = fmt.Print(pn);
            return err;
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(string ClassName, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{ClassName}.{MethodName}", pnL.date(now), pnL.eFails(pnL.txt(msgs[0])));
                }
            }
            else
            {
                pn = pnL.i($"{ClassName}.{MethodName}", pnL.date(now), pnL.eFails(pnL.txt(String.Join(" ", msgs))));
            }

            var err = fmt.Print(pn);
            return err;
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung        
        ///  
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(string Assembly, string ClassName, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{Assembly}.{ClassName}.{MethodName}", pnL.date(now), pnL.eFails(pnL.txt(msgs[0])));
                }
            }
            else
            {
                pn = pnL.i($"{Assembly}.{ClassName}.{MethodName}", pnL.date(now), pnL.eFails(pnL.txt(String.Join(" ", msgs))));
            }

            var err = fmt.Print(pn);
            return err;
        }



        /// <summary>
        /// Erzeugt eine Warnmeldung mit allen notwendigen Informationen wie Ort, Zeit, Beschreibung der Warnung      
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(object Obj, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{Obj.GetType().FullName}.{MethodName}", pnL.date(now), pnL.eWarn(pnL.txt(msgs[0])));
                }
            }
            else
            {
                pn = pnL.i($"{Obj.GetType().FullName}.{MethodName}", pnL.date(now), pnL.eWarn(pnL.txt(String.Join(" ", msgs))));
            }

            var err = fmt.Print(pn);
            return err;
        }


        /// <summary>
        /// Erzeugt eine Warnmeldung mit allen notwendigen Informationen wie Ort, Zeit, Beschreibung der Warnung   
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(string ClassName, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;
            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{ClassName}.{MethodName}", pnL.date(now), pnL.eWarn(pnL.txt(msgs[0])));
                }
            }
            else
            {
                pn = pnL.i($"{ClassName}.{MethodName}", pnL.date(now), pnL.eWarn(pnL.txt(String.Join(" ", msgs))));
            }

            var err = fmt.Print(pn);
            return err;

        }


        /// <summary>
        /// Erzeugt eine Infomeldung mit allen notwendigen Informationen wie Ort, Zeit, Information
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(object Obj, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{Obj.GetType().FullName}", pnL.m(MethodName, pnL.date(now), pnL.ret(pnL.txt(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i($"{Obj.GetType().FullName}", pnL.m(MethodName, pnL.date(now), pnL.ret(pnL.txt(String.Join(" ", msgs)))));
            }

            var err = fmt.Print(pn);
            return err;

        }

        /// <summary>
        /// Erzeugt eine Infomeldung mit allen notwendigen Informationen wie Ort, Zeit, Information
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(string ClassName, string MethodName, params string[] msgs)
        {

            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i(ClassName, pnL.m(MethodName, pnL.date(now), pnL.ret(pnL.txt(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i(ClassName, pnL.m(MethodName, pnL.date(now), pnL.ret(pnL.txt(String.Join(" ", msgs)))));
            }

            var err = fmt.Print(pn);
            return err;

            //return DateTime.Now.ToShortTimeString() + " INFO  " + ClassName + "." + MethodName + " " + MsgsToString(msgs);
        }

        /// <summary>
        /// Wandelt eine Ketten von Ausnahmen (Ausnahme -> innere Ausnahme -> innere Ausnahme ...)
        /// in eine Zeichenkette um
        /// 
        /// mko, 26.9.2017
        /// Teile der Meldungen durch ; separiert
        /// 
        /// mko, 2.3.2018
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string FlattenExceptionMessages(Exception ex)
        {
            var rcParse = PNDocuTerms.Parser.Parser.Parse(ex.Message, PNDocuTerms.Fn._);

            if (rcParse.Succeeded)
            {
                if (ex.InnerException != null)
                {
                    return fmt.Print(pnL.i(ex.GetType().Name, pnL.p("msg", rcParse.Value), pnL.p("inner", FlattenExceptionMessagesPN(ex.InnerException))));
                }
                else
                {
                    return fmt.Print(pnL.i(ex.GetType().Name, pnL.p("msg", rcParse.Value)));
                }

            }
            else
            {
                if (ex.InnerException != null)
                {
                    return fmt.Print(pnL.i(ex.GetType().Name, pnL.p("msg", pnL.txt(ex.Message)), pnL.p("inner", FlattenExceptionMessagesPN(ex.InnerException))));
                }
                else
                {
                    return fmt.Print(pnL.i(ex.GetType().Name, pnL.p("msg", pnL.txt(ex.Message))));
                }
            }
        }

        public static PNDocuTerms.DocuEntities.IDocuEntity FlattenExceptionMessagesPN(Exception ex)
        {
            var rcParse = PNDocuTerms.Parser.Parser.Parse(ex.Message, PNDocuTerms.Fn._);

            if (rcParse.Succeeded)
            {
                if (ex.InnerException != null)
                {
                    return pnL.i(ex.GetType().Name, pnL.p("msg", rcParse.Value), pnL.p("inner", FlattenExceptionMessagesPN(ex.InnerException)));
                }
                else
                {
                    return pnL.i(ex.GetType().Name, pnL.p("msg", rcParse.Value));
                }
            }
            else
            {
                if (ex.InnerException != null)
                {
                    return pnL.i(ex.GetType().Name, pnL.p("msg", pnL.txt(ex.Message)), pnL.p("inner", FlattenExceptionMessagesPN(ex.InnerException)));
                }
                else
                {
                    return pnL.i(ex.GetType().Name, pnL.p("msg", pnL.txt(ex.Message)));
                }
            }
        }
    }
}