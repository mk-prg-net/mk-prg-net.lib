using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.TestMockUps.AutomatDrehkreuz
{
    public class Controller : mko.BI.StateMachine.FinitStateMachine<StateFactory>
    {
        public override StateFactory StateFactory
        {
            get { return new StateFactory(); }
        }        
    }
}
