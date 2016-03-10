//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: NameDir.cs
//  Aufgabe/Fkt...: Erweiterbares Namensverzeichnis für NaLisp
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
//  Version 1.0...: 1.4.2014
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
        /// <summary>
        /// Liefert einen Eintrag aus dem Namensverzeichnis zu dem NaLisp- Ausdruck
        /// </summary>
        /// <param name="Expr"></param>
        /// <returns></returns>
        public static Entry Get(NaLisp Expr)
        {
            if (Tab.ContainsKey(Expr.Name))
                return Tab[Expr.Name];
            else
                throw new Exception();
        }

        static Dictionary<int, Entry> Tab = new Dictionary<int, Entry>();

        /// <summary>
        /// Erzeugt einen neuen Eintrag im Namensverzeichnis.
        /// </summary>
        /// <param name="NameId"></param>
        /// <param name="NameTxt"></param>
        /// <param name="Description"></param>
        private static void CreateKeyWordEntryInternal(Names NameId, string NameTxt, string Description)
        {
            Tab.Add((int)NameId, new Entry() { IsKeyword = true, Name = NameTxt, Description= Description });
        }

        /// <summary>
        /// Erzeugt einen neuen int- Namen, basierend auf einem relativ eindeutigen Namen aus einer Anwendung 
        /// </summary>
        /// <param name="NameRelative"></param>
        /// <returns></returns>
        public static int CreateNewName(int NameRelative)
        {
            return (int)Names.StartUserNames + NameRelative;
        }

        public static void CreateKeyWordEntry(int NameId, string NameTxt, string Description)
        {
            Tab.Add(NameId, new Entry() { IsKeyword = true, Name = NameTxt, Description = Description });
        }

        /// <summary>
        /// mko, 22.10.2015
        /// Erzeugt einen neuen Eintrag für anwendungsspezifische Erweiterungen von NaLisp. Diese sind keine Schlüsselworte
        /// </summary>
        /// <param name="NameId"></param>
        /// <param name="NameTxt"></param>
        /// <param name="Description"></param>
        public static void CreateEntry(int NameId, string NameTxt, string Description)
        {
            Tab.Add(NameId, new Entry() { IsKeyword = false, Name = NameTxt, Description = Description });
        }

        /// <summary>
        /// Statischer Konstruktor
        /// </summary>
        static NameDir()
        {
            CreateKeyWordEntryInternal(Names.StackDepth, "StackDepth", "aktuelle Tiefe des Stapelspeichers beim Auswerten des Ausdrucks");

            // Liste
            CreateKeyWordEntryInternal(Names.Tupel, "Tupel", "Fragment einer Parameterliste einer Funktion");
           
            // Listenoperationen
            CreateKeyWordEntryInternal(Names.First, "First", "Isoliert das erste Element in einer Liste");
            CreateKeyWordEntryInternal(Names.Last, "Last", "Isoliert das letzte Element in einer Liste");
            CreateKeyWordEntryInternal(Names.Reverse, "Reverse", "Dreht die Reihenfolge in einer Liste um");
            CreateKeyWordEntryInternal(Names.Take, "Take", "Entnimmt die ersten n Elemente und gibt sie als Liste zurück");
            CreateKeyWordEntryInternal(Names.Skip, "Skip", "Überspringt die ersten n Elemente und gibt den Rest als Liste zurück");

            // Konstanten
            CreateKeyWordEntryInternal(Names.ConstBool, "bool", "bool - Konstante (true/false)");
            CreateKeyWordEntryInternal(Names.ConstInt, "int", "Integerkonstante");
            CreateKeyWordEntryInternal(Names.ConstDbl, "dbl", "Gleitkommakonstante");
            CreateKeyWordEntryInternal(Names.ConstString, "txt", "Zeichenkettenkonstante");
            CreateKeyWordEntryInternal(Names.ConstDateTime, "DateTime", "Datumskonstante");

            // Variablen
            CreateKeyWordEntryInternal(Names.VarBool, "VarBool", "bool - Variable (true/false)");
            CreateKeyWordEntryInternal(Names.VarDbl, "VarDbl", "double - Variable");
            CreateKeyWordEntryInternal(Names.VarInt, "VarInt", "int - Variable");
            CreateKeyWordEntryInternal(Names.VarString, "VarTxt", "String - Variable");
            CreateKeyWordEntryInternal(Names.VarDateTime, "VarDateTime", "DateTime - Variable");

            // Kontrollstrukturen
            CreateKeyWordEntryInternal(Names.Pipe, "Pipe", "Schaltet Funktionen hintereinandet: f(a) | g -> g(f(a))");
            CreateKeyWordEntryInternal(Names.IfThen, "IfThen", "Wenn 1. Element == (ConstBool true), dann Eval 2. Listenelement, sonst Eval 3. Listenelement");

            // Mathematische Operatoren
            CreateKeyWordEntryInternal(Names.ADDtoInt, "ADDint", "Summiert alle Listenelemente nummerisch auf: (ADD a b c) -> a+b+c");
            CreateKeyWordEntryInternal(Names.ADDtoDbl, "ADDdbl", "Summiert alle Listenelemente nummerisch auf: (ADD a b c) -> a+b+c");
            CreateKeyWordEntryInternal(Names.SUBtoInt, "SUBint", "Subtrahiert vom ersten Listenelement alle Restlichen: (SUB a b c) -> a-b-c");
            CreateKeyWordEntryInternal(Names.SUBtoDbl, "SUBdbl", "Subtrahiert vom ersten Listenelement alle Restlichen: (SUB a b c) -> a-b-c");
            CreateKeyWordEntryInternal(Names.MULtoInt, "MULint", "Bildet das Produkt aus allen Listenelementen: (MUL a b c) -> a*b*c");
            CreateKeyWordEntryInternal(Names.MULtoDbl, "MULdbl", "Bildet das Produkt aus allen Listenelementen: (MUL a b c) -> a*b*c");
            CreateKeyWordEntryInternal(Names.DIVtoInt, "DIVint", "Bildet aus allen Listen Elementen einen Kettenbruch: (DIV a b c) -> (a/b)/c");
            CreateKeyWordEntryInternal(Names.DIVtoDbl, "DIVdbl", "Bildet aus allen Listen Elementen einen Kettenbruch: (DIV a b c) -> (a/b)/c");
            
            // Vergleichsoperatoren
            CreateKeyWordEntryInternal(Names.LT, "LT", "Vergleich a < b");            
            CreateKeyWordEntryInternal(Names.LE, "LE", "Vergleich a <= b");
            CreateKeyWordEntryInternal(Names.GT, "GT", "Vergleich a > b");
            CreateKeyWordEntryInternal(Names.GE, "GE", "Vergleich a >= b");
            CreateKeyWordEntryInternal(Names.EQU, "EQU", "Vergleich a == b");
            
            // Logische Operatoren
            CreateKeyWordEntryInternal(Names.AND, "AND", "logisches UND: a && b && c ...");
            CreateKeyWordEntryInternal(Names.OR, "OR", "logisches ODER: a || b || c ...");
            CreateKeyWordEntryInternal(Names.NOT, "NOT", "logisches NICHT: !a");
            

            // Beginn des benutzerdefinierten Bereiches
            CreateKeyWordEntryInternal(Names.StartUserNames, "StartUserNames", "nummerischer Offset für alle benutzerdefinierten Namen");
        }

    }
}
