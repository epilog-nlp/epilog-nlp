using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Immutable;
using System.Linq;

namespace Epilog.Collections.Graphs
{
    public class ImmutableDirectedMultiGraph<TVertex,TEdge> : ReadOnlyDirectedMultiGraph<TVertex,TEdge>
    {

        internal ImmutableDirectedMultiGraph(IDictionary<TVertex,IDictionary<TVertex,IList<TEdge>>> outgoing,
            IDictionary<TVertex, IDictionary<TVertex, IList<TEdge>>> incoming) 
            : base(outgoing.ToImmutableDictionary(
                outerKey => outerKey.Key, 
                outerValue => outerValue.Value
                    .ToImmutableDictionary(
                        innerKey => innerKey.Key, 
                        innerVal => innerVal.Value.ToImmutableList() as IList<TEdge>) 
                as IDictionary<TVertex, IList<TEdge>>), 
            incoming.ToImmutableDictionary(
                outerKey => outerKey.Key, 
                outerValue => outerValue.Value
                    .ToImmutableDictionary(
                        innerKey => innerKey.Key, 
                        innerVal => innerVal.Value.ToImmutableList() as IList<TEdge>) 
                as IDictionary<TVertex,IList<TEdge>>))
        {
            
        }
    }
}
