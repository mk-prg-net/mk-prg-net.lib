using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    public class FunctionNameToken : IToken
    {
        public FunctionNameToken(string functionName, int CountOfEvaluatedTokens = 1)
        {
            FunctionName = functionName;
            _CountOfEvaluatedTokens = CountOfEvaluatedTokens;
        }

        public bool IsFunctionName
        {
            get { return true; }
        }

        public bool IsInteger
        {
            get { return false; }
        }

        public bool IsBoolean
        {
            get { return false; }
        }

        public bool IsNummeric
        {
            get { return false; }
        }

        public string Value
        {
            get { return FunctionName; }
        }
        string FunctionName;


        public int CountOfEvaluatedTokens
        {
            get { return _CountOfEvaluatedTokens; }
        }

        int _CountOfEvaluatedTokens;

        public IToken Copy()
        {
            return new FunctionNameToken(FunctionName, CountOfEvaluatedTokens);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
