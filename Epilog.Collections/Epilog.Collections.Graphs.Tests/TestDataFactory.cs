using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.Concurrent;

namespace Epilog.Collections.Graphs.Tests
{
    public static class TestDataFactory
    {
        public static int GenerateExpectedVerticeCount<TVertex,TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.Select(edge => edge.Dest)
                                .Concat(initialEdges.Select(edge => edge.Source))
                                .Distinct()
                                .Count();

        public static int GenerateExpectedEdgeCount<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.Select(edge => edge.Weight)
                           .Count();

        public static IDictionary<TVertex, IEnumerable<TEdge>> GenerateExpectedOutgoingEdges<TVertex,TEdge>(this IEnumerable<IEdge<TVertex,TEdge>> initialEdges)
            => initialEdges.GroupBy(edge => edge.Source)
                           .ToDictionary(
                                key => key.Key,
                                val => val.Select(e => e.Weight));

        public static IDictionary<TVertex, IEnumerable<TEdge>> GenerateExpectedIncomingEdges<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.GroupBy(edge => edge.Dest)
                           .ToDictionary(
                                key => key.Key,
                                val => val.Select(e => e.Weight));

        public static IDictionary<TVertex, int> GenerateExpectedInDegrees<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.GroupBy(edge => edge.Dest)
                           .ToDictionary(
                                key => key.Key,
                                val => val.Count());

        public static IDictionary<TVertex, int> GenerateExpectedOutDegrees<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.GroupBy(edge => edge.Source)
                           .ToDictionary(
                                key => key.Key,
                                val => val.Count());

        public static IDictionary<TVertex, IEnumerable<TVertex>> GenerateExpectedParents<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.GroupBy(edge => edge.Dest)
                           .ToDictionary(
                                key => key.Key,
                                val => val.Select(e => e.Source).Distinct());

        public static IDictionary<TVertex, IEnumerable<TVertex>> GenerateExpectedChildren<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.GroupBy(edge => edge.Source)
                           .ToDictionary(
                                key => key.Key,
                                val => val.Select(e => e.Dest).Distinct());

        public static IDictionary<TVertex, IEnumerable<TVertex>> GenerateExpectedNeighbors<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => GenerateExpectedParents(initialEdges).Concat(GenerateExpectedChildren(initialEdges))
                              .GroupBy(kvp => kvp.Key)
                              .ToDictionary(
                                    key => key.Key,
                                    val => val.SelectMany(kvp => kvp.Value).Distinct());

        public static IEnumerable<TVertex> GenerateExpectedVertices<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.SelectMany(edge => new[] { edge.Source, edge.Dest })
                           .Distinct();

        public static IEnumerable<TEdge> GenerateExpectedEdges<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.Select(edge => edge.Weight);

        public static IDictionary<(TVertex source, TVertex dest), IEnumerable<TEdge>> GenerateExpectedConnectedEdges<TVertex, TEdge>(this IEnumerable<IEdge<TVertex, TEdge>> initialEdges)
            => initialEdges.GroupBy(edge => (edge.Source, edge.Dest))
                           .ToDictionary(
                                key => key.Key,
                                val => val.Select(edge => edge.Weight));

        public static IEnumerable<IEdge<string, int>> ValueTypeEdges
            => new ConcurrentBag<IEdge<string, int>>
            {
                Edge("vertex1","vertex2", 62340),
                Edge("vertex1","vertex2", 53921),
                Edge("vertex1","vertex7", 73291),
                Edge("vertex1","vertex9", 38423),
                Edge("vertex2","vertex1", 96732),
                Edge("vertex2","vertex0", 22293),
                Edge("vertex2","vertex8", 34343),
                Edge("vertex2","vertex3", 93823),
                Edge("vertex3","vertex5", 34522),
                Edge("vertex4","vertex7", 19245),
                Edge("vertex4","vertex8", 53240),
                Edge("vertex4","vertex6", 72343),
                Edge("vertex5","vertex7", 55131),
                Edge("vertex5","vertex4", 24324),
                Edge("vertex5","vertex1", 12679),
                Edge("vertex5","vertex0", 12458),
                Edge("vertex6","vertex9", 90923),
                Edge("vertex6","vertex7", 78976),
                Edge("vertex7","vertex3", 89809),
                Edge("vertex7","vertex4", 23743),
                Edge("vertex7","vertex6", 78342),
                Edge("vertex8","vertex5", 12520),
                Edge("vertex8","vertex4", 46235),
                Edge("vertex8","vertex8", 23946),
                Edge("vertex8","vertex1", 12934),
                Edge("vertex8","vertex2", 14834),
                Edge("vertex9","vertex7", 87352),
                Edge("vertex9","vertex5", 54893),
                Edge("vertex0","vertex2", 12094)
            };

        public static IEnumerable<IEdge<string,int>> RandomEdges(int vertexCount, int edgeCount)
        {
            var vertices = Enumerable.Range(0, vertexCount).Select(i => RandomAlpha).ToList();
            var max = vertexCount - 1;
            return Enumerable.Range(0, edgeCount)
                             .Select(i => new Edge<string, int>
                             {
                                 Source = vertices[rand.Next(0, max)],
                                 Dest = vertices[rand.Next(0,max)],
                                 Weight = rand.Next(0, 99999)
                             }).ToList();
        }

        const string Alpha = "abcdefghijklmnopqrstuvwxyz";

        private static string RandomAlpha
            => new string(Enumerable.Range(1, rand.Next(5, 10)).Select(i => Alpha[rand.Next(0, Alpha.Count() - 1)]).ToArray());

        private static readonly Random rand = new Random();

        private static IEdge<TVertex, TEdge> Edge<TVertex, TEdge>(TVertex source, TVertex dest, TEdge weight)
            => new Edge<TVertex, TEdge>
            {
                Source = source,
                Dest = dest,
                Weight = weight
            };
    }
}
