//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.3.2017
//
//  Projekt.......: mko.RPN
//  Name..........: ParserHelper.cs
//  Aufgabe/Fkt...: Erweiterungsmethoden zum Bearbeiten von Tokenlisten
//                  
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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    public static partial class ParserHelper
    {

        /// <summary>
        /// Struktur des Rückgabewertes von IndexOfFunction etc.
        /// </summary>
        public struct TokenIndex
        {
            public TokenIndex(int IX, int CountOfEvaluatedToken)
            {
                this.IX = IX;
                this.CountOfEvaluatedTokens = CountOfEvaluatedToken;
            }

            /// <summary>
            /// nummerischer Index eines Tokens in einem indizierten Tokenarray
            /// </summary>
            public int IX { get; set; }

            /// <summary>
            /// Anzahl der Token links von diesem, welche den Subtree der Funktion bilden
            /// </summary>
            public int CountOfEvaluatedTokens { get; set; }
        }


        /// <summary>
        /// Aus einer Liste von Tokens wird ein String mit einem Term in umgekehrt, polnischer Notation erzeugt.        
        /// z.B. a b add
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string ToRPNString(this mko.RPN.IToken[] Tokens, int begin = 0, int end = -1)
        {
            mko.TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            mko.TraceHlp.ThrowArgExIfNot(end == -1 || begin <= end, Properties.Resources.begin_greater_then_end);

            // RPN- Terme werden immer in der US- Cultur dargestellt    
            var currentCultBak = System.Globalization.CultureInfo.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            try
            {


                if (end == -1)
                {
                    end = Tokens.Length - 1;
                }
                if (begin < Tokens.Length)
                {
                    end = Math.Min(end, Tokens.Length - 1);

                    var bld = new System.Text.StringBuilder();
                    for (int i = begin; i <= end; i++)
                    {
                        bld.Append(Tokens[i].Value);
                        bld.Append(" ");
                    }
                    return bld.ToString();
                }
                else
                {
                    return "";
                }
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = currentCultBak;
            }
        }


        /// <summary>
        /// Aus einer Liste von Tokens wird ein String mit einem Term in polnischer Notation erzeugt.    
        /// z.B. add a b
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string ToPNString(this mko.RPN.IToken[] Tokens, int begin = 0, int end = -1)
        {
            mko.TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            mko.TraceHlp.ThrowArgExIfNot(end == -1 || begin <= end, Properties.Resources.begin_greater_then_end);

            // RPN- Terme werden immer in der US- Cultur dargestellt    
            var currentCultBak = System.Globalization.CultureInfo.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            try
            {
                if (end == -1)
                {
                    end = Tokens.Length - 1;
                }

                if (begin < Tokens.Length)
                {
                    end = Math.Min(end, Tokens.Length - 1);

                    var section = Tokens.Skip(begin).Take(end - begin + 1).Reverse();

                    var bld = new System.Text.StringBuilder();
                    foreach (var token in section)
                    {
                        bld.Append(token.Value);
                        bld.Append(" ");
                    }
                    return bld.ToString();
                }
                else
                {
                    return "";
                }
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = currentCultBak;
            }
        }

        public static IToken[] Copy(this IToken[] Tokens)
        {
            mko.TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            return Tokens.Select(r => r.Copy()).ToArray();
        }


        /// <summary>
        /// Löscht die allgemeinste (am weitesten rechts stehende) Funktion in der Tokenliste samt Subtree
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        public static mko.RPN.IToken[] RemoveFunction(this mko.RPN.IToken[] Tokens, string FunctionName)
        {
            mko.TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            var res = LastIndexOfFunction(Tokens, FunctionName);

            if (res.IX != -1)
            {
                return Tokens.Take(res.IX - res.CountOfEvaluatedTokens + 1)
                             .Concat(Tokens.Skip(res.IX + 1).Take(Tokens.Length - res.IX - 1))
                             .ToArray();
            }
            else
            {
                // Enthält di Funktion nicht
                return Tokens;
            }
        }


        /// <summary>
        /// Löscht die speziellste (am weitesten links stehende) Funktion in der Tokenliste samt Subtree
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        public static mko.RPN.IToken[] RemoveSubFunction(this mko.RPN.IToken[] Tokens, string FunctionName)
        {
            mko.TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            var res = IndexOfFunction(Tokens, FunctionName);

            if (res.IX != -1)
            {
                return Tokens.Take(res.IX - res.CountOfEvaluatedTokens + 1)
                             .Concat(Tokens.Skip(res.IX + 1).Take(Tokens.Length - res.IX - 1))
                             .ToArray();
            }
            else
            {
                // Enthält di Funktion nicht
                return Tokens;
            }
        }


        /// <summary>
        /// Ersetzt den Subtree einer allgemeinen (am weitesten rechts stehenden) Funktion durch einen anderen
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="FunctionName"></param>
        /// <param name="FunctionSubTree"></param>
        /// <returns></returns>
        public static IToken[] ReplaceFunction(this IToken[] Tokens, string FunctionName, IToken[] FunctionSubTree)
        {
            TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            var res = LastIndexOfFunction(Tokens, FunctionName);

            if (res.IX != -1)
            {

                return Tokens.Take(res.IX - res.CountOfEvaluatedTokens + 1)
                             .Concat(FunctionSubTree)
                             .Concat(Tokens.Skip(res.IX + 1).Take(Tokens.Length - res.IX - 1))
                             .ToArray();
            }
            else
            {
                // Enthält die Funktion nicht
                return Tokens;
            }

        }


        /// <summary>
        /// Ersetzt den Subtree eine spezielle (am weitesten links stehenden) Funktion durch einen anderen
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="FunctionName"></param>
        /// <param name="FunctionSubTree"></param>
        /// <returns></returns>
        public static IToken[] ReplaceSubFunction(this IToken[] Tokens, string FunctionName, IToken[] FunctionSubTree)
        {
            TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            var res = IndexOfFunction(Tokens, FunctionName);

            if (res.IX != -1)
            {

                return Tokens.Take(res.IX - res.CountOfEvaluatedTokens + 1)
                             .Concat(FunctionSubTree)
                             .Concat(Tokens.Skip(res.IX + 1).Take(Tokens.Length - res.IX - 1))
                             .ToArray();
            }
            else
            {
                // Enthält die Funktion nicht
                return Tokens;
            }

        }


        /// <summary>
        /// Vergleich von zwei Tokenliste auf Identität. Identität wird auf einen elementweisen Vergleich 
        /// zurückgeführt.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool EQ(this IToken[] left, IToken[] right)
        {
            TraceHlp.ThrowArgExIfNot(left != null && right != null, Properties.Resources.Null_Ref_Tokens);
            if (left.Count() != right.Count())
            {
                return false;
            } else
            {
                // Wert für Wert vergleichen
                bool res = true;
                for(int i = 0, count = left.Count(); i < count; i++)
                {
                    if(left[i].IsNummeric && right[i].IsNummeric)
                    {
                        // Nummerische Werte in gleichen Typ umwandeln. So wird verhindert, das z.B. 2 <> 2.0 ist
                        double vleft = left[i].IsInteger ? ((IntToken)left[i]).ValueAsInt : ((DoubleToken)left[i]).ValueAsDouble;
                        double vright = right[i].IsInteger ? ((IntToken)right[i]).ValueAsInt : ((DoubleToken)right[i]).ValueAsDouble;
                        res &= Math.Round(vleft, 14) == Math.Round(vright, 14);
                    } else
                    {
                        // Fall: Strings, Bools, Funktionsnamen
                        res &= left[i].Value == right[i].Value;
                    }
                }
                return res;
            }
        }


        /// <summary>
        /// Liefert den Index einer Funktion in einer Tokenliste. Es wird immer die speziellste,
        /// bzw. in RPN am weitesten links stehende Funktion ermittelt, falls es im betrachteten
        /// Abschnitt der Tokenliste (begin, end) mehrere Exemplare geben sollte
        /// </summary>
        /// <param name="Tokens">Tokenliste bzw. Syntaxbaum als Tokenliste</param>
        /// <param name="FunctionName">Funktion, die in der Liste gesucht wird</param>
        /// <param name="begin">Index, ab dem gesucht wird</param>
        /// <param name="end">Index, bis zu dem gesucht wird</param>
        /// <returns></returns>
        public static TokenIndex IndexOfFunction(this mko.RPN.IToken[] Tokens, string FunctionName, int begin = 0, int end = 0)
        {
            TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            TraceHlp.ThrowArgExIf(string.IsNullOrEmpty(FunctionName), Properties.Resources.Functionname_null_or_empty);

            end = end == 0 ? Tokens.Length : end;
            TokenIndex res = new TokenIndex();
            res.CountOfEvaluatedTokens = -1;
            res.IX = -1;

            //FunctionName = FunctionName.ToLower();

            if (Tokens.Skip(begin).Take(end - begin).Any(t => t.IsFunctionName && t.Value == FunctionName))
            {
                var tok = Tokens.Skip(begin).Take(end - begin).First(t => t.IsFunctionName && t.Value == FunctionName);
                res.CountOfEvaluatedTokens = tok.CountOfEvaluatedTokens;
                res.IX = Array.LastIndexOf(Tokens, tok);
            }

            return res;
        }



        /// <summary>
        /// Liefert den Index einer Funktion in einer Tokenliste. Es wird immer die allgemeinste,
        /// bzw. in RPN am weitesten rechts stehende Funktion ermittelt, falls es im betrachteten
        /// Abschnitt der Tokenliste (begin, end) mehrere Exemplare geben sollte
        /// </summary>
        /// <param name="Tokens">Tokenliste bzw. Syntaxbaum als Tokenliste</param>
        /// <param name="FunctionName">Funktion, die in der Liste gesucht wird</param>
        /// <param name="begin">Index, ab dem gesucht wird</param>
        /// <param name="end">Index, bis zu dem gesucht wird</param>
        /// <returns></returns>
        public static TokenIndex LastIndexOfFunction(this mko.RPN.IToken[] Tokens, string FunctionName, int begin = 0, int end = 0)
        {
            TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            TraceHlp.ThrowArgExIf(string.IsNullOrEmpty(FunctionName), Properties.Resources.Functionname_null_or_empty);

            end = end == 0 ? Tokens.Length : end;
            TokenIndex res = new TokenIndex();
            res.CountOfEvaluatedTokens = -1;
            res.IX = -1;

            //FunctionName = FunctionName.ToLower();

            if (Tokens.Skip(begin).Take(end - begin).Any(t => t.IsFunctionName && t.Value == FunctionName))
            {
                var tok = Tokens.Skip(begin).Take(end - begin).Last(t => t.IsFunctionName && t.Value == FunctionName);
                res.CountOfEvaluatedTokens = tok.CountOfEvaluatedTokens;
                res.IX = Array.LastIndexOf(Tokens, tok);
            }

            return res;
        }

        /// <summary>
        /// Bestimmt den aboluten Index des i-ten Parameters von rechts betrachtet einer Funktion, die sich an einem gegebenen PArameter befindet.
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="IndexOfFunction"></param>
        /// <param name="ParameterRightIndex"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns>Struktur TokenIndex, die IX mit absoluten Index und Anzahl der Tokens, die den Parameter bilden, darstellt</returns>
        public static TokenIndex IndexOfFunctionParameter(this mko.RPN.IToken[] Tokens, int IndexOfFunction, int ParameterRightIndex, int begin = 0, int end = 0)
        {
            if (end == 0)
            {
                end = Tokens.Length - 1;
            }

            TraceHlp.ThrowArgExIfNot(begin <= end, Properties.Resources.begin_greater_then_end);
            TraceHlp.ThrowArgExIfNot(begin <= IndexOfFunction && IndexOfFunction <= end, Properties.Resources.ParserHelpler_IndexOfFunctionParameter_Function_Index_out_of_Range);

            var fToken = Tokens[IndexOfFunction];

            TraceHlp.ThrowArgExIfNot(fToken.IsFunctionName, Properties.Resources.functiontoken_expected);

            int ixSubtree = IndexOfFunction - 1;
            if (ParameterRightIndex == 1)
            {
                TraceHlp.ThrowArgExIfNot(begin <= ixSubtree, string.Format(Properties.Resources.begin_starts_later_then_param_i_of_function, 1));
                return new TokenIndex(ixSubtree, Tokens[ixSubtree].CountOfEvaluatedTokens);
            }
            else
            {
                // Überspringen des ersten und eventuell weiterer Parameter bis zum gesuchten            
                for (int i = 2; i <= ParameterRightIndex; i++)
                {
                    ixSubtree -= Tokens[ixSubtree].CountOfEvaluatedTokens;
                    TraceHlp.ThrowArgExIfNot(begin <= ixSubtree, string.Format(Properties.Resources.begin_starts_later_then_param_i_of_function, i));
                }
            }

            return new TokenIndex(ixSubtree, Tokens[ixSubtree].CountOfEvaluatedTokens);
        }


        /// <summary>
        /// Bestimmt die Anzahl der Parameter
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="IndexOfFunction"></param>
        /// <param name="ParameterRightIndex"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int FunctionParameterCount(this IToken[] Tokens, int IndexOfFunction, int begin = 0, int end = 0)
        {
            if (end == 0)
            {
                end = Tokens.Length - 1;
            }

            TraceHlp.ThrowArgExIfNot(begin <= end, Properties.Resources.begin_greater_then_end);
            TraceHlp.ThrowArgExIfNot(begin <= IndexOfFunction && IndexOfFunction <= end, Properties.Resources.ParserHelpler_IndexOfFunctionParameter_Function_Index_out_of_Range);

            var fToken = Tokens[IndexOfFunction];
            TraceHlp.ThrowArgExIfNot(fToken.IsFunctionName, Properties.Resources.functiontoken_expected);            
            
            if (fToken.CountOfEvaluatedTokens == 1)
            {
                return 0;
            }
            else
            {
                int count = 0;
                int ixSubtree = IndexOfFunction - 1;
                TraceHlp.ThrowArgExIfNot(begin <= ixSubtree, Properties.Resources.range_of_search_starts_inside_parameterlist);

                int ixNextSubtree = IndexOfFunction - fToken.CountOfEvaluatedTokens;
                TraceHlp.ThrowArgExIfNot(begin <= ixNextSubtree + 1, string.Format(Properties.Resources.begin_starts_later_then_param_i_of_function, count));


                // Überspringen des ersten und eventuell weiterer Parameter bis zum gesuchten            
                for (; ixSubtree > ixNextSubtree; count++)
                {
                    ixSubtree -= Tokens[ixSubtree].CountOfEvaluatedTokens;                    
                }

                return count;
            }

        }

        /// <summary>
        /// Zählt die Anzahl der allgemeinen (am weitesten rechts stehenden) Funktion
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="fn"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int FunctionParameterCount(this IToken[] Tokens, string fn, int begin = 0, int end = 0)
        {
            var ixF = Tokens.LastIndexOfFunction(fn, begin, end);
            return Tokens.FunctionParameterCount(ixF.IX, begin, end);

        }


        /// <summary>
        /// Liefert die Anzahl der Parameter für eine Liste von Tokens, die den Subtree einer Funktion darstellt
        /// (letztes Token ist ein Funktionsname, alle davor bilden die Parameterliste)
        /// </summary>
        /// <param name="FunctionSubTree"></param>
        /// <returns></returns>
        public static int FunctionParameterCount(this IToken[] FunctionSubTree)
        {
            TraceHlp.ThrowArgExIfNot(FunctionSubTree.IsFunctionSubtree(), Properties.Resources.FunctionSubtreeExpected);
            return FunctionSubTree.FunctionParameterCount(FunctionSubTree.Length - 1);
        }

        /// <summary>
        /// Zählt die Anzahl der speziellen (am weitesten links stehenden) Funktion
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="fn"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int SubFunctionParameterCount(this IToken[] Tokens, string fn, int begin = 0, int end = 0)
        {
            var ixF = Tokens.IndexOfFunction(fn, begin, end);
            return Tokens.FunctionParameterCount(ixF.IX, begin, end);

        }



        /// <summary>
        /// Liefert das Token inklusive seiner untergeordnenten Tokens (Tree)
        /// </summary>
        /// <param name="Tokens">Syntaxbaum als Tokenliste</param>
        /// <param name="IxOfToken">Index des Tokens, dessen Subtree zurückgegeben werden soll</param>
        /// <returns></returns>
        public static IToken[] GetSubtree(this IToken[] Tokens, int IxOfToken)
        {
            TraceHlp.ThrowArgExIf(IxOfToken < 0 || Tokens.Length - 1 < IxOfToken, Properties.Resources.GetFunction_index_out_of_range);

            var countOfEvaluated = Tokens[IxOfToken].CountOfEvaluatedTokens;
            return Tokens.Skip(IxOfToken + 1 - countOfEvaluated).Take(countOfEvaluated).ToArray();
        }


        /// <summary>
        /// Liefert den Subtree einer Funktion mit gegebenen Namen, falls diese existiert.
        /// Es wird in einem RPN- Baum immer die allgemeinste, am weitesten rechts stehende Funktion geliefert.
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        public static IToken[] GetFunctionSubtree(this IToken[] Tokens, string FunctionName)
        {
            TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            TraceHlp.ThrowArgExIf(string.IsNullOrEmpty(FunctionName), Properties.Resources.Functionname_null_or_empty);

            var ixF = LastIndexOfFunction(Tokens, FunctionName);
            TraceHlp.ThrowArgExIf(ixF.IX == -1, Properties.Resources.ParserHelpler_IndexOfFunctionParameter_Function_Index_out_of_Range);

            return GetSubtree(Tokens, ixF.IX);
        }


        /// <summary>
        /// Liefert den Subtree einer Funktion mit gegebenen Namen, falls diese existiert.
        /// Es wird in einem RPN- Baum immer die speziellste, am weitesten links stehende Funktion geliefert.
        /// </summary>
        /// <param name="Tokens"></param>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        public static IToken[] GetSubFunctionSubtree(this IToken[] Tokens, string FunctionName)
        {
            TraceHlp.ThrowArgExIfNot(Tokens != null, Properties.Resources.Null_Ref_Tokens);
            TraceHlp.ThrowArgExIf(string.IsNullOrEmpty(FunctionName), Properties.Resources.Functionname_null_or_empty);

            var ixF = IndexOfFunction(Tokens, FunctionName);
            TraceHlp.ThrowArgExIf(ixF.IX == -1, Properties.Resources.ParserHelpler_IndexOfFunctionParameter_Function_Index_out_of_Range);

            return GetSubtree(Tokens, ixF.IX);
        }



        /// <summary>
        /// Liefert für den Subtree einer Funktion den i-ten Parametersubtree
        /// </summary>
        /// <param name="FunctionSubtree"></param>
        /// <param name="ParameterRightIndex"></param>
        /// <returns></returns>
        public static IToken[] GetParameterSubtree(this IToken[] FunctionSubtree, int ParameterRightIndex)
        {
            TraceHlp.ThrowArgExIfNot(FunctionSubtree.IsFunctionSubtree(), Properties.Resources.FunctionSubtreeExpected);

            var ixParam = FunctionSubtree.IndexOfFunctionParameter(FunctionSubtree.Length - 1, ParameterRightIndex);
            TraceHlp.ThrowArgExIf(ixParam.IX == -1, Properties.Resources.ParameterSubtreeDontExists);

            return GetSubtree(FunctionSubtree, ixParam.IX);
        }

        /// <summary>
        /// Wenn der das letzte Token eine Funktion ist, und die Anzahl der für sie evaluiierten Tokens
        /// gleich der Länge der Subtree Tokenliste ist, dann ist die Liste eine Funktions- Subtree
        /// </summary>
        /// <param name="Subtree"></param>
        /// <returns></returns>
        public static bool IsFunctionSubtree(this IToken[] Subtree)
        {
            if (Subtree == null)
            {
                return false;
            }
            else
            {
                var last = Subtree.Last();
                return last.IsFunctionName && last.CountOfEvaluatedTokens == Subtree.Length;
            }
        }

        /// <summary>
        /// Wenn der das letzte Token eine Funktion mit dem gegebenen Namen ist, und die Anzahl der für sie evaluiierten Tokens
        /// gleich der Länge der Subtree Tokenliste ist, dann ist die Liste eine Funktions- Subtree
        /// </summary>
        /// <param name="Subtree"></param>
        /// <param name="FunctionName">Name der Funktion, deren Vorliegen hier geprüft wird</param>
        /// <returns></returns>
        public static bool IsFunctionSubtree(this IToken[] Subtree, string FunctionName)
        {
            if(Subtree == null)
            {
                return false;
            } else
            {
                var last = Subtree.Last();
                return last.IsFunctionName && last.CountOfEvaluatedTokens == Subtree.Length && last.Value == FunctionName;
            }

        }

        public static string FunctionName(this IToken[] fSubtree)
        {
            TraceHlp.ThrowArgExIfNot(fSubtree.IsFunctionSubtree(), Properties.Resources.FunctionSubtreeExpected);
            return fSubtree.Last().Value;
        }


    }
}
