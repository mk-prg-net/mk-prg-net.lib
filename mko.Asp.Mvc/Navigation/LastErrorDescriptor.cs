using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GB.Dispo.Web.Mvc.AppCode
{
    public class LastErrorDescriptor
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Exception Ex { get; set; }
        public string ShortErrorDescription { get; set; }
    }
}
