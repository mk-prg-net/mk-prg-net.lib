using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine.Test.Drehkreuz.Auf
{
    public class Ausgabe : IOutput
    {
        public void Write(IInput input)
        {
            Debug.WriteLine("Tor ist offen");
        }
    }
}
