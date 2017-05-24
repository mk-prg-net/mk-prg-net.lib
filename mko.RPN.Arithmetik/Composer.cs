using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.Arithmetik
{
    public class Composer : mko.RPN.Composer        
    {
        
        public Composer(mko.RPN.IFunctionNames fnBase, IFunctionNames fnames)
            :base(fnBase)
        {
            this.fnames = fnames;
        }

        IFunctionNames fnames;


        public string SUMAT(params string[] sumands)
        {
            return pnL(fnames.SUMAT, sumands);
        }

        /// <summary>
        /// ADD
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public string ADD(string a, string b)
        {
            return pn(fnames.ADD, a, b);
        }

        public string rADD(string a, string b)
        {
            return rpn(fnames.ADD, a, b);
        }

        /// <summary>
        /// Sub
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public string SUB(string a, string b)
        {
            return pn(fnames.SUB, a, b);
        }

        public string rSUB(string a, string b)
        {
            return rpn(fnames.SUB, a, b);
        }

        /// <summary>
        /// Mul
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public string MUL(string a, string b)
        {
            return pn(fnames.MUL, a, b);
        }

        public string rMUL(string a, string b)
        {
            return rpn(fnames.MUL, a, b);
        }

        /// <summary>
        /// DIV
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public string DIV(string a, string b)
        {
            return pn(fnames.DIV, a, b);
        }

        public string rDIV(string a, string b)
        {
            return rpn(fnames.DIV, a, b);
        }

        public string rSUMAT(params string[] sumands)
        {
            return rpnL(fnames.SUMAT, sumands);
        }

    }
}
