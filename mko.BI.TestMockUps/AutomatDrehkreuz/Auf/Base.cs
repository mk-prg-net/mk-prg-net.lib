using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.TestMockUps.AutomatDrehkreuz.Auf
{
    public class Base : mko.BI.StateMachine.State
    {
        public Base() : base(CreateStartStateBehavior()) { }

    }
}
