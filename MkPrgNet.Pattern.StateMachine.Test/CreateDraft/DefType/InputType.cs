//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.12.2016
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
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

namespace MkPrgNet.Pattern.StateMachine.Test.CreateDraft.DefType
{
    public abstract class InputType : InputBase
    {
        public override void TryRead(DefDraftToken Token)
        {
            if (Token.TokenType == TokenType && Token.TokenContent == TypeName)
            {
                SetOn();
                SetToken(Token);                
            }
        }

        public abstract string TypeName
        {
            get;
        }

        protected override DefDraftToken.EnumTokenType TokenType
        {
            get { return DefDraftToken.EnumTokenType.Type; }
        }
    }
}
