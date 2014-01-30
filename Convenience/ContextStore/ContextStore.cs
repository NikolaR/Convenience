using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;

namespace Convenience
{
    public class ContextStore
    {
        private static readonly WcfContextStore _wcfCtx = new WcfContextStore();
        private static readonly ThreadStaticContextStore _tsCtx = new ThreadStaticContextStore();
        private static CustomContextStore _customCtx;
        private static ContextStoreType _storeType;

        static ContextStore()
        {
            StoreType = ContextStoreType.Auto;
        }

        /// <summary>
        /// Gets or sets type of current backing context store. Setting custom backing context store
        /// directly is not allowed. Use <c>UseCustomContextStore</c> method instead. By default it's
        /// set to Auto.
        /// </summary>
        public static ContextStoreType StoreType
        {
            get { return _storeType; }
            set
            {
                if (value == ContextStoreType.Custom && _storeType != ContextStoreType.Custom)
                    throw new Exception("Custom context store type cannot be set directly. Use UseCustomContextStore method.");
                _storeType = value;
            }
        }

        public static void UseCustomContextStore(CustomContextStore customContextStore)
        {
            _customCtx = customContextStore;
            _storeType = ContextStoreType.Custom;
        }

        public static void UseCustomContextStore(Func<IDictionary<string, object>> contextDictionaryGetter)
        {
            _customCtx = new CustomContextStore(contextDictionaryGetter);
            _storeType = ContextStoreType.Custom;
        }

        protected static IContextStore Store
        {
            get
            {
                if (StoreType == ContextStoreType.Auto)
                {
                    if (IsWcfRequest)
                        return _wcfCtx;
                    else
                        return _tsCtx;
                }
                else if (StoreType == ContextStoreType.ThreadStatic)
                    return _tsCtx;
                else if (StoreType == ContextStoreType.Wcf)
                    return _wcfCtx;
                throw new InvalidOperationException("Unknown backing data store type is set");
            }
        }

        public static bool IsWcfRequest
        {
            get
            {
                return (
                    OperationContext.Current != null &&
                    OperationContext.Current.InstanceContext != null
                );
            }
        }

        public static bool TryGet<T>(string key, out T value)
        {
            return Store.TryGet<T>(key, out value);
        }

        public static T Get<T>(string key)
        {
            return Store.Get<T>(key);
        }

        public static void Set<T>(string key, T value)
        {
            Store.Set<T>(key, value);
        }
    }

    public enum ContextStoreType
    {
        Auto,
        Wcf,
        ThreadStatic,
        Custom
    }
}
