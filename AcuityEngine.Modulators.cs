
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


/*****************************************************************************
 *                                                                           *
 *  AcuityEngine.cs                                                          *
 *                                                                           *
 *  Methods for doing general calculations.                                  *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

using System.Diagnostics;
using MetaphysicsIndustries.Solus;

namespace MetaphysicsIndustries.Acuity
{
    public partial class AcuityEngine
    {
        public static float Convert24gToFloat(float value)
        {
            value = Math.Max(0, Math.Min(0xFFFFFF, value));
            float r = ((int)(value) & 0x000000FF) / 255.0f;
            float g = (((int)(value) & 0x0000FF00) >> 8) / 255.0f;
            float b = (((int)(value) & 0x00FF0000) >> 16) / 255.0f;
            return (r + g + b) / 3;
        }

        public static float ConvertFloatTo24g(float value)
        {
            value = Math.Max(0, Math.Min(1, value));

            int b = (int)(value * 255) & 0xFF;
            int g = b << 8;
            int r = g << 8;

            return r | g | b;
        }

        public static float Convert24cTo24g(float value)
        {
            return ConvertFloatTo24g(Convert24gToFloat(value));
        }

        public static float ConvertRgbTo24cTriModulator(float r, float g, float b)
        {
            r = Math.Max(0, Math.Min(1, r));
            g = Math.Max(0, Math.Min(1, g));
            b = Math.Max(0, Math.Min(1, b));

            int rr = ((int)(r * 255) & 0xFF)<<16;
            int gg = ((int)(g * 255) & 0xFF)<<8;
            int bb = (int)(b * 255) & 0xFF;

            return rr | gg | bb;
        }

        public class MultiplyModulator
        {
            public MultiplyModulator(float factor) { _factor = factor; }
            private float _factor;
            public float Modulate(float value) { return value * _factor; }
        }

        public class AdditionModulator
        {
            public AdditionModulator(float factor) { _factor = factor; }
            private float _factor;
            public float Modulate(float value) { return value + _factor; }
        }

        public class MaximumModulator
        {
            public MaximumModulator(float factor) { _factor = factor; }
            private float _factor;
            public float Modulate(float value) { return Math.Max(value, _factor); }
        }

        public class MinimumModulator
        {
            public MinimumModulator(float factor) { _factor = factor; }
            private float _factor;
            public float Modulate(float value) { return Math.Min(value, _factor); }
        }

        public static float AdditionBiMod(float x, float y)
        {
            return x + y;
        }

        public static float MultiplicationBiMod(float x, float y)
        {
            return x * y;
        }

        public static float ConvertNegOneOneToZeroOne(float x)
        {
            //convert a number on the interval of [-1,1] to [0,1]
            return (x + 1) / 2;
        }

        public static float ConvertZeroOneToNegOneOne(float x)
        {
            //convert a number on the interval of [0,1] to [-1,1]
            return x * 2 - 1;
        }

        public static STuple<float, float> ConvertNegOneOneToZeroOne(float x, float y)
        {
            return new STuple<float, float>(
                ConvertNegOneOneToZeroOne(x),
                ConvertNegOneOneToZeroOne(y));
        }

        public static STuple<float, float> ConvertZeroOneToNegOneOne(float x, float y)
        {
            return new STuple<float, float>(
                ConvertZeroOneToNegOneOne(x),
                ConvertZeroOneToNegOneOne(y));
        }

        public static STuple<float, float> ConvertEuclideanToPolar(float x, float y)
        {
            STuple<float, float> pair = new STuple<float, float>();
            pair.Value1 = (float)Math.Sqrt(x * x + y * y);
            pair.Value2 = (float)Math.Atan2(y, x);
            return pair;
        }

        public static STuple<float, float> ConvertPolarToEuclidean(float r, float theta)
        {
            STuple<float, float> pair = new STuple<float, float>();
            pair.Value1 = (float)(r * Math.Cos(theta));
            pair.Value2 = (float)(r * Math.Sin(theta));
            return pair;
        }

        public static float ConvertDegreesToRadians(float degrees)
        {
            return (float)(Math.PI * degrees / 180.0);
        }

        public static float ConvertRadiansToDegrees(float radians)
        {
            return (float)(180.0 * radians / Math.PI);
        }

        public static float ComplexMagnitude(float real, float imaginary)
        {
            return (float)Math.Sqrt(real * real + imaginary * imaginary);
        }

        public static float ComplexPhase(float real, float imaginary)
        {
            return (float)Math.Atan2(imaginary, real);
        }

        public static float IntervalFit(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        public static float MeanSquareError(Matrix a, Matrix b)
        {
            if (a == null) { throw new ArgumentNullException("a"); }
            if (b == null) { throw new ArgumentNullException("b"); }
            if (a.RowCount != b.RowCount ||
                a.ColumnCount != b.ColumnCount)
            {
                throw new ArgumentException("Matrix sizes do not match", "matrixToCompare");
            }

            int i;
            int j;

            float sum = 0;
            float v;

            for (i = 0; i < a.RowCount; i++)
            {
                for (j = 0; j < a.ColumnCount; j++)
                {
                    v = a[i, j] - b[i, j];
                    sum += v * v;
                }
            }

            sum /= a.RowCount;
            sum /= a.ColumnCount;

            return sum;
        }

        public static float MaxError(Matrix a, Matrix b)
        {
            if (a == null) { throw new ArgumentNullException("a"); }
            if (b == null) { throw new ArgumentNullException("b"); }
            if (a.RowCount != b.RowCount ||
                a.ColumnCount != b.ColumnCount)
            {
                throw new ArgumentException("Matrix sizes do not match", "matrixToCompare");
            }

            int i;
            int j;

            float max = 0;

            for (i = 0; i < a.RowCount; i++)
            {
                for (j = 0; j < a.ColumnCount; j++)
                {
                    max = Math.Max(Math.Abs(a[i, j] - b[i, j]), max);
                }
            }

            return max;
        }

        public static STuple<float, float, float> ConvertRgbToHsl(STuple<float, float, float> rgb)
        {
            float r = rgb.Value1;
            float g = rgb.Value2;
            float b = rgb.Value3;

            float h;
            float s;
            float l;

            ConvertRgbToHsl(r, g, b, out h, out s, out l);

            return new STuple<float, float, float>(h, s, l);
        }

        public static void ConvertRgbToHsl(float r, float g, float b, out float h, out float s, out float l)
        {
            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));

            if (max == min) { h = 0; }
            else if (max == r)
            {
                h = (g - b) / 6.0f;
                if (g < b) { h += 1; }
            }
            else if (max == g) { h = (b - r + 2) / 6.0f; }
            else { h = (r - g + 4) / 6.0f; }

            l = (max + min) / 2;

            if (max == min) { s = 0; }
            else if (l <= 0.5f) { s = (max - min) / (2 * l); }
            else { s = (max - min) / (2 - 2 * l); }
        }

        public static STuple<float, float, float> ConvertHslToRgb(STuple<float, float, float> hsl)
        {
            float h = hsl.Value1;
            float s = hsl.Value2;
            float l = hsl.Value3;

            float r;
            float g;
            float b;

            ConvertHslToRgb(h, s, l, out r, out g, out b);

            return new STuple<float, float, float>(r, g, b);
        }

        public static void ConvertHslToRgb(float h, float s, float l, out float r, out float g, out float b)
        {
            float q;
            float p;
            float tr = h + 1 / 3.0f;
            float tg = h;
            float tb = h - 1 / 3.0f;

            if (tr > 1) { tr -= 1; }
            if (tb < 1) { tb += 1; }

            if (l < 0.5) { q = l * (1 + s); }
            else { q = l + s - l * s; }

            p = 2 * l - q;

            r = CalcHslToRgbConversion(q, p, tr);
            g = CalcHslToRgbConversion(q, p, tg);
            b = CalcHslToRgbConversion(q, p, tb);
        }

        public static float CalcHslToRgbConversion(float q, float p, float t)
        {
            float c;
            if (t < 1 / 6.0f) { c = q + ((q - p) * 6 * t); }
            else if (t < 0.5f) { c = q; }
            else if (t < 2 / 3.0f) { c = p + ((q - p) * 6 * ((2 / 3.0f) - t)); }
            else { c = p; }
            return c;
        }
    }
}
