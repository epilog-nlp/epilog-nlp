using System;
using System.Collections.Generic;
using System.Text;
using Epilog.Text.Contracts;

namespace Epilog.Text.Tagging.Contracts
{

    public interface ITag<TContent>
    {
        ILabel Label { get; }

        TContent TaggedValue { get; }
    }
}
