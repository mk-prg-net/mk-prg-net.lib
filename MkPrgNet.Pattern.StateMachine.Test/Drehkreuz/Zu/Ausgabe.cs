using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine.Test.Drehkreuz.Zu
{
    public class Ausgabe : IOutput
    {
        public void Write(IInput input)
        {
            Debug.WriteLine("Das Tor ist zu");
        }
    }
}
