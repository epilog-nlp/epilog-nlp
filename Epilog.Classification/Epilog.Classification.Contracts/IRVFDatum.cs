using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Classification.Contracts
{
    public interface IRVFDatum<TLabel, TFeature> : IDatum<TLabel, TFeature>
        where TLabel : ILabel
    {

    }
}
