using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    public class AssertUtils
    {
        public static void NotNull(object obj, string objName)
        {
            if (obj == null)
                throw new ArgumentNullException(string.Format("Object {0} is required but it holds a null reference.", objName));
        }

        public static void IsTrue(bool trueExp, string message)
        {
            if (!trueExp)
                throw new Exception(message);
        }

        public static void HasText(string text, string varName)
        {
            if (text == null)
                throw new ArgumentNullException(string.Format("String {0} is required but it holds a null reference.", varName));
            if (text.Trim().Length == 0)
                throw new ArgumentException(string.Format("String {0} is required but is empty, or stores only whitespaces.", varName));
        }

        public static void HasLength(string text, string varName)
        {
            if (text == null)
                throw new ArgumentNullException(string.Format("String {0} is required but it holds a null reference.", varName));
            if (text.Length == 0)
                throw new ArgumentException(string.Format("String {0} is required but is empty", varName));
        }

        public static void IsOfType<T>(Type type, string varName)
        {
            NotNull(type, "type");
            if (!type.IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException(string.Format("Variable {0} is not of required type ({1})", varName, typeof(T).Name));
        }

        public static void IsOfType(object obj, Type type, string varName)
        {
            NotNull(obj, "obj");
            NotNull(type, "type");

            if (!obj.GetType().IsAssignableFrom(type))
                throw new Exception(string.Format("Parameter {0} is not of type {1}.", varName, type.FullName));
        }

        public static void IsInterface(Type type, string varName)
        {
            NotNull(type, "type");
            if (!type.IsInterface)
                throw new Exception(string.Format("Parameter '{0}' is not an interface.", varName));
        }
    }
}
