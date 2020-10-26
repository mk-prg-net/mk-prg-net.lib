using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 22.11.2018
    /// Enhanced a DocuEntity with EntityType Property with special accessors to property value
    /// </summary>
    public class DocuEntityAsPropertyLinqDeco : DocuEntityWithNameLinqDeco
    {
        public DocuEntityAsPropertyLinqDeco(IDocuEntity entity)
            : base(entity)
        {
            Debug.Assert(entity.EntityType == DocuEntityTypes.Property);
        }


        public string PropValueAsString
        {
            get
            {
                return DocuEntityHlp.EntityValue(this).GetText();
            }
        }

        public int PropValueAsInt
        {
            get
            {
                return int.Parse(DocuEntityHlp.EntityValue(this).GetText());
            }
        }

        public long PropValueAsLong
        {
            get
            {
                // mko, 25.3.2019
                // Das Suffix L entfernt, da sonst eine Format- Exception geworfen wird
                return long.Parse(DocuEntityHlp.EntityValue(this).GetText().ToUpper().Replace("L", ""));
            }
        }

        public double PropValueAsDouble
        {
            get
            {
                return double.Parse(DocuEntityHlp.EntityValue(this).GetText());
            }
        }

        public DateTime PropValueAsDateTime
        {
            get
            {
                return DateTime.Parse(DocuEntityHlp.EntityValue(this).GetText());
            }
        }

    }
}
