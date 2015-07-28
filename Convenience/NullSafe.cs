using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Provides extension methods for safely fetching values from long chain of
    /// property accessors. If <c>null</c> is encountered in the chain, returns <c>null</c>.
    /// </summary>
    public static class NullSafe
    {
        /// <summary>
        /// Gets a value from object <param name="instance">instance</param> according to specified
        /// <param name="expression">expression</param>. If one of properties in expression is <c>null</c>,
        /// or one of collections accessed is empty, returns <c>null</c>.
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TValue">Type of value expected to be returned.</typeparam>
        /// <param name="instance">Object instance from which to get value.</param>
        /// <param name="expression">Property accessor chain.</param>
        /// <returns>Value found in property; otherwise <c>null</c>.</returns>
        public static TValue ValueOrDefault<TSource, TValue>(
            this TSource instance,
            Expression<Func<TSource, TValue>> expression
        )
        {
            if (instance == null)
                return default(TValue);
            var result = EvaluateExpression(instance, expression);
            if (ReferenceEquals(result, default(TValue)))
                return default(TValue);
            return (TValue)result;
        }

        /// <summary>
        /// Gets a value from object <param name="instance">instance</param> according to specified
        /// <param name="expression">expression</param>. If one of properties in expression is <c>null</c>,
        /// or one of collections accessed is empty, returns specified <param name="defaultValue">default value</param>.
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TValue">Type of value expected to be returned.</typeparam>
        /// <param name="instance">Object instance from which to get value.</param>
        /// <param name="expression">Property accessor chain.</param>
        /// <param name="defaultValue">
        /// Value to return if property is <c>null</c>, if one of properties in chain
        /// is <c>null</c>, or one of collections in chain is empty.
        /// </param>
        /// <returns>Value found in property; otherwise returns specified <param name="defaultValue">default value</param>.</returns>
        public static TValue ValueOrDefault<TSource, TValue>(
            this TSource instance,
            Expression<Func<TSource, TValue>> expression,
            TValue defaultValue
        )
        {
            if (instance == null)
                return defaultValue;
            var result = EvaluateExpression(instance, expression);
            if (ReferenceEquals(result, default(TValue)))
                return defaultValue;
            return (TValue)result;
        }

        private static object EvaluateExpression(
            object source,
            Expression expression
        )
        {
            var method = expression as MethodCallExpression;
            if (method != null)
                return EvaluateMethodCallExpression(source, method);

            var body = expression as MemberExpression;
            if (body != null)
                return EvaluateMemberExpression(source, body);

            var param = expression as ParameterExpression;
            if (param != null)
                return source;

            var lambda = expression as LambdaExpression;
            if (lambda != null)
                return EvaluateExpression(source, lambda.Body);

            const string format = "Expression '{0}' must refer to a property.";
            string message = string.Format(format, expression);
            throw new ArgumentException(message);
        }

        private static object EvaluateMemberExpression(object instance, MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                return instance;
            }
            instance = EvaluateExpression(instance, memberExpression.Expression);
            if (instance == null)
                return null;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            return propertyInfo.GetValue(instance, null);
        }

        private static object EvaluateMethodCallExpression(object instance, MethodCallExpression expression)
        {
            if (instance == null)
                return null;
            var method = expression.Method;
            if (
                    method.DeclaringType != typeof(Enumerable) ||
                    !method.IsDefined(typeof(ExtensionAttribute), false) ||
                    method.GetParameters().Length != 1
                )
                throw new InvalidOperationException(
                    "NullSafe dereferencer supports only method calls to collection methods which" +
                    "take no parameters (such as First(), Single(), FirstOrDefault() etc)"
                );
            var target = EvaluateExpression(instance, expression.Arguments[0]);
            if (target == null)
                return null;
            var enumerable = target as IEnumerable;
            var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
                return null;
            enumerator.Reset();
            return method.Invoke(instance, new[] { target });
        }
    }
}
