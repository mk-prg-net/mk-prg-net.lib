using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.NaLisp;

namespace mko.NaLisp.JSon
{
    public abstract class ParseUnary<T> : FuncParserBase
    {
        protected abstract Base.Core.INaLisp CreateNaLisp(T Parameter);

        Newtonsoft.Json.JsonToken TokenType
        {
            get
            {
                if (typeof(T) == typeof(string))
                {
                    return Newtonsoft.Json.JsonToken.String;
                }
                else if(typeof(T) == typeof(int)){
                    return Newtonsoft.Json.JsonToken.Integer;
                }
                else if (typeof(T) == typeof(long))
                {
                    return Newtonsoft.Json.JsonToken.Integer;
                }
                else if (typeof(T) == typeof(double))
                {
                    return Newtonsoft.Json.JsonToken.Float;
                }
                else if (typeof(T) == typeof(DateTime))
                {
                    return Newtonsoft.Json.JsonToken.Date;
                }
                else if (typeof(T) == typeof(bool))
                {
                    return Newtonsoft.Json.JsonToken.Boolean;
                }
                else
                {
                    throw new Exception(mko.TraceHlp.FormatErrMsg(this, "TokenType", "unbekannter TokenTyp"));
                }
            }
        }


        public override bool TryParse(Newtonsoft.Json.JsonTextReader reader, Dictionary<string, IFuncParser> FuncParsers, out mko.NaLisp.Core.INaLisp NaExp)
        {
            NaExp = null;

            // einen  Integer einlesen
            if (reader.Read() && reader.TokenType == this.TokenType)
            {
                var value = (T)reader.Value;
                Debug.Write(" (" + FunctionName + " " + value.ToString() + ")");
                NaExp = CreateNaLisp(value);
                return reader.Read() && reader.TokenType == Newtonsoft.Json.JsonToken.EndObject;
            }
            else
            {
                Debug.WriteLine("ERROR: " + FunctionName + " als " + typeof(T).Name + " erwartet");
                return false;
            }

        }

    }
}
