using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Testdata.XY
{
    /// <summary>
    /// Basisklasse aller Generatoren. Implementiert die Erzeugung von X- Werten auf einem 
    /// abzutastenden Intervall
    /// </summary>
    /// <remarks></remarks>
    public class GeneratorBase
    {

        public double[] GenerateXImpl(mko.BI.Bo.Interval<double> Interval, int SampleCount)
        {

            // Array anlegen, das die X- Achse aufnimmt
            double[] X = new double[SampleCount];

            // Weginkremente berechnen
            double weginkrement = (Interval.End - Interval.Begin) / SampleCount;

            // Array mit Daten Füllen
            for (int i = 0; i < X.Length; i++)
            {
                // Kurzform von X(i) = X(i) + weginkrement
                X[i] = i * weginkrement + Interval.Begin;
            }
            return X;
        }


    }
}
