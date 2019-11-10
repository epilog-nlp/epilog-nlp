using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace Epilog.Collections.Graphs
{
    public class ConcurrentDirectedMultiGraph<TVertex,TEdge> : DirectedMultiGraphBase<TVertex,TEdge, ConcurrentDictionary<TVertex,ConcurrentDictionary<TVertex,List<TEdge>>>,ConcurrentDictionary<TVertex,List<TEdge>>,List<TEdge>>
    {
        public override void Add(TVertex source, TVertex dest, TEdge data) => throw new NotImplementedException();
        public override bool AddVertex(TVertex vertex) => throw new NotImplementedException();
        public override bool RemoveEdges(TVertex source, TVertex dest) => throw new NotImplementedException();
        public override bool RemoveEdge(TVertex source, TVertex dest, TEdge data) => throw new NotImplementedException();
        public override bool RemoveVertex(TVertex vertex) => throw new NotImplementedException();
        public override bool RemoveVertices(IEnumerable<TVertex> vertices) => throw new NotImplementedException();
        public override IEnumerable<TEdge> GetOutgoingEdges(TVertex vertex) => throw new NotImplementedException();
        public override IEnumerable<TEdge> GetIncomingEdges(TVertex vertex) => throw new NotImplementedException();
        public override void RemoveZeroDegreeNodes() => throw new NotImplementedException();

        private ConcurrentDictionary<TVertex,List<TEdge>> GetOutgoingEdgesMap(TVertex vertex)
        {
            if (OutgoingEdges.TryGetValue(vertex, out var map))
                return map;
            map = InnerMap;
            OutgoingEdges[vertex] = map;
            IncomingEdges[vertex] = InnerMap;
            return map;
        }

        private ConcurrentDictionary<TVertex, List<TEdge>> GetIncomingEdgesMap(TVertex vertex)
        {
            if (IncomingEdges.TryGetValue(vertex, out var map))
                return map;
            map = InnerMap;
            OutgoingEdges[vertex] = InnerMap;
            IncomingEdges[vertex] = map;
            return map;
        }
    }
}
