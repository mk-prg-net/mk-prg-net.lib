using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using pnL = ATMO.mko.Logging.PNDocuTerms.Composer;

namespace ATMO.mko.Logging.PNDocuTerms.Parser
{
    /// <summary>
    /// mko, 6.3.2018
    /// Parser for log- messages in polish notation
    /// mko, 17.5.2018
    /// fix: dict- Key's where generated from pnL.fn- this is static Fn. So a definition of IFn fn where without meaning.
    ///      Now keys's will be generated with fn, defined in constructor.
    /// </summary>
    public class FunctionEvalMapperFunctor : IFnameEvalMapper
    {
        IFn fn = Fn._;

        public FunctionEvalMapperFunctor() { }

        public FunctionEvalMapperFunctor(IFn fn)
        {
            this.fn = fn;
        }

        public void MapFnameToEvalIn(Dictionary<string, IEval> dict)
        {
            dict[fn.Instance] = new InstanceEval(fn);
            dict[fn.Property] = new PropertyEval(fn);
            dict[fn.PropertySet] = new PropertySetEval(fn);
            dict[fn.Method] = new MethodEval(fn);
            dict[fn.Event] = new EventEval(fn);
            dict[fn.Version] = new VersionEval(fn);
            dict[fn.Txt] = new TextEval(fn);
            dict[fn.Date] = new DateEval(fn);
            dict[fn.Time] = new TimeEval(fn);
            dict[fn.ListEnd] = new ListEndEval(fn);
            dict[fn.List] = new ListEval(fn);
            dict[fn.Return] = new ReturnEval(fn);
        }
    }
}
