using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Algo.Listprocessing
{
    /// <summary>
    /// Implementierung elementarer Listenoperationen mittels Linq. Dienen zur Einführung in
    /// die Programmierung über das funktionale Kalkül
    /// </summary>
    public static class Fn
    {
        /// <summary>
        /// Erzeugt eine leere Liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> L<T>()
        {
            return new T[] { };
        }

        /// <summary>
        /// Erzeugt eine einelementige Liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Element"></param>
        /// <returns></returns>
        public static IEnumerable<T> L<T>(T Element)
        {
            return new T[] { Element };
        }

        public static IEnumerable<T> L<T>(params T[] Elements)
        {
            return Elements;
        }

        /// <summary>
        /// Liefert das erste Element einer Liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T First<T>(IEnumerable<T> list)
        {
            return list.First();
        }


        /// <summary>
        /// Liefert das Letzte Element einer Liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Last<T>(IEnumerable<T> list)
        {
            return list.Last();
        }

        /// <summary>
        /// Liefert den Rest einer Liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<T> Rest<T>(IEnumerable<T> list)
        {
            return list.Skip(1);
        }

        /// <summary>
        /// Überspringt die ersten i- Elemente einer Liste und liefert den Rest.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static IEnumerable<T> Skip<T>(IEnumerable<T> list, long i)
        {
            return list.Skip((int)i);
        }

        /// <summary>
        /// Liefert die ersten i- Elemente einer Liste als Teilliste zurück
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static IEnumerable<T> Take<T>(IEnumerable<T> list, long i)
        {
            return list.Take((int)i);
        }

        public static T Get<T>(IEnumerable<T> list, long index)
        {
            return First(Take(Skip(list, index), 1));
        }

        public static IEnumerable<T> Set<T>(IEnumerable<T> list, long index, T newValue)
        {
            return Concat(Take(list, index), Concat(Fn.L(newValue), Skip(list, index + 1)));
        }


        /// <summary>
        /// Liefert eine Liste, in der alle Elemente in der umgekehrten Reihenfolge 
        /// bezüglich der übergebenen Liste stehen.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<T> Reverse<T>(IEnumerable<T> list)
        {
            return list.Reverse();
        }

        /// <summary>
        /// Liefert eine Liste, die für jedes Element aus der übergebenen Liste das 
        /// Abbild durch die Funktion f enthält.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(IEnumerable<T> list, Func<T, T> f)
        {
            return list.Select(r => f(r));
        }

        /// <summary>
        /// Zählt alle Einträge der übergebenen Liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static long Count<T>(IEnumerable<T> list)
        {
            return list.Count();
        }

        /// <summary>
        /// Liefert True, wenn beide Listen den gleichen Inhalt haben, sonst false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listA"></param>
        /// <param name="listB"></param>
        /// <returns></returns>
        public static bool Equal<T>(IEnumerable<T> listA, IEnumerable<T> listB)
        {
            return listA.SequenceEqual(listB);
        }

        // rein funktionale Implementierung von Equal
        public static bool EqualPureFn<T>(IEnumerable<T> listA, IEnumerable<T> listB) {
            if (Count(listA) != Count(listB))
                // Ungleich lange Listen können nie gleich sein
                return false;
            else if (Count(listA) > 0)
                // Rückführung auf die Prüfung, das der Anfang gleich ist, und der Rest auch
                return First(listA).Equals(First(listB)) && EqualPureFn(Skip(listA, 1), Skip(listB, 1));
            else
                // zwei leere Listen sind immer gleich
                return true;
        }

        /// <summary>
        /// Liefert eine Liste, die am Anfang alle Elemente aus listA enthält,
        /// und danach bis zum Ende mit allen Elementen aus listB fortsetzt.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listA"></param>
        /// <param name="listB"></param>
        /// <returns></returns>
        public static IEnumerable<T> Concat<T>(IEnumerable<T> listA, IEnumerable<T> listB)
        {
            return listA.Concat(listB);
        }

        /// <summary>
        /// Liefert eine Kopie der Liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<T> Clone<T>(IEnumerable<T> list)
        {
            if (list.Count() == 0)
                return L<T>();
            else
            {
                return Concat(L(First(list)), Clone(Rest(list)));
            }
                
        }

        //-----------------------------------------------------------------------------------------
        // Spezielle Algorithmen

        /// <summary>
        /// Vertausch in einer Liste die Inhalte der Plätze i und j.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static IEnumerable<T> SwapComplex<T>(IEnumerable<T> list, long i, long j)
        {
            if (i < j)
            {
                if (i == 0)
                {
                    if (j == Count(list) - 1)
                        return Concat(L(Last(list)), Concat(Take(Skip(list, 1), Count(Skip(list, 1)) - 1), Take(list, 1)));
                    else
                        return Concat(Take(Skip(list, j), 1), Concat(Concat(Take(Skip(list, 1), j - 1), Take(list,1)), Skip(list, j+1)));
                } else
                    return Concat(Take(list, i), Concat(Take(Skip(list, j), 1), Concat(Concat(Take(Skip(list, i + 1), j - i - 1), Take(Skip(list, i), 1)), Skip(list, j + 1))));
            }
            else if (i > j)
            {
                // wie oben, jedoch wird i durch j, und j durch i ersetzt
                if (j == 0)
                {
                    if (i == Count(list) - 1)
                        return Concat(L(Last(list)), Concat(Take(Skip(list, 1), Count(Skip(list, 1)) - 1), Take(list, 1)));
                    else
                        return Concat(Take(Skip(list, i), 1), Concat(Concat(Take(Skip(list, 1), i - 1), Take(list, 1)), Skip(list, i + 1)));
                }
                else
                    return Concat(Take(list, j), Concat(Take(Skip(list, i), 1), Concat(Concat(Take(Skip(list, j + 1), i - j - 1), Take(Skip(list, j), 1)), Skip(list, i + 1))));

                
            }
            else
            {
                return list;
            }            
        }

        /// <summary>
        /// Optimierte Form von Swap. 
        /// Die Optimierung erfolg durch Aufteilen in zwei Stufen
        /// 1 Stufe: Alle benötigten Teillisten bilden
        /// 2 Stufe: Teillisten in neuer Reihenfolge zusammenkleben und als eine Liste zurückgeben.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static IEnumerable<T> Swap<T>(IEnumerable<T> list, long i, long j)
        {
            if (i < j)
                return SwapOpt(Take(list, i), Take(Skip(list, i), 1), Take(Skip(list, i + 1), j - i - 1), Take(Skip(list, j), 1), Take(Skip(list, j + 1), 1));
            else if (i > j)
                return SwapOpt(Take(list, j), Take(Skip(list, j), 1), Take(Skip(list, j + 1), i - j - 1), Take(Skip(list, i), 1), Take(Skip(list, i + 1), 1));
            else
                return list;

        }


        private static IEnumerable<T> SwapOpt<T>(IEnumerable<T> header, IEnumerable<T> i, IEnumerable<T> body, IEnumerable<T> j, IEnumerable<T> end)
        {
            return Concat(header, Concat(j, Concat(body, Concat(i, end))));
        }

    }
}
