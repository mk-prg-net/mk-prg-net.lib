using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Tools.Async
{
    public class ClassifyByComponents : IClassifier
    {
        Func<int, int, bool>[] eqFuncs;
        Queue<int[]> history = new Queue<int[]>();

        List<Tuple<int, int[]>> Categories;
        List<int> matchesPerRow;

        readonly int maxHistory;

        public ClassifyByComponents(int maxHistory,  params Func<int, int, bool>[] eqFuncs)
        {
            this.maxHistory = maxHistory;
            this.eqFuncs = new Func<int, int, bool>[eqFuncs.Count()];
            Array.Copy(eqFuncs, this.eqFuncs, this.eqFuncs.Length);
        }

        /// <summary>
        /// Classifies the vector. Returns a category to which the vector is assigned.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public int CategoryOf(params int[] vector)
        {
            mko.TraceHlp.ThrowArgExIfNot(vector.Length > eqFuncs.Length, "vector.length > eqFuncs.lenght!");

            // test equality for each category
            for(int i = 0, count = Categories.Count; i < count; i++)
            {
                // count equal matches per category
                matchesPerRow[i] = 0;
                for(int j = 0, countCol = vector.Length; j < countCol; j++)
                {
                    matchesPerRow[i] += eqFuncs[j](Categories[i].Item2[j], vector[j]) ? 1 : 0;
                }
            }

            // find category with max matches
            int ix_max = -1, max = int.MinValue;
            for(int i = 0, count = matchesPerRow.Count; i < count; i++)
            {
                if(matchesPerRow[i] >= max)
                {
                    max = matchesPerRow[i];
                    ix_max = i;
                }
            }

            return Categories[ix_max].Item1;           
        }

        public void Classify(params int[] vector)
        {
            throw new NotImplementedException();
        }
    }
}
