using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    public interface IContextStore
    {
        bool TryGet<T>(string key, out T value);
        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Remove(string key);
    }
}
