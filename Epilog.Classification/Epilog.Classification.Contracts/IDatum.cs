using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Classification.Contracts
{
    public interface IDatum<TLabel, TFeature> : ILabeled<TLabel>, IFeaturizable<TFeature>
        where TLabel : ILabel
    {
    }
}
