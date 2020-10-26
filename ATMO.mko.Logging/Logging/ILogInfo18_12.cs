using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Logging
{
    /// <summary>
    /// mko, 19.12.2018
    /// Format von Laufzeitinformationen, die ein ILoggingServer an einen 
    /// ILoggingHandler übergibt.
    /// </summary>
    public interface ILogInfo18_12
    {
        DateTime TimeStamp { get; }

        long SessionId { get; }

        EnumLogTypeDFC LogType { get; }

        long LogCounter { get; }

        string AssemblyName { get; }

        string TypeName { get; }

        string FunctionName { get; }

        PNDocuTerms.DocuEntities.IDocuEntity Msg { get; }

    }
}
