using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class WeightedPMatrixFilter : OrderStatisticMatrixFilter
    {
        public WeightedPMatrixFilter(float pFactor, Matrix weights)
            : base(Math.Min(weights.RowCount, weights.ColumnCount))
        {
            //_operator = new PFactorOperator(pFactor, weights);
            _pFactor = Math.Max(0, Math.Min(1, pFactor));
            _weights = weights.Clone();
        }

        //protected class PFactorOperator : IPerPixelOperator
        //{
        //    public PFactorOperator(float pFactor, Matrix weights)
        //    {
        //        int i;
        //        int j;
        //        float w;

        //        _weights = new float[weights.RowCount, weights.ColumnCount];
        //        _weightTotal = 0;
        //        for (i = 0; i < weights.RowCount; i++)
        //        {
        //            for (j = 0; j < weights.ColumnCount; j++)
        //            {
        //                w = weights[i, j];
        //                _weightTotal += w;
        //                _weights[i, j] = w;
        //            }
        //        }

        //        _radius = (int)(Math.Min(weights.RowCount, weights.ColumnCount) / 2.0);

        //        _pFactor = Math.Max(0, Math.Min(1, pFactor));

        //        _measures = new List<float>(Math.Max((int)Math.Ceiling(_weightTotal), weights.Count));
        //    }

        //    List<float> _measures;
        //    private int _radius;
        //    private float[,] _weights;
        //    private float _weightTotal;
        //    private float[,] _values;
        //    private float _pFactor;
        //    public int GetExtraWidth(Matrix input) { return 0; }
        //    public int GetExtraHeight(Matrix input) { return 0; }
        //    public void SetValues(float[,] values)
        //    {
        //        _values = values;
        //    }

        //    public float Operate(int row, int column)
        //    {
        //        int i;
        //        int j;
        //        int x;
        //        float value;
        //        float weight;
        //        int width = _values.GetLength(0);
        //        int height = _values.GetLength(1);

        //        _measures.Clear();

        //        for (i = 0; i < 2 * _radius + 1; i++)
        //        {
        //            if (row - _radius + i < 0) { continue; }
        //            if (row - _radius + i >= width) { break; }

        //            for (j = 0; j < 2 * _radius + 1; j++)
        //            {
        //                if (column - _radius + j < 0) { continue; }
        //                if (column - _radius + j >= height) { break; }

        //                value = _values[row - _radius + i, column - _radius + j];
        //                weight = _weights[i, j];

        //                if (weight < 0)
        //                {
        //                    weight = -weight;
        //                    value = -value;
        //                }

        //                for (x = 0; x < weight; x++)
        //                {
        //                    _measures.Add(value);
        //                }
        //            }
        //        }

        //        _measures.Sort(Compare);

        //        int index = (int)Math.Ceiling((_measures.Count - 1) * _pFactor);
        //        return _measures[index];
        //    }

        //    protected int Compare(float x, float y)
        //    {
        //        return x.CompareTo(y);
        //    }

        //}

        //private PFactorOperator _operator;

        //public override Matrix Apply(Matrix input)
        //{
        //    return input.PerPixelOperation(_operator);
        //}

        private float _pFactor;
        private Matrix _weights;

        protected override void AddValueToMeasures(float value, int row, int column, List<float> measures)
        {
            int x;
            float weight;

            weight = _weights[row, column];

            if (weight < 0)
            {
                weight = -weight;
                value = -value;
            }

            for (x = 0; x < weight; x++)
            {
                measures.Add(value);
            }
        }

        protected override float SelectValueFromOrderedMeasures(List<float> measures)
        {
            int index = (int)Math.Ceiling((measures.Count - 1) * _pFactor);
            return measures[index];
        }

    }
}
