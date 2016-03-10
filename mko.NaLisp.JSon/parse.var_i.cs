using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.Algo.FormalLanguages.NaLisp;

namespace mko.NaLisp.JSon
{
    public class var_i : ParseUnary<string>
    {
        public const string Name = "var_i";

        public static int NameToID(string name)
        {
            return Name.GetHashCode();

        }


        /// <summary>
        /// Achtung: der Parameter ist nicht der Initialwert, sondern der Name der Variable
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        protected override Base.Core.NaLisp CreateNaLisp(string Parameter)
        {
            return Base.Factory.VarInt(NameToID(Parameter));
        }

        public override string FunctionName
        {
            get { return Name; }
        }

        
    }
}
