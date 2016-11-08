using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Testdata.XY
{
    /// <summary>
    /// Generator, der aus der überlagerung der Ausgänge mehrerer Generatoren
    /// (Superposition) ein Signal erzeugt
    /// </summary>
    /// <remarks></remarks>
    public class Superposition : GeneratorBase, IGenerator
    {


        private IGenerator[] _Generatoren;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="Generatoren">Generatoren, deren Signale überlagert werden sollen</param>
        /// <remarks></remarks>
        public Superposition(params IGenerator[] Generatoren)
        {
            _Generatoren = Generatoren;
        }


        public double[] GenerateX(mko.BI.Bo.Interval<double> Interval, int SampleCount)
        {
            return base.GenerateXImpl(Interval, SampleCount);
        }

        public double[] GenerateY(mko.BI.Bo.Interval<double> Interval, int SampleCount)
        {
            double[] sup = new double[SampleCount + 1];

            foreach (var gen_loopVariable in _Generatoren)
            {
                var gen = gen_loopVariable;
                var y = gen.GenerateY(Interval, SampleCount);
                for (int i = 0; i <= SampleCount - 1; i++)
                {
                    sup[i] += y[i];
                }
            }
            return sup;

        }


    }
}
