using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epilog.Collections.Graphs
{
    public abstract class DirectedMultiGraphBase<TVertex, TEdge, TOuterMap, TInnerMap, TInnerList> : IGraph<TVertex, TEdge>
        where TInnerList : IList<TEdge>, new()
        where TInnerMap : IDictionary<TVertex, TInnerList>, new()
        where TOuterMap : IDictionary<TVertex, TInnerMap>, new()
    {
        protected static TOuterMap OuterMap => new TOuterMap();

        protected static TInnerMap InnerMap => new TInnerMap();

        protected static TInnerList InnerList => new TInnerList();

        protected TOuterMap OutgoingEdges { get; } = OuterMap;

        protected TOuterMap IncomingEdges { get; } = OuterMap;

        public abstract void Add(TVertex source, TVertex dest, TEdge data);
        public abstract bool AddVertex(TVertex vertex);
        public abstract bool RemoveEdges(TVertex source, TVertex dest);
        public abstract bool RemoveEdge(TVertex source, TVertex dest, TEdge data);
        public abstract bool RemoveVertex(TVertex vertex);
        public abstract bool RemoveVertices(IEnumerable<TVertex> vertices);

        public int NumberOfVertices => OutgoingEdges.Count;
        public int NumberOfEdges => OutgoingEdges.Values.Sum(outer => outer.Values.Sum(inner => inner.Count));

        public abstract IEnumerable<TEdge> GetOutgoingEdges(TVertex vertex);
        public abstract IEnumerable<TEdge> GetIncomingEdges(TVertex vertex);
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

        public abstract void RemoveZeroDegreeNodes();
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
    }
}
