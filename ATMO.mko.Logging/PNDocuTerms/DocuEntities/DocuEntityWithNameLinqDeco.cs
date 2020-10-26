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
    /// </summary>
    public class DocuEntityWithNameLinqDeco : DocuEntityLinqDeco
    {

        public DocuEntityWithNameLinqDeco(IDocuEntity entity)
            : base(entity) =>
            // Funktion niemals aufrufen für folgende Typen
            Debug.Assert(!(entity.EntityType == DocuEntityTypes.Date
                || entity.EntityType == DocuEntityTypes.KillIfNot
                || entity.EntityType == DocuEntityTypes.Name
                || entity.EntityType == DocuEntityTypes.String
                || entity.EntityType == DocuEntityTypes.Text
                || entity.EntityType == DocuEntityTypes.Time
                || entity.EntityType == DocuEntityTypes.Version
                ));

        public string Name
        {
            get
            {                
                return DocuEntityHlp.Name(this);
            }
        }

        /// <summary>
        /// returns true, if instance is named with given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsNamed(string name)
        {
            return Name == name;
        }

    }
}
