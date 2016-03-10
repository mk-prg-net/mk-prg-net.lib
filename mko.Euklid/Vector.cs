//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.6.2012
//
//  Projekt.......: mko.Euklid
//  Name..........: Vector.cs
//  Aufgabe/Fkt...: Darstellung eines Raumpunktes durch einen Ortsvektor
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
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace mko.Euklid
{
    public class Vector : Space
    {
        public class VectorException : Exception
        {
            public VectorException(string msg) : base(msg) { }
            public VectorException(string msg, Exception InnerException) : base(msg, InnerException) { }
        }

        public Vector(int dimensions)
            : base(dimensions)
        {
            coordinates = new double[dimensions];
        }

        public Vector(params double[] coordinates)
            : base(coordinates.Length)
        {
            this.coordinates = new double[coordinates.Length];
            Array.Copy(coordinates, this.coordinates, coordinates.Length);
        }

        public Vector(IEnumerable<double> coordinates)
            : base(coordinates.Count())
        {
            long count = coordinates.Count();
            this.coordinates = new double[count];
            Array.Copy(coordinates.ToArray(), this.coordinates, count);
        }

        public Vector(Vector P)
            : base(P.Dimensions)
        {
            this.coordinates = new double[P.Dimensions];
            Array.Copy(P.coordinates, coordinates, Dimensions);
        }

        public Vector(Vector P, Transformations.Transformation trafo)
            : base(P.Dimensions)
        {
            this.coordinates = trafo.apply(P).coordinates;
        }

        public double[] coordinates;

        public override string ToString()
        {
            try
            {
                var bld = new StringBuilder();
                bld.Append("(");
                for (int i = 0; i < coordinates.Length; i++)
                    bld.Append(coordinates[i].ToString() + ", ");
                bld.Remove(bld.Length - 2, 2);
                bld.Append(")");
                return bld.ToString();
            }
            catch (Exception ex)
            {
                throw new VectorException("ToString()", ex);
            }
        }

        public string ToString(string FormatString)
        {
            try
            {
                var bld = new StringBuilder();
                bld.Append("(");
                for (int i = 0; i < coordinates.Length; i++)
                    bld.Append(coordinates[i].ToString(FormatString) + ", ");
                bld.Remove(bld.Length - 2, 2);
                bld.Append(")");
                return bld.ToString();
            }
            catch (Exception ex)
            {
                throw new VectorException("ToString(" + FormatString + ")", ex);
            }
        }

        public string ToString(string FormatString, System.Globalization.NumberFormatInfo nif)
        {
            try
            {
                var bld = new StringBuilder();
                bld.Append("(");
                for (int i = 0; i < coordinates.Length; i++)
                    bld.Append(coordinates[i].ToString(FormatString, nif) + ", ");
                bld.Remove(bld.Length - 2, 2);
                bld.Append(")");
                return bld.ToString();
            }
            catch (Exception ex)
            {
                throw new VectorException("ToString(" + FormatString + ")", ex);
            }
        }



        /// <summary>
        /// Zugriff auf die Koordinaten über einen Index
        /// </summary>
        /// <param name="ix"></param>
        /// <returns></returns>
        public double this[int ix]
        {
            get
            {
                return coordinates[ix];
            }

            set
            {
                coordinates[ix] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public void Set(params double[] c)
        {
            Debug.Assert(c.Length <= Dimensions);
            for (int i = 0; i < c.Length; i++)
                coordinates[i] = c[i];
        }

        /// <summary>
        /// Liefert true, wenn Raumpunkt P in Epsilon- Umgebung zu diesem Raumpunkt enthalten ist.
        /// </summary>
        /// <param name="P"></param>
        /// <param name="Epsilon"></param>
        /// <returns></returns>
        public override bool Contains(Vector P, double Epsilon)
        {
            if (DistanceBetween(this, P) <= Epsilon)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Euklidischer Abstand des Punktes vom Koordinatenursprung 
        /// Polarkoordinatendarstellung, Anteil Betrag
        /// </summary>
        public double Length
        {
            get
            {
                try
                {
                    double qs = 0.0;
                    foreach (double c in coordinates)
                        qs += c * c;
                    return Math.Sqrt(qs);
                }
                catch (Exception ex)
                {
                    throw new VectorException("Length", ex);
                }

            }
        }

        /// <summary>
        /// Liefert den Einheitsvektor, der auf den Raumpunkt zeigt
        /// </summary>
        public Vector UnitVector
        {
            get
            {
                double len = Length;
                return new Vector(coordinates.Select(c => c / len).ToArray());
                
            }
        }


        /// <summary>
        /// Siehe http://de.wikipedia.org/wiki/Kugelkoordinaten
        /// </summary>
        /// <param name="i"></param>
        /// <param name="Vn"></param>
        /// <returns></returns>
        public double Phi(int i)
        {
            try
            {
                Debug.Assert(i > 0);
                Vector Vi_minus_1 = new Vector(coordinates.Take(i - 1).ToArray());
                double Vi_minus_1_length = Vi_minus_1.Length;

                if (this[i] == 0 && Vi_minus_1_length == 0)
                    return 0;
                if (i == Dimensions - 1 && this[i] < 0)
                    return Math.Atan2(Vi_minus_1_length, this[i]) + Math.PI;
                else return Math.Atan2(Vi_minus_1_length, this[i]);
            }
            catch (Exception ex)
            {
                throw new VectorException("Phi", ex);
            }
        }

        /// <summary>
        /// Winkel Phi zur Drehachse eines Zylinderkoordinatensystems
        /// </summary>
        /// <param name="ixXAxis"></param>
        /// <param name="ixYAxis"></param>
        /// <returns></returns>
        public double PhiCylindrical(int ixXAxis, int ixYAxis)
        {
            try
            {
                Debug.Assert(ixXAxis < Dimensions && ixYAxis < Dimensions);
                Debug.Assert(ixXAxis != ixYAxis);

                double phi = 0.0;
                if (this[ixYAxis] == 0 && this[ixXAxis] == 0)
                    return double.NaN;
                else
                    phi = Math.Atan2(this[ixYAxis], this[ixXAxis]);

                // Fall: (x,y) liegt im 3. oder 4. Quadranten

                if (this[ixYAxis] < 0.0)
                    phi += 2 * Math.PI;

                return phi;
            }
            catch (Exception ex)
            {
                throw new VectorException("PhiCylindrical", ex);
            }
        }
        

        /// <summary>
        /// Euklidischer Abstand zwischen zwei Punkten
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double DistanceBetween(Vector P1, Vector P2)
        {
            try
            {
                Debug.Assert(P1.Dimensions == P2.Dimensions);
                return (P1 - P2).Length;
            }
            catch (Exception ex)
            {
                throw new VectorException("DistanceBetween", ex);
            }
        }

        public static bool operator ==(Vector P1, Vector P2)
        {            
            return P1.Dimensions == P2.Dimensions && P1.coordinates.SequenceEqual(P2.coordinates);
        }

        public static bool operator !=(Vector P1, Vector P2)
        {
            return !(P1 == P2);
        }

        const int HCB = 51;

        /// <summary>
        /// Überschreibt die GetHashCode- Methode aus der Basisklasse Object, indem jede coordinate zu  
        /// einem Koeffizienten ki in dem Polynom hash = k0 + k1*HCB + k2*HCB^2 + ... kn*HCB^n,
        /// wobei ki = Integerprojektion(ci aus coordinates) % HCB ist. Die Integerprojektion von c
        /// ist |c| für c >= 1.0, oder |1/c|
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            try
            {
                int hash = 0;
                int polynomfactor = 1;
                foreach (double c in coordinates)
                {
                    if (Math.Abs(c) >= 1.0)
                        hash += polynomfactor * ((int)Math.Floor(c) % HCB);
                    else
                        hash += polynomfactor * ((int)Math.Floor(1 / c) % HCB);

                    polynomfactor *= HCB;
                }

                return hash;
            }
            catch (Exception ex)
            {
                throw new VectorException("GetHashCode", ex);
            }
        }

        /// <summary>
        /// Differenzvektor berechnen
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static Vector operator -(Vector P1, Vector P2)
        {
            Debug.Assert(P1.Dimensions == P2.Dimensions);

            Vector Pdiff = new Vector(P1);

            for (int i = 0; i < Pdiff.Dimensions; i++)
            {
                Pdiff.coordinates[i] -= P2.coordinates[i];
            }

            return Pdiff;
        }

        /// <summary>
        /// Vektoraddition
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static Vector operator +(Vector P1, Vector P2)
        {
            Debug.Assert(P1.Dimensions == P2.Dimensions);

            Vector Psum = new Vector(P1.Dimensions);

            for (int i = 0; i < P1.Dimensions; i++)
            {
                Psum[i] = P1[i] + P2[i];
            }

            return Psum;
        }

        /// <summary>
        /// Strecken/Stauchen eines Vectors
        /// </summary>
        /// <param name="fact"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        public static Vector operator *(double fact, Vector P)
        {
            Vector factP = new Vector(P);
            for (int i = 0; i <  P.Dimensions; i++)
            {
                factP.coordinates[i] *= fact;
            }

            return factP;
        }

        public static Vector operator *( Vector P, double fact){
            return fact * P;
        }

        public static Vector operator /(Vector P, double fact)
        {
            return (1/fact) * P;
        }



        /// <summary>
        /// Inneres Produkt
        /// </summary>
        /// <param name="V1">1. Vektor</param>
        /// <param name="V2">2. Vector</param>
        /// <returns>Skalarprodukt</returns>
        public static double operator *(Vector V1, Vector V2)
        {
            Debug.Assert(V1.Dimensions == V2.Dimensions);
            double scalar = 0.0;
            for (int i = 0; i < V1.Dimensions; i++)
            {
                scalar += V1[i] * V2[i];
            }

            return scalar;
        }

        /// <summary>
        /// Vektor kopieren
        /// </summary>
        /// <param name="dest"></param>
        public void CopyTo(Vector dest)
        {
            Debug.Assert(Dimensions == dest.Dimensions);
            for (int i = 0; i < Dimensions; i++)
            {
                dest.coordinates[i] = coordinates[i];
            }
        }

        IEnumerable<Tuple<double, double>> Combine(Vector v1, Vector v2)
        {
            for (int i = 0; i < v1.Dimensions; i++)
                yield return new Tuple<double, double>(v1[i], v2[i]);
        }

        public override bool Equals(object obj)
        {
            Vector v2 = (Vector)obj;

            if (Dimensions != v2.Dimensions)
                return false;

            if (Combine(this, v2).Any(t => Math.Abs(t.Item1 - t.Item2) > Resolution))
                return false;

            return true;
        }


        /// <summary>
        /// Rundet alle Koordinaten eines Vektors auf digit Nachkommastellen
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public Vector Round(int digits)
        {
            Vector Vr = new Vector(this.coordinates.Select(c => Math.Round(c, digits)).ToArray());
            return Vr;           
        }

        public static Vector Normale2D(Vector v)
        {
            Debug.Assert(v.Dimensions == 2);

            double nyny = 1/(1+ v[1]*v[1]/(v[0]*v[0]));
            double nxnx = 1 - nyny;

            double nx = Math.Sqrt(nxnx);
            double ny = Math.Sqrt(nyny);

            if (v[0] >= 0 && v[1] >= 0)
                return new Vector(-nx, ny);
            else if (v[0] < 0 && v[1] > 0)
                return new Vector(-nx, -ny);
            else if (v[0] < 0 && v[1] < 0)
                return new Vector(nx, -ny);
            else
                return new Vector(nx, ny);
        }

    }
}
