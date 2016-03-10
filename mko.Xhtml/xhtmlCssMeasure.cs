using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    public abstract class CssMeasure : ICloneable
    {

        /// <summary>
        /// Liefert den Wert eines Measures als druckbaren Text zur Ausgabe in einen Style
        /// </summary>
        public abstract string TextValue
        {
            get;
        }

        protected abstract object _Clone();


        public object Clone()
        {
            return _Clone();
        }
    }
}
