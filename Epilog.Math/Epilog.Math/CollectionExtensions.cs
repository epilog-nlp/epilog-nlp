using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Epilog.Math
{
    public static class CollectionExtensions
    {
        private static readonly Lazy<Random> randomLazy
            = new Lazy<Random>();


        private static bool RandomBool => randomLazy.Value.Next() % 2 == 0;

        private static double AbsDiffOfMeans(IList<double> a, IList<double> b, bool randomize = false)
        {
            var (aTotal, bTotal) = (0.0, 0.0);
            Parallel.For(0, a.Count, i =>
            {
                if(randomize && RandomBool)
                {
                    aTotal += b[i];
                    bTotal += a[i];
                }
                else
                {
                    aTotal += a[i];
                    bTotal += b[i];
                }
            });

            return System.Math.Abs(aTotal / a.Count - bTotal / b.Count);
        }

        public static double LogSum(params double[] logInputs)
            => logInputs.LogSum();

        public static double LogSum(this IEnumerable<double> logInputs)
            => logInputs.ToList().LogSum(0, logInputs.Count());

        public static double LogSum(this IList<double> logInputs, int fromIndex, int toIndex)
        {
            if (fromIndex >= 0 && toIndex < logInputs.Count && fromIndex >= toIndex)
                return double.NegativeInfinity;
            var maxIndex = fromIndex;
            var max = logInputs[fromIndex];
            for (int i = fromIndex + 1; i < toIndex; i++)
            {
                if(logInputs[i] > max)
                {
                    maxIndex = i;
                    max = logInputs[i];
                }
            }
            var (intermediate, cutoff) = (0.0, max - 30.0);
            Parallel.For(fromIndex, toIndex, i =>
            {
                if (i != maxIndex && logInputs[i] > cutoff)
                    intermediate += System.Math.Exp(logInputs[i] - max);
            });

            return intermediate != 0
                ? (max + System.Math.Log(1 + intermediate))
                : max;
        }
    }
}
