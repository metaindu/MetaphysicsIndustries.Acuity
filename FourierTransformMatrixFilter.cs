
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


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class FourierTransformMatrixFilter : MatrixFilter
    {
        //public struct Pair
        //{
        //    public Pair(Matrix real, Matrix imaginary)
        //    {
        //        _real = real;
        //        _imaginary = imaginary;
        //    }

        //    private Matrix _real;
        //    public Matrix Real
        //    {
        //        get { return _real; }
        //    }

        //    private Matrix _imaginary;
        //    public Matrix Imaginary
        //    {
        //        get { return _imaginary; }
        //    }

        //}

        public override Matrix Apply(Matrix input)
        {
            //Xk = sigma(n=0,N-1,xn*e^(-2pi*i*n*k/N)

            //e^(i*theta) = cos(theta) + i*sin(theta)

            return Apply2(input).Value1;











            //Matrix x = new Matrix(input.RowCount, input.ColumnCount);
            //Matrix phase = new Matrix(input.RowCount, input.ColumnCount);

            //int i;
            //int j;

            //Vector row;
            //Vector column;

            //FourierTransformVectorFilter vFilter = new FourierTransformVectorFilter();
            ////InverseFourierTransformVectorFilter vFilter2 = new InverseFourierTransformVectorFilter();

            //for (i = 0; i < input.RowCount; i++)
            //{
            //    row = input.GetRow(i);
            //    row = vFilter.Apply(row);

            //    for (j = 0; j < input.ColumnCount; j++)
            //    {
            //        x[i, j] = row[j];
            //    }
            //}

            //for (j = 0; j < input.ColumnCount; j++)
            //{
            //    column = input.GetColumn(j);
            //    column = vFilter.Apply(column);

            //    for (i = 0; i < input.RowCount; i++)
            //    {
            //        x[i, j] += column[i];
            //    }
            //}

            //return x;


            ////    for (kc = 0; kc < input.ColumnCount; kc++)
            ////    {
            ////        columnAmount = kc / (float)input.ColumnCount;

            ////        real = 0;
            ////        imag = 0;
            ////        for (nr = 0; nr < input.RowCount; nr++)
            ////        {
            ////            for (nc = 0; nc < input.ColumnCount; nc++)
            ////            {
            ////                z = twoPi * (nr * rowAmount);

            ////                value = input.GetValueNoCheck(nr, nc);

            ////                real += value * Math.Cos(z);
            ////                imag += value * Math.Sin(z);
            ////            }
            ////        }
            ////        x[kr,kc] = Math.Sqrt(real * real + imag * imag);
            ////        //phase[kr,kc] = Math.Atan2(imag, real);
            ////    }
            ////}

            //return x;
        }

        protected virtual FourierTransformVectorFilter GetVectorFilter()
        {
            return new FourierTransformVectorFilter();
        }

        public  STuple<Matrix, Matrix> Apply2(Matrix input)
        {
            return Apply2(new STuple<Matrix, Matrix>(input, null));
        }

        public virtual STuple<Matrix, Matrix> Apply2(STuple<Matrix, Matrix> input)
        {
            Matrix inputReal = input.Value1;
            Matrix inputImag = input.Value2;

            if (inputImag == null) { inputImag = new Matrix(inputReal.RowCount, inputReal.ColumnCount); }

            //output
            Matrix outputReal = new Matrix(inputReal.RowCount, inputReal.ColumnCount);
            Matrix outputImaginary = new Matrix(inputReal.RowCount, inputReal.ColumnCount);

            Dft2D(inputReal, inputImag, outputReal, outputImaginary);

            return new STuple<Matrix, Matrix>(outputReal, outputImaginary);
        }

        private void Dft2D(Matrix inputReal, Matrix inputImag, Matrix outputReal, Matrix outputImaginary)
        {
            Matrix tempReal = new Matrix(inputReal.RowCount, inputReal.ColumnCount);
            Matrix tempImaginary = new Matrix(inputReal.RowCount, inputReal.ColumnCount);

            int r;
            int c;

            FourierTransformVectorFilter vFilter = GetVectorFilter();

            for (r = 0; r < inputReal.RowCount; r++)
            {
                vFilter.dft(
                    inputReal.GetRowWindow(r),
                    inputImag.GetRowWindow(r),
                    tempReal.GetRowWindow(r),
                    tempImaginary.GetRowWindow(r));
            }

            for (c = 0; c < tempReal.ColumnCount; c++)
            {
                vFilter.dft(
                    tempReal.GetColumnWindow(c),
                    tempImaginary.GetColumnWindow(c),
                    outputReal.GetColumnWindow(c),
                    outputImaginary.GetColumnWindow(c));
            }

            for (r = 0; r < outputReal.RowCount; r++)
            {
                for (c = 0; c < outputReal.ColumnCount; c++)
                {
                    outputReal[r, c] = ScaleForInverse(outputReal[r, c], outputReal.RowCount, outputReal.ColumnCount);
                    outputImaginary[r, c] = ScaleForInverse(outputImaginary[r, c], outputImaginary.RowCount, outputImaginary.ColumnCount);
                }
            }
        }

        protected virtual float ScaleForInverse(float x, int rows, int cols)
        {
            return x;
        }
    }
}
