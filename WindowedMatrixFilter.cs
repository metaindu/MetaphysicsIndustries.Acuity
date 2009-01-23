using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class WindowedMatrixFilter : MatrixFilter
    {
        public WindowedMatrixFilter(int windowSize)
        {
            _windowSize = windowSize;
        }

        private int _windowSize;
        public int WindowSize
        {
            get { return _windowSize; }
        }


        public override Matrix Apply(Matrix input)
        {
            Matrix result = input.CloneSize();

            int i;
            int j;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    result[i, j] = PerPixelOperation(input, i, j);
                }
            }

            return result;
        }

        protected abstract float PerPixelOperation(Matrix input, int row, int column);

        //protected void DoWindowPass<T>(Matrix input, int row, int column, PixelPass<T> passFunc)
        //{
        //    DoWindowPass(input, row, column, passFunc, default(T));
        //}

        protected void DoWindowPass<T>(Matrix input, int row, int column, PixelPass<T> passFunc, T arg)
        {
            DoWindowPass(input, row, column, WindowSize, passFunc, arg);
        }

        protected void DoWindowPass<T>(Matrix input, int row, int column, int localWindowSize, PixelPass<T> passFunc, T arg)
        {
            int i;
            int j;
            float value;
            int width = input.ColumnCount;
            int height = input.RowCount;

            int windowRadius = localWindowSize / 2; //this will ignore any remainder
            int row2;
            int column2;

            for (i = 0; i < 2 * windowRadius + 1; i++)
            {
                if (row - windowRadius + i < 0) { continue; }
                if (row - windowRadius + i >= height) { break; }

                row2 = row - windowRadius + i;

                for (j = 0; j < 2 * windowRadius + 1; j++)
                {
                    if (column - windowRadius + j < 0) { continue; }
                    if (column - windowRadius + j >= width) { break; }

                    column2 = column - windowRadius + j;

                    value = input[row2, column2];

                    DoPixelPass<T>(passFunc, value, row2, column2, i, j, arg);
                }
            }
        }

        protected void DoPixelPass<T>(PixelPass<T> passFunc, float value, int pixelRow, int pixelColumn, int rowWithinWindow, int columnWithinWindow, T arg)
        {
            passFunc(value, pixelRow, pixelColumn, rowWithinWindow, columnWithinWindow, arg);
        }

        //protected void DoPixelPass(PixelPass passFunc, float value, int pixelRow, int PixelColumn)
        //{
        //    passFunc(value, pixelRow, PixelColumn);
        //}

        //protected delegate void PixelPass(float value, int pixelRow, int pixelColumn);
        protected delegate void PixelPass<T>(float value, int pixelRow, int pixelColumn, int rowWithinWindow, int columnWithinWindow, T arg);
    }
}
