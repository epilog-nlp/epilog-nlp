using System.Collections.Generic;

namespace Epilog.Collections.Graphs
{
    public static class GraphExtensions
    {
        //public static IEnumerable<IEnumerable<TVertex>> ConnectedComponents<TVertex,TEdge>(this IGraph<TVertex,TEdge> graph)
        //{
        //    var connectedComponents = new List<IEnumerable<TVertex>>();
        //    var todo = new LinkedList<TVertex>();
        //    var verticesLeft = new LinkedList<TVertex>(graph.Vertices);
        //    while(verticesLeft.Count > 0)
        //    {
        //        todo.AddLast(verticesLeft.First);
        //        verticesLeft.RemoveFirst();
        //        connectedComponents.Add(BreadthFirstSearch(todo, graph, verticesLeft));
        //    }
        //    return connectedComponents;
        //}



        //private static IEnumerable<TVertex> BreadthFirstSearch<TVertex,TEdge>(LinkedList<TVertex> todo, IGraph<TVertex,TEdge> graph, LinkedList<TVertex> verticesLeft)
        //{
        //    var cc = new LinkedList<TVertex>();
        //    while(todo.Count > 0)
        //    {
        //        var node = todo.First;
        //        cc.AddLast(node);
        //        todo.RemoveFirst();

        //        foreach(var neighbor in graph.GetNeighbors(node.Value))
        //        {
        //            if(verticesLeft.Contains(neighbor))
        //            {
        //                cc.AddLast(neighbor);
        //                todo.AddLast(neighbor);
        //                verticesLeft.Remove(neighbor);
        //            }
        //        }
        //    }

        //    return cc;
        //}

        public static ImmutableDirectedMultiGraph<TVertex, TEdge> ToImmutableGraph<TVertex, TEdge>(this ReadOnlyDirectedMultiGraph<TVertex, TEdge> graph)
            => new ImmutableDirectedMultiGraph<TVertex, TEdge>(graph.OutgoingEdges, graph.IncomingEdges);

        public static IEnumerable<IEnumerable<TVertex>> ConnectedComponents<TVertex, TEdge>(this IDirectedMultigraph<TVertex, TEdge> graph)
        {
            var todo = new List<TVertex>();
            var verticesLeft = new List<TVertex>(graph.Vertices);
            while (verticesLeft.Count > 0)
            {
                todo.Add(verticesLeft[0]);
                verticesLeft.RemoveAt(0);
                yield return BreadthFirstSearch(todo, graph, verticesLeft);
            }
        }

        private static IEnumerable<TVertex> BreadthFirstSearch<TVertex, TEdge>(IList<TVertex> todo, IDirectedMultigraph<TVertex, TEdge> graph, IList<TVertex> verticesLeft)
        {
            while (todo.Count > 0)
            {
                yield return todo[0];
                
                foreach (var neighbor in graph.GetNeighbors(todo[0]))
                {
                    if (verticesLeft.Contains(neighbor))
                    {
                        yield return neighbor;
                        todo.Add(neighbor);
                        verticesLeft.Remove(neighbor);
                    }
                }

                todo.RemoveAt(0);
            }
        }
    }
}
