﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.NaLisp;

namespace mko.NaLisp.JSon
{
    public class add_i : ParseParameters
    {
        public const string Name = "add_i";

        public override string FunctionName
        {
            get { return Name; }
        }

        protected override Base.Core.NaLisp CreateNaLisp(Base.Core.INaLisp[] parameters)
        {
            return Base.Factories.Int._.ADD(parameters);
        }
    }
}
