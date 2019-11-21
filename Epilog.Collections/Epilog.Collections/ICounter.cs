using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICounter<TKey> : IDictionary<TKey, double>
    {
        double DefaultReturnValue { get; set; }

        double IncrementCount(TKey key, double value = 1.0);

        double DecrementCount(TKey key, double value = -1.0);

        double LogIncrementCount(TKey key, double value);

        void AddAll(ICounter<TKey> counters);

        double TotalCount { get; }

    }
}
