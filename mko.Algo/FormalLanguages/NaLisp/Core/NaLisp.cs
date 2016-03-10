//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: NaLisp.cs
//  Aufgabe/Fkt...: Basisklasse aller NaLisp Ausdrücke.
//                  NaLisp = Named LISP Expressions
//  Um auf einer Web- Benutzeroberfläche Filter- und Sortierkriterien,
//  Formeln und Befehle einzugeben, wird eine auf den Ideen von LISP basierendes
//  System entwickelt mit folgenden Eigenschaften:
//  1. Darstellung von Formeln und Befehlen als Bäume aus NaLisp Elementen
//  2. Menge der NaLisp Elemente besteht nur aus den für die Baumdarstellung und
//     Evaluierung der Ausdrücke notwendigen Strukturen - minimale Syntax wie bei LISP
//  3. Einfache und schnelle Adaption von NaLisp an verschiedenste Aufgaben durch
//     3.1. Einfachen Aufbau von NaLisp- Ausdrücken
//     3.2. Einfache Darstellung von NaLisp- Bäumen auch in JavaScript
//     3.3. Integrierte Validierung in den NaLisp Ausdrücken
//     3.4. Anwendungsspezifischen Algorithmen zur Evaluierung mittels Vererbung und Polymorphismus
//     3.5. Einfaches, erweiterbares System zur Verwaltung von NaLisp- Namen
//
//  Jedes NaLisp- Element ist eine Liste, deren erstes Element einen Listennamen darstellt:
//  (Name …)
//  Der Name kann ein Schlüsselwort oder eine ID sein. Wenn er kein Schlüsselwort ist,
//  dann stellt er eine ID dar.
//  Ein NaLisp wird hier durch in runden Klammern () eingeschlossene Liste dargestellt.
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
    public abstract class NaLisp
    {
        protected NaLisp(int Name)
        {
            _Name = Name;
        }

        public int Name { get { return _Name; } }
        int _Name;

        //public abstract bool IsTerminal { get; }       

        /// <summary>
        /// Erzeugt vom aktuellen NaLisp Term einen Clone (Kopie). Wenn deep == true, dann werden
        /// auch alle Kindelemente geklont.
        /// </summary>
        /// <param name="deep"></param>
        /// <returns></returns>
        public abstract NaLisp Clone(bool deep = true);       


    }
}
