using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    /// <summary>
    /// mko, 27.03.2018
    /// Structure of expression tree node.
    /// The standard RPN Evaluator transform as valid RPN token sequenz 
    /// in a expression tree.
    /// </summary>
    public interface IExpression : IToken
    {
        /// <summary>
        /// All childs of this expression tree node
        /// </summary>
        IEnumerable<IExpression> Childs { get; }
    }
}
