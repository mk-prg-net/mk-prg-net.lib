
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 3.6.2012
//
//  Projekt.......: mko.Euklid
//  Name..........: Line.cs
//  Aufgabe/Fkt...: Linie im euklidischen Raum
//                  
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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace mko.Euklid
{
    public class Line : Space
    {
        public Line(int dimensions)
            : base(dimensions)
        {
            _p1 = new Vector(Dimensions);
            _p2 = new Vector(Dimensions);
        }

        public Line(Line line)
            : base(line.Dimensions)
        {
            _p1 = new Vector(line.P1);
            _p2 = new Vector(line.P2);
        }

        public Line(Vector P1, Vector P2)
            : base(P1.Dimensions)
        {
            Debug.Assert(P1.Dimensions == P2.Dimensions);
            _p1 = new Vector(P1);
            _p2 = new Vector(P2);
        }

        /// <summary>
        /// Koordinatentransformation: der Kunstruktor lieferte die Linie in den Koordinaten des Bildraumes
        /// </summary>
        /// <param name="line"></param>
        /// <param name="trafo"></param>
        public Line(Line line, Transformations.Transformation trafo)
            : base(line.Dimensions)
        {
            _p1 = trafo.apply(line.P1);
            _p2 = trafo.apply(line.P2);
        }


        Vector _p1;
        Vector _p2;

        /// <summary>
        /// Startpunkt
        /// </summary>
        public Vector P1
        {
            get
            {
                return _p1;
            }
        }

        /// <summary>
        /// Endpunkt
        /// </summary>
        public Vector P2
        {
            get
            {
                return _p2;
            }
        }


        /// <summary>
        /// Betrag des Richtungsvektors
        /// </summary>
        public double DirectionLength
        {
            get
            {
                return (P1 - P2).Length;
            }
        }

        /// <summary>
        /// Richtungsvektor
        /// </summary>
        public Vector Direction
        {
            get
            {
                return P1 - P2;
            }

            set
            {
                _p2 = P1 + value;
            }
        }

        /// <summary>
        ///  Prüft, ob der Punkt in der Linie enthalten ist
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public override bool Contains(Vector P, double Epsilon)
        {
            double[] n = new double[Dimensions];
            int count = 0;
            double mw = 0;
            //Lösen der Gleichung P = n*Direction + P1;

            var Pdiff = P - P1;
            for (int i = 0; i < Dimensions; i++)
            {
                n[count] = Pdiff[i] / Direction[i];

                if (!double.IsNaN(n[count]))
                {
                    // n/0 oder 0/0 
                    if (double.IsInfinity(n[i]))
                        return false;
                    mw += n[count++];
                }
            }

            // Fall: (0,0,..., 0)t = n*(0,0,...,0)t wurde gelöst
            if (count == 0)
                return true;

            mw /= count;

            // Fall: n[i] weichen zu stark voneinander ab
            if (n.Sum(ni => (ni - mw) * (ni - mw)) / count > Epsilon)
                return false;

            return true;
        }


        /// <summary>
        /// Berechnet den Schnittpunkt zwischen dieser Linie und line2. 
        /// </summary>
        /// <param name="line2">Linie, für die der Schnittpunkt mit dieser gesucht wird</param>
        /// <param name="Intersection">berechneter Schnittpunkt</param>
        /// <returns>true, wenn ein Schnittpunkt existiert</returns>
        public bool IntersectionWith(Line line2, out Vector Intersection)
        {
            Intersection = new Vector(Dimensions);

            // Lösen der Gleichung n*Direction + P1 = m*Direction2 + P2   
            // => P2 - P1 = n*Direction - m*Direction2  
            // => P2 - P1 = [Direction | Direction2] * (n, m)t
            // => (n,m)t =  [Direction | Direction2]^-1 * P2 - P1

            Vector Pdiff = line2.P1 - P1;
            Transformations.Matrix M = new Transformations.Matrix(Dimensions, 2);
            M.SetColumn(0, Direction);
            M.SetColumn(1, line2.Direction);

            Transformations.Matrix Minv;
            if (!M.Invert(out Minv))
                return false;

            Vector nm = Minv.RightMul(Pdiff);

            Intersection.Set((nm[0] * Direction + P1).coordinates);
            return true;            
        }

        /// <summary>
        /// Prüft, ob ein Schnittpunkt von n*l1 und m*l2 existiert
        /// </summary>
        /// <param name="n">Koeffizient</param>
        /// <param name="l1">Vektor</param>
        /// <param name="m">Koeffizient</param>
        /// <param name="l2">Vektor</param>
        /// <param name="Epsilon">Schranke, unterhalb der ein Abstand zwischen zwei Punkten beide als Identisch auszeichnet</param>
        /// <param name="I1">berechneter Schnittpunkt</param>
        /// <returns>true, wenn der Schnittpunkt existiert</returns>
        bool TestIntersection(double n, Line l1, double m, Line l2, double Epsilon, out Vector I1)
        {

            I1 = n * l1.Direction + l1.P1;
            Vector I2 = m * l2.Direction + l2.P2;

            Vector D = I1 - I2;

            double mw = D.coordinates.Sum() / D.coordinates.Length;
            double qsum = D.coordinates.Sum(c => (c - mw) * (c - mw));

            if (qsum / D.coordinates.Length <= Epsilon)
                return true;
            else
                return false;

        }
    }
}
