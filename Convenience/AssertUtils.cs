using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Utility for asserting certain conditions. If desired conditions are not met, utility throws
    /// an appropriate exception. Useful for as method gate for input paremeter checking (e.g. checking
    /// that provided parameters are not null).
    /// </summary>
    public class AssertUtils
    {
        /// <summary>
        /// Asserts that <paramref name="obj"/> is not <c>null</c>. Throws <see cref="ArgumentNullException"/> if
        /// argument is <c>null</c>.
        /// </summary>
        /// <param name="obj">Target object to check</param>
        /// <param name="objName">Name of the parameter to use in exception message</param>
        public static void NotNull(object obj, string objName = "")
        {
            if (obj == null)
                throw new ArgumentNullException(string.Format("Object {0} is required but it holds a null reference.", objName));
        }

        /// <summary>
        /// Asserts that value returned by <paramref name="property"/> expression is not <c>null</c>.
        /// Throws <see cref="ArgumentNullException"/> if value is <c>null</c>. Expression is also used to
        /// get name of the parameter for exception message.
        /// </summary>
        /// <param name="property">Expression to use for getting value and name of parameter</param>
        public static void NotNull<TProperty>(Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;

            var body = lambda.Body as UnaryExpression;
            if (body != null)
            {
                var unaryExpression = body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            object propValue = property.Compile().Invoke();
            string propName = memberExpression.Member.Name;
            NotNull(propValue, propName);
        }

        /// <summary>
        /// Throws an <see cref="Exception"/> if first parameter is <c>false</c> with specified <paramref name="message"/>.
        /// </summary>
        /// <param name="trueExp">Flag indicating whether an exception should be thrown</param>
        /// <param name="message">Message for the exception in case exception needs to be thrown</param>
        public static void IsTrue(bool trueExp, string message)
        {
            if (!trueExp)
                throw new Exception(message);
        }

        /// <summary>
        /// Asserts that provided string is not <c>null</c> and that it contains non-whitespace characters. If string is <c>null</c>,
        /// <see cref="ArgumentNullException"/> is thrown. If it only contains whitespace, <see cref="ArgumentException"/>
        /// is thrown.
        /// </summary>
        /// <param name="text">String whos content should be checked</param>
        /// <param name="varName">Name of the parameter used for exception message</param>
        public static void HasText(string text, string varName)
        {
            if (text == null)
                throw new ArgumentNullException(string.Format("String {0} is required but it holds a null reference.", varName));
            if (text.Trim().Length == 0)
                throw new ArgumentException(string.Format("String {0} is required but is empty, or stores only whitespaces.", varName));
        }

        /// <summary>
        /// Asserts that provided string is not <c>null</c> and that it's length is greater than 0 (it may be comprised
        /// only of whitespace e.g.). If string is <c>null</c>,  <see cref="ArgumentNullException"/> is thrown.
        /// If it length of the string is 0, <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="varName"></param>
        public static void HasLength(string text, string varName)
        {
            if (text == null)
                throw new ArgumentNullException(string.Format("String {0} is required but it holds a null reference.", varName));
            if (text.Length == 0)
                throw new ArgumentException(string.Format("String {0} is required but is empty", varName));
        }

        /// <summary>
        /// Asserts that provided <paramref name="type" /> is base for generic type parameter.
        /// Otherwise throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <typeparam name="T">Type which is expected to be derived from provided <paramref name="type"/></typeparam>
        /// <param name="type">Type from which generic type parameter should be derived</param>
        /// <param name="varName">Variable name to use in exception message</param>
        public static void IsOfType<T>(Type type, string varName)
        {
            NotNull(type, "type");
            if (!type.IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException(string.Format("Variable {0} is not of required type ({1})", varName, typeof(T).Name));
        }

        /// <summary>
        /// Asserts that provided <paramref name="type"/> is derived from provided objects type.
        /// Otherwise throws an <see cref="Exception"/>.
        /// </summary>
        /// <param name="obj">Object whos type is expected to be base of provided <paramref name="type"/></param>
        /// <param name="type">Type which is expected to be derived from provided objects type.</param>
        /// <param name="varName">Variable name to use in exception message</param>
        public static void IsOfType(object obj, Type type, string varName)
        {
            NotNull(obj, "obj");
            NotNull(type, "type");

            if (!obj.GetType().IsAssignableFrom(type))
                throw new Exception(string.Format("Parameter {0} is not of type {1}.", varName, type.FullName));
        }

        /// <summary>
        /// Asserts that provided <paramref name="type"/> is an interface. If it's not, an <see cref="Exception"/>
        /// is thrown.
        /// </summary>
        /// <param name="type">Type to perform check on</param>
        /// <param name="varName">Variable to use in exception message</param>
        public static void IsInterface(Type type, string varName)
        {
            NotNull(type, "type");
            if (!type.IsInterface)
                throw new Exception(string.Format("Parameter '{0}' is not an interface.", varName));
        }

        /// <summary>
        /// Asserts that <paramref name="subset"/> is a subset of <paramref name="superset"/> collection.
        /// If assertion fails, an <see cref="Exception"/> is thrown with provided <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T">Type of collection elements</typeparam>
        /// <param name="subset">Collection expected to be a subset</param>
        /// <param name="superset">Collection expected to be a superset</param>
        /// <param name="message">Exception message in case assertion fails</param>
        public static void IsSubset<T>(IEnumerable<T> subset, IEnumerable<T> superset, string message)
        {
            if (!CollectionUtils.IsSubset(subset, superset))
                throw new Exception(message);
        }

        /// <summary>
        /// Asserts that <paramref name="subset"/> is a subset of <paramref name="superset"/> collection.
        /// Uses provided <paramref name="comparer"/> to check equality of collection elements.
        /// If assertion fails, an <see cref="Exception"/> is thrown with provided <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T">Type of collection elements</typeparam>
        /// <param name="subset">Collection expected to be a subset</param>
        /// <param name="superset">Collection expected to be a superset</param>
        /// <param name="comparer">Comparer used for checking for element equality</param>
        /// <param name="message">Exception message in case assertion fails</param>
        public static void IsSubset<T>(IEnumerable<T> subset, IEnumerable<T> superset, Func<T, T, bool> comparer, string message)
        {
            if (!CollectionUtils.IsSubset(subset, superset, comparer))
                throw new Exception(message);
        }

        /// <summary>
        /// Asserts that provided collection is not <c>null</c> and that it has at least one item.
        /// </summary>
        /// <param name="collection">Collection to check for content</param>
        /// <param name="collectionName">Name of collection to use in exception message</param>
        public static void HasLength(IEnumerable collection, string collectionName)
        {
            if (!CollectionUtils.HasLength(collection))
            {
                string message = collectionName != null
                    ? string.Format("Collection {0} cannot be null and must have at least one", collectionName)
                    : "Collection cannot be null and must have at least one item";
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Asserts that provide collection is not <c>null</c>, contains items and none if the items is
        /// equal to <c>null</c>.
        /// </summary>
        /// <param name="collection">Collection to check</param>
        /// <param name="collectionName">Name of collection to use in exception message</param>
        public static void HasContent(IEnumerable collection, string collectionName)
        {
            if (!CollectionUtils.HasContent(collection))
            {
                string message = collectionName != null
                    ? string.Format("Collection {0} cannot be null, must have at least one item. None of the items may be null.", collectionName)
                    : "Collection cannot be null, must have at least one item. None of the items may be null.";
                throw new ArgumentException(message);
            }
        }
    }
}
