using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 7.2.2018
    /// </summary>
    public class DocuEntity : IDocuEntity
    {
        public DocuEntity(IFn fn, DocuEntityTypes docEntityType, IEnumerable<IDocuEntity> childs)
        {
            this.fn = fn;
            EntityType = docEntityType;

            Childs = childs;

            //if(childs == null)
            //{
            //    Childs = new IDocuEntity[] { };
            //} else
            //{
            //    Childs = childs;
            //}            
        }

        IFn fn;

        public DocuEntityTypes EntityType { get; }

        public bool IsFunctionName => true;

        public bool IsInteger => false;

        public bool IsBoolean => false;

        public bool IsNummeric => false;

        string IToken.Value => EntityType.ToString();

        public int CountOfEvaluatedTokens => Childs.Sum(r => r.CountOfEvaluatedTokens) + 1;

        public IEnumerable<IDocuEntity> Childs { get; }

        public IToken Copy()
        {
            return new DocuEntity(fn, EntityType, Childs);
        }

        public override string ToString()
        {
            var fmt = new PNFormater(fn);
            return fmt.Print(this);
        }

    }
}
