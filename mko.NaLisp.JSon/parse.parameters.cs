using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base = mko.Algo.FormalLanguages.NaLisp;

namespace mko.NaLisp.JSon
{
    public abstract class ParseParameters : FuncParserBase
    {
        protected abstract Base.Core.NaLisp CreateNaLisp(Base.Core.NaLisp[] parameters);

        public override bool TryParse(Newtonsoft.Json.JsonTextReader reader, Dictionary<string, IFuncParser> FuncParsers, out Base.Core.NaLisp NaExp)
        {

            Debug.Write("(" + FunctionName + " ");
            Base.Core.NaLisp[] parameter = null;
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
