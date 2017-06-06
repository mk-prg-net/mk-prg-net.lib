//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.4.2017
//
//  Projekt.......: KeplerBI.MVC
//  Name..........: Rpn.c
//  Aufgabe/Fkt...: Basisklasse aller Modelle für Views von RPN- Termen
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 22.5.2017
//  Änderungen....: Verschoben aus KeplerBI.MVC in mko.RPN.AspMvcHelper
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mko.RPN;

namespace mko.RPN.AspMvcHelper.Models
{
    public class Rpn
    {
        /// <summary>
        /// Basisklasse aller Models für RPN- Funktionen
        /// </summary>
        /// <param name="tokens">Alle Tokens</param>
        /// <param name="fSubtree">Tokens der anzuzeigenden oder zu konfigurierenden RPN- Funktion</param>
        /// <param name="ParameterNames">Optionale Parameternamen, wenn keine semantischen Funktionen vorliegen</param>
        public Rpn(string ControllerName, IFunctionNames fn, IToken[] tokens, mko.RPN.IToken[] fSubtree, params string[] ParameterNames)
        {
            mko.TraceHlp.ThrowArgExIfNot(fSubtree.IsFunctionSubtree(), Properties.Resources.ModelsRpnFunctionnameMissing);
            
            pn = tokens.ToPNString();
            pnFSubtree = fSubtree.ToPNString();
            pnWithoutFunction = tokens.RemoveFunction(fSubtree.FunctionName()).ToPNString();

            var ix_func = tokens.LastIndexOfFunction(fSubtree.FunctionName());

            pnRight = tokens.Take(ix_func.IX - ix_func.CountOfEvaluatedTokens + 1).ToArray().ToPNString();
            pnLeft = tokens.Skip(ix_func.IX + 1).ToArray().ToPNString();


            this.ControllerName = ControllerName;
            this.fn = fn;
            this.Name = fSubtree.FunctionName();
            this.ParameterCount = fSubtree.FunctionParameterCount();

            ParameterSubTrees = new List<KeyValuePair<string, IToken[]>>(ParameterNames.Length);

            for (int i = 0; i < ParameterCount; i++)
            {
                var pSubtree = fSubtree.GetParameterSubtree(i+1);

                if (pSubtree.IsFunctionSubtree() && fn.IsSemanticDescriptor(pSubtree.FunctionName()))
                {
                    Add(pSubtree.FunctionName(), pSubtree.GetParameterSubtree(1));
                }
                else if (i < ParameterNames.Length)
                {
                    Add(ParameterNames[i], pSubtree);
                }
                else
                {
                    // Parameter wird werder durch semantische Funktion, noch durch einen Parameternamen beschrieben
                    // -> automatischen Parameternamen bilden

                    var pName = "P" + i.ToString("D2");
                    while (ParameterSubTrees.Any(r => r.Key == pName))
                    {
                        // Parameternamen solange mit Unterstrich verlängern, bis er eindeutig ist
                        pName += "_";
                    }

                    Add(pName, pSubtree);
                }
            }
        }

        void Add(string pName, IToken[] pSubtree)
        {
            ParameterSubTrees.Add(new KeyValuePair<string, IToken[]>(pName, pSubtree));
        }

        /// <summary>
        /// Name des Controllers, für der für die Darstellung gefilterter Mengen zuständig ist
        /// </summary>
        public string ControllerName { get; }

        public mko.RPN.IFunctionNames fn { get; }

        /// <summary>
        /// Name der darzustellenden Funktion
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Anzahl der Parameter
        /// </summary>
        public int ParameterCount { get; }

        /// <summary>
        /// Subtree zu jedem Parameter. Der Schlüssel ist der Parametername
        /// </summary>
        public List<KeyValuePair<string, mko.RPN.IToken[]>> ParameterSubTrees { get; }


        /// <summary>
        /// Enthält einen Ausdruck in polnischer Notation ohne die Funktion, die in diesem 
        /// Model präsentiert wird. Dient zur Implemntierung von Filter.delete
        /// </summary>
        public string pnWithoutFunction { get; }

        /// <summary>
        /// Alle rechts vom zu analysierenden PN- Term stehenden Terme
        /// </summary>
        public string pnRight { get; }


        /// <summary>
        /// Alle links vom zu analysierenden PN- Term stehenden Terme
        /// </summary>
        public string pnLeft { get; }



        /// <summary>
        /// Gesamter Filterterm in polnischer Notation, der auch dieses Filter enthält
        /// </summary>
        public string pn { get; }

        /// <summary>
        /// Filterterm in polnischer Notation für diese Filter
        /// </summary>
        public string pnFSubtree { get; }


    }
}