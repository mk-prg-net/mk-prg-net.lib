//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: mko.NaLisp
//  Name..........: IfThen.cs
//  Aufgabe/Fkt...: Verzweigung
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

namespace mko.NaLisp.Control
{
    public class IfThen : Core.NaLispNonTerminal
    {
        public IfThen(params Core.INaLisp[] Elements)
        {
            Debug.Assert(Elements.Length == 3, "IfThen muss genau 3 elemente haben");
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (Elements.Length != 3)
            {
                return new Core.Inspector.ProtocolEntry(this, false, true, null, "IfThen muss genau 3 Elementehaben");
            }
            if (ElemValidationResult[0].TypeOfEvaluated != typeof(Data.ConstVal<bool>))
                return new Core.Inspector.ProtocolEntry(this, false, true, null, "IfThen, 1. Element muss zu ConstBool evaluierbar sein");

            if (ElemValidationResult[1].TypeOfEvaluated != ElemValidationResult[2].TypeOfEvaluated)
                return new Core.Inspector.ProtocolEntry(this, false, true, null, "IfThen, Auswertung des 2. und 3. Element müssen denselben Rückgabetyp liefern");

            return new Core.Inspector.ProtocolEntry(this, true, true, ElemValidationResult[1].TypeOfEvaluated,
                "IfThen(" +
                ElemValidationResult[0].NaLispTreeNode.ToString() + ", " +
                ElemValidationResult[0].NaLispTreeNode.ToString() + ", " +
                ElemValidationResult[0].NaLispTreeNode.ToString() + ")");

        }

        /// <summary>
        /// Eval ist hier nicht implementiert. Die Implementierung erfolg im Evaluator
        /// </summary>
        /// <param name="EvaluatedElements"></param>
        /// <param name="StackInstance"></param>
        /// <param name="DebugOn"></param>
        /// <returns></returns>
        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            throw new NotImplementedException();
        }

        public Core.INaLisp Condition
        {
            get
            {
                return Elements[0];
            }
        }

        public Core.INaLisp IfTrue
        {
            get
            {
                return Elements[1];
            }
        }

        public Core.INaLisp IfFalse
        {
            get
            {
                return Elements[2];
            }
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new IfThen(Elements);
        }

        //public override Core.NaLisp Clone(bool deep = true)
        //{
        //    if (deep)
        //        return new IfThen(Elements.Select(r => r.Clone()).ToArray());
        //    else
        //        return new IfThen(Elements);
        //}

        public override string ToString()
        {       
            var Inspector = new Core.Inspector();            
            var pe = Inspector.Validate(this);
            if (pe.IsCurrentValid)
            {
                return "IfThen(" + Condition.ToString() + ", " + IfTrue.ToString() + ", " + IfFalse.ToString() + ")";
            }
            else
            {
                return "IfThen: " + pe.Description;
            }
        }
    }
}
