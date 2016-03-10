using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Base = mko.Algo.FormalLanguages.NaLisp;

namespace mko.NaLisp.JSon
{
    public class Parser
    {

        public bool TryParse(string jsonNaLispExpression, Dictionary<string, IFuncParser> FuncParsers, out Base.Core.NaLisp NaLispExpression)
        {
            var reader = new Newtonsoft.Json.JsonTextReader(new StringReader(jsonNaLispExpression));
            return parseFunc(reader, FuncParsers, out NaLispExpression);
        }

        /// <summary>
        /// Parst einen NaLisp- Ausdruck in JSON Syntax ein
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static bool parseFunc(Newtonsoft.Json.JsonTextReader reader, Dictionary<string, IFuncParser> FuncParsers, out Base.Core.NaLisp NaExp)
        {
            NaExp = null;
            if(reader.Read())
            {
                if (reader.TokenType == Newtonsoft.Json.JsonToken.StartObject)
                {
                    if (reader.Read())
                    {
                        if (reader.Value != null)
                        {
                            var funcname = (string)reader.Value;

                            if (FuncParsers.ContainsKey(funcname))
                            {
                                return FuncParsers[funcname].TryParse(reader, FuncParsers, out NaExp);
                            }
                            else
                            {
                                Debug.WriteLine("unbekannte Funktion " + funcname);
                                return false;
                            }
                        }
                        else
                        {
                            Debug.WriteLine("ERROR: NaLisp- Funktionsliste muss immer mit einem Namen beginnen");
                            return false;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("ERROR: leeres Objekt");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Einlesen einer NaLisp- Variable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static bool parseParameterlist(Newtonsoft.Json.JsonTextReader reader, Dictionary<string, IFuncParser> FuncParsers, out Base.Core.NaLisp[] parameters)
        {
            parameters = null;
            if (reader.Read())
            {
                // Parameterliste einlesen

                if (reader.TokenType == Newtonsoft.Json.JsonToken.StartArray)
                {
                    var parametersDraft = new List<Base.Core.NaLisp>();
                    Base.Core.NaLisp expression;
                    while (parseFunc(reader, FuncParsers, out expression))
                    {
                        parametersDraft.Add(expression);
                    }

                    if (reader.TokenType == Newtonsoft.Json.JsonToken.EndArray)
                    {
                        if (reader.Read() && reader.TokenType == Newtonsoft.Json.JsonToken.EndObject)
                        {
                            Debug.Write(")");
                            parameters = parametersDraft.ToArray();
                            return true;
                        }
                        else
                        {
                            Debug.WriteLine(" ERROR: Funkmtion nicht abgeschlossen, endet mit " + reader.TokenType);
                            return false;
                        }
                    }
                    else
                    {
                        Debug.WriteLine(" ERROR: Parameterliste unvollständig, Endet mit " + reader.TokenType);
                        return false;
                    }

                }
                else
                {
                    Debug.WriteLine("Parameterliste  als Array erwartet");
                    return false;
                }
            }
            else
            {
                // Fehlerhafte Struktur
                Debug.WriteLine(" ERROR: Funktion hat keine Parameter- in NaLisp verboten");
                return false;
            }
        }



    }
}
