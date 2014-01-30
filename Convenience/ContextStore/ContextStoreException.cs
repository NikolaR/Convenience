using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    [Serializable]
    public class ContextStoreException : Exception
    {
        public ContextStoreException()
        { }

        public ContextStoreException(string message)
            : base(message)
        { }

        public ContextStoreException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
