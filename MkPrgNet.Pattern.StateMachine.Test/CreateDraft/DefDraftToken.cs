//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine
//  Name..........: DefDraftToken.cs
//  Aufgabe/Fkt...: Token, aus welchen Definition eines neuen Dokumentenentwurfes aufgebaut ist
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
    public class DefDraftToken
    {
        public enum EnumTokenType
        {
            Author,
            Node,
            Type,
            TocParent,
            TocTheme,
            DocToc,
            DocTheme,
            seal
        }

        public EnumTokenType TokenType
        {
            get
            {
                return _TokenType;
            }
        }
        EnumTokenType _TokenType;


        public string TokenContent
        {
            get
            {
                return _TokenContent;
            }
        }
        string _TokenContent;


        public DefDraftToken(EnumTokenType type, string content)
        {
            _TokenType = type;
            _TokenContent = content;
        }
    }
}
