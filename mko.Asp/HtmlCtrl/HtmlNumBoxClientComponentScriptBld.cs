using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp.HtmlCtrl
{
    partial class NumBox
    {
        class ClientComponentScriptBld : ClientComponentScriptBldBase
        {
            public ClientComponentScriptBld(string IdClientComponent)
                : base(IdClientComponent)
            {                
            }

            public string AddDigit(int digit)
            {
                // Auf einen Instanz der NumBoxUtils- Klasse wird zugegriffen, die für diese NumBox erzeugt wurde
                return CreateMethodCallAndReturn("AddDigit", digit.ToString());
            }

            public string AddKomma()
            {
                return CreateMethodCallAndReturn("AddDigit", "','");
            }

            public string AddPoint()
            {
                return CreateMethodCallAndReturn("AddDigit", "'.'");
            }

            public string BackSpace()
            {
                return CreateMethodCallAndReturn("BackSpace");
            }

            public string ShowNumPad()
            {
                return CreateMethodCallAndReturn("ShowNumPad");
            }

            public string HideNumPad()
            {
                return CreateMethodCallAndReturn("HideNumPad");
            }

            public string Clear()
            {
                return CreateMethodCallAndReturn("Clear");
            }

        }
    }
}
