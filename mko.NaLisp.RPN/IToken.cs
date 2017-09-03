using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.RPN
{
    public interface IToken
    {
        /// <summary>
        /// Liefert true, wenn das zuletzt eingelese Token eine Funktion ist
        /// </summary>
        bool IsFunctionName { get; }

        /// <summary>
        /// True, wenn das Token einen reinen Integerwert darstellt
        /// </summary>
        bool IsInteger { get; }

        /// <summary>
        /// True, wenn das Token einen Boolean darstellt
        /// </summary>
        bool IsBoolean { get; }

        /// <summary>
        /// True, wenn das Token einen allgemeinen nummerischen Wert (Festkomma oder Gleitkomma)
        /// darstellt.
        /// </summary>
        bool IsNummeric { get; }

        /// <summary>
        /// Das Token als Textfragment
        /// </summary>
        string Value { get; }

    }
}
