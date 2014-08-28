using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Facility for creating and caching result based on input parameter. After initial call
    /// for a result, all subsequent calls for same parameter return cached result. This can be
    /// used to improve performance for slow factory methods.
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class CachingFactory<TArg, TResult>
    {
        private readonly Func<TArg, TResult> _factory;
        private readonly Dictionary<TArg, TResult> _cache = new Dictionary<TArg, TResult>();

        /// <summary>
        /// Creates an instance of caching factory with specified factory method.
        /// </summary>
        /// <param name="factory">Factory function used to create elements which are currently not available in cache</param>
        public CachingFactory(Func<TArg, TResult> factory)
        {
            AssertUtils.NotNull(factory, "factory");
            _factory = factory;
        }

        /// <summary>
        /// Returns result for specified argument from cache or by calling factory method.
        /// </summary>
        /// <param name="arg">Argument for which result will be retrieved.</param>
        /// <returns>Result of factory method for provided argument.</returns>
        public TResult Get(TArg arg)
        {
            TResult res;
            if (!_cache.TryGetValue(arg, out res))
            {
                res = _factory(arg);
                _cache[arg] = res;
            }
            return res;
        }

        /// <summary>
        /// Returns result for specified argument from cache or by calling factory method.
        /// </summary>
        /// <param name="arg">Argument for which result will be retrieved.</param>
        /// <returns>Result of factory method for provided argument.</returns>
        public TResult this[TArg arg]
        {
            get { return Get(arg); }
        }

        /// <summary>
        /// Evicts from cache result for specified argument.
        /// </summary>
        /// <param name="arg">Argument for which result will be evicted from cache.</param>
        public void Evict(TArg arg)
        {
            _cache.Remove(arg);
        }
    }

    /// <summary>
    /// Facility for creating and caching result based on input parameter. After initial call
    /// for a result, all subsequent calls for same parameter return cached result. This can be
    /// used to improve performance for slow factory methods.
    /// </summary>
    /// <typeparam name="TArg1"></typeparam>
    /// <typeparam name="TArg2"></typeparam>
    /// <typeparam name="TResult">Type of result.</typeparam>
    public class CachingFactory<TArg1, TArg2, TResult>
    {
        private readonly Func<TArg1, TArg2, TResult> _factory;
        private readonly Dictionary<CompositeKey, TResult> _cache = new Dictionary<CompositeKey, TResult>();

        /// <summary>
        /// Creates an instance of caching factory with specified factory method.
        /// </summary>
        /// <param name="factory">Factory function used to create elements which are currently not available in cache</param>
        public CachingFactory(Func<TArg1, TArg2, TResult> factory)
        {
            AssertUtils.NotNull(factory, "factory");
            _factory = factory;
        }

        /// <summary>
        /// Returns result for specified argument from cache or by calling factory method.
        /// </summary>
        /// <param name="arg1">Argument for which result will be retrieved.</param>
        /// <param name="arg2">Argument for which result will be retrieved.</param>
        /// <returns>Result of factory method for provided argument.</returns>
        public TResult Get(TArg1 arg1, TArg2 arg2)
        {
            var key = new CompositeKey(arg1, arg2, null);
            TResult res;
            if (!_cache.TryGetValue(key, out res))
            {
                res = _factory(arg1, arg2);
                _cache[key] = res;
            }
            return res;
        }

        /// <summary>
        /// Evicts from cache result for specified argument.
        /// </summary>
        /// <param name="arg1">Argument for which result will be evicted from cache.</param>
        /// <param name="arg2">Argument for which result will be evicted from cache.</param>
        public void Evict(TArg1 arg1, TArg2 arg2)
        {
            _cache.Remove(new CompositeKey(arg1, arg2, null));
        }
    }

    internal struct CompositeKey
    {
        public CompositeKey(object key1, object key2, object key3)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
        }

        public object Key1;
        public object Key2;
        public object Key3;
    }
}
