using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Trd = System.Threading;
using Ta = System.Threading.Tasks;

namespace mko.Algo
{
    public class PrimScanner
    {

        public static long[] scan(long begin, long end)
        {
            var primList = new LinkedList<long>();
            for (long k = begin; k < end; k++)
            {
                long maxT = k / 2;
                long t = 2;
                while (t++ <= maxT && k % t != 0) { }
                if (t == maxT)
                    primList.AddLast(k);
            }

            long[] primArr = new long[primList.Count];
            primList.CopyTo(primArr, 0);
            primList.Clear();
            return primArr;
        }

        public static IEnumerable<long> scanParallel(long begin, long end, bool sort)
        {
            int tc = Environment.ProcessorCount * 4;
            long range = (end - begin) / tc;
            long tbegin = begin;
            long tend = begin + range;
            var primRanges = new LinkedList<long>[tc];
            for (int i = 0; i < tc; i++)
            {
                Ta.Task.Factory.StartNew(
                    (o) =>
                    {
                        var tp = o as Tuple<long, long, LinkedList<long>>;
                        for (long k = tp.Item1; k < tp.Item2; k++)
                        {
                            long maxT = k / 2;
                            long t = 2;
                            while (t++ <= maxT && k % t != 0) { }
                            if (t <= maxT)
                                tp.Item3.AddLast(k);
                        }
                    }, 
                    new Tuple<long, long, LinkedList<long>>(tbegin, tend, primRanges[i]),
                    Ta.TaskCreationOptions.LongRunning);

                tbegin = tend + 1;
                tend = end - tbegin < 2 * range ? end : tbegin + range;               
            }

            for (int i = 1; i < tc; i++)
            {
                primRanges[0].Concat(primRanges[i]);
            }

            return primRanges[0];
        }
    }
}
