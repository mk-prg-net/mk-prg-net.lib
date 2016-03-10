//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.10.2015
//
//  Projekt.......: mko.NaLisp
//  Name..........: Convert.cs
//  Aufgabe/Fkt...: Wandelt Constanten mit vergleichbaren Werten eines Typs Tin in solche vom Typ Tout um 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Data
{
    public class ConvertComp<Tin, Tout> : Core.NaLispNonTerminal
        where Tin : IComparable<Tin>
        where Tout : IComparable<Tout>
    {

        public ConvertComp(Core.INaLisp Value)
        {
            Elements = new Core.INaLisp[] { Value };
        }

        private ConvertComp(Core.INaLisp[] Elements)
        {
            this.Elements = Elements;
        }


        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            var IsSubTreeValid = SubTreeValid(ElemValidationResult);
            return new Core.Inspector.ProtocolEntry(this, IsSubTreeValid && ElemValidationResult.Length == 1 && ElemValidationResult[0].TypeOfEvaluated is ConstValComp<Tin>, IsSubTreeValid, typeof(ConstValComp<Tout>));
        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            try
            {
                var In = ((ConstValComp<Tin>)EvaluatedElements[0]);
                object objIn = In;
                return new ConstValComp<Tout>((Tout)objIn);
            }
            catch (Exception ex)
            {
                throw new Core.Evaluator.EvalException(this, mko.TraceHlp.FormatErrMsg(this, "Eval", ex.Message), ex);
            }
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new ConvertComp<Tin, Tout>(Elements);
        }

        //public override Core.NaLisp Clone(bool deep = true)
        //{
        //    return new ConvertComp<Tin, Tout>(Elements[0].Clone());

        //}
    }
}
