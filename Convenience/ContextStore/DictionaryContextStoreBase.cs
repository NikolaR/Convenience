using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    public abstract class DictionaryContextStoreBase : IContextStore
    {
        protected abstract IDictionary<string, object> Data
        { get; }

        public bool TryGet<T>(string key, out T value)
        {
            object val;
            value = default(T);
            if (!Data.TryGetValue(key, out val))
                return false;
            if (val == null)
                return Data.ContainsKey(key);
            if (!(val is T))
                return false;
            value = (T)val;
            return true;
        }

        public T Get<T>(string key)
        {
            T val;
            if (!TryGet<T>(key, out val))
                return default(T);
            return val;
        }

        public void Set<T>(string key, T value)
        {
            Data[key] = value;
        }

        public void Remove(string key)
        {
            if (Data.ContainsKey(key))
                Data.Remove(key);
        }
    }
}
