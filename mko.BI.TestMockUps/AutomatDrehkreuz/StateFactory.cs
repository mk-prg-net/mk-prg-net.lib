using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.TestMockUps.AutomatDrehkreuz
{
    public  class StateFactory
    {
        public Zu.Context CreateZu()
        {
            return new Zu.Context();
        }


        public Auf.Context CreateAuf()
        {
            return new Auf.Context();
        }


        public Cancel.Context CreateCancel()
        {
            return new Cancel.Context();
        }


        public Error.Context CreateError()
        {
            return new Error.Context();
        }

    }
}
