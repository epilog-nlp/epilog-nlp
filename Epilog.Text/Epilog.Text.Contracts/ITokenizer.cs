using System.Collections.Generic;

namespace Epilog.Text.Contracts
{
    public interface ITokenizer<out TToken> where TToken : IToken
    {
        IEnumerable<TToken> Tokenize(string text);
    }
}
