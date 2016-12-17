//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine.Test
//  Name..........: InputBase.cs
//  Aufgabe/Fkt...: Kurzbeschreibung
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine.Test.CreateDraft
{
    public abstract class InputBase : Pattern.StateMachine.InputBase
    {
        public override bool On
        {
            get { return _On; }
        }
        bool _On = false;

        protected void SetOn()
        {
            _On = true;
        }

        public override void Reset()
        {
            _On = false; ;
        }

        public DefDraftToken Token
        {
            get
            {
                return _Token;
            }
        }
        DefDraftToken _Token = null;

        protected void SetToken(DefDraftToken token){
            _Token = token;
        }

        public virtual void TryRead(DefDraftToken Token)
        {
            if (Token.TokenType == TokenType)
            {
                SetToken(Token);
                SetOn();
            }
        }


        protected abstract DefDraftToken.EnumTokenType TokenType
        {
            get;
        }

    }
}
