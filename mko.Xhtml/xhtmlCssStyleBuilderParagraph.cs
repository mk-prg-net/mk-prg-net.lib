using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mko.Newton;

namespace mkoIt.Xhtml.Css
{
    public class CssStyleBuilderParagraph : StyleBuilder
    {
        public CssStyleBuilderParagraph()
        {
            FontFamiliy = new Font();
            FontSize = FontSizeAbsolute.Parse("10pt");
        }
    }

}
