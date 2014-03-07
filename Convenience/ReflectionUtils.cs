using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convenience
{
    public class ReflectionUtils
    {
        // Notes:
        // GUID comparison compares if a type is created from open generic type ( List<int> implements IEnumerable<>)
        // Rename methods to ImplementsOpenInterface
        // Add extension methods to Type


        public static bool ImplementsInterface(Type type, Type @interface)
        {
            var interfaces = type.GetInterfaces();
            if (!@interface.IsGenericType)
            {
                if (interfaces.Contains(@interface))
                    return true;
            }
            else
            {
                // When generic interface is implemented, reflection returns a new
                // object type without FullName and AssemblyQualifiedName set. It's a different
                // 
                if (interfaces.Any(i => i.GUID == @interface.GUID && CollectionUtils.ContentEqual(i.GetGenericArguments(), @interface.GetGenericArguments())))
                    return true;
            }
            return false;
        }

        public static bool ImplementsInterface(object obj, Type @interface)
        {
            if (obj == null)
                return false;
            return ImplementsInterface(obj.GetType(), @interface);
        }

        public static bool ImplementsInterface<TInterface>(Type type)
        {
            return ImplementsInterface(type, typeof (TInterface));
        }

        public static bool ImplementsInterface<TInterface>(object obj)
        {
            if (obj == null)
                return false;
            return ImplementsInterface(obj.GetType(), typeof (TInterface));
        }
    }
}
