using System;

using mko.RPN;

namespace mko.PLX
{
    /// <summary>
    /// mko, 23.4.2018
    /// </summary>
    public class FN01 : IFunctionNames
    {
        public string constBool => "";

        public string constInt => "";

        public string constDbl => "";

        public string constStr => "";

        public string TextBeg => "$[";

        public string ListBeg => "[";

        public string ListEnd => "]";

        public string NamePrefix => ".";

        // Ohne Bedeutung hier
        public string ParamNamePrefix => throw new NotImplementedException();

        public string DerivedTokenPrefix => throw new NotImplementedException();

        public bool IsSemanticDescriptor(string FunctionName)
        {
            throw new NotImplementedException();
        }
    }
}
