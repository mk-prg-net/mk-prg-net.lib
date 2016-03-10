using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace mkoIt.Db
{
    static class TraceEnv
    {
        static TraceEnv()
        {
            TraceEnv.Switch = new TraceSwitch("mko_Db_TraceSwitch", "TraceSwitch mko.Db");
        }

        public static TraceSwitch Switch;

    }
}
