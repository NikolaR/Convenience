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
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class TwoWayDictionary<T1, T2> : IEnumerable<TwoWayDictionary<T1, T2>.Tuple>
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

        public T2 GetByFirst(T1 key)
        {
            return _dict1[key];
        }

        public T1 GetBySecond(T2 key)
        {
            return _dict2[key];
        }

        public void RemoveByFirst(T1 key)
        {
            T2 val;
            if (!_dict1.TryGetValue(key, out val))
            {
                _dict1.Remove(key);
                _dict2.Remove(val);
            }
        }

        public void RemoveBySecond(T2 key)
        {
            T1 val;
            if (!_dict2.TryGetValue(key, out val))
            {
                _dict2.Remove(key);
                _dict1.Remove(val);
            }
        }

        public void Remove(T1 key)
        {
            RemoveByFirst(key);
        }

        public void Remove(T2 key)
        {
            RemoveBySecond(key);
        }

        public bool ContainsByFirst(T1 obj)
        {
            return _dict1.ContainsKey(obj);
        }

        public bool ContainsBySecond(T2 obj)
        {
            return _dict2.ContainsKey(obj);
        }

        public bool Contains(T1 obj)
        {
            return ContainsByFirst(obj);
        }

        public bool Contains(T2 obj)
        {
            return ContainsBySecond(obj);
        }

        public bool TryGetValueByFirst(T1 key, out T2 value)
        {
            return _dict1.TryGetValue(key, out value);
        }

        public bool TryGetValueBySecond(T2 key, out T1 value)
        {
            return _dict2.TryGetValue(key, out value);
        }

        public bool TryGetValue(T1 key, out T2 value)
        {
            return TryGetValueByFirst(key, out value);
        }

        public bool TryGetValue(T2 key, out T1 value)
        {
            return TryGetValueBySecond(key, out value);
        }

        #endregion Methods

        #region Tuple class

        public class Tuple
        {
            public T1 First { get; set; }
            public T2 Second { get; set; }
        }

        #endregion Tuple class

        #region Enumerator class

        public class TwoWayDictionaryEnumerator : IEnumerator<Tuple>
        {
            private TwoWayDictionary<T1, T2> _dictionary;
            private IEnumerator _enumerator;

            public TwoWayDictionaryEnumerator(TwoWayDictionary<T1, T2> dictionary)
            {
                _dictionary = dictionary;
                _enumerator = _dictionary._dict1.GetEnumerator();
            }

            public void Dispose()
            {
                _dictionary = null;
                _enumerator = null;
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
            }

            public Tuple Current
            {
                get
                {
                    KeyValuePair<T1, T2> curr = (KeyValuePair<T1, T2>)(_enumerator.Current);
                    return new Tuple() { First = curr.Key, Second = curr.Value };
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }

        #endregion Enumerator class

        public IEnumerator<Tuple> GetEnumerator()
        {
            return new TwoWayDictionaryEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
