using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 23.3.2018
    /// Formater Objects serializes DocEntity trees in strings (i.e. html or polish notation)
    /// </summary>
    public interface IFormater
    {
        /// <summary>
        /// Serializes the given docEntity in a string of defined format
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        string Print(IDocuEntity entity);
    }
}
