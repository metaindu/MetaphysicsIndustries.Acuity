using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class MedianVectorFilter : VectorFilter
    {
        public MedianVectorFilter(int width)
        {
            _width = width;
        }

        private int _width;
        public int Width
        {
            get { return _width; }
        }

        public override Vector Apply(Vector input)
        {
            throw new NotImplementedException();
            //return null;
        }
    }
}
