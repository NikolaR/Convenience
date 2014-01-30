using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Custom contextual data store which can be instantiated using delegate methods.
    /// Can be useful for unit testing and reusing existing code in scenarios which 
    /// </summary>
    public class CustomContextStore : DictionaryContextStoreBase
    {
        private readonly Func<IDictionary<string, object>> _contextDictionaryGetter;

        public CustomContextStore(Func<IDictionary<string, object>> contextDictionaryGetter)
        {
            AssertUtils.NotNull(() => contextDictionaryGetter);
            _contextDictionaryGetter = contextDictionaryGetter;
        }

        protected override IDictionary<string, object> Data
        {
            get { return _contextDictionaryGetter(); }
        }
    }
}
