using System;
using System.Collections.Generic;
using System.Text;

namespace mko.PLX.Tree
{
    /// <summary>
    /// mko, 23.4.2018
    /// </summary>
    public interface INode: RPN.IToken
    {
        /// <summary>
        /// Childnodes
        /// </summary>
        IEnumerable<INode> Childs { get; }
    }
}
