using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class FourierTransformVectorFilter : VectorFilter
    {
        protected static Dictionary<int, float[,]> CosineLookupTables = new Dictionary<int, float[,]>();
        protected static Dictionary<int, float[,]> SineLookupTables = new Dictionary<int, float[,]>();

        protected static readonly int LookupTableMaxLength = 2048;

        protected void SetupLookupTables(int length)
        {
            if (length > LookupTableMaxLength)
            {
                return;
            }

            if (!CosineLookupTables.ContainsKey(length))
            {
                float[,] cc = new float[length, length];
                float[,] ss = new float[length, length];

                CosineLookupTables[length] = cc;
                SineLookupTables[length] = ss;

                int k;
                int n;
                float twoPiDivLength = (float)(2 * Math.PI / length);
                for (k = 0; k < length; k++)
                {
                    float s = k * twoPiDivLength;
                    for (n = 0; n < length; n++)
                    {
                        cc[k, n] = (float)Math.Cos(s * n);
                        ss[k, n] = (float)Math.Sin(s * n);
                    }
                }
            }
        }

        protected virtual bool IsInverse
        {
            get { return false; }
        }

        public override Vector Apply(Vector input)
        {
            Pair<Vector> output = Apply2(input);
            int i;
            for (i = 0; i < input.Length; i++)
            {
                output.First[i] =
                    (float)Math.Sqrt(
                        output.First[i] * output.First[i] +
                        output.Second[i] * output.Second[i]);
            }

            return output.First;
        }

        protected virtual float ScaleForInverse(float x, int length)
        {
            return x;
        }

        public virtual Pair<Vector> Apply2(Vector input)
        {
            return Apply2(new Pair<Vector>(input, null));
        }

        public virtual Pair<Vector> Apply2(Pair<Vector> input)
        {
            Vector inputReal2 = input.First;
            Vector inputImag2 = input.Second;

            if (inputImag2 == null) { inputImag2 = new Vector(inputReal2.Length); }

            Vector outputReal2 = new Vector(inputReal2.Length);
            Vector outputImaginary2 = new Vector(inputImag2.Length);

            IArrayReadable<float> inputReal = inputReal2;
            IArrayReadable<float> inputImag = inputImag2;
            IArrayWriteable<float> outputReal = outputReal2;
            IArrayWriteable<float> outputImaginary = outputImaginary2;

            dft(inputReal, inputImag, outputReal, outputImaginary);

            return new Pair<Vector>(outputReal2, outputImaginary2);
        }

        public void dft(IArrayReadable<float> inputReal, IArrayReadable<float> inputImag, IArrayWriteable<float> outputReal, IArrayWriteable<float> outputImaginary)
        {
            //Xk = sigma(n=0,N-1,xn*e^(-2pi*i*n*k/N)
            //e^(i*theta) = cos(theta) + i*sin(theta)

            int length = inputReal.Length;

            int k;
            int n;
            float real;
            float imag;
            float theta;

            float twoPiDivLength = (float)(2 * Math.PI / length);
            float scale;
            float valueReal;
            float valueImag;
            float ca;
            float sa;

            SetupLookupTables(length);
            float[,] cc = null;
            float[,] ss = null;
            if (CosineLookupTables.ContainsKey(length))
            {
                cc = CosineLookupTables[length];
                ss = SineLookupTables[length];
            }

            for (k = 0; k < length; k++)
            {
                real = 0;
                imag = 0;

                if (cc == null)
                {
                    scale = k * twoPiDivLength * (IsInverse ? -1 : 1);
                    for (n = 0; n < length; n++)
                    {
                        theta = n * scale;
                        ca = (float)Math.Cos(theta);
                        sa = (float)Math.Sin(theta);

                        valueReal = inputReal[n];
                        valueImag = inputImag[n];
                        real += valueReal * ca - valueImag * sa;
                        imag += valueReal * sa + valueImag * ca;
                    }
                }
                else if (IsInverse)
                {
                    for (n = 0; n < length; n++)
                    {
                        ca = cc[k, n];
                        sa = -ss[k, n];

                        valueReal = inputReal[n];
                        valueImag = inputImag[n];
                        real += valueReal * ca - valueImag * sa;
                        imag += valueReal * sa + valueImag * ca;
                    }
                }
                else
                {
                    for (n = 0; n < length; n++)
                    {
                        ca = cc[k, n];
                        sa = ss[k, n];

                        valueReal = inputReal[n];
                        valueImag = inputImag[n];
                        real += valueReal * ca - valueImag * sa;
                        imag += valueReal * sa + valueImag * ca;
                    }
                }

                outputReal[k] = ScaleForInverse(real, length);
                outputImaginary[k] = ScaleForInverse(imag, length);
            }
        }
    }
}