using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class InverseFourierTransformMatrixFilter : FourierTransformMatrixFilter
    {
        //public override Matrix Apply(Matrix input)
        //{
        //    Matrix x = new Matrix(input.RowCount, input.ColumnCount);
        //    Matrix phase = new Matrix(input.RowCount, input.ColumnCount);

        //    int kr;
        //    int kc;

        //    Vector row;
        //    Vector column;

        //    InverseFourierTransformVectorFilter vFilter = new InverseFourierTransformVectorFilter();

        //    for (kr = 0; kr < input.RowCount; kr++)
        //    {
        //        row = input.GetRow(kr);
        //        row = vFilter.Apply(row);

        //        for (kc = 0; kc < input.ColumnCount; kc++)
        //        {
        //            x[kr, kc] = row[kc];
        //        }
        //    }

        //    for (kc = 0; kc < input.ColumnCount; kc++)
        //    {
        //        column = input.GetColumn(kc);
        //        column = vFilter.Apply(column);

        //        for (kr = 0; kr < input.RowCount; kr++)
        //        {
        //            x[kr, kc] += column[kr];
        //        }
        //    }

        //    return x;
        //}

        public override STuple<Matrix, Matrix> Apply2(STuple<Matrix, Matrix> input)
        {
            return base.Apply2(input);

            //Matrix inputReal = input.Value1;
            //Matrix inputImag = input.Value2;
            //
            //int width = inputReal.RowCount;
            //
            //if (inputImag == null) { inputImag = new Matrix(inputReal.RowCount, inputReal.ColumnCount); }
            //
            ////output
            //Matrix outputReal = new Matrix(width, width);
            //Matrix outputImaginary = new Matrix(width, width);
            //
            ////temp
            //Matrix tempReal = new Matrix(width, width);
            //Matrix tempImaginary = new Matrix(width, width);
            //
            //int i;
            //int j;
            //int k;
            //
            //Vector rowReal;
            //Vector rowImag;
            //Vector colReal;
            //Vector colImag;
            //
            //Pair<Vector> tempVectors;
            //
            //FourierTransformVectorFilter vFilter = GetVectorFilter();
            //
            //for (i = 0; i < inputReal.RowCount; i++)
            //{
            //    rowReal = inputReal.GetRow(i);
            //    rowImag = inputImag.GetRow(i);
            //
            //    tempVectors = vFilter.Apply2(new Pair<Vector>(rowReal, rowImag));
            //
            //    for (k = 0; k < inputReal.ColumnCount; k++)
            //    {
            //        tempReal[i, k] = tempVectors.Value1[k];
            //        tempImaginary[i, k] = tempVectors.Value2[k];
            //    }
            //}
            //
            //for (j = 0; j < tempReal.ColumnCount; j++)
            //{
            //    colReal = tempReal.GetColumn(j);
            //    colImag = tempImaginary.GetColumn(j);
            //
            //    tempVectors = vFilter.Apply2(new Pair<Vector>(colReal, colImag));
            //
            //    for (k = 0; k < inputReal.RowCount; k++)
            //    {
            //        outputReal[k, j] = tempVectors.Value1[k];
            //        outputImaginary[k, j] = tempVectors.Value2[k];
            //    }
            //}
            //
            //for (i = 0; i < outputReal.RowCount; i++)
            //{
            //    for (j = 0; j < outputReal.ColumnCount; j++)
            //    {
            //        outputReal[i, j] = ScaleForInverse(outputReal[i, j], outputReal.RowCount, outputReal.ColumnCount);
            //        outputImaginary[i, j] = ScaleForInverse(outputImaginary[i, j], outputImaginary.RowCount, outputImaginary.ColumnCount);
            //    }
            //}
            //
            //
            //return new Pair<Matrix>(outputReal, outputImaginary);
        }

        //protected override float ScaleForInverse(float x, int rows, int cols)
        //{
        //    return x / cols;
        //}

        protected override FourierTransformVectorFilter GetVectorFilter()
        {
            return new InverseFourierTransformVectorFilter();
        }

    }
}
