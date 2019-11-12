using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Collections.Graphs
{
    public interface IReadOnlyDirectedMultiGraph<TVertex,TEdge>
    {
        int NumberOfVertices { get; }

        int NumberOfEdges { get; }

        IEnumerable<TEdge> GetOutgoingEdges(TVertex vertex);

        IEnumerable<TEdge> GetIncomingEdges(TVertex vertex);

        IEnumerable<TVertex> GetParents(TVertex vertex);

        IEnumerable<TVertex> GetChildren(TVertex vertex);

        IEnumerable<TVertex> GetNeighbors(TVertex vertex);

        bool ContainsVertex(TVertex vertex);

        bool IsEdge(TVertex source, TVertex dest);

        bool IsNeighbor(TVertex source, TVertex dest);

        IEnumerable<TVertex> Vertices { get; }

        IEnumerable<TEdge> Edges { get; }

        bool IsEmpty();

        IEnumerable<TEdge> GetEdges(TVertex source, TVertex dest);

        int GetInDegree(TVertex vertex);

        int GetOutDegree(TVertex vertex);
    }
}
