
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
using MetaphysicsIndustries.Solus;


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class Vector : Tensor, IArray<float>
    {
        public static Vector FromUniformSequence(float value, int length)
        {
            Vector ret = new Vector(length);

            int i;
            for (i = 0; i < length; i++)
            {
                ret[i] = value;
            }

            return ret;
        }

        public Vector(int length)
        {
            _length = length;
            _array = new float[_length];

            int i;

            for (i = 0; i < length; i++)
            {
                _array[i] = 0;
            }
        }

        public Vector(int length, params float[] initialContents)
            : this(length)
        {
            int i;
            int j = Math.Min(length, initialContents.Length);
            for (i = 0; i < j; i++)
            {
                _array[i] = initialContents[i];
            }
        }

        public Vector Clone()
        {
            return new Vector(Length, _array);
        }

        private float[] _array;
        private int _length;
        public int Length
        {
            get { return _length; }
        }

        public float this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) { throw new IndexOutOfRangeException("index"); }

                return _array[index];
            }
            set
            {
                if (index < 0 || index >= Length) { throw new IndexOutOfRangeException("index"); }

                _array[index] = value;
            }
        }

        #region IEnumerable<float> Members

        public override IEnumerator<float> GetEnumerator()
        {
            IList<float> list = _array;
            return list.GetEnumerator();
        }

        #endregion

        //methods and overloaded operators

        public Vector Convolution(Vector convolvee)
        {
            return AdvancedConvolution(convolvee, MultiplicationOperation.Value, AdditionOperation.Value);
        }

        public Vector AdvancedConvolution(Vector convolvee, Operation Value1Op, AssociativeCommutativeOperation Value2Op)
        {
            int r = Length + convolvee.Length - 1;

            Vector ret = new Vector(r);

            List<float> group = new List<float>();

            int n;
            int k;
            float term;
            for (n = 0; n < r; n++)
            {
                term = 0;

                for (k = 0; k < Length; k++)
                {
                    if (n - k < 0) { break; }
                    if (n - k >= convolvee.Length) { continue; }

                    term += this[k] * convolvee[n - k];
                }

                ret[n] = term;
            }

            return ret;
        }

        public override void ApplyToAll(Modulator mod)
        {
            int i;
            for (i = 0; i < Length; i++)
            {
                this[i] = mod(this[i]);
            }
        }

        public Vector GetSlice(int startIndex, int length)
        {
            Vector ret = new Vector(length);

            int i;
            int j = Math.Min(length, Length - startIndex);
            for (i = 0; i < j; i++)
            {
                ret[i] = this[i + startIndex];
            }

            return ret;
        }
    }
}
