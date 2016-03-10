//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: NameDir.Names.cs
//  Aufgabe/Fkt...: Liste aller Namen, die zum Kernsystem gehören.
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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

namespace mko.Algo.FormalLanguages.NaLisp.Core
{
    public partial class NameDir
    {
        public enum Names
        {
            // Aktuelle Tiefe beim Validieren/Auswerten eines TASP
            StackDepth,

            // Listenoperationen
            Tupel,
            First,
            Last,
            Reverse,
            Take,
            Skip,

            // Fehlerbeschreibung
            ERROR,

            // Konstanten
            ConstBool,
            ConstInt,
            ConstDbl,
            ConstString,
            ConstDateTime,
            ConstLong,

            AddLong,

            CastLong,

            VarBool,
            VarInt,
            VarDbl,
            VarString,
            VarDateTime,

            IfThen,
            Pipe,
            ADDtoInt,
            ADDtoDbl,
            SUBtoInt,
            SUBtoDbl,
            MULtoInt,
            MULtoDbl,
            DIVtoInt,
            DIVtoDbl,
            LT,
            LE,
            GT,
            GE,
            EQU,
            AND,
            OR,
            NOT,

            StartUserNames = 0x1000

        }
    }
}
