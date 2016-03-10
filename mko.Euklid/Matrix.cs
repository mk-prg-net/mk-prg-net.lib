using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Euklid.Transformations
{
    public class Matrix
    {
        Vector[] _lines;
        int _dimension;

        public int Dimensions
        {
            get
            {
                return _dimension;
            }
        }

        public int CountLines
        {
            get
            {
                return _lines.Length;
            }
        }

        public Matrix(int countLines, int dimensions)
        {
            _dimension = dimensions;
            _lines = new Vector[countLines];
            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i] = new Vector(dimensions);
            }
        }

        public Matrix(params Vector[] Lines)
        {
            _dimension = Lines.First().Dimensions;
            if (Lines.Any(r => r.Dimensions != Dimensions))
                throw new Exception("Es wird versucht, eine Matrix aus einer MEnge von Vektroren zu erstellen, wobei dies unterschiedlich lange Dimensionen haben");

            // Vektoren im Lines- Array kopieren ins _line- Array
            _lines = Lines.Select(r => new Vector(r)).ToArray();
        }

        public Matrix(Matrix M)
        {
            _dimension = M._dimension;
            _lines = new Vector[M._lines.Length];
            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i] = new Vector(M._lines[i]);
            }
        }

        public static Matrix CreateIdentityMatrix(int dimension)
        {
            Matrix u = new Matrix(dimension, dimension);
            for (int i = 0; i < dimension; i++)
                u[i, i] = 1.0;

            return u;
        }

        public Vector Line(int ix)
        {
            return _lines[ix];
        }

        public void SetLine(int ix, Vector newLine)
        {
            Debug.Assert(newLine.Dimensions == Dimensions);
            Debug.Assert(ix < CountLines && ix >= 0);

            _lines[ix] = newLine;
        }

        public void SetLine(int ix, params double[] newLine)
        {
            Debug.Assert(newLine.Length == Dimensions);
            Debug.Assert(ix < CountLines && ix >= 0);

            _lines[ix].Set(newLine);

        }

        public Vector Column(int ix)
        {
            Vector col = new Vector(_lines.Select(r => r[ix]).ToArray());
            return col;
        }

        public void SetColumn(int ix, Vector newColumn)
        {
            Debug.Assert(newColumn.Dimensions == CountLines);
            Debug.Assert(ix < Dimensions && ix >= 0);
            for (int i = 0; i < CountLines; i++)
            {
                _lines[i][ix] = newColumn[i];
            }
        }

        public double this[int ixLine, int ixCol]
        {
            get
            {
                Debug.Assert(ixLine < _lines.Length && ixCol < _dimension);
                return _lines[ixLine][ixCol];
            }

            set
            {
                Debug.Assert(ixLine < _lines.Length && ixCol < _dimension);
                _lines[ixLine][ixCol] = value;
            }
        }


        /// <summary>
        /// Transponierte Matrix bilden
        /// </summary>
        /// <returns>Transponierte</returns>
        public Matrix Transpose()
        {
            Matrix mt = new Matrix(CountLines, Dimensions);

            for (int line = 0; line < CountLines; line++)
            {
                for (int dimension = 0; dimension < Dimensions; dimension++)
                {
                    mt[dimension, line] = this[line, dimension];
                }
            }

            return mt;
        }

        public Vector LeftMul(Vector v)
        {
            return Transpose().RightMul(v);
        }

        public static Vector operator *(Vector v, Matrix m)
        {
            return m.LeftMul(v);
        }

        public Vector RightMul(Vector v)
        {
            Vector prod = new Vector(_lines.Select(line => line * v).ToArray());
            return prod;
        }

        public static Vector operator *(Matrix m, Vector v)
        {
            return m.RightMul(v);
        }

        public Matrix RightMul(Matrix m)
        {
            Debug.Assert(Dimensions == m.CountLines);

            Matrix mt = m.Transpose();

            Matrix prod = new Matrix(mt._lines.Select(line => this * line).ToArray());           

            return prod.Transpose();
        }

        /// <summary>
        /// Matrixprodukt
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {

            return A.RightMul(B);

        }

        /// <summary>
        /// Matrix wird in Dreiecksform gebracht
        /// </summary>
        /// <param name="countColsToNormalize">Anzahl der zu normalisierenden Spalen</param>
        /// <returns></returns>
        public Matrix Dreieck(int countColsToNormalize)
        {
            Matrix M = new Matrix(this);

            for (int col = 0; col < countColsToNormalize; col++)
            {
                // Zeile mit Koeff != 0 in der zu normalisierenden Spalte suchen
                int line;
                for (line = 0; line < M.CountLines; line++)
                    if ((col == 0 || M[line, col - 1] == 0) && M[line, col] != 0) break;

                if (line < M.CountLines)
                {
                    Vector normLine = M.Line(line) * (1.0 / M[line, col]);
                    normLine[col] = 1.0;

                    // Alle restlichen Zeilen in der Matrix umformen, so daß in der Spalte nur eine 1 stehen bleibt
                    for (int line2 = 0; line2 < M.CountLines; line2++)
                    {
                        for (int col2 = 0; col2 < M.Dimensions; col2++)
                        {
                            if (line == line2)
                                M[line2, col2] = normLine[col2];
                            else
                                M[line2, col2] -= normLine[col2] * M[line2, col2];
                        }
                    }
                }
            }

            return M;
        }

        /// <summary>
        /// Erzeugt eine Liste aufsteigender Indizes
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        IEnumerable<int> IndexGenerator(int max)
        {
            for (int i = 0; i <= max; i++)
                yield return i;

        }

        IEnumerable<bool> BoolFieldGenerator(bool value, int max)
        {
            for (int i = 0; i < max; i++)
                yield return value;

        }

        IEnumerable<Tuple<int, double, bool>> ColAsTriple(Matrix M, int ixCol, bool[] Ychanged)
        {
            for (int line = 0; line < CountLines; line++)
                yield return new Tuple<int, double, bool>(line, M[line, ixCol], Ychanged[line]);
        }

        
        /// <summary>
        /// Invertierung nach Austauschverfahren (Pivot)
        /// y = M x  -> x = MInv y
        /// </summary>
        /// <param name="Minv"></param>
        /// <returns></returns>
        public bool Invert(out Matrix Minv)
        {
            Minv = new Matrix(this);

            int[] ixX = IndexGenerator(Dimensions).ToArray();
            bool[] Xchanged = BoolFieldGenerator(false, Dimensions).ToArray();

            int[] ixY = IndexGenerator(CountLines).ToArray();
            bool[] Ychanged = BoolFieldGenerator(false, Dimensions).ToArray();

            int pivotC = 0, pivotL = 0;
            try
            {
                // Tauschen aller x gegen alle y
                for (pivotC = 0; pivotC < Math.Min(this.Dimensions, this.CountLines); pivotC++)
                {
                    // Suchen in aktueller Spalte nach einem Koeff für eine Zeile, deren y noch nicht getauscht wurde
                    pivotL = ColAsTriple(Minv, pivotC, Ychanged).Where(line => line.Item2 != 0 && !line.Item3).First().Item1;

                    // x und y tauschen. Tausch protokollieren
                    int tausch = ixX[pivotC];
                    ixX[pivotC] = ixY[pivotL];
                    ixY[pivotL] = tausch;

                    Xchanged[pivotC] = Ychanged[pivotL] = true;

                    // Inversion in Hilfsmatrix durchführen
                    Matrix M2 = new Matrix(Minv);
                    double p = Minv[pivotC, pivotL];

                    M2.SetLine(pivotL, Minv.Line(pivotL) * (-1 / p));
                    M2.SetColumn(pivotC, Minv.Column(pivotC) * (1 / p));
                    M2[pivotL, pivotC] = 1 / p;

                    for (int line = 0; line < Dimensions; line++)
                    {
                        if (line != pivotL)
                        {
                            for (int col = 0; col < Dimensions; col++)
                            {
                                if (col != pivotC)
                                {
                                    M2[line, col] = Minv[line, col] + -Minv[line, pivotC] * Minv[pivotL, col] / p;
                                }
                            }
                        }
                    }


                    // Hilfsmatrix zur neuen gültigen erklären
                    Minv = M2;
                }


                // Umsortieren der Zeilen
                Matrix M3 = new Matrix(Minv.CountLines, Minv.Dimensions);
                for (int line = 0; line < CountLines; line++)
                {
                    M3.SetLine(ixY[line], Minv.Line(line));                    
                }

                Matrix M4 = new Matrix(Minv.CountLines, Minv.Dimensions);
                for (int col = 0; col < Dimensions; col++)
                {
                    M4.SetColumn(ixX[col], M3.Column(col));
                }

                Minv = M4;

            }
            catch (Exception)
            {
                return false;
            }

            // Reihenfolge wiederherstellen

            return true;
        }

        IEnumerable<Tuple<Vector, Vector>> Combine(Matrix M1, Matrix M2)
        {
            Debug.Assert(M1.CountLines == M2.CountLines);
            for (int i = 0; i < M1.CountLines; i++)
                yield return new Tuple<Vector, Vector>(M1.Line(i), M2.Line(i));
        }

        public override bool Equals(object obj)
        {
            Matrix M2 = (Matrix)obj;

            if (Dimensions != M2.Dimensions)
                return false;

            if (CountLines != M2.CountLines)
                return false;

            if (Combine(this, M2).Any(t => !t.Item1.Equals(t.Item2)))
                return false;

            return true;
        }

        public Matrix Round(int digits)
        {
            Matrix Mr = new Matrix(_lines.Select(line => line.Round(digits)).ToArray());
            return Mr;            
        }

    }
}

