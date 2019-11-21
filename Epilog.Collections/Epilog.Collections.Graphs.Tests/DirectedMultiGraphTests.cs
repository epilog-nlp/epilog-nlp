using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epilog.Collections.Graphs.Tests
{
    [TestClass]
    public abstract class DirectedMultiGraphTests<TGraph, TVertex, TEdge> : ReadOnlyDirectedMultiGraphTests<TGraph, TVertex, TEdge>
        where TGraph : IDirectedMultigraph<TVertex, TEdge>
    {
        protected override TGraph Populate(TGraph graph, IEnumerable<IEdge<TVertex, TEdge>> edges)
        {
            //Parallel.ForEach(edges, edge => graph.Add(edge));
            foreach (var edge in edges)
                graph.Add(edge);
            return graph;
        }

        [TestMethod]
        public void Add_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            graph.Add(edge.Source, edge.Dest, edge.Weight);
            Assert.AreEqual(2, graph.NumberOfVertices);
            Assert.AreEqual(1, graph.NumberOfEdges);
            ValidateAdd(graph, edge);
        }

        [TestMethod]
        public void Add_Edge_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            graph.Add(edge);
            Assert.AreEqual(2, graph.NumberOfVertices);
            Assert.AreEqual(1, graph.NumberOfEdges);
            ValidateAdd(graph, edge);
        }

        [TestMethod]
        public void AddVertex_Test()
        {
            var graph = GraphFactory;
            var vertex = NonExistentVertex;
            Assert.IsTrue(graph.AddVertex(vertex));
            ValidateAdd(graph, vertex);
            Assert.IsFalse(graph.AddVertex(vertex));
        }

        [TestMethod]
        public void RemoveEdges_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            Assert.IsFalse(graph.RemoveEdges(edge.Source, edge.Dest));
            graph.Add(edge);
            graph.Add(edge);
            ValidateAdd(graph, edge);
            Assert.AreEqual(2, graph.GetEdges(edge.Source, edge.Dest).Count());
            Assert.IsTrue(graph.RemoveEdges(edge.Source, edge.Dest));
            Assert.AreEqual(0, graph.GetEdges(edge.Source, edge.Dest).Count());
        }

        [TestMethod]
        public void RemoveEdge_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            Assert.IsFalse(graph.RemoveEdge(edge.Source, edge.Dest, edge.Weight));
            graph.Add(edge);
            graph.Add(edge);
            ValidateAdd(graph, edge);
            Assert.AreEqual(2, graph.GetEdges(edge.Source, edge.Dest).Count());
            Assert.IsTrue(graph.RemoveEdge(edge.Source, edge.Dest, edge.Weight));
            Assert.AreEqual(1, graph.GetEdges(edge.Source, edge.Dest).Count());
            Assert.IsTrue(graph.RemoveEdge(edge.Source, edge.Dest, edge.Weight));
            Assert.AreEqual(0, graph.GetEdges(edge.Source, edge.Dest).Count());
            Assert.IsFalse(graph.RemoveEdge(edge.Source, edge.Dest, edge.Weight));
            Assert.AreEqual(0, graph.GetEdges(edge.Source, edge.Dest).Count());
        }

        [TestMethod]
        public void RemoveEdge_Edge_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            Assert.IsFalse(graph.RemoveEdge(edge.Source, edge.Dest, edge.Weight));
            graph.Add(edge);
            graph.Add(edge);
            ValidateAdd(graph, edge);
            Assert.AreEqual(2, graph.GetEdges(edge.Source, edge.Dest).Count());
            Assert.IsTrue(graph.RemoveEdge(edge));
            Assert.AreEqual(1, graph.GetEdges(edge.Source, edge.Dest).Count());
            Assert.IsTrue(graph.RemoveEdge(edge));
            Assert.AreEqual(0, graph.GetEdges(edge.Source, edge.Dest).Count());
            Assert.IsFalse(graph.RemoveEdge(edge));
            Assert.AreEqual(0, graph.GetEdges(edge.Source, edge.Dest).Count());
        }

        [TestMethod]
        public void RemoveVertex_Test()
        {
            var graph = GraphFactory;
            var vertex = NonExistentVertex;
            Assert.IsFalse(graph.RemoveVertex(vertex));
            Assert.IsTrue(graph.AddVertex(vertex));
            ValidateAdd(graph, vertex);
            Assert.IsTrue(graph.RemoveVertex(vertex));
            Assert.IsFalse(graph.Vertices.Contains(vertex));
            Assert.IsFalse(graph.RemoveVertex(vertex));
        }

        [TestMethod]
        public void RemoveVertex_Edge_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            Assert.IsFalse(graph.RemoveVertex(edge.Source));
            graph.Add(edge);
            ValidateAdd(graph, edge);
            Assert.IsTrue(graph.RemoveVertex(edge.Source));
            Assert.IsFalse(graph.Vertices.Contains(edge.Source));
            Assert.IsFalse(graph.IsEdge(edge.Source, edge.Dest));
            Assert.AreEqual(0, graph.GetInDegree(edge.Dest));
            Assert.IsFalse(graph.RemoveVertex(edge.Source));
        }

        [TestMethod]
        public void RemoveVertices_Test()
        {
            var graph = DefaultGraph;
            Assert.IsTrue(graph.RemoveVertices(graph.Vertices));
            Assert.IsFalse(graph.Vertices.Any());
            Assert.IsFalse(graph.Edges.Any());
        }

        [TestMethod]
        public void Clear_Test()
        {
            var graph = DefaultGraph;
            graph.Clear();
            Assert.IsFalse(graph.Vertices.Any());
            Assert.IsFalse(graph.Edges.Any());
            Assert.AreEqual(0, graph.NumberOfEdges);
            Assert.AreEqual(0, graph.NumberOfVertices);
            Assert.IsTrue(graph.IsEmpty());
        }

        [TestMethod]
        public void RemoveZeroDegreeNodes_Test()
        {
            var graph = GraphFactory;
            var edge = NonExistentEdge;
            var vertex = NonExistentVertex;
            graph.Add(edge);
            ValidateAdd(graph, edge);
            graph.AddVertex(vertex);
            ValidateAdd(graph, vertex);
            graph.RemoveZeroDegreeNodes();
            ValidateAdd(graph, edge);
            Assert.IsFalse(graph.ContainsVertex(vertex));
        }

        private static void ValidateAdd(TGraph graph, TVertex vertex)
        {
            Assert.IsTrue(graph.NumberOfVertices > 0);
            Assert.IsTrue(graph.ContainsVertex(vertex));
            Assert.IsTrue(graph.Vertices.Contains(vertex));
        }

        private static void ValidateAdd(TGraph graph, IEdge<TVertex, TEdge> edge)
        {
            ValidateAdd(graph, edge.Source);
            ValidateAdd(graph, edge.Dest);
            Assert.IsTrue(graph.GetOutgoingEdges(edge.Source).Contains(edge.Weight));
            Assert.IsTrue(graph.GetIncomingEdges(edge.Dest).Contains(edge.Weight));
            Assert.IsTrue(graph.GetParents(edge.Dest).Contains(edge.Source));
            Assert.IsTrue(graph.GetChildren(edge.Source).Contains(edge.Dest));
            Assert.IsTrue(graph.GetNeighbors(edge.Dest).Contains(edge.Source));
            Assert.IsTrue(graph.GetNeighbors(edge.Source).Contains(edge.Dest));
            Assert.IsTrue(graph.IsEdge(edge.Source, edge.Dest));
            Assert.IsTrue(graph.IsNeighbor(edge.Source, edge.Dest));
            Assert.IsTrue(graph.IsNeighbor(edge.Dest, edge.Source));
            Assert.IsTrue(graph.Edges.Contains(edge.Weight));
            Assert.IsFalse(graph.IsEmpty());
            Assert.IsTrue(graph.GetEdges(edge.Source, edge.Dest).Contains(edge.Weight));
        }
    }

    [TestClass]
    public class DirectedMultiGraphTests_ValueType : DirectedMultiGraphTests<DirectedMultiGraph<string, int>, string, int>
    {
        protected override string NonExistentVertex => "TestString";

        protected override IEdge<string, int> NonExistentEdge => new Edge<string, int>
        {
            Dest = "TestDest",
            Source = "TestSource",
            Weight = 1000
        };

        protected override DirectedMultiGraph<string, int> GraphFactory => new DirectedMultiGraph<string, int>();

        protected override IEnumerable<IEdge<string, int>> InitialEdges => TestDataFactory.ValueTypeEdges;

    }

    [TestClass]
    public class DirectedMultiGraphTests_Load : DirectedMultiGraphTests<DirectedMultiGraph<string, int>, string, int>
    {
        protected override string NonExistentVertex => "ABCDEFG";
        protected override IEdge<string, int> NonExistentEdge => new Edge<string, int>
        {
            Dest = "ABCD",
            Source = "FGHI",
            Weight = 1000
        };

        protected override DirectedMultiGraph<string, int> GraphFactory => new DirectedMultiGraph<string, int>();

        protected override IEnumerable<IEdge<string, int>> InitialEdges { get; } = TestDataFactory.RandomEdges(500000, 50000).ToList();
    }
}
