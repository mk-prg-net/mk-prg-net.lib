using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Trd = System.Threading;
using TPL = System.Threading.Tasks;
using TSave = System.Collections.Concurrent;
using System.Diagnostics;

namespace mko.Algo.NumberTheory
{
    public static class PrimeFactors
    {
        /// <summary>
        /// Prüft, ob eine gegebene ganze Zahl eine Primzahl ist
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="primefactor"></param>
        /// <returns></returns>
        public static bool PrimeTest(long candidate, out long primefactor)
        {            
            primefactor = 2;
            // 1 von den Primzahlen ausschließen, da neutrales Element bezüglich Multiplikation
            if (candidate == 1) return false;

            // Prüfen gegen die Liste der allgemein bekannten Primzahlen
            if ((new long[] { 2, 3, 5, 7, 11, 13, 17, 19 }).Any(r => r == candidate)) return true;

            // Prüfen auf mögliche Teiler
            while (primefactor * primefactor < candidate && candidate % primefactor != 0) { primefactor++; }
            if (primefactor * primefactor > candidate)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Liefert alle Primfaktoren zu einer gegebenen Zahl z
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static IEnumerable<long> Factorization(long z)
        {
            long zz = z;
            long p = 2;
            var primeFactQueue = new Queue<long>(8);

            while (zz != 1)
            {
                while (zz % p == 0)
                {
                    primeFactQueue.Enqueue(p);
                    zz /= p;
                }

                p = NextUpperPrime(p);
            }

            return primeFactQueue;
        }


        /// <summary>
        /// Liefert für eine Zahl z eine Primzahl p, wobei gilt:
        /// z kleiner p und es gibt kein pm mit pm ist Primzahl und z kleiner pm kleiner p
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static long NextUpperPrime(long z)
        {
            if (z % 2 == 0)
                z++;
            else
                z += 2;

            while (!z.IsPrime())
                z += 2;

            return z;
        }        

        /// <summary>
        /// Liefert eine Liste aller Primzahlen in einem Intervall [begin, end]
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<long> scan(long begin, long end)
        {
            var primList = new LinkedList<long>();
            long primefactor;
            for (long k = begin; k < end; k++)
            {
                if(PrimeTest(k, out primefactor))                
                    primList.AddLast(k);
            }

            return primList;            
        }

        public static IEnumerable<long> scan(IntervalLong interval)
        {
            var primList = new LinkedList<long>();
            long primefactor;
            for (long k = interval.Begin; k < interval.End; k++)
            {
                if (PrimeTest(k, out primefactor))
                    primList.AddLast(k);
            }

            return primList;
        }

        public static IEnumerable<long> scan(Tuple<long, long> interval)
        {
            var primList = new LinkedList<long>();
            long primefactor;
            for (long k = interval.Item1; k < interval.Item2; k++)
            {
                if (PrimeTest(k, out primefactor))
                    primList.AddLast(k);
            }

            return primList;
        }


        /// <summary>
        /// Delegate für CallBacs, mit denen der Arbeitsforschritt in den Scannern dokumentiert werden kann
        /// </summary>
        /// <param name="p"></param>
        public delegate void DGProgress(Tuple<long, long> p);

        public static TSave.ConcurrentBag<IEnumerable<long>> scanParallelWithTasks(long startScope, long endScope, DGProgress ProgressCallback)
        {
            var alleTasks = new List<TPL.Task>();

            // Die Ergebnisse werden in dieser Liste abgelegt
            var results = new System.Collections.Concurrent.ConcurrentBag<IEnumerable<long>>();

            // Partitionierung des Auftrages
            for (long start = startScope + 1, ende = startScope + 10000; start < endScope; start += 10000, ende += 10000)
            {
                // Pro Partition wird ein Task aufgesetzt
                var t = new TPL.Task(ParamPartitionAsObject =>
                {
                    // Downcast des Parameters vom Typ Object in den Typ Partition
                    var Arbeitsauftrag = ParamPartitionAsObject as Tuple<long, long>;

                    var result = mko.Algo.NumberTheory.PrimeFactors.scan(Arbeitsauftrag);
                    results.Add(result);

                    // Informieren über die Fertigstellung der Partition
                    if (ProgressCallback != null)
                        ProgressCallback(Arbeitsauftrag);

                }, new Tuple<long, long>(start, ende));
                t.Start();

                alleTasks.Add(t);
            }

            // Warten, bis alle Tasks fertiggestellt sind
            TPL.Task.WaitAll(alleTasks.ToArray());

            return results;
        }

        /// <summary>
        /// Liefert eine Liste aller Primzahlen in einem Intervall [begin, end].
        /// Die Suche wird mittels der Task Parallel Library auf einem Multicore- System
        /// durch Aufteilen in kleine Suchbereiche und absuchen dieser in jeweils einem Thread
        /// beschleunigt.
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static TSave.ConcurrentBag<IEnumerable<long>> scanParallelWithParalleForEach(long begin, long end, DGProgress ProgressCallback)
        {
            // Suchbereich in Teilbereiche aufteilen, die parallel nach Primzahlen durchsucht werden
            var partitionen = System.Collections.Concurrent.Partitioner.Create(begin, end);

            // Liste der Teilergebnisse. Die ConcurrentBag ist threadsafe !
            var results = new System.Collections.Concurrent.ConcurrentBag<IEnumerable<long>>();

            // Paralleles starten aller Suchaufträge
            TPL.Parallel.ForEach(partitionen, (part) =>
            {
                results.Add(scan(part));                

                // Informieren über die Fertigstellung der Partition
                Debug.WriteLine("results.Count = " + results.Count);

                if (ProgressCallback != null)
                    ProgressCallback(part);

            });

            return results;
        }

        /// <summary>
        /// Liefert eine Liste aller Primzahlen in einem Intervall [begin, end].
        /// Die Suche wird mittels der Task Parallel Library auf einem Multicore- System
        /// durch Aufteilen in kleine Suchbereiche und absuchen dieser in jeweils einem Thread
        /// beschleunigt.
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static IEnumerable<long> scanParallel(long begin, long end, bool orderByDesc)
        {
            // Suchbereich in Teilbereiche aufteilen, die parallel nach Primzahlen durchsucht werden
            var partitionen = System.Collections.Concurrent.Partitioner.Create(begin, end);

            // Liste der Teilergebnisse. Die ConcurrentBag ist threadsafe !
            var results = new System.Collections.Concurrent.ConcurrentBag<IEnumerable<long>>();

            // Paralleles starten aller Suchaufträge
            TPL.Parallel.ForEach(partitionen, (part) =>
            {
                results.Add(scan(part));
                Debug.WriteLine("results.Count = " + results.Count);
            });

            // Zusammenführen der Resultate der parallelen Suchaufträge
            IEnumerable<long> all = null;
            foreach (var part in results)
            {
                if (all == null)
                    all = part;
                else
                    all = all.Concat(part);
            }
            if (all != null)
            {
                // Nachbearbeitung der Ergebnisses
                if (orderByDesc)
                    return all.OrderByDescending(p => p).ToArray();
                else
                    return all.OrderBy(p => p).ToArray();
            }
            else
                return new long[] { };
        }


    }
}
