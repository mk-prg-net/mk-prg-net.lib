using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

using System.Collections.Generic;
using System.Diagnostics;

namespace mko.NaLisp.JSon.Test
{
    [TestClass]
    public class JSonExpressions
    {
        [TestMethod]
        public void JSonToNalisp_simple_terms()
        {
            // 3 * (A + 5*B)
            var jsonString = @"{'mul': [{'const': 3}, {'add': [{'varInt': 'A'}, {'varInt': 'B'}]}]}";

            dynamic term = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

            var term2 = Newtonsoft.Json.Linq.JObject.Parse(jsonString);

            var root = term2.First;

            var reader = new Newtonsoft.Json.JsonTextReader(new StringReader(jsonString));
            parseFunc(reader);
            Debug.WriteLine("");

        }

        /// <summary>
        /// Parst einen NaLisp- Ausdruck in JSON Syntax ein
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static bool parseFunc(Newtonsoft.Json.JsonTextReader reader)
        {
            while (reader.Read())
            {
                if (reader.TokenType == Newtonsoft.Json.JsonToken.StartObject)
                {
                    if (reader.Read())
                    {
                        if (reader.Value != null)
                        {
                            var funcname = (string)reader.Value;
                            switch (funcname)
                            {
                                case "mul":
                                    Debug.Write("(mul ");
                                    if (!parseParameterlist(reader))
                                        return false;
                                    break;
                                case "add":
                                    Debug.Write("(add ");
                                    if (!parseParameterlist(reader))
                                        return false;
                                    break;
                                case "const":
                                    return ReadInt(reader);
                                case "varInt":
                                    return ReadVar(reader);
                                default:
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
        /// Einlesen einer NaLisp- Integerkonstante
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static bool ReadInt(Newtonsoft.Json.JsonTextReader reader)
        {
            // einen  Integer einlesen
            if (reader.Read() && reader.TokenType == Newtonsoft.Json.JsonToken.Integer)
            {
                Debug.Write("" + (long)reader.Value + " ");
                return reader.Read() && reader.TokenType == Newtonsoft.Json.JsonToken.EndObject;
            }
            else
            {
                Debug.WriteLine("ERROR: Integerkonstante erwartet");
                return false;
            }
        }


        /// <summary>
        /// Einlesen einer NaLisp Variable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static bool ReadVar(Newtonsoft.Json.JsonTextReader reader)
        {
            // einen  Integer einlesen
            if (reader.Read() && reader.TokenType == Newtonsoft.Json.JsonToken.String)
            {
                Debug.Write(" (var " + (string)reader.Value + ")");
                return reader.Read() && reader.TokenType == Newtonsoft.Json.JsonToken.EndObject;
            }
            else
            {
                Debug.WriteLine("ERROR: Varaiblenname als String erwartet");
                return false;
            }
        }

        /// <summary>
        /// Einlesen einer NaLisp- Variable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static bool parseParameterlist(Newtonsoft.Json.JsonTextReader reader)
        {
            if (reader.Read())
            {
                // Parameterliste einlesen

                if (reader.TokenType == Newtonsoft.Json.JsonToken.StartArray)
                {
                    while (parseFunc(reader)) { }

                    if (reader.TokenType == Newtonsoft.Json.JsonToken.EndArray)
                    {
                        if (reader.Read() && reader.TokenType == Newtonsoft.Json.JsonToken.EndObject)
                        {
                            Debug.Write(")");
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
