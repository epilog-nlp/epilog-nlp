using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Collections
{
    using Math;

    public static class CounterExtensions
    {
        private static readonly double LogE2 = System.Math.Log(2.0);

        public static double LogSum<TKey>(ICounter<TKey> counter)
            => counter.Values.LogSum();

        public static void LogNormalizeInPlace<TKey>(ICounter<TKey> counter)
        {
            var logSum = LogSum(counter);
            foreach(var key in counter.Keys)
                counter[key] -= logSum;
        }
    }
}
