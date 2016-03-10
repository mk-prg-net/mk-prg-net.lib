using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp
{
    partial class Factory
    {

        public static Lisp.Tuple Tuple(params Core.NaLisp[] Elements)
        {
            return new Lisp.Tuple(Elements);
        }

        public static Lisp.Tuple Tuple(params int[] intElems)
        {
            return new Lisp.Tuple(intElems.Select(e => Int(e)).ToArray());
        }

        public static Lisp.Tuple Tuple(params double[] dblElems)
        {
            return new Lisp.Tuple(dblElems.Select(e => Dbl(e)).ToArray());
        }

        public static Lisp.Tuple Tuple(params string[] txtElems)
        {
            return new Lisp.Tuple(txtElems.Select(e => Txt(e)).ToArray());
        }

        public static Lisp.Tuple Tuple(params bool[] boolElems)
        {
            return new Lisp.Tuple(boolElems.Select(e => Bool(e)).ToArray());
        }

        public static Lisp.Tuple Tuple(params DateTime[] datElems)
        {
            return new Lisp.Tuple(datElems.Select(e => DateTime(e)).ToArray());
        }

        // Listenoperationen

        public static Core.NaLisp First(params Core.NaLisp[] Elements)
        {
            return new Lisp.First(Elements);
        }

        public static Core.NaLisp Last(params Core.NaLisp[] Elements)
        {
            return new Lisp.Last(Elements);
        }

        public static Core.NaLisp Take(int Count, params Core.NaLisp[] Elements)
        {
            return new Lisp.Take(Int(Count), Tuple(Elements));
        }

        public static Core.NaLisp Skip(int Count, params Core.NaLisp[] Elements)
        {
            return new Lisp.Skip(Int(Count), Tuple(Elements));
        }

        public static Core.NaLisp Reverse(params Core.NaLisp[] Elements)
        {
            return new Lisp.Reverse(Elements);
        }







    }
}
