using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class RotateCoordinatesMatrixFilter : PolarCoordinateTransformMatrixFilter
    {
        public RotateCoordinatesMatrixFilter(float angle)
            //: base(this.Modulate)
        {
            _angle = angle;
        }

        float _angle;
        public virtual float Angle
        {
            get { return _angle; }
        }

        //protected override Pair<float> Modulate(float x, float y)
        //{
        //    x = SolusEngine.ConvertZeroOneToNegOneOne(x);
        //    y = SolusEngine.ConvertZeroOneToNegOneOne(y);
        //
        //    float r = Math.Sqrt(x * x + y * y);
        //    float theta = Math.Atan2(y, x);
        //
        //    theta += _angle;
        //
        //    x = r * Math.Cos(theta);
        //    y = r * Math.Sin(theta);
        //
        //    x = SolusEngine.ConvertNegOneOneToZeroOne(x);
        //    y = SolusEngine.ConvertNegOneOneToZeroOne(y);
        //
        //    return new Pair<float>(x, y);
        //}

        protected override STuple<float, float> InternalModulate2(STuple<float, float> pair)
        {
            pair.Value2 += Angle;
            return pair;
        }
    }
}
