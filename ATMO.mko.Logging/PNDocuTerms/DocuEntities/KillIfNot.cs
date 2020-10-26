using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 8.5.2018
    /// Kills DocuEntity in Parameterlist, if condition were not met.
    /// Used to implement optional parts in DocuEntity- trees.
    /// </summary>
    public class KillIfNot : IDocuEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Condition">Result of evaluated condition</param>
        /// <param name="docuEntity"></param>
        public KillIfNot(bool Condition, Func<IDocuEntity> docuEntity)
        {
            this.Condition = Condition;
            _CreateDocEntity = docuEntity;
        }

        Func<IDocuEntity> _CreateDocEntity;

        public IDocuEntity DocuEntity => _CreateDocEntity();

        public bool Condition { get; }

        public DocuEntityTypes EntityType => DocuEntityTypes.KillIfNot;

        public IEnumerable<IDocuEntity> Childs => throw new NotImplementedException();

        public bool IsFunctionName => throw new NotImplementedException();

        public bool IsInteger => throw new NotImplementedException();

        public bool IsBoolean => throw new NotImplementedException();

        public bool IsNummeric => throw new NotImplementedException();

        public string Value => throw new NotImplementedException();

        public int CountOfEvaluatedTokens => throw new NotImplementedException();

        public IToken Copy()
        {
            throw new NotImplementedException();
        }
    }
}
