using System;
using System.Collections.Generic;
using System.Text;
using mko.RPN;

namespace mko.PLX.Tree
{
    public abstract class BaseNode : INode
    {
        public abstract IEnumerable<INode> Childs { get; }

        public bool IsFunctionName => true;

        public bool IsInteger => false;

        public bool IsBoolean => false;

        public bool IsNummeric => false;

        public abstract string Value { get; }

        public abstract int CountOfEvaluatedTokens { get; }

        public IToken Copy()
        {
            throw new NotImplementedException();
        }
    }
}
