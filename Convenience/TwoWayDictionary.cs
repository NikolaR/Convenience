using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Collection of paired values which provides way to look up one value of the pair by the other value.
    /// </summary>
    /// <typeparam name="T1">Type of first dictionary key</typeparam>
    /// <typeparam name="T2">Type of second dictionary key</typeparam>
    public class TwoWayDictionary<T1, T2> : IEnumerable<TwoWayDictionary<T1, T2>.KeyPair>
    {
        #region Fields

        private Dictionary<T1, T2> _dict1 = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _dict2 = new Dictionary<T2, T1>();

        #endregion Fields

        #region Ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TwoWayDictionary()
        { }


        /// <summary>
        /// Constructs two way dictionary with provided collections. Each element in first collection
        /// is matched up with element on same index in second collection. Collections must contain
        /// same number of elements and must not contain <c>null</c> values.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public TwoWayDictionary(IEnumerable<T1> first, IEnumerable<T2> second)
        {
            AssertUtils.NotNull(first, "first");
            AssertUtils.NotNull(second, "second");
            var fst = first.ToArray();
            var snd = second.ToArray();
            if (fst.Length != snd.Length)
                throw new ArgumentException("Collections of keys and values must have same number of elements");

            for (int i = 0; i < fst.Length; i++)
                if (fst[i] == null || snd[i] == null)
                    throw new ArgumentException("Collections of keys and values must not contain null values");

            for (int i = 0; i < fst.Length; i++)
                AddByFirst(fst[i], snd[i]);
        }

        #endregion Ctors

        #region Properties

        /// <summary>
        /// Returns number of pairs in current collection.
        /// </summary>
        public int Count
        {
            get { return _dict1.Count; }
        }

        /// <summary>
        /// Returns value stored in the collection paired with specified key. 
        /// Can be used only is T1 and T2 are different types and
        /// are not inheriting one from another. Otherwise use GetByX methods.
        /// </summary>
        /// <param name="key">Key for which to find value in the collection.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If key is <c>null</c>.</exception>
        public T2 this[T1 key]
        {
            get { return _dict1[key]; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value in a TwoWayDictionary cannot be null");
                if (key == null)
                    throw new ArgumentNullException("key of a TwoWayDictionary cannot be null");
                _dict1[key] = value;
                _dict2[value] = key;
            }
        }

        /// <summary>
        /// Returns value stored in the collection paired with specified key. 
        /// Can be used only is T1 and T2 are different types and
        /// are not inheriting one from another. Otherwise use GetByX methods.
        /// </summary>
        /// <param name="key">Key for which to find value in the collection.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If key is <c>null</c>.</exception>
        public T1 this[T2 key]
        {
            get { return _dict2[key]; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value in a TwoWayDictionary cannot be null");
                if (key == null)
                    throw new ArgumentNullException("key of a TwoWayDictionary cannot be null");
                _dict2[key] = value;
                _dict1[value] = key;
            }
        }

        internal Dictionary<T1, T2> ByFirst
        {
            get { return _dict1; }
        }

        internal Dictionary<T2, T1> BySecond
        {
            get { return _dict2; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Empties the collection.
        /// </summary>
        public void Clear()
        {
            _dict1.Clear();
            _dict2.Clear();
        }

        /// <summary>
        /// Adds values to current collection.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddByFirst(T1 key, T2 value)
        {
            if (_dict1.ContainsKey(key) || _dict2.ContainsKey(value))
                throw new ArgumentException("Duplicate key or value");
            _dict1[key] = value;
            _dict2[value] = key;
        }

        /// <summary>
        /// Adds values to current collection.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddBySecond(T2 key, T1 value)
        {
            if (_dict2.ContainsKey(key) || _dict1.ContainsKey(value))
                throw new ArgumentException("Duplicate key or value");
            _dict2[key] = value;
            _dict1[value] = key;
        }

        /// <summary>
        /// Adds values to current collection. Can be used only is T1 and T2 are different types and
        /// are not inheriting one from another. Otherwise use AddByX methods.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(T1 key, T2 value)
        {
            AddByFirst(key, value);
        }

        /// <summary>
        /// Adds values to current collection. Can be used only is T1 and T2 are different types and
        /// are not inheriting one from another. Otherwise use AddByX methods.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(T2 key, T1 value)
        {
            AddBySecond(key, value);
        }

        /// <summary>
        /// Finds value by first key and returns it.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Value found under specified key</returns>
        public T2 GetByFirst(T1 key)
        {
            return _dict1[key];
        }

        /// <summary>
        /// Finds value by second key and returns it.
        /// </summary>
        /// <param name="key">Key to look for</param>
        /// <returns>Value found under specified key</returns>
        public T1 GetBySecond(T2 key)
        {
            return _dict2[key];
        }

        /// <summary>
        /// Removes entry identified by first key if it exists; otherwise just returns.
        /// </summary>
        /// <param name="key">Key for which to remove entry from dictionary</param>
        public void RemoveByFirst(T1 key)
        {
            T2 val;
            if (!_dict1.TryGetValue(key, out val))
            {
                _dict1.Remove(key);
                _dict2.Remove(val);
            }
        }

        /// <summary>
        /// Removes entry identified by second key if it exists; otherwise just returns.
        /// </summary>
        /// <param name="key">Key for which to remove entry from dictionary</param>
        public void RemoveBySecond(T2 key)
        {
            T1 val;
            if (!_dict2.TryGetValue(key, out val))
            {
                _dict2.Remove(key);
                _dict1.Remove(val);
            }
        }

        /// <summary>
        /// Removes entry identified by first key if it exists; otherwise just returns.
        /// Alias to <see cref="RemoveByFirst"/> when two-way dictionary generic parameters are different.
        /// </summary>
        /// <param name="key">Key for which to remove entry from dictionary</param>
        public void Remove(T1 key)
        {
            RemoveByFirst(key);
        }

        /// <summary>
        /// Removes entry identified by second key if it exists; otherwise just returns.
        /// Alias to <see cref="RemoveBySecond"/> when two-way dictionary generic parameters are different.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(T2 key)
        {
            RemoveBySecond(key);
        }

        /// <summary>
        /// Checks whether specified object is present as first key.
        /// </summary>
        /// <param name="obj">Object which may exist in dictionary as first key</param>
        /// <returns><c>true</c> if object exists as first key; otherwise <c>false</c></returns>
        public bool ContainsByFirst(T1 obj)
        {
            return _dict1.ContainsKey(obj);
        }

        /// <summary>
        /// Checks whether specified object is present as second key.
        /// </summary>
        /// <param name="obj">Object which may exist in dictionary as second key</param>
        /// <returns><c>true</c> if object exists as second key; otherwise <c>false</c></returns>
        public bool ContainsBySecond(T2 obj)
        {
            return _dict2.ContainsKey(obj);
        }

        /// <summary>
        /// Checks whether specified object is present as first key.
        /// Alias to <see cref="ContainsByFirst"/> when two-way dictionary generic parameters are different.
        /// </summary>
        /// <param name="obj">Object which may exist in dictionary as fist key</param>
        /// <returns><c>true</c> if object exists as first key; otherwise <c>false</c></returns>
        public bool Contains(T1 obj)
        {
            return ContainsByFirst(obj);
        }

        /// <summary>
        /// Checks whether specified object is present as second key.
        /// Alias to <see cref="ContainsBySecond"/> when two-way dictionary generic parameters are different.
        /// </summary>
        /// <param name="obj">Object which may exist in dictionary as second key</param>
        /// <returns><c>true</c> if object exists as second key; otherwise <c>false</c></returns>
        public bool Contains(T2 obj)
        {
            return ContainsBySecond(obj);
        }

        /// <summary>
        /// Tries to get value with specified <paramref name="key"/> as first key and places it in
        /// <paramref name="value"/>. Returns <c>true</c> if key is found and value is set; otherwise
        /// returns <c>false</c>.
        /// </summary>
        /// <param name="key">First key used to search for value</param>
        /// <param name="value">Reference in which to set found value</param>
        /// <returns><c>true</c> if key is found and value is set; otherwise <c>false</c></returns>
        public bool TryGetValueByFirst(T1 key, out T2 value)
        {
            return _dict1.TryGetValue(key, out value);
        }

        /// <summary>
        /// Tries to get value with specified <paramref name="key"/> as second key and places it in
        /// <paramref name="value"/>. Returns <c>true</c> if key is found and value is set; otherwise
        /// returns <c>false</c>.
        /// </summary>
        /// <param name="key">Second key used to search for value</param>
        /// <param name="value">Reference in which to set found value</param>
        /// <returns><c>true</c> if key is found and value is set; otherwise <c>false</c></returns>
        public bool TryGetValueBySecond(T2 key, out T1 value)
        {
            return _dict2.TryGetValue(key, out value);
        }

        /// <summary>
        /// Tries to get value with specified <paramref name="key"/> as first key and places it in
        /// <paramref name="value"/>. Returns <c>true</c> if key is found and value is set; otherwise
        /// returns <c>false</c>.
        /// Alias to <see cref="TryGetValueByFirst"/> when two-way dictionary generic type parameters are different.
        /// </summary>
        /// <param name="key">First key used to search for value</param>
        /// <param name="value">Reference in which to set found value</param>
        /// <returns><c>true</c> if key is found and value is set; otherwise <c>false</c></returns>
        public bool TryGetValue(T1 key, out T2 value)
        {
            return TryGetValueByFirst(key, out value);
        }

        /// <summary>
        /// Tries to get value with specified <paramref name="key"/> as second key and places it in
        /// <paramref name="value"/>. Returns <c>true</c> if key is found and value is set; otherwise
        /// returns <c>false</c>.
        /// Alias to <see cref="TryGetValueBySecond"/> when two-way dictionary generic type parameters are different.
        /// </summary>
        /// <param name="key">Second key used to search for value</param>
        /// <param name="value">Reference in which to set found value</param>
        /// <returns><c>true</c> if key is found and value is set; otherwise <c>false</c></returns>
        public bool TryGetValue(T2 key, out T1 value)
        {
            return TryGetValueBySecond(key, out value);
        }

        #endregion Methods

        #region Tuple class

        /// <summary>
        /// Entry of a two-way dictionary
        /// </summary>
        public class KeyPair
        {
            /// <summary>
            /// First key
            /// </summary>
            public T1 First { get; set; }

            /// <summary>
            /// Second key
            /// </summary>
            public T2 Second { get; set; }
        }

        #endregion Tuple class

        #region Enumerator class

        /// <summary>
        /// Enumerator of <seealso cref="TwoWayDictionary{T1,T2}"/>
        /// </summary>
        public class TwoWayDictionaryEnumerator : IEnumerator<KeyPair>
        {
            private TwoWayDictionary<T1, T2> _dictionary;
            private IEnumerator _enumerator;

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="dictionary">Dictionary over which to enumerate</param>
            public TwoWayDictionaryEnumerator(TwoWayDictionary<T1, T2> dictionary)
            {
                _dictionary = dictionary;
                _enumerator = _dictionary._dict1.GetEnumerator();
            }

            /// <summary>
            /// Cleans up after enumerator and releases references to underlying collection
            /// </summary>
            public void Dispose()
            {
                _dictionary = null;
                _enumerator = null;
            }

            /// <summary>
            /// Advances enumerator to the next element of the collection
            /// </summary>
            /// <returns><c>true</c> if enumerator has successfully advanced the pointer; <c>false</c> if it has passed the last element</returns>
            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            /// <summary>
            /// Sets enumerator to it's initial position, which is before first element
            /// </summary>
            public void Reset()
            {
                _enumerator.Reset();
            }

            /// <summary>
            /// Pointer to current entry of the dictionary
            /// </summary>
            public KeyPair Current
            {
                get
                {
                    KeyValuePair<T1, T2> curr = (KeyValuePair<T1, T2>)(_enumerator.Current);
                    return new KeyPair() { First = curr.Key, Second = curr.Value };
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }

        #endregion Enumerator class

        /// <summary>
        /// Returns enumerator of the dictionary
        /// </summary>
        /// <returns>Enumerator of current dictionary</returns>
        public IEnumerator<KeyPair> GetEnumerator()
        {
            return new TwoWayDictionaryEnumerator(this);
        }

        /// <summary>
        /// Returns enumerator of the dictionary
        /// </summary>
        /// <returns>Enumerator of current dictionary</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
