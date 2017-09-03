using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine.Test.Drehkreuz.Inputs
{
    public class Muenzeinwurf : InputBase
    {
        public override void Reset() {
            _Euro = 0;
            _Cent = 0;
        }
    
        public void Einwurf(int Euro, int Cent){
            _Euro = Euro;
            _Cent = Cent;
        }

        int _Euro;
        int _Cent;


        public override bool On
        {
            get { 
                return _Euro == 1 && _Cent == 50; 
            }
        }
    }
}
