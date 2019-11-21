using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Classification.Contracts
{
    public interface ILabeled<TLabel> where TLabel : ILabel
    {
        TLabel PrimaryLabel { get; }

        IEnumerable<TLabel> Labels { get; }
    }
}
