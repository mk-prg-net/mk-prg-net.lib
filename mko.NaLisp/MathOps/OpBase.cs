//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.10.2015
//
//  Projekt.......: mko.NaLisp
//  Name..........: OpBase.cs
//  Aufgabe/Fkt...: Basisklasse arithmetischer NaLisp- Funktionen
//                  Liefern immer einen ConstValComp<T> zurück.
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

namespace mko.NaLisp.MathOps
{
    public abstract class OpBase<T> : Core.NaLispNonTerminal 
        where T : IComparable<T>
    {

        // Typ der NaLisp- Ausdrücke, zu dem die Parameter evaluiert werden.
        readonly Type ParamType;

        public OpBase(IArithmetikOps<T> AOps, Core.INaLisp[] Elements)
        {
            ParamType = typeof(Data.ConstValComp<T>);
            this.AOps = AOps;
            this.Elements = Elements; //.Select(r => r.Clone()).ToArray();
        }

        protected OpBase(IArithmetikOps<T> AOps, params T[] operands)
        {
            ParamType = typeof(Data.ConstValComp<T>);
            this.AOps = AOps;
            Elements = operands.Select(e => new Data.ConstValComp<T>(e)).ToArray();
        }

        /// <summary>
        /// Liefert streng typisierte arithmetische Operationen
        /// </summary>
        protected IArithmetikOps<T> AOps;


        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            bool IsSubTreeValid = SubTreeValid(ElemValidationResult);

            if (Stack.ParentIs(typeof(Control.Pipe)))
            {
                if (ElemValidationResult.Count() < 1)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, ParamType, Name + ": innerhalb eines Pipe- Opearators muss die Anzahl der Operanden muss mindestens 1 sein");
            }
            else
            {
                if (ElemValidationResult.Count() < 2)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, ParamType, Name + ": Anzahl der Operanden muss mindestens 2 sein");
            }

            if (ElemValidationResult.All(e => e.TypeOfEvaluated == ParamType || e.TypeOfEvaluated == typeof(Lisp.Tuple)))
                return new Core.Inspector.ProtocolEntry(this, true, IsSubTreeValid, ParamType, "");
            else
                return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, ParamType, Name + " darf nur Parameter haben, die zu " + ParamType.Name + " ausgewertet werden.");
        }

        public Core.NaLisp EvalImpl(Core.INaLisp Current, Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn, Func<T, T, T> op)
        {
            try
            {
                T akku = ((Data.ConstValComp<T>)EvaluatedElements.First()).Value;

                foreach (Data.ConstValComp<T> c in EvaluatedElements.Skip(1))
                {
                    akku = op(akku, c.Value);
                }

                return new Data.ConstValComp<T>(akku);
            }
            catch (Exception ex)
            {
                throw new Core.Evaluator.EvalException(Current, mko.TraceHlp.FormatErrMsg(this, "EvalImpl", ex.Message), ex);
            }
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            throw new NotImplementedException();
        }

    }
}
