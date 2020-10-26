using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 27.2.2019
    /// List mit Docu-Termen, die zB. als zusätzliche Parameter in einer Methode oder Member einer instanz einzubetten sind.
    /// </summary>
    public class ListToEmbed : IDocuEntity
    {

        public ListToEmbed(IEnumerable<IDocuEntity> ToEmbed)
        {
            Childs =  ToEmbed;
        }

        public DocuEntityTypes EntityType => DocuEntityTypes.ListToEmbed;

        public IEnumerable<IDocuEntity> ToEmbed { get; }

        public IEnumerable<IDocuEntity> Childs { get; }

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
