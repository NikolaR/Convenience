using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Provides utilities for some common type checks.
    /// </summary>
    public class TypeUtils
    {
        /// <summary>
        /// Checks whether an object is a collection.
        /// </summary>
        /// <param name="obj">Objects whos type will be checked.</param>
        /// <returns><c>true</c> if object is a collection; otherwise <c>false</c>.</returns>
        public static bool IsCollection(object obj)
        {
            if (obj == null)
                return false;
            var type = obj.GetType();
            return IsCollection(type);
        }

        /// <summary>
        /// Checks whether specified type is a collection type and returns <c>true</c>
        /// if it is; otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns><c>true</c> if type is a collection type; otherwise <c>false</c>.</returns>
        public static bool IsCollection(Type type)
        {
            return ImplementsInterface(type, typeof (IEnumerable));
        }

        /// <summary>
        /// Checks whether specified type implements specified interface and returns <c>true</c>
        /// if it does; otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="type">Type which will be checked.</param>
        /// <param name="interface">Interface which target type should implement.</param>
        /// <returns><c>true</c> if type implements specified interface; otherwise <c>false</c>.</returns>
        public static bool ImplementsInterface(Type type, Type @interface)
        {
            AssertUtils.NotNull(type, "type");
            AssertUtils.NotNull(@interface, "interface");

            return type.GetInterfaces().Contains(@interface);
        }

        /// <summary>
        /// Checks whether provided type is based on generic interface definition (e.g. <c>IEnumerable&lt;&gt;</c>)
        /// and returns <c>true</c> if it does; otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="type">Type which will be checked.</param>
        /// <param name="interface">Generic interface definition which type should be based on.</param>
        /// <returns><c>true</c> if type is based on provided generic interface definition; otherwise <c>false</c>.</returns>
        public static bool BasedOnGenericDefinitionInterface(Type type, Type @interface)
        {
            AssertUtils.NotNull(type, "type");
            AssertUtils.NotNull(@interface, "interface");
            AssertUtils.IsTrue(@interface.IsGenericTypeDefinition, "Provided interface is not generic definition.");

            var interfaces = type.GetInterfaces();
            return interfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == @interface);
        }

        /// <summary>
        /// Checks whether provided type is based on generic type definition (e.g. <c>List&lt;&gt;</c>)
        /// and returns <c>true</c> if it does; otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="type">Type which will be checked.</param>
        /// <param name="baseClass">Generic class definition which target type should be based on.</param>
        /// <returns><c>true</c> if provided type is based on provided generic class definition; otherwise <c>false</c>.</returns>
        public static bool BasedOnGenericDefinitionType(Type type, Type baseClass)
        {
            AssertUtils.NotNull(type, "type");
            AssertUtils.NotNull(baseClass, "baseType");
            AssertUtils.IsTrue(baseClass.IsGenericTypeDefinition, "Provided base type is not generic definition.");

            while (type != null && type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == baseClass)
                    return true;
                type = type.BaseType;
            }
            return false;
        }

        /// <summary>
        /// Checks whether provided type is based on provided generic class or interface type (e.g. 
        /// <c>IEnumerable&lt;&gt;</c> or <c>List&lt;&gt;</c>) and returns <c>true</c> if it does;
        /// otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="type">Type which will be checked.</param>
        /// <param name="baseClassOrInterface">Generic class or interface definition which target type should be based on.</param>
        /// <returns><c>true</c> if target type is based on provided generic type definition; otherwise <c>false</c>.</returns>
        public static bool BasedOnGenericDefinition(Type type, Type baseClassOrInterface)
        {
            return (BasedOnGenericDefinitionType(type, baseClassOrInterface) ||
                    BasedOnGenericDefinitionInterface(type, baseClassOrInterface));
        }

        /// <summary>
        /// Checks whether provided object is based on provided generic class or interface type (e.g. 
        /// <c>IEnumerable&lt;&gt;</c> or <c>List&lt;&gt;</c>) and returns <c>true</c> if it does;
        /// otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="obj">Object which will be checked.</param>
        /// <param name="baseClassOrInterface">Generic class or interface definition which target type should be based on.</param>
        /// <returns><c>true</c> if target object is based on provided generic type definition; otherwise <c>false</c>.</returns>
        public static bool BasedOnGenericDefinition(object obj, Type baseClassOrInterface)
        {
            if (obj == null)
                return false;
            return BasedOnGenericDefinition(obj.GetType(), baseClassOrInterface);
        }

        /// <summary>
        /// Checks whether provided object is based on provided generic class or interface type (e.g. 
        /// <c>IEnumerable&lt;&gt;</c> or <c>List&lt;&gt;</c>) and returns <c>true</c> if it does;
        /// otherwise returns <c>false</c>.
        /// </summary>
        /// <param name="obj">Object which will be checked.</param>
        /// <returns><c>true</c> if target object is based on provided generic type definition; otherwise <c>false</c>.</returns>
        public static bool BasedOnGenericDefinition<TBaseType>(object obj)
        {
            if (obj == null)
                return false;
            return BasedOnGenericDefinition(obj.GetType(), typeof(TBaseType));
        }
    }
}
