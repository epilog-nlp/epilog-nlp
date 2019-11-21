using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epilog.Collections.Graphs.Tests
{

    [TestClass]
    public abstract class ReadOnlyDirectedMultiGraphTests<TGraph, TVertex, TEdge>
        where TGraph : IReadOnlyDirectedMultigraph<TVertex, TEdge>
    {
        protected ReadOnlyDirectedMultiGraphTests()
        {
            expectedVerticeCount = new Lazy<int>(InitialEdges.GenerateExpectedVerticeCount);
            expectedEdgeCount = new Lazy<int>(InitialEdges.GenerateExpectedEdgeCount);
            expectedOutgoingEdges = new Lazy<IDictionary<TVertex, IEnumerable<TEdge>>>(InitialEdges.GenerateExpectedOutgoingEdges);
            expectedIncomingEdges = new Lazy<IDictionary<TVertex, IEnumerable<TEdge>>>(InitialEdges.GenerateExpectedIncomingEdges);
            expectedInDegrees = new Lazy<IDictionary<TVertex, int>>(InitialEdges.GenerateExpectedInDegrees);
            expectedOutDegrees = new Lazy<IDictionary<TVertex, int>>(InitialEdges.GenerateExpectedOutDegrees);
            expectedParents = new Lazy<IDictionary<TVertex, IEnumerable<TVertex>>>(InitialEdges.GenerateExpectedParents);
            expectedChildren = new Lazy<IDictionary<TVertex, IEnumerable<TVertex>>>(InitialEdges.GenerateExpectedChildren);
            expectedNeighbors = new Lazy<IDictionary<TVertex, IEnumerable<TVertex>>>(InitialEdges.GenerateExpectedNeighbors);
            expectedVertices = new Lazy<IEnumerable<TVertex>>(InitialEdges.GenerateExpectedVertices);
            expectedEdges = new Lazy<IEnumerable<TEdge>>(InitialEdges.GenerateExpectedEdges);
            expectedConnectedEdges = new Lazy<IDictionary<(TVertex source, TVertex dest), IEnumerable<TEdge>>>(InitialEdges.GenerateExpectedConnectedEdges);

        }

        protected readonly Lazy<int> expectedVerticeCount;
        protected readonly Lazy<int> expectedEdgeCount;
        private readonly Lazy<IDictionary<TVertex, IEnumerable<TEdge>>> expectedOutgoingEdges;
        private readonly Lazy<IDictionary<TVertex, IEnumerable<TEdge>>> expectedIncomingEdges;
        private readonly Lazy<IDictionary<TVertex, int>> expectedInDegrees;
        private readonly Lazy<IDictionary<TVertex, int>> expectedOutDegrees;
        private readonly Lazy<IDictionary<TVertex, IEnumerable<TVertex>>> expectedParents;
        private readonly Lazy<IDictionary<TVertex, IEnumerable<TVertex>>> expectedChildren;
        private readonly Lazy<IDictionary<TVertex, IEnumerable<TVertex>>> expectedNeighbors;
        private readonly Lazy<IEnumerable<TVertex>> expectedVertices;
        private readonly Lazy<IEnumerable<TEdge>> expectedEdges;
        private readonly Lazy<IDictionary<(TVertex source, TVertex dest), IEnumerable<TEdge>>> expectedConnectedEdges;

        protected abstract TVertex NonExistentVertex { get; }

        protected abstract IEdge<TVertex, TEdge> NonExistentEdge { get; }

        protected abstract TGraph GraphFactory { get; }

        protected virtual IEnumerable<IEdge<TVertex, TEdge>> InitialEdges => Enumerable.Empty<IEdge<TVertex, TEdge>>();

        protected abstract TGraph Populate(TGraph graph, IEnumerable<IEdge<TVertex, TEdge>> edges);

        protected IDictionary<TVertex, IEnumerable<TEdge>> ExpectedOutgoingEdges
            => expectedOutgoingEdges.Value;

        protected IDictionary<TVertex, IEnumerable<TEdge>> ExpectedIncomingEdges
            => expectedIncomingEdges.Value;

        protected IDictionary<TVertex, int> ExpectedInDegrees
            => expectedInDegrees.Value;

        protected IDictionary<TVertex, int> ExpectedOutDegrees
            => expectedOutDegrees.Value;

        protected IDictionary<TVertex, IEnumerable<TVertex>> ExpectedParents
            => expectedParents.Value;

        protected IDictionary<TVertex, IEnumerable<TVertex>> ExpectedChildren
            => expectedChildren.Value;

        protected IDictionary<TVertex, IEnumerable<TVertex>> ExpectedNeighbors
            => expectedNeighbors.Value;

        protected IEnumerable<TVertex> ExpectedVertices
            => expectedVertices.Value;

        protected IEnumerable<TEdge> ExpectedEdges
            => expectedEdges.Value;

        protected IDictionary<(TVertex source, TVertex dest), IEnumerable<TEdge>> ExpectedConnectedEdges
            => expectedConnectedEdges.Value;

        protected virtual TGraph DefaultGraph => Populate(GraphFactory, InitialEdges);

        [TestMethod]
        public void NumberOfVertices_Test()
        {
            var graph = DefaultGraph;
            Assert.AreEqual(expectedVerticeCount.Value, graph.NumberOfVertices);
        }

        [TestMethod]
        public void NumberOfVertices_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.AreEqual(0, graph.NumberOfVertices);
        }

        [TestMethod]
        public void NumberOfEdges_Test()
        {
            var graph = DefaultGraph;
            Assert.AreEqual(expectedEdgeCount.Value, graph.NumberOfEdges);
        }

        [TestMethod]
        public void NumberOfEdges_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.AreEqual(0, graph.NumberOfEdges);
        }

        [TestMethod]
        public void GetOutgoingEdges_Test()
        {
            var graph = DefaultGraph;
            Validate(ExpectedOutgoingEdges, graph.GetOutgoingEdges);
        }

        [TestMethod]
        public void GetOutgoingEdges_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.IsFalse(graph.GetOutgoingEdges(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetOutgoingEdges_Invalid_Test()
        {
            var graph = DefaultGraph;
            Assert.IsFalse(graph.GetOutgoingEdges(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetIncomingEdges_Test()
        {
            var graph = DefaultGraph;
            Validate(ExpectedIncomingEdges, graph.GetIncomingEdges);
        }

        [TestMethod]
        public void GetIncomingEdges_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.IsFalse(graph.GetIncomingEdges(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetIncomingEdges_Invalid_Test()
        {
            var graph = DefaultGraph;
            Assert.IsFalse(graph.GetIncomingEdges(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetParents_Test()
        {
            var graph = DefaultGraph;
            Validate(ExpectedParents, graph.GetParents);
        }

        [TestMethod]
        public void GetParents_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.IsFalse(graph.GetParents(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetParents_Invalid_Test()
        {
            var graph = DefaultGraph;
            Assert.IsFalse(graph.GetParents(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetChildren_Test()
        {
            var graph = DefaultGraph;
            Validate(ExpectedChildren, graph.GetChildren);
        }

        [TestMethod]
        public void GetChildren_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.IsFalse(graph.GetChildren(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetChildren_Invalid_Test()
        {
            var graph = DefaultGraph;
            Assert.IsFalse(graph.GetChildren(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetNeighbors_Test()
        {
            var graph = DefaultGraph;
            Validate(ExpectedNeighbors, graph.GetNeighbors);
        }

        [TestMethod]
        public void GetNeighbors_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.IsFalse(graph.GetNeighbors(NonExistentVertex).Any());
        }

        [TestMethod]
        public void GetNeighbors_Invalid_Test()
        {
            var graph = DefaultGraph;
            Assert.IsFalse(graph.GetNeighbors(NonExistentVertex).Any());
        }

        [TestMethod]
        public void ContainsVertex_Test()
        {
            var graph = DefaultGraph;
            Assert.IsTrue(ExpectedVertices.All(graph.ContainsVertex));
        }

        [TestMethod]
        public void ContainsVertex_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.IsFalse(graph.ContainsVertex(NonExistentVertex));
        }

        [TestMethod]
        public void ContainsVertex_Invalid_Test()
        {
            var graph = DefaultGraph;
            Assert.IsFalse(graph.ContainsVertex(NonExistentVertex));
        }

        [TestMethod]
        public void IsEdge_Test()
        {
            var graph = DefaultGraph;
            Assert.IsTrue(ExpectedConnectedEdges.Keys.All(k => graph.IsEdge(k.source, k.dest)));
        }

        [TestMethod]
        public void IsEdge_Empty_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            Assert.IsFalse(graph.IsEdge(edge.Source, edge.Dest));
        }

        [TestMethod]
        public void IsEdge_Invalid_Test()
        {
            var graph = DefaultGraph;
            var edge = NonExistentEdge;
            Assert.IsFalse(graph.IsEdge(edge.Source, edge.Dest));
        }

        [TestMethod]
        public void IsNeighbor_Test()
        {
            var graph = DefaultGraph;
            Assert.IsTrue(ExpectedNeighbors.All(kvp => kvp.Value.All(dest => graph.IsNeighbor(kvp.Key, dest))));
        }

        [TestMethod]
        public void IsNeighbor_Empty_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            Assert.IsFalse(graph.IsNeighbor(edge.Source, edge.Dest));
        }

        [TestMethod]
        public void IsNeighbor_Invalid_Test()
        {
            var graph = DefaultGraph;
            var edge = NonExistentEdge;
            Assert.IsFalse(graph.IsNeighbor(edge.Source, edge.Dest));
        }

        [TestMethod]
        public void Vertices_Test()
        {
            var graph = DefaultGraph;
            CollectionAssert.AreEquivalent(ExpectedVertices.ToList(), graph.Vertices.ToList());
        }

        [TestMethod]
        public void Vertices_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.IsFalse(graph.Vertices.Any());
        }

        [TestMethod]
        public void Edges_Test()
        {
            var graph = DefaultGraph;
            CollectionAssert.AreEquivalent(ExpectedEdges.ToList(), graph.Edges.ToList());
        }

        [TestMethod]
        public void Edges_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.IsFalse(graph.Edges.Any());
        }

        [TestMethod]
        public void GetInDegree_Test()
        {
            var graph = DefaultGraph;
            Validate(ExpectedInDegrees, graph.GetInDegree);
        }

        [TestMethod]
        public void GetInDegree_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.AreEqual(0, graph.GetInDegree(NonExistentVertex));
        }

        [TestMethod]
        public void GetInDegree_Invalid_Test()
        {
            var graph = DefaultGraph;
            Assert.AreEqual(0, graph.GetInDegree(NonExistentVertex));
        }

        [TestMethod]
        public void GetOutDegree_Test()
        {
            var graph = DefaultGraph;
            Validate(ExpectedOutDegrees, graph.GetOutDegree);
        }

        [TestMethod]
        public void GetOutDegree_Empty_Test()
        {
            var graph = GraphFactory;
            Assert.AreEqual(0, graph.GetOutDegree(NonExistentVertex));
        }

        [TestMethod]
        public void GetOutDegree_Invalid_Test()
        {
            var graph = DefaultGraph;
            Assert.AreEqual(0, graph.GetOutDegree(NonExistentVertex));
        }

        protected static void Validate<TKey, TValue>(IDictionary<TKey, TValue> expected, Func<TKey, TValue> producer)
        {
            foreach (var key in expected.Keys)
                Assert.AreEqual(expected[key], producer(key));
        }

        protected static void Validate<TKey, TValue>(IDictionary<TKey, IEnumerable<TValue>> expected, Func<TKey, IEnumerable<TValue>> producer)
        {
            foreach (var key in expected.Keys)
                CollectionAssert.AreEquivalent(expected[key].ToList(), producer(key).ToList());
        }
    }
}
