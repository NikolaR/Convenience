using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Provides facility to create a singleton of arbitrary class. Target class must have parameterless
    /// constructor.
    /// </summary>
    /// <typeparam name="T">Class for which to create singleton.</typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static readonly T _instance = Activator.CreateInstance<T>();

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static T Instance
        {
            get { return _instance; }
        }
    }
}
