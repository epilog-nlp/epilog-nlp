using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epilog.Collections.Graphs
{

    public abstract class ReadOnlyDirectedMultiGraph<TVertex, TEdge> : IReadOnlyDirectedMultiGraph<TVertex, TEdge>
    {
        protected ReadOnlyDirectedMultiGraph()
        {
            OutgoingEdges = OuterMap;
            IncomingEdges = OuterMap;
        }

        protected internal ReadOnlyDirectedMultiGraph(IDictionary<TVertex, IDictionary<TVertex, IList<TEdge>>> outgoing,
            IDictionary<TVertex, IDictionary<TVertex, IList<TEdge>>> incoming)
        {
            OutgoingEdges = outgoing;
            IncomingEdges = incoming;
        }

        protected virtual IDictionary<TVertex, IDictionary<TVertex, IList<TEdge>>> OuterMap => new Dictionary<TVertex, IDictionary<TVertex, IList<TEdge>>>();

        protected virtual IDictionary<TVertex, IList<TEdge>> InnerMap => new Dictionary<TVertex, IList<TEdge>>();

        protected virtual IList<TEdge> InnerList => new List<TEdge>();

        internal virtual IDictionary<TVertex, IDictionary<TVertex, IList<TEdge>>> OutgoingEdges { get; }

        internal virtual IDictionary<TVertex, IDictionary<TVertex, IList<TEdge>>> IncomingEdges { get; }

        #region IReadOnlyGraph Implementation
        public int NumberOfVertices => OutgoingEdges.Count;
        public int NumberOfEdges => OutgoingEdges.Values.Sum(outer => outer.Values.Sum(inner => inner.Count));

        public IEnumerable<TEdge> GetOutgoingEdges(TVertex vertex)
        {
            if (!OutgoingEdges.ContainsKey(vertex))
                return Enumerable.Empty<TEdge>();
            return OutgoingEdges[vertex].SelectMany(v => v.Value);
        }

        public IEnumerable<TEdge> GetIncomingEdges(TVertex vertex)
        {
            if (!IncomingEdges.ContainsKey(vertex))
                return Enumerable.Empty<TEdge>();
            return IncomingEdges[vertex].SelectMany(v => v.Value);
        }
        public IEnumerable<TVertex> GetParents(TVertex vertex)
            => IncomingEdges.TryGetValue(vertex, out var parentMap)
                ? parentMap.Keys
                : Enumerable.Empty<TVertex>();

        public IEnumerable<TVertex> GetChildren(TVertex vertex)
            => OutgoingEdges.TryGetValue(vertex, out var childMap)
                ? childMap.Keys
                : Enumerable.Empty<TVertex>();

        public IEnumerable<TVertex> GetNeighbors(TVertex vertex) => GetParents(vertex).Concat(GetChildren(vertex));

        public void Clear()
        {
            IncomingEdges.Clear();
            OutgoingEdges.Clear();
        }

        public bool ContainsVertex(TVertex vertex)
            => OutgoingEdges.ContainsKey(vertex);

        public bool IsEdge(TVertex source, TVertex dest)
            => OutgoingEdges.TryGetValue(source, out var childrenMap)
            && childrenMap.Any()
            && childrenMap.TryGetValue(dest, out var edges)
            && edges.Any();

        public bool IsNeighbor(TVertex source, TVertex dest)
            => IsEdge(source, dest) || IsEdge(dest, source);

        public IEnumerable<TVertex> Vertices => OutgoingEdges.Keys;

        public IEnumerable<TEdge> Edges => OutgoingEdges.Values.SelectMany(inner => inner.Values.SelectMany(edge => edge));

        public bool IsEmpty() => !OutgoingEdges.Any();

        public IEnumerable<TEdge> GetEdges(TVertex source, TVertex dest)
            => OutgoingEdges.TryGetValue(source, out var childrenMap) && childrenMap.TryGetValue(dest, out var edges)
            ? edges
            : Enumerable.Empty<TEdge>();

        public int GetInDegree(TVertex vertex)
        {
            if (!ContainsVertex(vertex))
                return 0;
            return IncomingEdges[vertex].Sum(edges => edges.Value.Count);
        }

        public int GetOutDegree(TVertex vertex)
        {
            if (!OutgoingEdges.TryGetValue(vertex, out var outer))
                return 0;
            return outer.Sum(edges => edges.Value.Count);
        }
        #endregion
    }

}
