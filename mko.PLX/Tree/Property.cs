using System;
using System.Collections.Generic;
using System.Text;
using mko.RPN;

using System.Linq;
using System.Diagnostics;

namespace mko.PLX.Tree
{
    /// <summary>
    /// mko, 23.4.2018
    /// </summary>
    public class Property : BaseNode { 


        public Property(FunctionNameToken fnTok, INode value)
        {
           // fnTok.Value
        }

        /// <summary>
        /// First child of the Property is the property name
        /// </summary>
        public string Name {
            get
            {
                Debug.Assert(Childs.Any());
                Debug.Assert(StringToken.Test(Childs.First()));
                return Childs.First().Value;
            }
        }

        public INode ValueAsNode
        {
            get
            {
                Debug.Assert(Childs.Count() > 1);
                return Childs.Skip(1).First();
            }
        }

        public override IEnumerable<INode> Childs { get; }

        public override string Value => throw new NotImplementedException();

        public override int CountOfEvaluatedTokens => throw new NotImplementedException();
    }
}
