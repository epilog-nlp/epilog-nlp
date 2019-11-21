using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Classification.Contracts
{
    public interface IFeature<TContent>
    {
        bool HasFeature(TContent content);
    }
}
