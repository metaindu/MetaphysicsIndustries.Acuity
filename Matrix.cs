
/*
 *  MetaphysicsIndustries.Acuity
 *  Copyright (C) 2009-2021 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;

using System.Diagnostics;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class Matrix : Tensor
    {
        private static AcuityEngine _engine = new AcuityEngine();

        public static Matrix FromUniform(int rows, int columns, float value)
        {
            Matrix ret = new Matrix(rows, columns);

            int row;
            int col;
            for (row = 0; row < rows; row++)
            {
                for (col = 0; col < columns; col++)
                {
                    ret[row,col] = value;
                }
            }

            return ret;
        }

        public Matrix(int rows, int columns)
        {
            _rowCount = rows;
            _columnCount = columns;
            _array = new float[_rowCount, _columnCount];

            int i;
            int j;

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    _array[i, j] = 0;
                }
            }
        }

        public Matrix(int rows, int columns, params float[] initialContents)
            : this(rows, columns)
        {
            int i;
            int j;
            int x = 0;
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    if (x >= initialContents.Length) { break; }
                    this[i, j] = initialContents[x];
                    x++;
                }
            }
        }

        public Matrix Clone()
        {
            Matrix output = this.CloneSize();

            int r;
            int c;

            for (r = 0; r < this.RowCount; r++)
            {
                for (c = 0; c < this.ColumnCount; c++)
                {
                    output[r, c] = this[r, c];
                }
            }

            return output;
        }

        public Matrix CloneSize()
        {
            return new Matrix(RowCount, ColumnCount);
        }

        private int _rowCount;
        public int RowCount
        {
            get { return _rowCount; }
        }
        private int _columnCount;
        public int ColumnCount
        {
            get { return _columnCount; }
        }
        private float[,] _array;

        public int Count { get { return RowCount * ColumnCount; } }

        //public Vector GetRow(int row)
        //{
        //    Vector ret = new Vector(ColumnCount);
        //    int i;

        //    for (i = 0; i < ColumnCount; i++)
        //    {
        //        float expr = this[row, i];
        //        ret[i] = expr;
        //    }

        //    return ret;
        //}

        //public Vector GetColumn(int column)
        //{
        //    Vector ret = new Vector(RowCount);
        //    int i;

        //    for (i = 0; i < RowCount; i++)
        //    {
        //        ret[i] = this[i, column];
        //    }

        //    return ret;
        //}

        public MatrixRowWindow GetRowWindow(int row)
        {
            return new MatrixRowWindow(this, row);
        }

        public MatrixColumnWindow GetColumnWindow(int column)
        {
            return new MatrixColumnWindow(this, column);
        }

        public Matrix GetSlice(int startRow, int startColumn, int numberOfRows, int numberOfColumns)
        {
            int i;
            int j;

            Matrix mat = new Matrix(numberOfRows, numberOfColumns);

            for (i = 0; i < numberOfRows; i++)
            {
                for (j = 0; j < numberOfColumns; j++)
                {
                    mat[i, j] = this[i + startRow, j + startColumn];
                }
            }

            return mat;
        }

        //RowCollection Rows
        //ColumnCollection Columns

        //public Matrix2 Multiply(float scaleFactor)
        //{
        //    //check dimensions
        //}
        ////public Vector2 Multiply(Vector2 vector)
        ////{
        //    //check dimensions
        ////}
        //public Matrix2 Multiply(Matrix2 matrix)
        //{
        //    //check dimensions
        //}

        //public Matrix2 Add(Matrix2 matrix)
        //{
        //    //check dimensions
        //}

        //operator overloads

        //public float GetDeterminant()
        //{
        //}

        public Matrix Convolution(Matrix kernel)
        {
            //return AdvancedConvolution(convolvee, AcuityEngine.MultiplicationBiMod, AcuityEngine.AdditionBiMod);
            
            int r = RowCount + kernel.RowCount - 1;
            int c = ColumnCount + kernel.ColumnCount - 1;

            Matrix output = new Matrix(r, c);

            float sum;

            int n1;
            int n2;
            int k1;
            int k2;

            for (n1 = 0; n1 < r; n1++)
            {
                for (n2 = 0; n2 < c; n2++)
                {
                    sum = 0;

                    for (k1 = 0; k1 < RowCount; k1++)
                    {
                        if (n1 - k1 >= kernel.RowCount) { continue; }
                        if (n1 - k1 < 0) { break; }

                        for (k2 = 0; k2 < ColumnCount; k2++)
                        {
                            if (n2 - k2 >= kernel.ColumnCount) { continue; }
                            if (n2 - k2 < 0) { break; }

                            //sum += this[k1, k2] * kernel[n1 - k1, n2 - k2];
                            sum += this.GetValueNoCheck(k1, k2) * kernel.GetValueNoCheck(n1 - k1, n2 - k2);
                        }

                    }

                    output[n1, n2] = sum;
                }
            }

            return output;
        }

        public Matrix AdvancedConvolution(Matrix convolvee, 
            BiModulator Value1Op, BiModulator Value2Op)
        {



            int r = RowCount + convolvee.RowCount - 1;
            int c = ColumnCount + convolvee.ColumnCount - 1;

            Matrix ret = new Matrix(r, c);

            //int iiend;
            //int jjend;

            //List<float> group = new List<float>();
            float term;



            int n1;
            int n2;
            int k1;
            int k2;

            int[] times = new int[16];
            int lasttime = System.Environment.TickCount;
            int time;

            time = System.Environment.TickCount; times[0] += time - lasttime; lasttime = time;
            for (n1 = 0; n1 < r; n1++)
            {
                ////time = Environment.TickCount; times[1] += time - lasttime; lasttime = time;
                for (n2 = 0; n2 < c; n2++)
                {
                    //group.Clear();
                    term = 0;//Literal.Zero;

                    for (k1 = 0; k1 < RowCount; k1++)
                    {
                        ////time = Environment.TickCount; times[2] += time - lasttime; lasttime = time;
                        if (n1 - k1 < 0) { break; }
                        if (n1 - k1 >= convolvee.RowCount) { continue; }
                        ////time = Environment.TickCount; times[3] += time - lasttime; lasttime = time;

                        ////time = Environment.TickCount; times[4] += time - lasttime; lasttime = time;

                        ////time = Environment.TickCount; times[5] += time - lasttime; lasttime = time;
                        for (k2 = 0; k2 < ColumnCount; k2++)
                        {
                            ////time = Environment.TickCount; times[6] += time - lasttime; lasttime = time;
                            if (n2 - k2 < 0) { break; }
                            if (n2 - k2 >= convolvee.ColumnCount) { continue; }

                            ////time = Environment.TickCount; times[7] += time - lasttime; lasttime = time;

                            //float expr = new FunctionCall(
                            //    Value1Op,
                            //    this[k1, k2],
                            //    convolvee[n1 - k1, n2 - k2]);

                            term = Value2Op(term, Value1Op(this[k1, k2], convolvee[n1 - k1, n2 - k2]));
                            ////time = Environment.TickCount; times[11] += time - lasttime; lasttime = time;
                            //////expr = expr.CleanUp();
                            ////time = Environment.TickCount; times[10] += time - lasttime; lasttime = time;
                            //////expr = new FunctionCall(
                            //////    Value2Op,
                            //////    term,
                            //////    expr);

                            //group.Add(
                            //    (expr));

                            ////time = Environment.TickCount; times[9] += time - lasttime; lasttime = time;
                            //////term = expr.CleanUp();
                            ////time = Environment.TickCount; times[8] += time - lasttime; lasttime = time;
                        }

                    }

                    //float[] terms = group.ToArray();
                    //FunctionCall fc = new FunctionCall(Value2Op, terms);
                    //float expr2 = fc.CleanUp();
                    //time = Environment.TickCount; times[12] += time - lasttime; lasttime = time;
                    ret[n1, n2] = term;// fc.CleanUp();
                    ////time = Environment.TickCount; times[13] += time - lasttime; lasttime = time;
                }
                ////time = Environment.TickCount; times[14] += time - lasttime; lasttime = time;
            }
            time = System.Environment.TickCount; times[15] += time - lasttime; lasttime = time;




            ////////int i;
            ////////int j;
            ////////int ii;
            ////////int jj;
            //////int r = RowCount + convolvee.RowCount - 1;
            //////int c = ColumnCount + convolvee.ColumnCount - 1;

            //////Matrix2 ret = new Matrix2(r, c);

            ////////int iiend;
            ////////int jjend;

            //////List<float> group = new List<float>();
            //////float term;

            //////int n1;
            //////int n2;
            //////int k1;
            //////int k2;

            //////int[] times = new int[16];
            //////int lasttime = Environment.TickCount;
            //////int time;

            //////time = Environment.TickCount; times[0] += time - lasttime; lasttime = time;
            //////for (n1 = 0; n1 < r; n1++)
            //////{
            //////    time = Environment.TickCount; times[1] += time - lasttime; lasttime = time;
            //////    for (k1 = 0; k1 < RowCount; k1++)
            //////    {
            //////        time = Environment.TickCount; times[2] += time - lasttime; lasttime = time;
            //////        if (n1 - k1 < 0) { break; }
            //////        if (n1 - k1 >= convolvee.RowCount) { continue; }
            //////        time = Environment.TickCount; times[3] += time - lasttime; lasttime = time;

            //////        //group.Clear();
            //////        term = Literal.Zero;

            //////        time = Environment.TickCount; times[4] += time - lasttime; lasttime = time;
            //////        for (n2 = 0; n2 < c; n2++)
            //////        {
            //////            time = Environment.TickCount; times[5] += time - lasttime; lasttime = time;
            //////            for (k2 = 0; k2 < ColumnCount; k2++)
            //////            {
            //////                time = Environment.TickCount; times[6] += time - lasttime; lasttime = time;
            //////                if (n2 - k2 < 0) { break; }
            //////                if (n2 - k2 >= convolvee.ColumnCount) { continue; }

            //////                time = Environment.TickCount; times[7] += time - lasttime; lasttime = time;

            //////                float expr = new FunctionCall(
            //////                    Value1Op,
            //////                    this[k1, k2],
            //////                    convolvee[n1 - k1, n2 - k2]);
            //////            time = Environment.TickCount; times[11] += time - lasttime; lasttime = time;
            //////                expr = expr.CleanUp();
            //////            time = Environment.TickCount; times[10] += time - lasttime; lasttime = time;
            //////                expr = new FunctionCall(
            //////                    Value2Op,
            //////                    term,
            //////                    expr);

            //////                //group.Add(
            //////                //    (expr));

            //////            time = Environment.TickCount; times[9] += time - lasttime; lasttime = time;
            //////                term = expr.CleanUp();
            //////                time = Environment.TickCount; times[8] += time - lasttime; lasttime = time;
            //////            }

            //////            float[] terms = group.ToArray();
            //////            FunctionCall fc = new FunctionCall(Value2Op, terms);
            //////            ret[n1, n2] = term;// fc.CleanUp();
            //////            time = Environment.TickCount; times[12] += time - lasttime; lasttime = time;
            //////        }
            //////        time = Environment.TickCount; times[13] += time - lasttime; lasttime = time;
            //////    }
            //////    time = Environment.TickCount; times[14] += time - lasttime; lasttime = time;
            //////}
            //////time = Environment.TickCount; times[15] += time - lasttime; lasttime = time;

            //////for (i = 0; i < r; i++)
            //////{
            //////    for (j = 0; j < c; j++)
            //////    {
            //////        iiend = Math.Min(i, convolvee.RowCount);
            //////        jjend = Math.Min(j, convolvee.ColumnCount);
            //////
            //////        group.Clear();
            //////        term = this[i - convolvee.RowCount + 1, j - convolvee.ColumnCount + 1];
            //////
            //////        for (ii = Math.Max(0, i - r); ii < iiend; ii++)
            //////        {
            //////            for (jj = Math.Min(0, j - c); jj < jjend; jj++)
            //////            {
            //////                group.Add(
            //////                    new FunctionCall(
            //////                    Value1Op,
            //////                    term,
            //////                    convolvee[ii, jj]));
            //////            }
            //////        }
            //////
            //////        ret[i, j] = _engine.CleanUp(new FunctionCall(Value2Op, group.ToArray()));
            //////    }
            //////}

            return ret;
        }

        public Matrix PerPixelOperation(IPerPixelOperator oper)
        {
            int r = RowCount + oper.GetExtraWidth(this);
            int c = ColumnCount + oper.GetExtraHeight(this);

            Matrix ret = new Matrix(r, c);

            float[,] values = new float[RowCount, ColumnCount];
            //float[,] values2 = new float[convolvee.RowCount, convolvee.ColumnCount];

            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    values[i, j] = this[i, j];
                }
            }

            oper.SetValues(values);

            int row;
            int column;

            for (row = 0; row < r; row++)
            {
                for (column = 0; column < c; column++)
                {
                    ret[row, column] = oper.Operate(row, column);
                }
            }

            return ret;
        }

        public float this[int row, int column]
        {
            get
            {
                CheckBounds(row, column);

                return _array[row, column];
            }
            set
            {
                CheckBounds(row, column);

                _array[row, column] = value;
            }
        }

        [Conditional("DEBUG")]
        protected void CheckBounds(int row, int column)
        {
            if (row < 0 || row >= RowCount) { throw new IndexOutOfRangeException("row"); }
            if (column < 0 || column >= ColumnCount) { throw new IndexOutOfRangeException("column"); }
        }

        protected class MatrixEnumerator : IEnumerator<float>
        {
            public MatrixEnumerator(Matrix matrix)  
            {
                if (matrix == null) { throw new ArgumentNullException("matrix"); }
                 
                //attach collection change notification

                _matrix = matrix;
            }

            Matrix _matrix;
            int _row = -1;
            int _column = 0;

            #region IEnumerator<float> Members

            public float Current
            {
                get
                {
                    if (_row < 0)
                    {
                        //before Value1 element
                        return float.NaN;
                    }
                    else if (_column < 0)
                    {
                        //after last element
                        return float.NaN;
                    }
                    else
                    {
                        //normal operation
                        return _matrix[_row, _column];
                    }
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                //detach collection change notification
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (_row < 0)
                {
                    //before Value1 element

                    _row = 0;
                    _column = 0;

                    return true;
                }
                else if (_column < 0)
                {
                    //after last element
                    return false;
                }
                else
                {
                    //normal operation
                    _column++;
                    if (_column >= _matrix.ColumnCount)
                    {
                        _row++;
                        if (_row >= _matrix.RowCount)
                        {
                            _column = -1;
                            return false;
                        }
                        _column = 0;
                    }

                    return true;
                }
            }

            public void Reset()
            {
                _row = -1;
                _column = 0;
            }

            #endregion
        }

        #region IEnumerable<float> Members

        public override IEnumerator<float> GetEnumerator()
        {
            return new MatrixEnumerator(this);
        }

        #endregion


        public override void ApplyToAll(Modulator mod)
        {
            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    this[i, j] = mod(this[i, j]);
                }
            }
        }



        internal float GetValueNoCheck(int row, int column)
        {
            return _array[row, column];
        }

        public delegate void DoWithValue(float value);
        public delegate void DoWithIndex(int row, int column);
        public delegate void DoWithValue2(Matrix matrix, float value);
        public delegate void DoWithIndex2(Matrix matrix, int row, int column);

        public void DoWith(DoWithIndex operation)
        {
            int i;
            int j;

            for (j = 0; j < RowCount; j++)
            {
                for (i = 0; i < ColumnCount; i++)
                {
                    operation(j, i);
                }
            }
        }
        public void DoWith(DoWithIndex2 operation)
        {
            int i;
            int j;

            for (j = 0; j < RowCount; j++)
            {
                for (i = 0; i < ColumnCount; i++)
                {
                    operation(this, j, i);
                }
            }
        }
        public void DoWith(DoWithValue operation)
        {
            int i;
            int j;

            for (j = 0; j < RowCount; j++)
            {
                for (i = 0; i < ColumnCount; i++)
                {
                    operation(this[j, i]);
                }
            }
        }
        public void DoWith(DoWithValue2 operation)
        {
            int i;
            int j;

            for (j = 0; j < RowCount; j++)
            {
                for (i = 0; i < ColumnCount; i++)
                {
                    operation(this, this[j, i]);
                }
            }
        }

        public void Autoscale()
        {
            float sum = 0;
            DoWith(delegate(float value) { sum += value; });
            ApplyToAll(delegate(float value) { return value / sum; });
        }
    }

    public class MatrixRowWindow : IArray<float>
    {
        public MatrixRowWindow(Matrix matrix, int row)
        {
            if (matrix == null) throw new ArgumentNullException("matrix");
            if (row < 0 || row >= matrix.RowCount) throw new ArgumentOutOfRangeException("row");

            _matrix = matrix;
            _row = row;
        }

        Matrix _matrix;
        int _row;

        #region IArray<float> Members

        public float this[int index]
        {
            get { return _matrix[_row, index]; }
            set { _matrix[_row, index] = value; }
        }

        public int Length
        {
            get { return _matrix.ColumnCount; }
        }

        #endregion
    }

    public class MatrixColumnWindow : IArray<float>
    {
        public MatrixColumnWindow(Matrix matrix, int column)
        {
            if (matrix == null) throw new ArgumentNullException("matrix");
            if (column < 0 || column >= matrix.ColumnCount) throw new ArgumentOutOfRangeException("column");

            _matrix = matrix;
            _column = column;
        }

        Matrix _matrix;
        int _column;

        #region IArrayReadable<float> Members

        public float this[int index]
        {
            get { return _matrix[index, _column]; }
            set { _matrix[index, _column] = value; }
        }

        public int Length
        {
            get { return _matrix.RowCount; }
        }

        #endregion
    }
}
