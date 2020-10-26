//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2016
//
//  Projekt.......: mko.RPN
//  Name..........: Parser.cs
//  Aufgabe/Fkt...: Ein String mit einem Ausdruck in der RPN- Notation 
//                  (Reverse Polish Notation => Eingabesyntax der programmierbaren 
//                   HP- Taschenrechner aus den 1970-er Jahren)
//                  wie 2 3 ADD 5 MUL <=> (2 + 3) * 5 
//                  wird eingelesen, analysiert und im Falle eines korrekten 
//                  Ausdruckes ausgewertet.
//                  Der Parser ist generisch implementiert, und kann 
//                  zum Einlesen bliebiger Typ 2- Sprachenterme verwendet werden.
//                  Das Motiv zur Entwicklung war ein System von Filter und Sortierausdrücken
//                  in Uri's zu implementieren. Sollen z.B. in einem Webkatalog für 
//                  astronomische Daten nach allen Riesenplaneten gesucht werden,
//                  (schwerer als 2 Erdmassen), mit mehr als 10 Monden, sortiert nach dem 
//                  Sonnenabstand, könnte dies in einem URL wie folgt kodiert werden:
//                  http://<host:port>/Kepler/Planets/Index?filter=2 EM dblMax MassRng 10 intMax MoonCount asc OrderBySemiMajorAxisLength     
//                  2 EM dblMax MassRng             -> MassRange(mko.Newton.Mass.Earthmass(2), mko.Newton.Mass.Kilogramm(double.MaxValue)
//                  10 intMax MoonCount             -> MoonCount(10, int.MaxValue)
//                  asc OrderBySemiMajorAxisLength  -> OrderBySemiMajoRAxisLength(false)
// 
//                  Ab 6.4.2017 werden die Methoden TokenizeRPN un TokenizePN angeboten
//                  TokenizePN kann dabei insbesondere eine Quelltext in der polnischen Notation
//                  einlesen, und gibt diesen dann als Liste von Tokens in der umgekehrt polnischen
//                  Notation zurück
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
//  Datum.........: 6.4.2017
//  Änderungen....: Die Methoden TokenizeRPN und TokenizePN wurden implementiert.
//                  TokenizePN kann dabei insbesondere eine Quelltext in der polnischen Notation
//                  einlesen, und gibt diesen dann als Liste von Tokens in der umgekehrt polnischen
//                  Notation zurück.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 11.3.2018
//  Änderungen....: Neue Version ParserV2, die eine reimplementierung der Klasse Parser aus den Vorversionen
//                  darstellt.
//                  Reimplementierung des Parsers. Vorversion dokumentierte unzureichend die beim Parsen 
//                  eine PN/RPN Strings gefundenen Fehler. Konfiguration des Parsers war auf vielfältige 
//                  weise möglich.
//                  1) Verinfachte Konfiguration (Evaluator- Tabelle nur noch im Konstruktor injezieren)
//                  2) Rückmeldung von syntaktischen Fehlern im Parser in strukturierter Form
//                  3) Anstatt mit Exceptions mit mko.Logging.RC Fehler rückmelden
//
//</unit_history>
//</unit_header>        


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.Logging;


namespace mko.RPN
{
    /// <summary>
    /// mko, 11.3.2018
    /// Reimplementierung des Parsers. Vorversion dokumentierte unzureichend die beim Parsen 
    /// eine PN/RPN Strings gefundenen Fehler. Konfiguration des Parsers war auf vielfältige 
    /// weise möglich.
    /// 1) Verinfachte Konfiguration (Evaluator- Tabelle nur noch im Konstruktor injezieren)
    /// 2) Rückmeldung von syntaktischen Fehlern im Parser in strukturierter Form
    /// 3) Anstatt mit Exceptions mit mko.Logging.RC Fehler rückmelden
    /// </summary>
    public class ParserV2
    {
        /// <summary>
        /// mko, 11.3.2018
        /// Liefert das Ergebnis der Evaluierung eines pn/rpn- Ausdruckes mittels Parser
        /// </summary>
        public class Result
        {
            public Result(
                Stack<IToken> Stack,
                Stack<int> ParamCountStack,
                BufferedTokenizer EvaluatedTokenBuffer,
                int IndexOfLastEvaluatedToken)
            {
                this.Stack = Stack;
                this.ParamCountStack = ParamCountStack;
                this.EvaluatedTokenBuffer = EvaluatedTokenBuffer;
                this.IndexOfLastEvaluatedToken = IndexOfLastEvaluatedToken;
            }

            public int IndexOfLastEvaluatedToken { get; }

            /// <summary>
            /// Stack, used for evaluation of pn/rpn term
            /// </summary>
            public Stack<IToken> Stack { get; }

            /// <summary>
            /// Stack wich reflects for each evaluated function token count of function parameters
            /// </summary>
            public Stack<int> ParamCountStack { get; }

            /// <summary>
            /// After evaluation of an pn/rpn term all tokens with evaluated EvaluatedParamCount Tokens
            /// </summary>
            public BufferedTokenizer EvaluatedTokenBuffer { get; }

            /// <summary>
            /// Initialwert, es fand noch keine Evaluierung statt
            /// </summary>
            public static Result NullObject
            {
                get
                {
                    if (_NullObject == null)
                    {
                        _NullObject = new Result(null, null, null, -1);
                    }
                    return _NullObject;
                }

            }
            public static Result _NullObject;

        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="tokenizer">Achtung: kann ein tokenizer für polish notation oder reverse polish notation sein</param>
        /// <param name="evalTab">Tabelle, die einem Funktionsnamen einen Evaluator zuordnet</param>
        public ParserV2(IReadOnlyDictionary<string, IEval> FuncEvaluators)
        {
            this.FuncEvaluators = FuncEvaluators;
        }

        IReadOnlyDictionary<string, IEval> FuncEvaluators;

       /// <summary>
       /// mko, 11.3.2018
       /// Parst eine Liste von Tokens und evaluiert sie, falls möglich. Das Ergebnis ist wird 
       /// durch den Zustand des Stapelspeichers des Kellerautomaten repräsentiert.
       /// </summary>
       /// <param name="Tokens">Liste von Tokens. Wurde zuvor mittels BasicTokenizer.TokenizePN oder TokenizeRPN erstellt</param>
       /// <returns></returns>
        public RC<Result> Parse(IToken[] Tokens)
        {
            RC<Result> rc = RC<Result>.Ok(Result.NullObject);

            // Stack der Tokens
            var stack = new Stack<IToken>();

            // Stack der Parameterzahlen
            var paramCountStack = new Stack<int>();

            var TokenBuffer = new BufferedTokenizer(); 

            int ixToken = -1;

            try
            {
                int count; // = Tokens.Count();
                for (ixToken = 0, count = Tokens.Count(); ixToken < count && rc.Succeeded; ixToken++)
                {
                    IToken token = Tokens[ixToken];
                    if (token.IsFunctionName)
                    {
                        var funcname = token.Value;
                        if (FuncEvaluators.ContainsKey(funcname))
                        {
                            var fe = FuncEvaluators[funcname];

                            int paramCount = stack.Count;
                            fe.Eval(stack);
                            if (!fe.Succesful)
                            {
                                rc = RC<Result>.Failed(value: new Result(stack, paramCountStack, TokenBuffer, ixToken),
                                    ErrorDescription: $"Evaluation of {funcname} fails: {fe.EvalException}");
                            }
                            else
                            {
                                // Anzahl verarbeiteter Parameter pro Prozedur bestimmen und protokollieren
                                paramCount -= stack.Count;
                                paramCount++; // Korrektur, da stack nun das Ergebnis enthält
                                int CountOfEvaluatedTokens = 0;
                                for (int i = 0; i < paramCount; i++)
                                {
                                    CountOfEvaluatedTokens += paramCountStack.Pop();
                                }
                                paramCountStack.Push(CountOfEvaluatedTokens + 1);
                                TokenBuffer.Add(new FunctionNameToken(token.Value, CountOfEvaluatedTokens + 1));
                            }
                        }
                        else
                        {
                            rc = RC<Result>.Failed(value: new Result(stack, paramCountStack, TokenBuffer, ixToken),
                                    ErrorDescription: $"unknown function {funcname} found");
                        }
                    }
                    else
                    {
                        stack.Push(token);
                        TokenBuffer.Add(token);
                        paramCountStack.Push(1);
                    }

                }

                if (rc.Succeeded)
                {
                    // Lestzen Stand des Stacks sichern nach erfolgreicher Evaluierung sichern
                    rc = RC<Result>.Ok(new Result(stack, paramCountStack, TokenBuffer, ixToken));
                }

            }
            catch (Exception ex)
            {
                rc = RC<Result>.Failed(
                    value: new Result(stack, paramCountStack, TokenBuffer, ixToken),
                    ErrorDescription: ExceptionHelper.FlattenExceptionMessages(ex));
            }

            return rc;
        }
    }
}
