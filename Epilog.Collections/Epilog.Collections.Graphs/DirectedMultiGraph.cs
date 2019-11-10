using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epilog.Collections.Graphs
{

    public class DirectedMultiGraph<TVertex, TEdge, TOuterMap, TInnerMap, TInnerList> : DirectedMultiGraphBase<TVertex,TEdge,TOuterMap,TInnerMap,TInnerList>, IGraph<TVertex, TEdge>
        where TInnerList : IList<TEdge>, new()
        where TInnerMap : IDictionary<TVertex, TInnerList>, new()
        where TOuterMap : IDictionary<TVertex, TInnerMap>, new()
    {

        #region IGraph Implementation
        public override void Add(TVertex source, TVertex dest, TEdge data)
        {
            var outgoingMap = GetOutgoingEdgesMap(source);
            var incomingMap = GetIncomingEdgesMap(dest);

            if (!outgoingMap.TryGetValue(dest, out var outgoingList))
            {
                outgoingList = InnerList;
                outgoingMap[dest] = outgoingList;
            }

            if (!incomingMap.TryGetValue(source, out var incomingList))
            {
                incomingList = InnerList;
                incomingMap[source] = incomingList;
            }

            outgoingList.Add(data);
            incomingList.Add(data);
        }

        public override bool AddVertex(TVertex vertex)
        {
            if (OutgoingEdges.ContainsKey(vertex))
                return false;
            OutgoingEdges[vertex] = InnerMap;
            IncomingEdges[vertex] = InnerMap;
            return true;
        }

        public override bool RemoveEdges(TVertex source, TVertex dest)
        {
            if (!OutgoingEdges.ContainsKey(source))
                return false;
            if (!IncomingEdges.ContainsKey(dest))
                return false;
            if (!OutgoingEdges[source].ContainsKey(dest))
                return false;
            OutgoingEdges[source].Remove(dest);
            IncomingEdges[dest].Remove(source);
            return true;
        }

        public override bool RemoveEdge(TVertex source, TVertex dest, TEdge data)
        {
            if (!OutgoingEdges.ContainsKey(source))
                return false;
            if (!IncomingEdges.ContainsKey(dest))
                return false;
            if (!OutgoingEdges[source].ContainsKey(dest))
                return false;

            bool foundOut = OutgoingEdges.ContainsKey(source) && OutgoingEdges[source].ContainsKey(dest) && OutgoingEdges[source][dest].Remove(data);
            bool foundIn = IncomingEdges.ContainsKey(dest) && IncomingEdges[dest].ContainsKey(source) && IncomingEdges[dest][source].Remove(data);

            if (foundOut && !foundIn)
                throw new Exception("Edge found in outgoing but not incoming"); // TODO: Specialized Exception
            if (foundIn && !foundOut)
                throw new Exception("Edge found in incoming but not outgoing"); // TODO: Specialized Exception

            if (OutgoingEdges.ContainsKey(source) && (!OutgoingEdges[source].ContainsKey(dest) || OutgoingEdges[source][dest].Count == 0))
                OutgoingEdges[source].Remove(dest);
            if (IncomingEdges.ContainsKey(dest) && (!IncomingEdges[dest].ContainsKey(source) || IncomingEdges[dest][source].Count == 0))
                IncomingEdges[dest].Remove(source);

            return foundOut;
        }

        public override bool RemoveVertex(TVertex vertex)
        {
            if (!OutgoingEdges.ContainsKey(vertex))
                return false;
            foreach (var other in OutgoingEdges[vertex].Keys)
                IncomingEdges[other].Remove(vertex);
            foreach (var other in IncomingEdges[vertex].Keys)
                OutgoingEdges[other].Remove(vertex);
            OutgoingEdges.Remove(vertex);
            IncomingEdges.Remove(vertex);
            return true;
        }

        public override bool RemoveVertices(IEnumerable<TVertex> vertices)
        {
            bool changed = false;
            foreach (var vertex in vertices)
            {
                if (RemoveVertex(vertex))
                    changed = true;
            }
            return changed;
        }

        public override IEnumerable<TEdge> GetOutgoingEdges(TVertex vertex)
        {
            if (!OutgoingEdges.ContainsKey(vertex))
                return Enumerable.Empty<TEdge>();
            return OutgoingEdges[vertex].SelectMany(v => v.Value);
        }

        public override IEnumerable<TEdge> GetIncomingEdges(TVertex vertex)
        {
            if (!IncomingEdges.ContainsKey(vertex))
                return Enumerable.Empty<TEdge>();
            return IncomingEdges[vertex].SelectMany(v => v.Value);
        }

        public override void RemoveZeroDegreeNodes()
        {
            var toDelete = OutgoingEdges.Keys.Where(vertex => !OutgoingEdges[vertex].Any() && !IncomingEdges[vertex].Any());
            foreach (var vertex in toDelete)
            {
                OutgoingEdges.Remove(vertex);
                IncomingEdges.Remove(vertex);
            }
        }

        #endregion

        private TInnerMap GetOutgoingEdgesMap(TVertex vertex)
        {
            if (OutgoingEdges.TryGetValue(vertex, out var map))
                return map;
            map = InnerMap;
            OutgoingEdges[vertex] = map;
            IncomingEdges[vertex] = InnerMap;
            return map;
        }

        private TInnerMap GetIncomingEdgesMap(TVertex vertex)
        {
            if (IncomingEdges.TryGetValue(vertex, out var map))
                return map;
            map = InnerMap;
            OutgoingEdges[vertex] = InnerMap;
            IncomingEdges[vertex] = map;
            return map;
        }
    }

    public class DirectedMultiGraph<TVertex, TEdge, TOuterMap, TInnerMap> : DirectedMultiGraph<TVertex, TEdge, TOuterMap, TInnerMap, List<TEdge>>
        where TInnerMap : IDictionary<TVertex, List<TEdge>>, new()
        where TOuterMap : IDictionary<TVertex, TInnerMap>, new()
    {

    }

    public class DirectedMultiGraph<TVertex,TEdge,TOuterMap> : DirectedMultiGraph<TVertex,TEdge,TOuterMap,Dictionary<TVertex,List<TEdge>>>
        where TOuterMap : IDictionary<TVertex, Dictionary<TVertex,List<TEdge>>>, new()
    {

    }

    public class DirectedMultiGraph<TVertex,TEdge> : DirectedMultiGraph<TVertex,TEdge, Dictionary<TVertex, Dictionary<TVertex, List<TEdge>>>>
    {

    }
}
