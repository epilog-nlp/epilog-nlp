using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Classification.Contracts
{
    public interface ICategory<TContent>
    {
        IEnumerable<IFeature<TContent>> Features { get; }

        IEnumerable<IFeature<TContent>> Differentiae { get; }
    }
}
