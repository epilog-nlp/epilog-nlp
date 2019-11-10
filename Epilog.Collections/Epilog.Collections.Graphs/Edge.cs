using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Collections.Graphs
{
    public class Edge<TVertex,TWeight> : IEdge<TVertex,TWeight>
    {
        public TVertex Source { get; set; }

        public TVertex Dest { get; set; }

        public TWeight Weight { get; set; }

        public void Deconstruct(out TVertex source, out TVertex dest, out TWeight weight)
        {
            source = Source;
            dest = Dest;
            weight = Weight;
        }
    }
}
