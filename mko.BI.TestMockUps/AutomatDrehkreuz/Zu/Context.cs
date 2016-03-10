using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.TestMockUps.AutomatDrehkreuz.Zu
{
    public class Input : mko.BI.StateMachine.Input<Input.Tags>{
        public enum Tags{
            tueNix,
            EinEuro
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
            set { }           
        }

        public StateMachine.State Transition(Input input)
        {
            switch (input.Tag)
            {
                case Input.Tags.EinEuro:
                    return StateFactory.CreateAuf();
                case Input.Tags.tueNix:
                    return StateFactory.CreateZu();
                default:
                    return StateFactory.CreateError();
            }
        }
    }
}
