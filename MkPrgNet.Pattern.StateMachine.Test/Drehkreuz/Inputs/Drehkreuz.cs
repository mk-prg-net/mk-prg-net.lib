using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine.Test.Drehkreuz.Inputs
{
    public class Drehkreuz : InputBase
    {

        public void drehen()
        {
            _On = true;
        }

        public override bool On
        {
            get { return _On; }
        }

        bool _On;

        public override void Reset()
        {
            _On = false;
        }
    }
}
