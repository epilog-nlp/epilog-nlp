using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Collections.Graphs
{
    public interface IEdge<TVertex,TWeight>
    {
        TVertex Source { get; }

        TVertex Dest { get; }

        TWeight Weight { get; }

        void Deconstruct(out TVertex source, out TVertex dest, out TWeight weight);

    }
}
