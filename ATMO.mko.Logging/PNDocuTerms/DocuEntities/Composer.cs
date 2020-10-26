using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static mko.Algo.Listprocessing.Fn;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{

    /// <summary>
    /// mko, 26.3.2018
    /// </summary>
    public partial class Composer : IComposer
    {

        IFn fn = Fn._;

        public Composer() { }

        public Composer(IFn fn)
        {
            this.fn = fn;
        }


        // ----------------------------------------------------------------------------------------------------
        // Hilfsmethoden zum Formatieren von Werten

        /// <summary>
        /// Formatiert einen bool- Wert als docuEntity
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        IDocuEntity CreateBoolEntity(bool b) => b ? txt(TechTerms.BooleanOps.valTrue) : txt(TechTerms.BooleanOps.valFalse);

        /// <summary>
        /// Formatiert einen Integer als DocuEntity
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        IDocuEntity CreateIntEntity(int i) => txt(i.ToString());

        /// <summary>
        /// Formatiert einen Long als DocuEntity
        /// </summary>
        /// <param name="lng"></param>
        /// <returns></returns>
        IDocuEntity CreateLongEntity(long lng) => txt(lng.ToString() + "L");


        /// <summary>
        /// Formatiert einen Double als DokuEntity
        /// </summary>
        /// <param name="dbl"></param>
        /// <returns></returns>
        IDocuEntity CreateDblEntity(double dbl)
        {
            var sign = Math.Sign(dbl);

            // siehe: https://stackoverflow.com/questions/389993/extracting-mantissa-and-exponent-from-double-in-c-sharp
            // Translate the double into sign, exponent and mantissa.
            long bits = BitConverter.DoubleToInt64Bits(dbl);
            // Note that the shift is sign-extended, hence the test against -1 not 1
            bool negative = (bits & (1L << 63)) != 0;
            int exponent = (int)((bits >> 52) & 0x7ffL);
            long mantissa = bits & 0xfffffffffffffL;

            // Subnormal numbers; exponent is effectively one higher,
            // but there's no extra normalisation bit in the mantissa
            if (exponent == 0)
            {
                exponent++;
            }
            // Normal numbers; leave exponent as it is but add extra
            // bit to the front of the mantissa
            else
            {
                mantissa = mantissa | (1L << 52);
            }

            // Bias the exponent. It's actually biased by 1023, but we're
            // treating the mantissa as m.0 rather than 0.m, so we need
            // to subtract another 52 from it.
            exponent -= 1075;

            if (mantissa == 0)
            {
            }
            else
            {
                /* Normalize */
                while ((mantissa & 1) == 0)
                {    /*  i.e., Mantissa is even */
                    mantissa >>= 1;
                    exponent++;
                }
            }

            return i(TechTerms.Numbers.Dbl,
                    p(TechTerms.Numbers.Signum, sign.ToString()),
                    p(TechTerms.Numbers.Mantissa, mantissa),
                    p(TechTerms.Numbers.Exp, exponent));
        }


        /// <summary>
        /// Erzeugt ein Datumswert
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public IDocuEntity date(DateTime dat)
        {
            return new DocuEntity(fn, DocuEntityTypes.Date, L(new String($"{dat.Year}-{dat.Month}-{dat.Day}")));
        }

        public IDocuEntity time(TimeSpan dat, bool showMilliseconds = false) => new DocuEntity(fn, DocuEntityTypes.Time, L(new String($"{dat.Hours.ToString("D2")}:{dat.Minutes.ToString("D2")}:{dat.Seconds.ToString("D2")}")));

        public IDocuEntity txt(string text)
        {
            var str = text.Replace("#", " ").Split(L(' ').ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(r => new String(r));
            return new DocuEntity(fn, DocuEntityTypes.Text, str);
        }

        public IDocuEntity ver(string versionStr) => new DocuEntity(fn, DocuEntityTypes.Version, L(new String(versionStr)));



        /// <summary>
        /// Dokuentity wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IDocuEntity KillIfNot(bool Condition, Func<IDocuEntity> docuEntityFactory)
        {
            return new KillIfNot(Condition, docuEntityFactory);
        }

        /// <summary>
        /// mko, 24.7.2018
        /// DokuEntity wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IDocuEntity KillIf(bool Condition, Func<IDocuEntity> docuEntityFactory)
        {
            return new KillIfNot(!Condition, docuEntityFactory);
        }


        /// <summary>
        /// Embeds properties in current instance or method parameter list
        /// Note that 
        ///    thisMethod(p1, ... px, px+1 = embed(entities), px+2 ...) 
        /// is not the same like 
        ///    thisMethod(p1, ... px, px+1 = List(entities), px+2 ...).
        ///    
        /// List then will be a parameter of Method, and entities are childs of list:
        /// thisMethod             
        ///    +--> List (Parameters as List)
        ///           +--> p1
        ///           : 
        ///           +--> px
        ///           +--> px+1 = List
        ///           |             +-->> entities
        ///           +--> px+2
        ///           :
        /// Instead of after calling thisMethod(.., embed(entities), ..) entities are direct parameters of thisMethod
        /// thisMethod             
        ///    +--> List (Parameters as List)
        ///           +--> p1
        ///           : 
        ///           +--> px
        ///           +-->> entities        
        ///           +--> px+2
        ///           :
        ///           
        /// Note: embeding will be done in CreateMemebers
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IDocuEntity EmbedMembers(IDocuEntity[] entities)
        {
            return new ListToEmbed(entities);
        }



        /// <summary>
        ///  Allgemeines Event
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDocuEntity e(string name, IDocuEntity value)
        {
            return CreateEntity(DocuEntityTypes.Event, name, value);
        }


        public IDocuEntity e(string name)
        {
            return new DocuEntity(fn, DocuEntityTypes.Event, L(new String(name)));
        }


        /// <summary>
        /// mko, 10.4.2018
        /// Creates a event with paramters, encapsulated in a list.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDocuEntity ePrms(string name, params IDocuEntity[] pn)
        {
            return new DocuEntity(fn, DocuEntityTypes.Event, L(new String(name), List(KillIfNotFilter(pn))));
        }


        public IDocuEntity eEnd(IDocuEntity value)
        {
            return e(TechTerms.eEnd, value);
        }

        public IDocuEntity eEnd()
        {
            return e(TechTerms.eEnd);
        }

        public IDocuEntity eEnd(string value)
        {
            return eEnd(txt(value));
        }


        public IDocuEntity eSucceeded(IDocuEntity value)
        {
            return CreateEntity(DocuEntityTypes.Event, TechTerms.eSucceeded, value);
        }

        public IDocuEntity eSucceeded()
        {
            return e(TechTerms.eSucceeded);
        }

        public IDocuEntity eSucceeded(string value)
        {
            return eSucceeded(txt(value));            
        }

        /// <summary>
        /// mko, 2.4.2019
        /// eFails mit KillIfNot Filtersemantik
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDocuEntity eFails(IDocuEntity value)
        {
            return CreateEntity(DocuEntityTypes.Event, TechTerms.eFails, value);
        }


        public IDocuEntity eFails()
        {
            return e(TechTerms.eFails);
        }

        public IDocuEntity eFails(string value)
        {
            return eFails(txt(value));
        }


        public IDocuEntity eNotCompleted()
        {            
            return e(TechTerms.eNotCompleted);
        }

        public IDocuEntity eNotCompleted(IDocuEntity value)
        {
            return CreateEntity(DocuEntityTypes.Event, TechTerms.eNotCompleted, value);            
        }


        public IDocuEntity eInfo(IDocuEntity value)
        {
            return CreateEntity(DocuEntityTypes.Event, TechTerms.eInfo, value);            
        }

        public IDocuEntity eInfo()
        {
            return e(TechTerms.eInfo);
        }

        public IDocuEntity eInfo(string value)
        {
            return eInfo(txt(value));
        }


        public IDocuEntity eStart(IDocuEntity value)
        {
            return CreateEntity(DocuEntityTypes.Event, TechTerms.eStart, value);
        }

        public IDocuEntity eStart()
        {
            return e(TechTerms.eStart);            
        }

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public IDocuEntity eStart(string info)
        {
            return eStart(txt(info));
        }


        public IDocuEntity eWarn(IDocuEntity value)
        {
            return CreateEntity(DocuEntityTypes.Event, TechTerms.eWarn, value);            
        }

        public IDocuEntity eWarn()
        {
            return e(TechTerms.eWarn);
        }

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDocuEntity eWarn(string value)
        {
            return eWarn(txt(value));
        }

        public IDocuEntity i(string name, params IDocuEntity[] pn)
        {
            IDocuEntity res = null;
            res = CreateObjectWithMembers(DocuEntityTypes.Instance, name, pn);
            return res;
        }

        public IDocuEntity m(string name, params IDocuEntity[] pn)
        {
            IDocuEntity res = null;
            res = CreateObjectWithMembers(DocuEntityTypes.Method, name, pn);
            return res;
        }

        /// <summary>
        /// mko, 10.4.2018
        /// Returnvalue
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IDocuEntity ret(params IDocuEntity[] pn)
        {
            return new DocuEntity(fn, DocuEntityTypes.ReturnValue, L(List(pn)));
        }

        /// <summary>
        /// Boolean zurückgeben
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IDocuEntity ret(bool res) => ret(CreateBoolEntity(res));

        /// <summary>
        /// Integer zurückgeben
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IDocuEntity ret(int res) => ret(CreateIntEntity(res));

        /// <summary>
        /// Long zurückgeben
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IDocuEntity ret(long res) => ret(CreateLongEntity(res));

        /// <summary>
        /// String zurückgeben
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IDocuEntity ret(string res) => ret(txt(res));


        /// <summary>
        /// mko, 21.12.2018
        /// Erweitert um flexible Parameterliste
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IDocuEntity p(string Name, IDocuEntity Value) => CreateEntity(DocuEntityTypes.Property, Name, Value);

        /// <summary>
        /// mko, 27.2.2019
        /// Eigenschaft mit bool- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IDocuEntity p(string Name, bool Value) => p(Name, CreateBoolEntity(Value));

        /// <summary>
        /// mko, 10.4.2018
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IDocuEntity p(string Name, string Value) => p(Name, txt(Value));


        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit int- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IDocuEntity p(string Name, int Value) => p(Name, CreateIntEntity(Value));

        /// <summary>
        /// mko, 22.02.2019
        /// Eigenschaft mit long- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IDocuEntity p(string Name, long Value) => p(Name, CreateLongEntity(Value));

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit float- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IDocuEntity p(string Name, double Value) => p(Name, CreateDblEntity(Value));

        /// <summary>
        /// Dokumentiert das Setzen einer Eigenschaft auf einen neuen Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IDocuEntity pSet(string Name, IDocuEntity Value) => CreateEntity(DocuEntityTypes.PropertySet, Name, Value);


        /// <summary>
        /// Erzeugt eine Liste von DocuEntities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IDocuEntity List(params IDocuEntity[] entities) => new DocuEntity(fn, DocuEntityTypes.List, CreateListElements(entities));

        /// <summary>
        /// Erzeugt eine Liste von DocuEntities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IDocuEntity List(IEnumerable<IDocuEntity> entities) => new DocuEntity(fn, DocuEntityTypes.List, CreateListElements(entities));


        //---------------------------------------------------------------------------------------------------
        // private members for implementation

        /// <summary>
        /// Realisiert Kill- Kommandos
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public IDocuEntity ExecuteKillCommand(IDocuEntity details)
        {

            IDocuEntity ret = null;

            if (details != null)
            {
                if (details.EntityType == DocuEntityTypes.KillIfNot
                    && ((KillIfNot)details).Condition)
                {
                    // nicht killen
                    ret = ((KillIfNot)details).DocuEntity;
                }
                else if (details.EntityType == DocuEntityTypes.KillIfNot
                  && !((KillIfNot)details).Condition)
                {
                    // killen
                    ret = null;
                }
                else
                {
                    // kein KillIfNot- Kommando
                    ret = details;
                }
            }

            return ret;
        }

        /// <summary>
        /// mko, 8.5.2018
        /// Kill all parameters where conditions is not met
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private static IDocuEntity[] KillIfNotFilter(IDocuEntity[] pn)
        {
            // Alle herausfiltern, bei denen die kein kill durchgeführt werden soll, bzw. die kein KillIfNot sind
            var prms = pn.Where(r => (r.EntityType == DocuEntityTypes.KillIfNot && ((KillIfNot)r).Condition) || r.EntityType != DocuEntityTypes.KillIfNot);
            return prms.Select(r => r.EntityType == DocuEntityTypes.KillIfNot ? ((KillIfNot)r).DocuEntity : r).ToArray();
        }

        /// <summary>
        /// mko, 8.5.2018
        /// Kill all parameters where conditions is not met
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private static IDocuEntity[] KillIfNotFilter(IEnumerable<IDocuEntity> pn)
        {
            var prms = pn.Where(r => (r.EntityType == DocuEntityTypes.KillIfNot && ((KillIfNot)r).Condition) || r.EntityType != DocuEntityTypes.KillIfNot);
            return prms.Select(r => r.EntityType == DocuEntityTypes.KillIfNot ? ((KillIfNot)r).DocuEntity : r).ToArray();
        }

        /// <summary>
        /// mko, 1.3.2019
        /// Embeds sub- lists in current pn docu entity list
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private static IDocuEntity[] ResolveListsToEmbed(IEnumerable<IDocuEntity> pn)
        {
            var lst = new List<IDocuEntity>();

            // mko, 27.2.2019
            // Auflösen aller Einbettungen
            foreach (var dt in pn)
            {
                if (dt is ListToEmbed)
                {
                    var lEmbed = (ListToEmbed)dt;
                    if (lEmbed.Childs != null)
                        lst.AddRange(lEmbed.Childs);
                }
                else
                {
                    lst.Add(dt);
                }
            }

            return lst.ToArray();
        }

        /// <summary>
        /// Erzeugt ein Name- Wertepaar
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        //private IDocuEntity CreateNameValuePair(DocuEntityTypes type, string name, IDocuEntity pn)
        //    => new DocuEntity(fn, type, L(new String(name), pn));

        /// <summary>
        /// mko, 2.4.2019
        /// Erzeugt ein DocuEntity und berücksichtigt dabei, dass das Argument ein KillIfNot sein kann.
        /// </summary>
        /// <param name="docEType"></param>
        /// <param name="Name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private IDocuEntity CreateEntity(DocuEntityTypes docEType, string Name, IDocuEntity value)
        {
            IDocuEntity entity = null;

            if (value is KillIfNot kill)
            {
                if (!kill.Condition)
                {
                    // Muss killen !
                    entity = new DocuEntity(fn, docEType, L(new String(Name)));
                }
                else
                {
                    entity = new DocuEntity(fn, docEType, L(new String(Name), kill.DocuEntity));
                }
            }
            else
            {

                entity = new DocuEntity(fn, docEType, L(new String(Name), value));
            }

            return entity;
        }


        /// <summary>
        /// mko, 21.12.2018
        /// Instanzen und Methoden haben stets in Listen verpackte Member
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        private IDocuEntity CreateObjectWithMembers(DocuEntityTypes type, string name, params IDocuEntity[] _pn)
        {
            IDocuEntity res;

            // Member müssen stets in einer Liste verpackt werden
            // Achtung: beim erstellen der Liste werden Bedingt aufzunehmende Elemente und Einbettungslisten evaluiert. Die Liste 
            // kann dadurch schrumpfen oder wachsen.
            var memberList = List(_pn);

            // mko 21.12.2018
            // Parameter werden ab jetzt stets in eine Parameterliste eingekapselt
            //if (pn.Length > 1 || MandantoryWrapValuesInList)
            if (memberList.Childs.Any())
            {
                // Instanz mit Membern erzeugen
                res = new DocuEntity(fn, type, L<IDocuEntity>(new String(name), memberList));
            }
            else
            {
                // Instanz ohne Member erzeugen (leere Instanz)
                res = new DocuEntity(fn, type, L(new String(name)));
            }

            return res;
        }


        /// <summary>
        /// mko, 28.2.2019
        /// Creates list elements
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private IEnumerable<IDocuEntity> CreateListElements(IEnumerable<IDocuEntity> _pn)
        {
            IEnumerable<IDocuEntity> res;

            var pn = KillIfNotFilter(_pn);

            // mko 21.12.2018
            // Parameter werden ab jetzt stets in eine Parameterliste eingekapselt
            //if (pn.Length > 1 || MandantoryWrapValuesInList)
            if (pn.Any())
            {
                res = ResolveListsToEmbed(pn);
            }
            else
            {
                res = new IDocuEntity[] { };
            }

            return res;
        }



    }
}
