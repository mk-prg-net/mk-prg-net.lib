using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using mko.Algo.Listprocessing;
using System.Diagnostics;

using T = System.Threading.Tasks;
using C = System.Collections.Concurrent;

using Lint = System.Collections.Generic.IEnumerable<int>;
using LLint = System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<int>>;

namespace mko.Algo.GraphTheory
{
    /// <summary>
    /// Findet alle Pfade in einem Raster aus 3x3 Knoten, die alle Knoten enthalten und maximal aus 
    /// 4 geraden Kanten bestehen.
    /// Raster:
    /// 1  2  3
    /// 4  5  6
    /// 7  8  9
    /// </summary>
    public class Pathfinder
    {
        // Liste aller Horizontalen im Gitter
        LLint Horizontals = Fn.L(
                Fn.L(22, 32, 1, 2, 3, 27, 13),
                Fn.L(4, 5, 6),
                Fn.L(21, 31, 7, 8, 9, 28, 14)
            );

        // Liste aller Vertikalen im Gitter
        LLint Verticals = Fn.L(
                Fn.L(25, 33, 1, 4, 7, 30, 18),
                Fn.L(2, 5, 8),
                Fn.L(10, 26, 3, 6, 9, 29, 17)
            );

        // Liste aller Diagonalen im Gitter
        LLint Diagonals = Fn.L(
                Fn.L(10, 2, 7, 19),
                Fn.L(11, 26, 2, 4, 31, 20),
                Fn.L(11, 3, 8, 18),
                Fn.L(3, 5, 7),
                Fn.L(12, 3, 4, 21),
                Fn.L(12, 27, 6, 8, 30, 19),
                Fn.L(13, 6, 7, 20),
                Fn.L(14, 6, 1, 23),
                Fn.L(15, 28, 6, 2, 33, 24),
                Fn.L(15, 9, 4, 22),
                Fn.L(9, 5, 1),
                Fn.L(16, 9, 2, 25),
                Fn.L(16, 29, 8, 4, 32, 23),
                Fn.L(17, 8, 1, 24)
            );



        // Alle untersuchungen reduzieren sich auf Pfade, die mit folgenden Kanten beginnen (aufgrund von Symetriebetrachtungen) 
        LLint Starts = Fn.L(
                Fn.L(25, 33, 1, 4, 7, 30, 18), 
                Fn.L(10, 2, 7, 19), 
                Fn.L(11, 26, 2, 4, 31, 20), 
                Fn.L(3, 5, 7)                
            );

        /// <summary>
        /// Gibt true zurück, wenn node ein Endpunkt der Kante mit EdgeIndex aus Edges ist
        /// </summary>
        /// <param name="node"></param>
        /// <param name="EdgeIndex"></param>
        /// <returns></returns>
        bool IsEndpoint(int node, Lint Edge)
        {
            return Edge.First() == node || Edge.Last() == node;
        }

        /// <summary>
        /// Wenn ein Pfad einer Lösung entspricht, dann gibt das System true zurück
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        bool IsSolution(LLint Path)
        {
            bool[] ContainsNode =  {false, false, false,
                                    false, false, false,
                                    false, false, false};


            foreach (var edge in Path)
            {
                foreach (var node in edge.Where(n => n < 10))
                    ContainsNode[node - 1] = true;
            }

            return ContainsNode.All(n => n == true);
        }

        public int MaxPathLength { get; set; }

        public int MaxCountSolutions { get; set; }

        // Liste aller Resultate
        public C.ConcurrentBag<LLint> AllSolutions;

        void Backtracking(LLint Edges, LLint Path)
        {
            // Nur bis zu 4 Linienzüge untersuchen
            if (Path.Count() < MaxPathLength && AllSolutions.Count() <= MaxCountSolutions)
            {
                int currentEndpoint = Path.Last().Last();

                foreach (var edge in Edges.Where(e => IsEndpoint(currentEndpoint, e)))
                {
                    var PathExt = edge.First() == currentEndpoint ? Path.Concat(Fn.L(edge)) : Path.Concat(Fn.L(edge.Reverse()));

                    if (IsSolution(PathExt))
                    {
                        AllSolutions.Add(PathExt);
                    }
                    else
                    {
                        Backtracking(Edges.Where(e => !Fn.Equal(e, edge)), PathExt);
                    }
                }

            }
        }


        LLint AllEdges(LLint edges)
        {
            var all = Fn.Clone(edges);
            foreach (var edge in edges)
            {
                if (edge.Count() > 2)
                {
                    all = all.Concat(SubEdges(edge.Skip(1)).Concat(SubEdges(edge.Take(edge.Count() - 1))));
                }
            }

            return all;
        }

        LLint SubEdges(Lint edge)
        {
            if (edge.Count(p => p <= 9) < 2)
                // Fall: Kante besteht maximal aus einem Gitterpunkt und sonst nur aus Hilfspunkten-> wird nicht weiter betrachtet
                return Fn.L<Lint>();
            else if (edge.Count() > 2)
            {
                // Fall: aus Kante können durch Reduzieren noch weitere Kanten gewonnen werden
                return Fn.L(edge).Concat(SubEdges(edge.Skip(1)).Concat(SubEdges(edge.Take(edge.Count() - 1))));
            }
            else
                // Fall: Kante gültig, aber nicht weiter reduzierbar
                return Fn.L(edge);
        }

        public void SolveSerial()
        {
            AllSolutions = new C.ConcurrentBag<LLint>();
            foreach (var start in AllEdges(Starts))
                Backtracking(AllEdges(Horizontals.Concat(Verticals.Concat(Diagonals))).Where(e => !Fn.Equal(e, start)), Fn.L(start));

#if DEBUG
            DebugViewResult();
#endif
        }


        public void Solve()
        {
            AllSolutions = new C.ConcurrentBag<LLint>();

            List<T.Task> allTasks = new List<T.Task>();
            foreach (var start in AllEdges(Starts))
            {
                if (AllSolutions.Count() < MaxCountSolutions)
                {
                    var Ta = new T.Task(
                            (edgeAndStartObject) =>
                            {
                                var edgeAndStart = (Tuple<LLint, LLint>)edgeAndStartObject;
                                Backtracking(edgeAndStart.Item1, edgeAndStart.Item2);
                            }, new Tuple<LLint, LLint>(Fn.Clone(AllEdges(Horizontals.Concat(Verticals.Concat(Diagonals))).Where(e => !Fn.Equal(e, start))), Fn.L(start)));

                    allTasks.Add(Ta);
                    Ta.Start();
                }
            }

            T.Task.WaitAll(allTasks.ToArray());

#if DEBUG
            DebugViewResult();
#endif

        }

#if DEBUG

        void DebugViewResult()
        {
            Debug.WriteLine("------------------------------------------------------------");
            Debug.WriteLine("Lösungen für max. Anzahl Linien = " + MaxPathLength);
            int SolutionNo = 0;
            foreach (var solution in AllSolutions)
            {
                Debug.WriteLine("Lösung " + (++SolutionNo));
                foreach (var edge in solution)
                {
                    Debug.Write("(");
                    foreach (var node in edge)
                        Debug.Write(node + ",");
                    Debug.Write(") ");
                }
                Debug.WriteLine("");
            }
        }
#endif



    }
}
