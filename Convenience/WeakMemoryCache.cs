using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace Convenience
{
    public class WeakMemoryCache
    {
        private readonly object _nullRef = new object();
        private readonly WeakReference _nullWeakRef = new WeakReference(new object());
        private Dictionary<object, WeakReference> _lookup;

        private readonly KeyValuePair<object, WeakReference> _defaultKvp =
            default(KeyValuePair<object, WeakReference>);

        public WeakMemoryCache()
        {
            _lookup = new Dictionary<object, WeakReference>();
        }

        public void Set(object key, object value)
        {
            object rKey = key;
            if (key == null)
                rKey = _nullRef;

            WeakReference rVal;
            if (value == null)
                rVal = _nullWeakRef;
            else
                rVal = new WeakReference(value);

            _lookup[rKey] = rVal;
        }

        public object Get(object key)
        {
            if (key == null)
                key = _nullRef;
            WeakReference val;
            if (!_lookup.TryGetValue(key, out val) || !val.IsAlive)
                throw new KeyNotFoundException("Could not find specified key in cache");
            if (!val.IsAlive)
                _lookup.Remove(key);

            if (val.Target == _nullWeakRef)
                return null;
            return val.Target;
        }

        public TValue Get<TValue>(object key)
        {
            return (TValue)Get(key);
        }

        public TValue GetSafeCast<TValue>(object key)
        {
            if (key is TValue)
                return (TValue)Get(key);
            return default(TValue);
        }

        public bool ContainsKey(object key)
        {
            if (key == null)
                key = _nullWeakRef;
            return _lookup.ContainsKey(key);
        }

        public bool ContainsValue(object value)
        {
            if (value == null)
                value = _nullWeakRef;
            return _lookup.Any(kvp => kvp.Value.IsAlive && kvp.Value.Target == value);
        }

        public void Clear()
        {
            _lookup = new Dictionary<object, WeakReference>();
        }

        public void Purge()
        {
            var toRemove = new List<object>();
            foreach (var kvp in _lookup)
            {
                if (!kvp.Value.IsAlive)
                    toRemove.Add(kvp.Key);
            }
            foreach (var removeKey in toRemove)
                _lookup.Remove(removeKey);
        }
    }
}
