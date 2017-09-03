using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Testdata.XY
{
    /// <summary>
    /// Generator für Sinusfunktionen
    /// </summary>
    /// <remarks></remarks>
    public class Sinusgenerator : GeneratorBase, IGenerator
    {

        /// <summary>
        /// Schwingungsperioden pro 2Pi
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double Frequency { get; set; }

        /// <summary>
        /// "Ausschlag" der Sinusschwingungen
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double Amplitude { get; set; }

        /// <summary>
        /// Erzeugt aus den Eingaben eine X- Achse
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public double[] GenerateX(mko.BI.Bo.Interval<double> Interval, int SampleCount)
        {
            return base.GenerateXImpl(Interval, SampleCount);
        }

        /// <summary>
        /// Erzeugt aus den Eingaben eine X- Achse
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public double[] GenerateY(mko.BI.Bo.Interval<double> Interval, int SampleCount)
        {

            // Array anlegen, das die Y- Achse aufnimmt
            double[] Y = new double[SampleCount];

            // X- Werte erzeugen
            double[] X = GenerateX(Interval, SampleCount);

            // Array mit Daten Füllen

            for (int i = 0; i <= Y.GetUpperBound(0); i++)
            {
                Y[i] = Amplitude * Math.Sin(Frequency * X[i]);
            }
            return Y;
        }


        //Public Function GeneratePoints() As Tuple(Of Double, Double)()
        //    Dim X() As Double = GenerateX()
        //    Dim Y() As Double = GenerateY()

        //    Dim p(Count - 1) As Tuple(Of Double, Double)
        //    For i As Integer = 0 To Count - 1
        //        p(i) = New Tuple(Of Double, Double)(X(i), Y(i))
        //    Next

        //    Return p

        //End Function

    }
}
