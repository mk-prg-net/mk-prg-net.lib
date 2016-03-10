using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base = mko.NaLisp;

namespace mko.NaLisp.JSon
{
    public abstract class ParseParameters : FuncParserBase
    {
        protected abstract Base.Core.NaLisp CreateNaLisp(Base.Core.INaLisp[] parameters);

        public override bool TryParse(Newtonsoft.Json.JsonTextReader reader, Dictionary<string, IFuncParser> FuncParsers, out Base.Core.INaLisp NaExp)
        {

            Debug.Write("(" + FunctionName + " ");
            Base.Core.INaLisp[] parameter = null;
            if (!Parser.parseParameterlist(reader, FuncParsers, out parameter))
            {
                NaExp = null;
                return false;
            }
            NaExp = CreateNaLisp(parameter);
            return true;

        }
    }
}
