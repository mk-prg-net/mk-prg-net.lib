using System;
using System.Collections.Generic;
using System.Text;

namespace mko.algorithm
{
    public delegate void DGOp(string EnumName, int EnumValue);
    public class ForEachEnumMember<TEnum>
    {        
        public static void execute(DGOp op)
        {
            foreach (int member in System.Enum.GetValues(typeof(TEnum)))
            {
                string EnumName = System.Enum.GetName(typeof(TEnum), member);
                op(EnumName, member);
            }


        }
    }    
}
