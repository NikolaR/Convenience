using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Convenience
{
    public class ThreadStaticContextStore : DictionaryContextStoreBase
    {
        [ThreadStatic]
        private static Dictionary<string, object> _data;

        protected override IDictionary<string, object> Data
        {
            get
            {
                Interlocked.CompareExchange(ref _data, new Dictionary<string, object>(), null);
                return _data;
            }
        }
    }
}
