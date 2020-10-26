using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Logging
{
    /// <summary>
    /// mko, 3.1.2019
    /// Stellt Routinen bereit, die Logmeldungen verarbeiten
    /// </summary>
    public interface ILoggingHandler
    {
        /// <summary>
        /// Registriert die bereitgestellten Routinen zur Bearbeitung von 
        /// Logmeldungen an einem LogServer
        /// </summary>
        /// <param name="loggingServer"></param>
        void Register(ILoggingServer loggingServer);
    }
}
