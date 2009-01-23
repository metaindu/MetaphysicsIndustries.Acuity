using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class MmseEdgeDetectionMatrixfilter : MinimalMeanSquareErrorMatrixFilter
    {
        public MmseEdgeDetectionMatrixfilter(int windowSize, float noiseVariance)
            : this(windowSize, noiseVariance, 1)
        {
        }

        public MmseEdgeDetectionMatrixfilter(int windowSize, float noiseVariance, float gamma)
            : base(windowSize, noiseVariance)
        {
            _gamma = gamma;
        }

        private float  _gamma;
        public float  Gamma
        {
            get { return _gamma; }
        }


        //private float _noiseVariance;

        //protected class SignalMeanInfo
        //{
        //    public float sum = 0;
        //    public int count = 0;
        //}

        //protected class SignalVarianceInfo
        //{
        //    public float sum = 0;
        //    public int count = 0;
        //    public float signalMean = 0;
        //}

        //protected override float PerPixelOperation(Matrix input, int row, int column)
        //{
        //    float signalMean;
        //    float signalVariance;
        //    float noiseVariance;

        //    signalMean = CalculateSignalMean(input, row, column);

        //    signalVariance = CalculateSignalVariance(input, row, column, signalMean);

        //    noiseVariance = CalculateNoiseVariance();

        //    float ratio = noiseVariance / signalVariance;

        //    return CalculateFinalValue(input, row, column, signalMean, ratio);
        //}

        protected override float CalculateFinalValue(Matrix input, int row, int column, float signalMean, float ratio)
        {
            return (float)(1 - Math.Pow(ratio, Gamma));// *input[row, column] + ratio * signalMean;
        }

        //protected virtual float CalculateNoiseVariance()
        //{
        //    return _noiseVariance;
        //}

        //protected virtual float CalculateSignalVariance(Matrix input, int row, int column, float signalMean)
        //{
        //    float signalVariance;
        //    SignalVarianceInfo signalVarianceInfo = new SignalVarianceInfo();
        //    signalVarianceInfo.signalMean = signalMean;
        //    DoWindowPass(input, row, column, InternalCalcSignalVariance, signalVarianceInfo);

        //    signalVariance = signalVarianceInfo.sum / (signalVarianceInfo.count - 1);
        //    return signalVariance;
        //}

        //protected virtual float CalculateSignalMean(Matrix input, int row, int column)
        //{
        //    float signalMean;
        //    SignalMeanInfo signalMeanInfo = new SignalMeanInfo();
        //    DoWindowPass(input, row, column, InternalCalcSignalMean, signalMeanInfo);

        //    //signalMean = sum / count;
        //    signalMean = signalMeanInfo.sum / signalMeanInfo.count;
        //    return signalMean;
        //}

        //protected virtual void InternalCalcSignalMean(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalMeanInfo signalMeanInfo)
        //{
        //    signalMeanInfo.sum += value;
        //    signalMeanInfo.count++;
        //}
        //protected virtual void InternalCalcSignalVariance(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalVarianceInfo signalVarianceInfo)
        //{
        //    value -= signalVarianceInfo.signalMean;
        //    signalVarianceInfo.sum += value * value;
        //    signalVarianceInfo.count++;
        //}
    }
}
