using System;
using System.Collections.Generic;
using System.Text;

namespace Epilog.Taggers
{
    public class TaggerException : Exception
    {
        public TaggerException(string message) : base(message)
        {
        }

        public TaggerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TaggerException()
        {
        }
    }
}
