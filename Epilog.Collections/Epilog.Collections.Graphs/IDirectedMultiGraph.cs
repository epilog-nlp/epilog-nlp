using System.Collections.Generic;

namespace Epilog.Collections.Graphs
{

    public interface IDirectedMultigraph<TVertex, TEdge> : IReadOnlyDirectedMultigraph<TVertex, TEdge>
    {
        void Add(TVertex source, TVertex dest, TEdge data);

        void Add(IEdge<TVertex, TEdge> edge) => Add(edge.Source, edge.Dest, edge.Weight);

        bool AddVertex(TVertex vertex);

        bool RemoveEdges(TVertex source, TVertex dest);

        bool RemoveEdge(TVertex source, TVertex dest, TEdge data);

        bool RemoveEdge(IEdge<TVertex, TEdge> edge) => RemoveEdge(edge.Source, edge.Dest, edge.Weight);

        bool RemoveVertex(TVertex vertex);

        bool RemoveVertices(IEnumerable<TVertex> vertices);

        void Clear();

        void RemoveZeroDegreeNodes();

    }
}
