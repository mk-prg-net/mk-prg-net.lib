using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.Algebra
{
    public interface FiniteGroups<T>        
    {

        /// <summary>
        /// Ordnung einer endlichen Gruppe
        /// </summary>
        long Order { get; }


        /// <summary>
        /// Gibt true zurück, wenn a ein Element der Gruppe ist
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        bool IsElement(T a);

        /// <summary>
        /// Berechnet einen Abstand, die Spanne zwischen zwei Elementen einer Gruppe.
        /// Die Spanne ist die maximal mögliche Anzahl von Verknüpfungen Combine mit einem 
        /// Element 1 aus der Gruppe, so dass gilt: b = a combine 1 combine .... combine 1
        /// </summary>
        /// <param name="OrderOfGorup"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        long Span(T a, T b);

        /// <summary>
        /// Verknüpfungsoperation, die in einer endlichen Gruppe definiert ist
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        T Combine(T a, T b);

        /// <summary>
        /// Neutrales Element
        /// </summary>
        T Unity { get; }

        /// <summary>
        /// Inversers Element von
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        T InverseOf(T a);



    }
}
