using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Classification.Contracts
{
    public interface ITaxonomy<TContent> : ICategory<TContent>
    {
        ITaxonomy<TContent> Genus { get; }


        IEnumerable<ITaxonomy<TContent>> Species { get; }

    }

    
}
