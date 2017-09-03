
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine
//  Name..........: State.cs
//  Aufgabe/Fkt...: Basisklasse der Zustände endlicher Automaten.
//                  Die endlichen Automaten sind Grundlage von Workflows.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.0
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 12.2014
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 23.4.2015
//  Änderungen....: Umzug vom Projekt mko.Automaton.StateMachine in mko.BI.
//                  Integration der Zustandsüberführungsfunktion in die Zustandsklasse 
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 4.2.2016
//  Änderungen....: BehaviorOfState gegen IStateBehavior ausgetauscht.
//                  Next gibt im Fall eines Endzustandes Liste mit Endzustand selber zurück.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 14.12.2016
//  Änderungen....: Weiterentwickelt  aus mko.BI.StateMachine
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine
{
    public abstract class StateBase : IState
    {

        /// <summary>
        /// Name des Zustandes. Wird aus dem Namen der Klasse ermittelt.
        /// Konvention: Wenn der Name der Klasse Context oder Base lautet, 
        ///             dann wird der letzte Partikel des Namespce zurückgeliefert.
        ///             Voraussetzung ist, die gesamte Klassenhirarchie für einen 
        ///             Zustand in einem eineindeutigen Namespace eingeschlossen ist.
        /// </summary>
        public virtual string Name
        {
            get
            {
                var type = GetType();
                string n = type.Name.ToLower();
                if (n == "state" || n == "zustand")
                {
                    return type.Namespace.Split('.').Last();
                }
                else
                {
                    return type.Name;
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Wenn True, dann ist der Zustand ein Startzustand
        /// </summary>
        public abstract bool IsStart
        {
            get;
        }

        /// <summary>
        /// Wenn True, dann ist der Zustand ein Endzustand
        /// </summary>
        public abstract bool IsFinal
        {
            get;
        }
    }
}
