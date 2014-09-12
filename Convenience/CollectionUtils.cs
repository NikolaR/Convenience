using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Collection of methods which extend functionality of collections.
    /// </summary>
    public static class CollectionUtils
    {
        /// <summary>
        /// Checks whether size of collections is same and returns <c>true</c> if they
        /// are; otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="col1">First collection to check</param>
        /// <param name="col2">Second colleciton to check</param>
        /// <returns><c>true</c> if collection sizes are same; otherwise <c>false</c>.</returns>
        public static bool SizeEqual(IEnumerable col1, IEnumerable col2)
        {
            AssertUtils.NotNull(col1, "col1");
            AssertUtils.NotNull(col2, "col2");

            return (col1.Count() == col2.Count());
        }

        /// <summary>
        /// Checks whether content of two collections is equal.
        /// </summary>
        /// <param name="col1">First collection to compare.</param>
        /// <param name="col2">Second collection to compare.</param>
        /// <returns><c>true</c> if content of collections is equal; otherwise <c>false</c>.</returns>
        public static bool ContentEqual<T>(IEnumerable<T> col1, IEnumerable<T> col2)
        {
            var comparer = EqualityComparer<T>.Default;
            return ContentEqual<T>(col1, col2, comparer.Equals);
        }

        /// <summary>
        /// Checks whether content of two collections is equal using provided equality comparer.
        /// </summary>
        /// <param name="col1">First collection to compare.</param>
        /// <param name="col2">Second collection to compare.</param>
        /// <param name="comparer">Equality comparer</param>
        /// <returns><c>true</c> if content of collections is equal; otherwise <c>false</c>.</returns>
        public static bool ContentEqual<T>(IEnumerable<T> col1, IEnumerable<T> col2, Func<T, T, bool> comparer)
        {
            AssertUtils.NotNull(() => comparer);

            // Materialize to a List to ensure there won't be multiple enumeration
            col1 = col1.ToList();
            col2 = col2.ToList();
            if (!SizeEqual(col1, col2))
                return false;

            var enumerator1 = col1.GetEnumerator();
            var enumerator2 = col2.GetEnumerator();

            while (enumerator1.MoveNext())
            {
                // If there's no more items in col2, collections are not equal
                if (!enumerator2.MoveNext())
                    return false;
                if (!comparer(enumerator1.Current, enumerator2.Current))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether content of two collections is equivalent using provided equality comparer.
        /// Content is equivalent if both collections contain same elements, ignoring element ordering within
        /// their collections.
        /// </summary>
        /// <param name="col1">First collection to compare.</param>
        /// <param name="col2">Second collection to compare.</param>
        /// <returns><c>true</c> if content of collections is equal; otherwise <c>false</c>.</returns>
        public static bool ContentEquivalent<T>(IEnumerable<T> col1, IEnumerable<T> col2)
        {
            var comparer = EqualityComparer<T>.Default;
            return ContentEquivalent<T>(col1, col2, comparer.Equals);
        }

        /// <summary>
        /// Checks whether content of two collections is equivalent using provided equality comparer.
        /// Content is equivalent if both collections contain same elements, ignoring element ordering within
        /// their collections.
        /// </summary>
        /// <param name="col1">First collection to compare.</param>
        /// <param name="col2">Second collection to compare.</param>
        /// <param name="comparer">Equality comparer</param>
        /// <returns><c>true</c> if content of collections is equal; otherwise <c>false</c>.</returns>
        public static bool ContentEquivalent<T>(IEnumerable<T> col1, IEnumerable<T> col2, Func<T, T, bool> comparer)
        {
            AssertUtils.NotNull(() => comparer);
            var enumerable1 = col1 as T[] ?? col1.ToArray();
            var enumerable2 = col2 as T[] ?? col2.ToArray();
            if (!SizeEqual(enumerable1, enumerable2))
                return false;

            var len = enumerable1.Length;
            for (int i = 0; i < len; i++)
            {
                T val1 = enumerable1[i];
                if (!enumerable2.Any(val2 => comparer(val1, val2)))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether <paramref name="subset"/> collection is subset of <paramref name="superset"/> collection
        /// and returns <c>true</c> if it is; otherwise returns <c>false</c>.
        /// </summary>
        /// <typeparam name="T">Type of collection elements</typeparam>
        /// <param name="subset">Subset collection</param>
        /// <param name="superset">Superset collection</param>
        /// <returns>
        /// <c>true</c> if <paramref name="subset"/> is a subset of <paramref name="superset"/> collection; 
        /// otherwise <c>false</c>.
        /// </returns>
        public static bool IsSubset<T>(IEnumerable<T> subset, IEnumerable<T> superset)
        {
            var comparer = EqualityComparer<T>.Default;
            return IsSubset<T>(subset, superset, comparer.Equals);
        }

        /// <summary>
        /// Checks whether <paramref name="subset"/> collection is subset of <paramref name="superset"/> collection
        /// and returns <c>true</c> if it is; otherwise returns <c>false</c>. Custom comparer is used when comparing
        /// elements of collections.
        /// </summary>
        /// <typeparam name="T">Type of collection elements</typeparam>
        /// <param name="subset">Subset collection</param>
        /// <param name="superset">Superset collection</param>
        /// <param name="comparer">Comparer used to compare collection elements for equality</param>
        /// <returns>
        /// <c>true</c> if <paramref name="subset"/> is a subset of <paramref name="superset"/> collection; 
        /// otherwise <c>false</c>.
        /// </returns>
        public static bool IsSubset<T>(IEnumerable<T> subset, IEnumerable<T> superset, Func<T, T, bool> comparer)
        {
            AssertUtils.NotNull(subset, "subset");
            AssertUtils.NotNull(superset, "superset");
            AssertUtils.NotNull(comparer, "comparer");

            var subsetA = subset as T[] ?? subset.ToArray();
            var supersetA = superset as T[] ?? superset.ToArray();

            var len = subsetA.Length;
            for (int i = 0; i < len; i++)
            {
                T val1 = subsetA[i];
                if (!supersetA.Any(val2 => comparer(val1, val2)))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if collection is not <c>null</c> and if it has at least one item and returns <c>true</c> if so;
        /// otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="collection">Collection to check for existance of elements</param>
        /// <returns><c>true</c> if collection has content and is not <c>null</c>; otherwise <c>false</c>.</returns>
        public static bool HasLength(IEnumerable collection)
        {
            if (collection == null)
                return false;
            var etor = collection.GetEnumerator();
            return etor.MoveNext();
        }

        /// <summary>
        /// Checks if collection is not <c>null</c>, that it has at least one item, and that all contained
        /// items are not <c>null</c> and returns <c>true</c> if so; otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="collection">Collection to check for existance of content.</param>
        /// <returns><c>true</c> if collection has items and none of it's items are <c>null</c>; otherwise <c>false</c>.</returns>
        public static bool HasContent(IEnumerable collection)
        {
            if (collection == null)
                return false;
            var etor = collection.GetEnumerator();
            if (!etor.MoveNext())
                return false;
            do
            {
                if (etor.Current == null)
                    return false;
            } while (etor.MoveNext());
            return true;
        }

        /// <summary>
        /// Checks if collection contains at least one <c>null</c> item and returns <c>true</c>; otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="collection">Collection which may contain <c>null</c> item.</param>
        /// <returns><c>true</c> if collection contains <c>null</c> item; otherwise <c>false</c>.</returns>
        public static bool ContainsNull(IEnumerable collection)
        {
            if (collection == null)
                return false;
            var etor = collection.GetEnumerator();
            while (etor.MoveNext())
            {
                if (etor.Current == null)
                    return true;
            }
            return false;
        }
    }
}
