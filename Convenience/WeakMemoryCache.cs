using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// In-memory cache of objects which uses weak references to allow early memory collection
    /// of objects which are no longer in use. When memory load is high, this allows GC to collect
    /// objects which are no longer necessary.
    /// </summary>
    public class WeakMemoryCache
    {
        private readonly object _nullRef = new object();
        private readonly WeakReference _nullWeakRef = new WeakReference(new object());
        private Dictionary<object, WeakReference> _lookup;

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeakMemoryCache()
        {
            _lookup = new Dictionary<object, WeakReference>();
        }

        /// <summary>
        /// Adds a <paramref name="value"/> to cache under specified <paramref name="key"/>, or updates it
        /// if key is already present in cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
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

        /// <summary>
        /// Gets value under specified <paramref name="key"/> from cache and returns it. If no value
        /// has been added to cache under this key, or if it has been collected by GC, returns <c>null</c>.
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Cached item if found; otherwise <c>null</c></returns>
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

        /// <summary>
        /// Gets value under specified <paramref name="key"/> from cache and returns it. If no value
        /// has been added to cache under this key, or if it has been collected by GC, returns <c>null</c>.
        /// </summary>
        /// <typeparam name="TValue">Type to which to cast found item</typeparam>
        /// <param name="key">Key of cached item</param>
        /// <returns>Cached item if found; otherwise <c>null</c></returns>
        /// <exception cref="InvalidCastException">If found item cannot be cast to requested type</exception>
        public TValue Get<TValue>(object key)
        {
            return (TValue)Get(key);
        }

        /// <summary>
        /// Gets value under specified <paramref name="key"/> from cache and returns it. If no value
        /// has been added to cache under this key, or if it has been collected by GC, returns <c>null</c>.
        /// If found item is not of requested type, returns default value for that type.
        /// </summary>
        /// <typeparam name="TValue">Type to which to cast found item</typeparam>
        /// <param name="key">Key of cached item</param>
        /// <returns>Cached item if found; otherwise <c>null</c></returns>
        public TValue GetSafeCast<TValue>(object key)
        {
            if (key is TValue)
                return (TValue)Get(key);
            return default(TValue);
        }

        /// <summary>
        /// Checks whether provided <paramref name="key"/> is present in cache and returns <c>true</c> if it
        /// is; otherwise returns <c>false</c>. This is not a guarantee that item is alive in cache. It may have
        /// been collected by GC and weak reference may still be held in cache.
        /// </summary>
        /// <param name="key">Key to look for in cache</param>
        /// <returns><c>true</c> if key is found in cache; otherwise <c>false</c></returns>
        public bool ContainsKey(object key)
        {
            if (key == null)
                key = _nullWeakRef;
            return _lookup.ContainsKey(key);
        }

        /// <summary>
        /// Checks whether provided <paramref name="value"/> is present in cache and returns <c>true</c> if it
        /// is; otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="value">Value to look for in cache</param>
        /// <returns><c>true</c> if value is present in cache; otherwise <c>false</c></returns>
        public bool ContainsValue(object value)
        {
            if (value == null)
                value = _nullWeakRef;
            return _lookup.Any(kvp => kvp.Value.IsAlive && kvp.Value.Target == value);
        }

        /// <summary>
        /// Clears content of cache instance
        /// </summary>
        public void Clear()
        {
            _lookup = new Dictionary<object, WeakReference>();
        }

        /// <summary>
        /// Purges references to GC collected items.
        /// </summary>
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
