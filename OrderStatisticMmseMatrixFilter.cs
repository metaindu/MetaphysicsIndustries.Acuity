using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class OrderStatisticMmseMatrixFilter : MinimalMeanSquareErrorMatrixFilter
    {
        public OrderStatisticMmseMatrixFilter(int windowSize, float noiseVariance)
            : base(windowSize, noiseVariance)
        {
        }

        protected override float CalculateSignalMean(Matrix input, int row, int column)
        {
            List<float> measures = new List<float>(WindowSize * WindowSize);

            DoWindowPass(input, row, column, AddValueToMeasures, measures);

            return SelectValueFromMeasures(measures);
        }

        protected virtual float SelectValueFromMeasures(List<float> measures)
        {
            List<float> measures2 = new List<float>(measures);
            measures2.Sort(Compare);
            return SelectValueFromOrderedMeasures(measures);
        }

        protected abstract float SelectValueFromOrderedMeasures(List<float> measures);
    }
}
