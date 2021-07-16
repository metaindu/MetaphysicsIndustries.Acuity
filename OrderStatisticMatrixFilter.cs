
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

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class OrderStatisticMatrixFilter : WindowedMatrixFilter
    {
        public OrderStatisticMatrixFilter(int windowSize)
            : base(windowSize)
        {
            //_windowSize = windowSize;
        }

        //public override Matrix Apply(Matrix input)
        //{
        //    Matrix result = input.CloneSize();

        //    int i;
        //    int j;

        //    for (i = 0; i < input.RowCount; i++)
        //    {
        //        for (j = 0; j < input.ColumnCount; j++)
        //        {
        //            result[i, j] = PerPixelOperation(input, i, j);
        //        }
        //    }

        //    return result;
        //}

        protected override float PerPixelOperation(Matrix input, int row, int column)
        {
            int i;
            int j;
            float value;
            int width = input.ColumnCount;
            int height = input.RowCount;

            int windowRadius = WindowSize / 2; //this will ignore any remainder

            List<float> measures = new List<float>(WindowSize * WindowSize);

            //measures.Clear();

            for (i = 0; i < 2 * windowRadius + 1; i++)
            {
                if (row - windowRadius + i < 0) { continue; }
                if (row - windowRadius + i >= height) { break; }

                for (j = 0; j < 2 * windowRadius + 1; j++)
                {
                    if (column - windowRadius + j < 0) { continue; }
                    if (column - windowRadius + j >= width) { break; }

                    value = input[row - windowRadius + i, column - windowRadius + j];

                    AddValueToMeasures(value, i, j, measures);
                }
            }

            //DoWindowPass(input, row, column, AddValueToMeasures, measures);

            return SelectValueFromMeasures(measures);
        }


        protected virtual float SelectValueFromMeasures(List<float> measures)
        {
            List<float> measures2 = new List<float>(measures);
            measures2.Sort(Compare);
            return SelectValueFromOrderedMeasures(measures2);
        }

        protected abstract float SelectValueFromOrderedMeasures(List<float> measures);

        //protected virtual void AddValueToMeasures(float value, int row, int column, object arg)
        //{
        //    List<float> measures = (List<float>)arg;
        //    AddValueToMeasures(value, row, column, measures);
        //}

        protected virtual void AddValueToMeasures(float value, int row, int column, List<float> measures)
        {
            measures.Add(value);
        }

        protected int Compare(float x, float y)
        {
            return x.CompareTo(y);
        }


    }
}
