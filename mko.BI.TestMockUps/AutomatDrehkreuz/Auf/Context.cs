using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.TestMockUps.AutomatDrehkreuz.Auf
{
    public class Input : mko.BI.StateMachine.Input<Input.Tags>
    {
        public enum Tags
        {
            tueNix,
            passiere
        }
    }


    public class Context : Base, mko.BI.StateMachine.IStateTransition<Input, Input.Tags, StateFactory>
    {
        public StateFactory StateFactory
        {
            get
            {
                return new StateFactory();
            }
            set {
            }
        }

        public StateMachine.State Transition(Input input)
        {
            switch (input.Tag)
            {
                case Input.Tags.passiere:
                    return StateFactory.CreateZu();
                case Input.Tags.tueNix:
                    return StateFactory.CreateAuf();
                default:
                    return StateFactory.CreateError();
            }
        }
    }
}
