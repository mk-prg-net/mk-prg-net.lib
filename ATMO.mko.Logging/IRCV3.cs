using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 25.7.2018
    /// Redesing of IRCV2, because InnerRC was often a problem in cases of serialization.
    /// In this new interface there is no inner rc.
    /// </summary>
    public interface IRCV3 : ISucceeded, ITraceInfo
    {
        /// <summary>
        /// Additional Information in PLX (=property list expressions)
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        PNDocuTerms.DocuEntities.IDocuEntity MessageEntity { get; }

        /// <summary>
        /// converts all information in object in a property list expression (plx)
        /// </summary>
        /// <returns></returns>
        PNDocuTerms.DocuEntities.IDocuEntity ToPlx();


        /// <summary>
        /// if true, a inner RC exists (e.g. of called subfunction)
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        bool HasInnerRC { get; }

        /// <summary>
        /// common information about return code of subfunctions
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        IRCV3 InnerRC { get; }
    }
}
