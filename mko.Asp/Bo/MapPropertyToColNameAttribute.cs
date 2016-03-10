using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class MapPropertyToColNameAttribute : System.Attribute
    {
        public MapPropertyToColNameAttribute(string ColName)
        {
            this.ColName = ColName;
        }
        public string ColName;
    }
}
