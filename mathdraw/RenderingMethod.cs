using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathdraw
{
    public enum RenderingMethod
    {
        LINEAR,
        QUADRATIC,

        SINE,
        COSINE,
        TANGENT,

        MANDELBROT_SET,
        JULIA_SET
    }
    static class Constants
    {
        public const double PI = 3.1415926536;
    }
}
