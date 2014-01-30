
using System.ServiceModel;

namespace Convenience
{
    public class WcfContextStore : IContextStore
    {
        private static object _syncRoot = new object();

        private static WcfDataContext Data
        {
            get
            {
                lock (_syncRoot)
                {
                    AssertUtils.NotNull(OperationContext.Current, "OperationContext.Current");
                    AssertUtils.NotNull(OperationContext.Current.InstanceContext, "OperationContext.Current.InstanceContext");
                    var data = OperationContext.Current.InstanceContext.Extensions.Find<WcfDataContext>();
                    if (data == null)
                        throw new ContextStoreException("WcfDataContext is not initialized on WCF service. WcfDataContextAttribute behavior must be applied");
                    return data;
                }
            }
        }

        public bool TryGet<T>(string key, out T value)
        {
            return Data.TryGetValue<T>(key, out value);
        }

        public T Get<T>(string key)
        {
            return Data.Get<T>(key);
        }

        public void Set<T>(string key, T value)
        {
            Data.Set(key, value);
        }

        public void Remove(string key)
        {
            Data.Remove(key);
        }
    }
}
