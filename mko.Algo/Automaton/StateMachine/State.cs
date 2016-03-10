
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.2014
//
//  Projekt.......: mkl.Algo
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
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
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

namespace mko.Algo.Automaton.StateMachine
{
    public abstract class State
    {
        public abstract State[] Next { get; }


        BehaviorOfState behavior;

        public State(BehaviorOfState behavior)
        {
            this.behavior = behavior;
        }

        public static BehaviorOfState CreateStartStateBehavior()
        {
            return new StartState();
        }

        public static BehaviorOfState CreateNormalStateBehavior()
        {
            return new NormalState();
        }

        public static BehaviorOfState CreateFinalStateBehavior()
        {
            return new FinalState();
        }


        /// <summary>
        /// Wenn True, dann ist der Zustand ein Startzustand
        /// </summary>
        public bool IsStart
        {
            get
            {
                return behavior.IsStart;
            }
        }

        /// <summary>
        /// Wenn True, dann ist der Zustand ein Endzustand
        /// </summary>
        public bool IsFinal
        {
            get
            {
                return behavior.IsFinal;
            }
        }


        /// <summary>
        /// Wenn True, dann ist das Objekt ein Zustand vom Typ einer 
        /// von State abgeleiteten Klasse, die neben der Darstellung des Zustandes an sich 
        /// auch noch die mit dem Zustand verknüpften Zustand der Laufzeitumgebung speichert.
        /// Konvention:
        /// Eine Klasse mit der Basisklasse State, deren Name das Suffix "Context" hat, wird als
        /// Context- Klasse erkannt.
        /// </summary>
        public virtual bool IsContext
        {
            get
            {
                string name = GetType().Name;
                if (name.EndsWith("Context"))
                    return true;
                else
                    return false;   
            }
        }

        /// <summary>
        /// Name des Zustandes. Wird aus dem Namen der Klasse ermittelt.
        /// Konvention: Wenn der Name der Klasse Context oder Base lautet, 
        ///             dann wird der letzte PArtikel des Namespce zurückgeliefert.
        ///             Voraussetzung ist, die gesamte Klassenhirarchie für einen 
        ///             Zustand in einem eineindeutigen Namespace eingeschlossen ist.
        /// </summary>
        public virtual string Name
        {
            get
            {
                var type = GetType();
                string n = type.Name.ToLower();
                if (n == "context" || n == "base")
                {
                    return type.Namespace.Split('.').Last();
                }
                else
                {
                    return type.Name;
                }                
            }
        }


        
    }
}
