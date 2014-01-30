using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Convenience
{
    internal class WcfDataContext : IExtension<InstanceContext>
    {
        private Dictionary<string, object> m_Data;

        public WcfDataContext()
        {
            m_Data = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Data
        {
            get { return m_Data; }
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            value = default(T);
            object val;
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
            if (!TryGetValue(key, out val))
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

        public void Attach(InstanceContext owner)
        { }

        public void Detach(InstanceContext owner)
        { }
    }

    public class WcfDataContextAttribute : Attribute, IContractBehavior
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceContextInitializers.Add(new WcfDataInstanceContextInitializer());
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        { }
    }

    public class WcfDataInstanceContextInitializer : IInstanceContextInitializer
    {
        public void Initialize(InstanceContext instanceContext, Message message)
        {
            if (instanceContext.Extensions.Find<WcfDataContext>() == null)
                instanceContext.Extensions.Add(new WcfDataContext());
        }
    }
}
