using System.Collections.Generic;
using System;

namespace Epilog.Math
{
    public static class Extensions
    {
        private static readonly Lazy<IList<double>> logGammaCoefficients
            = new Lazy<IList<double>>(() => new List<double> 
            {
                76.18009172947146, 
                -86.50532032941677,
                24.01409824083091,
                -1.231739572450155,
                0.1208650973866179e-2,
                -0.5395239384953e-5 
            });

        const double LogTolerance = 30.0;

        const float LogToleranceFloat = 20.0f;

        public static double Round(this double x)
            => System.Math.Floor(x + 0.5);

        public static double Round(this double x, int precision)
        {
            var pow = System.Math.Pow(10.0, precision);
            return Round(x * pow) / pow;
        }

        public static int Max(int a, int b, int c)
            => System.Math.Max(System.Math.Max(a, b), c);

        public static int Min(int a, int b, int c)
            => System.Math.Min(System.Math.Min(a, b), c);

        public static (int min, int max) MinMax(int a, int b)
            => a < b
            ? (a, b)
            : (b, a);

        public static (float min, float max) MinMax(float a, float b)
            => a < b
            ? (a, b)
            : (b, a);

        public static (double min, double max) MinMax(double a, double b)
            => a < b
            ? (a, b)
            : (b, a);

        /// <summary>
        /// Returns Modulus with sign matching the provided <paramref name="mod"/>.
        /// </summary>
        /// <param name="num">The left operand.</param>
        /// <param name="mod">The right operand.</param>
        /// <returns>The Modulus with sign matching the provided <paramref name="mod"/>.</returns>
        public static int SignedMod(this int num, int mod)
            => (num % mod + mod) % mod;

        /// <summary>
        /// Approximates the log of the Gamma function of <paramref name="x"/>.
        /// </summary>
        /// <remarks>
        /// Laczos Approximation
        /// Reference: Numerical Recipes in C
        /// http://www.library.cornell.edu/nr/cbookcpdf.html
        /// From www.cs.berkeley.edu/~milch/blog/versions/blog-0.1.3/blog/distrib
        /// </remarks>
        /// <param name="x">Value to calculate.</param>
        /// <returns>The approximate Log of the Gamma function of <paramref name="x"/>.</returns>
        public static double LogGamma(this double x)
        {
            var xxx = x;
            var tmp = x + 5.5 - ((x + 0.5) * System.Math.Log(x + 5.5));
            var ser = 1.000000000190015;
            foreach(var coEfficient in logGammaCoefficients.Value)
            {
                xxx++;
                ser += coEfficient / xxx;
            }
            return -tmp + System.Math.Log(2.5066282746310005 * ser / x);
        }

        /// <summary>
        /// Approximates the Gamma function for <paramref name="n"/>. 
        /// </summary>
        /// <remarks>
        /// See http://www.rskey.org/CMS/index.php/the-library/11.
        /// </remarks>
        /// <param name="n">Value to calculate.</param>
        /// <returns>The Gamma of <paramref name="n"/>.</returns>
        public static double Gamma(this double n)
            => System.Math.Sqrt(2.0 * System.Math.PI / n) * System.Math.Pow(n / System.Math.E * System.Math.Sqrt(n * System.Math.Sinh((1.0 / n) + (1 / (810 * System.Math.Pow(n, 6))))), n);

        /// <summary>
        /// The log of the sum of two numbers.
        /// </summary>
        /// <param name="lx">First number, in log form.</param>
        /// <param name="ly">Second number, in log form.</param>
        /// <returns><c>System.Math.Log(System.Math.Exp(lx) + System.Math.Exp(ly));</c></returns>
        public static float LogAdd(this float lx, float ly)
            => (float)System.Math.Log(System.Math.Exp(lx) + System.Math.Exp(ly));

        /// <summary>
        /// The log of the sum of two numbers.
        /// </summary>
        /// <param name="lx">First number, in log form.</param>
        /// <param name="ly">Second number, in log form.</param>
        /// <returns>System.Math.Log(System.Math.Exp(lx) + System.Math.Exp(ly));</returns>
        public static double LogAdd(this double lx, double ly)
            => System.Math.Log(System.Math.Exp(lx) + System.Math.Exp(ly));


        public static int NChooseK(this int n, int k)
        {
            k = System.Math.Min(k, n - k);
            if (k == 0)
                return 1;
            var accum = n;
            for (int i = 1; i < k; i++)
                accum = accum * (n - i) / i;
            return accum / k;
        }


    }
}
