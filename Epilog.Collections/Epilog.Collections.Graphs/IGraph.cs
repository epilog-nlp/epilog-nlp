using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Collections.Graphs
{
    
    public interface IGraph<TVertex,TEdge>
    {
        void Add(TVertex source, TVertex dest, TEdge data);

        void Add(IEdge<TVertex, TEdge> edge) => Add(edge.Source, edge.Dest, edge.Weight);

        bool AddVertex(TVertex vertex);
        
        bool RemoveEdges(TVertex source, TVertex dest);

        bool RemoveEdge(TVertex source, TVertex dest, TEdge data);

        bool RemoveEdge(IEdge<TVertex, TEdge> edge) => RemoveEdge(edge.Source, edge.Dest, edge.Weight);

        bool RemoveVertex(TVertex vertex);

        bool RemoveVertices(IEnumerable<TVertex> vertices);

        int NumberOfVertices { get; }

        int NumberOfEdges { get; }

        IEnumerable<TEdge> GetOutgoingEdges(TVertex vertex);

        IEnumerable<TEdge> GetIncomingEdges(TVertex vertex);

        IEnumerable<TVertex> GetParents(TVertex vertex);

        IEnumerable<TVertex> GetChildren(TVertex vertex);

        IEnumerable<TVertex> GetNeighbors(TVertex vertex);

        void Clear();

        bool ContainsVertex(TVertex vertex);

        bool IsEdge(TVertex source, TVertex dest);

        bool IsNeighbor(TVertex source, TVertex dest);

        IEnumerable<TVertex> Vertices { get; }

        IEnumerable<TEdge> Edges { get; }

        bool IsEmpty();

        void RemoveZeroDegreeNodes();

        IEnumerable<TEdge> GetEdges(TVertex source, TVertex dest);

        int GetInDegree(TVertex vertex);

        int GetOutDegree(TVertex vertex);

    }
}
