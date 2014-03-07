using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Extends functionality of .NET collections
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>Returns the number of elements in a sequence.</summary>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <param name="source">A sequence that contains elements to be counted.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static int Count(this IEnumerable source)
        {
            AssertUtils.NotNull(() => source);
            ICollection collection = source as ICollection;
            if (collection != null)
                return collection.Count;
            int num = 0;
            checked
            {
                IEnumerator enumerator = source.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                        num++;
                }
                finally
                {
                    var disposableEnumerator = enumerator as IDisposable;
                    if (disposableEnumerator != null)
                        disposableEnumerator.Dispose();
                }
                return num;
            }
        }

        /// <summary>
        /// Sorts observable collection by provided key selector
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null) return;

            Comparer<TKey> comparer = Comparer<TKey>.Default;

            for (int i = source.Count - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    TSource o1 = source[j - 1];
                    TSource o2 = source[j];
                    if (comparer.Compare(keySelector(o1), keySelector(o2)) > 0)
                        source.Move(j - 1, j);
                }
            }
        }

        /// <summary>
        /// Sorts observable collection by provided key selector
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, TKey, int> comparer)
        {
            if (source == null) return;

            for (int i = source.Count - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    TSource o1 = source[j - 1];
                    TSource o2 = source[j];
                    if (comparer(keySelector(o1), keySelector(o2)) > 0)
                        source.Move(j - 1, j);
                }
            }
        }
    }
}
