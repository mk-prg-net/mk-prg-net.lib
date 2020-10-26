using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 15.11.2018
    /// Implementiert einen Generator zur Erzeugung von Sitzungsnummern.
    /// </summary>
    public class SessionIdGenerator
    {

        public long Get(string userid)
        {
            // mko: Startwert für den Sessionid- Generator erzeugen
            Random rnd = new Random((int)DateTime.Now.Ticks);

            var b8 = new byte[8];
            rnd.NextBytes(b8);

            // mko, 15.11.2018
            // Long- Guid erzeugen als Sessionid
            long sid = (b8[0]
                | ((long)b8[1] << 8)
                | ((long)b8[2] << 16)
                | ((long)b8[3] << 24)
                | ((long)b8[4] << 32)
                | ((long)b8[5] << 40)
                | ((long)b8[6] << 48)
                | ((long)b8[7] << 56)) & 0xFFFFFFFFL;

            return sid;
        }
    }
}
