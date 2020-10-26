using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 7.3.2018
    /// 
    /// mko, 18.12.2018
    /// Decodiert automatisch Zeichenumschreibungen in Strings
    /// </summary>
    public class String : IDocuEntity
    {
        public String(string value)
        {
            // mko, 18.12.2018
            // Zeichenumschreibungen werden automatisch dekodiert
            this.Value = global::mko.RPN.UrlSaveStringEncoder.RPNUrlSaveStringDecodeIf(value, true);
        }

        public DocuEntityTypes EntityType => DocuEntityTypes.String;

        public string Value { get; }

        public int CountOfEvaluatedTokens => 1;

        public IEnumerable<IDocuEntity> Childs => new IDocuEntity[]{};

        public bool IsFunctionName => true;

        public bool IsInteger => false;

        public bool IsBoolean => false;

        public bool IsNummeric => false;

        public IToken Copy()
        {
            return new String(Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
