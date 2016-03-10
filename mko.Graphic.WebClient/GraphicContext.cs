using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Graphic.WebClient
{
    public class GraphicContext
    {
        LinkedList<string> graphicScriptMethods = new LinkedList<string>();

        public void Clear()
        {
            graphicScriptMethods.Clear();
        }

        public void Add(string scriptMethod)
        {
            graphicScriptMethods.AddLast(scriptMethod);
        }

        

        public override string ToString()
        {
            StringBuilder bld = new StringBuilder();
            //bld.Append("<script type=\"text/javascript\">");
            foreach (string script in graphicScriptMethods)
            {
                bld.Append(script);
#if(DEBUG)
                bld.Append("\n");
#else
                bld.Append(" ");
#endif
            }
            //bld.Append("</script>");
            return bld.ToString();
        }
    }
}
