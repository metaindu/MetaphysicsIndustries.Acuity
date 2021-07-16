
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
    public class ZetaTrimmedMmseMatrixFilter : MinimalMeanSquareErrorMatrixFilter
    {
        public ZetaTrimmedMmseMatrixFilter(int windowSize, float noiseVariance, float zeta)
            : base(windowSize, noiseVariance)
        {
            _zeta = zeta;
        }

        private float _zeta;
        public float Zeta
        {
            get { return _zeta; }
            set { _zeta = value; }
        }

        public static float CalculateOptimumZtmmseZeta(float impulseProbability)
        {
            float optimumZtmmseZeta;
            int kMax = 128;
            float[] c = new float[kMax];
            c[0] = 1;
            float sum = 0;
            float s = (float)(Math.Sqrt(Math.PI) / 2);
            for (int k = 0; k < kMax; k++)
            {
                int kk = 2 * k + 1;
                float cc = c[k];
                for (int m = 0; m <= k - 1; m++)
                {
                    cc += c[m] * c[k - 1 - m] / ((m + 1) * (2 * m + 1));
                }
                c[k] = cc;

                sum += (float)(cc * Math.Pow(s * (impulseProbability - 1), 2 * k + 1) / (2 * k + 1));
            }
            optimumZtmmseZeta = (float)(sum * -Math.Sqrt(2));
            return optimumZtmmseZeta;
        }

        [Serializable]
        protected class ZtmmseInfo
        {
            public ZtmmseInfo(int windowSize)
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
            float signalMean1;
            float signalVariance1;
            float signalMean2;
            float signalVariance2;
            float noiseVariance;

            //calculate signal mean 1 and signal variance 1

            SignalMeanInfo signalMeanInfo = new SignalMeanInfo();
            DoWindowPass(input, row, column, InternalCalcSignalMean, signalMeanInfo);
            signalMean1 = signalMeanInfo.sum / signalMeanInfo.count;


            SignalVarianceInfo signalVarianceInfo = new SignalVarianceInfo();
            signalVarianceInfo.signalMean = signalMean1;
            DoWindowPass(input, row, column, InternalCalcSignalVariance, signalVarianceInfo);

            signalVariance1 = signalVarianceInfo.sum / (signalVarianceInfo.count - 1);

            //calculate z and eta and signal mean 2

            ZtmmseInfo info1 = new ZtmmseInfo(WindowSize);
            info1.mean = signalMean1;
            info1.stdev = (float)Math.Sqrt(signalVariance1);
            DoWindowPass(input, row, column, CalcEta, info1);

            signalMean2 = info1.sum / info1.eta;

            //calculate signal variance 2
            info1.sum = 0;
            info1.mean = signalMean2;
            DoWindowPass(input, row, column, CalcSignalVariance2, info1);

            signalVariance2 = info1.sum / (info1.eta - 1);

            //calculate noise variance

            noiseVariance = CalculateNoiseVariance();


            return CalculateFinalValue(input, row, column, signalMean2, signalVariance2, noiseVariance);
        }

        protected virtual float CalculateFinalValue(Matrix input, int row, int column, float signalMean2, float signalVariance2, float noiseVariance)
        {

            // impulse rejection stage goes here

            float ratio = noiseVariance / signalVariance2;
            float value = input[row, column];
            //float z = Math.Abs((value - signalMean2) / Math.Sqrt(signalVariance2));

            //if (z > Zeta)
            //{
            //    List<float> measures = new List<float>(WindowSize * WindowSize);
            //    int trimLeft = 2;
            //    int trimRight = 2;

            //    DoWindowPass(input, row, column, 3, AddValueToMeasures, measures);
            //    measures.Sort(Compare);
            //    if (measures.Count > trimLeft)
            //    {
            //        measures.RemoveRange(0, trimLeft);
            //    }
            //    if (measures.Count > trimRight)
            //    {
            //        measures.RemoveRange(measures.Count - trimRight, trimRight);
            //    }

            //    if (measures.Count > 0)
            //    {
            //        float sum = 0;
            //        foreach (float value2 in measures)
            //        {
            //            sum += value2;
            //        }
            //        value = sum / measures.Count;
            //    }
            //    else
            //    {
            //        return signalMean2;
            //    }
            //}

            return CalculateFinalValue(value, signalMean2, ratio);
        }

        protected virtual void CalcEta(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, ZtmmseInfo info)
        {
            float z = Math.Abs((value - info.mean) / info.stdev);
            info.z[rowWithinWindow, columnWithinWindow] = z;

            if (z <= Zeta)
            {
                info.eta++;
                info.sum += value;
            }
        }

        protected virtual void CalcSignalVariance2(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, ZtmmseInfo info)
        {
            float z = info.z[rowWithinWindow, columnWithinWindow];

            if (z <= Zeta)
            {
                value -= info.mean;
                value *= value;
                info.sum += value;
            }
        }

        //protected virtual void AddValueToMeasures(float value, int row, int column, int rowWithinWindow, int columnWithinWindow, List<float> measures)
        //{
        //    measures.Add(value);
        //}

        //public static int Compare(float x, float y)
        //{
        //    return x.CompareTo(y);
        //}
    }
}
