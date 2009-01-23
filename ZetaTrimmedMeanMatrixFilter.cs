using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class ZetaTrimmedMeanMatrixFilter : WindowedMatrixFilter
    {
        public ZetaTrimmedMeanMatrixFilter(int windowSize, float zeta)
            : base(windowSize)
        {
            _zeta = zeta;
        }

        private float _zeta;

        [Serializable]
        protected class SignalMeanInfo
        {
            public float sum = 0;
            public int count = 0;
        }

        [Serializable]
        protected class SignalVarianceInfo
        {
            public float sum = 0;
            public int count = 0;
            public float signalMean = 0;
        }

        [Serializable]
        protected class ZtmInfo
        {
            public ZtmInfo(int windowSize)
            {
                z = new float[windowSize, windowSize];
            }

            public float mean = 0;
            public float stdev = 0;
            public float[,] z;
            public float sum = 0;
            public int eta = 0;
        }

        protected override float PerPixelOperation(Matrix input, int row, int column)
        {
            float signalMean;
            float signalVariance;
            float trimmedMean;

            //calculate signal mean 1 and signal variance 1
            SignalMeanInfo signalMeanInfo = new SignalMeanInfo();
            DoWindowPass(input, row, column, CalculateSignalMean, signalMeanInfo);
            signalMean = signalMeanInfo.sum / signalMeanInfo.count;

            SignalVarianceInfo signalVarianceInfo = new SignalVarianceInfo();
            signalVarianceInfo.signalMean = signalMean;
            DoWindowPass(input, row, column, CalculateSignalVariance, signalVarianceInfo);
            signalVariance = signalVarianceInfo.sum / (signalVarianceInfo.count - 1);

            //calculate z and eta and trimmed mean
            ZtmInfo ztmInfo = new ZtmInfo(WindowSize);
            ztmInfo.mean = signalMean;
            ztmInfo.stdev = (float)Math.Sqrt(signalVariance);
            DoWindowPass(input, row, column, CalcTrimmedMean, ztmInfo);
            trimmedMean = ztmInfo.sum / ztmInfo.eta;

            return trimmedMean;
        }

        protected virtual void CalcTrimmedMean(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, ZtmInfo info)
        {
            float z = Math.Abs((value - info.mean) / info.stdev);
            info.z[rowWithinWindow, columnWithinWindow] = z;

            if (z <= _zeta)
            {
                info.eta++;
                info.sum += value;
            }
        }

        //protected virtual void CalcSignalVariance2(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, ZtmmseInfo info)
        //{
        //    float z = info.z[rowWithinWindow, columnWithinWindow];
        //
        //    if (z <= Zeta)
        //    {
        //        value -= info.mean;
        //        value *= value;
        //        info.sum += value;
        //    }
        //}

        //protected virtual void AddValueToMeasures(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, List<float> measures)
        //{
        //    measures.Add(value);
        //}

        //protected int Compare(float x, float y)
        //{
        //    return x.CompareTo(y);
        //}

        protected virtual void CalculateSignalMean(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalMeanInfo signalMeanInfo)
        {
            signalMeanInfo.sum += value;
            signalMeanInfo.count++;
        }
        protected virtual void CalculateSignalVariance(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalVarianceInfo signalVarianceInfo)
        {
            value -= signalVarianceInfo.signalMean;
            signalVarianceInfo.sum += value * value;
            signalVarianceInfo.count++;
        }

    }
}
