using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    //public delegate float ErrorFunction<T>(T a, T b)
    //    where T : Tensor;

    public delegate float ErrorFunction(Matrix a, Matrix b);
    //public delegate float ErrorFunction(Vector a, Vector b);
}
