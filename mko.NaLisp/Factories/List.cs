using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    partial class List
    {

        public static List _ = new List();

        public Lisp.Tuple Tuple(params Core.NaLisp[] Elements)
        {
            return new Lisp.Tuple(Elements);
        }

        public Lisp.Tuple Tuple(params int[] intElems)
        {
            return new Lisp.Tuple(intElems.Select(e => Int._.Create(e)).ToArray());
        }

        public Lisp.Tuple Tuple(params double[] dblElems)
        {
            return new Lisp.Tuple(dblElems.Select(e => Dbl._.Create(e)).ToArray());
        }

        public Lisp.Tuple Tuple(params string[] txtElems)
        {
            return new Lisp.Tuple(txtElems.Select(e => Txt._.Create(e)).ToArray());
        }

        public Lisp.Tuple Tuple(params bool[] boolElems)
        {
            return new Lisp.Tuple(boolElems.Select(e => Bool._.Create(e)).ToArray());
        }

        public Lisp.Tuple Tuple(params System.DateTime[] datElems)
        {
            return new Lisp.Tuple(datElems.Select(e => DateTime._.Create(e)).ToArray());
        }

        // Listenoperationen

        public Core.NaLisp First(params Core.NaLisp[] Elements)
        {
            return new Lisp.First(Elements);
        }

        public Core.NaLisp Last(params Core.NaLisp[] Elements)
        {
            return new Lisp.Last(Elements);
        }

        public Core.NaLisp Take(int Count, params Core.NaLisp[] Elements)
        {
            return new Lisp.Take(Int._.Create(Count), Tuple(Elements));
        }

        public Core.NaLisp Skip(int Count, params Core.NaLisp[] Elements)
        {
            return new Lisp.Skip(Int._.Create(Count), Tuple(Elements));
        }

        public Core.NaLisp Reverse(params Core.NaLisp[] Elements)
        {
            return new Lisp.Reverse(Elements);
        }

    }
}
