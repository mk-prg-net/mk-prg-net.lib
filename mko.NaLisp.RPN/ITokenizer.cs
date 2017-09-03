using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.RPN
{
    public interface ITokenizer
    {
        /// <summary>
        /// Liest das nächte Token ein
        /// </summary>
        void Read();

        /// <summary>
        /// Liefert true, wenn kein weiteres Token mehr eingelesen werden kann
        /// </summary>
        bool EOF { get; }


        /// <summary>
        /// Liefert das zuletzt eingelesene Token
        /// </summary>
        IToken Token { get; }

    }
}
