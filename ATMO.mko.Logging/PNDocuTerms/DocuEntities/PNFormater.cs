using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static mko.RPN.UrlSaveStringEncoder;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 23.3.2018
    /// Formats DocuEntities in polish notation
    /// 
    /// mko, 6.12.2018
    /// Beginning from mko.RPN 18.12.1 the tokenizer automaticly decodes from RPNUrlSaveString.
    /// This will exploited here: 
    /// </summary>
    public class PNFormater : IFormater
    {
        /// <summary>
        /// Function Name Prefixes (Table of keywords)
        /// </summary>
        IFn fn = Fn._;

        bool RPNUrlSaveEncode = false;

        global::mko.RPN.Composer basicComp;

        string delimitIfneeded(string txt)
        {
            var str = basicComp.Str(txt);
            return str.RPNUrlSaveStringEncodeIf(RPNUrlSaveEncode);
        }

        public PNFormater(bool RPNUrlSaveEncode = false)
        {
            basicComp = new global::mko.RPN.Composer(Fn._, RPNUrlSaveEncode);
            this.RPNUrlSaveEncode = RPNUrlSaveEncode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn">Table of Keywords</param>
        public PNFormater(IFn fn, bool RPNUrlSaveEncode = false)
        {
            this.fn = fn;
            this.RPNUrlSaveEncode = RPNUrlSaveEncode;
            basicComp = new global::mko.RPN.Composer(fn, RPNUrlSaveEncode);
        }

        public string Print(IDocuEntity entity)
        {
            var bld = new StringBuilder();
            switch (entity.EntityType)
            {
                case DocuEntityTypes.Event:
                    {
                        ToTypeNameValue(fn, entity, fn.Event, bld);
                    }
                    break;
                case DocuEntityTypes.Instance:
                    {
                        ToTypeNameValue(fn, entity, fn.Instance, bld);
                    }
                    break;
                case DocuEntityTypes.Method:
                    {
                        ToTypeNameValue(fn, entity, fn.Method, bld);
                    }
                    break;
                case DocuEntityTypes.List:
                    {
                        bld.Append($" {fn.List} ")
                           .Append(System.String.Join(" ", entity.Childs.Select(c => Print(c))))
                           .Append($" {fn.ListEnd} ");
                    }
                    break;
                case DocuEntityTypes.Property:
                    {
                        ToTypeNameValue(fn, entity, fn.Property, bld);
                    }
                    break;
                case DocuEntityTypes.PropertySet:
                    {

                    }
                    break;
                case DocuEntityTypes.String:
                    {
                        return delimitIfneeded(((String)entity).Value);
                    }
                case DocuEntityTypes.Text:
                    {
                        bld.Append($" {fn.Txt} ")
                           .Append(System.String.Join(" ", entity.Childs.Select(c => Print(c))))
                           .Append($" {fn.ListEnd}");

                        //bld.Append(delimitIfneeded(System.String.Join(" ", entity.Childs.Select(c => Print(c))).Trim()));

                    }
                    break;
                case DocuEntityTypes.Version:
                    {
                        bld.Append($" {fn.Version} {Print(entity.Childs.First())}");
                    }
                    break;
                case DocuEntityTypes.ReturnValue:
                    {
                        ToTypeNameValue(fn, entity, fn.Return, bld);
                    }
                    break;
                // mko, 5.3.2019
                // Ausgabe von Time
                case DocuEntityTypes.Time:
                    {
                        bld.Append($" {fn.Time} {Print(entity.Childs.First())}");
                    }
                    break;
                // mko, 5.3.2019
                // Ausgabe von Date
                case DocuEntityTypes.Date:
                    {
                        bld.Append($" {fn.Date} {Print(entity.Childs.First())}");
                    }
                    break;

                default:
                    ;
                    break;
            }

            return bld.ToString();
        }


        private void ToTypeNameValue(IFn fn, IDocuEntity entity, string TypeName, StringBuilder bld)
        {
            TraceHlp.ThrowArgExIf(entity.Childs.Count() < 1, "at least name and one value expected");
            bld.Append($" {TypeName} {Print(entity.Childs.First())}");

            foreach (var c in entity.Childs.Skip(1))
            {
                bld.Append($" {Print(c)}");
            }
        }
    }
}
