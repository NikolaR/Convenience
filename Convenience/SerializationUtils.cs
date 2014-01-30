using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Serialization helper methods
    /// </summary>
    public class SerializationUtils
    {
        /// <summary>
        /// Serializes an object into a byte array
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            ms.Flush();
            var len = (int)ms.Length;
            var buff = new byte[len];
            ms.Position = 0;
            ms.Read(buff, 0, len);

            return buff;
        }

        /// <summary>
        /// Deserializes an object from byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static object Deserialize(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return null;

            var ms = new MemoryStream(buffer);
            var bf = new BinaryFormatter();
            try
            {
                return bf.Deserialize(ms);
            }
            catch (SerializationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Deserializes an object from byte array and casts it to specified type. If deserialized
        /// object is not of specified type, throws <c>SerializationException</c>.
        /// </summary>
        /// <typeparam name="T">Expected type of serialized object</typeparam>
        /// <param name="buffer">Buffer holding serialized object</param>
        /// <returns>Deserialized object if types match, or <c>null</c> of serialized object does not match specified type.</returns>
        public static T Deserialize<T>(byte[] buffer)
        {
            object toReturn = Deserialize(buffer);
            if (toReturn == null)
                return default(T);

            if (toReturn is T)
                return (T)toReturn;
            throw new SerializationException("Cannot deserialize object. Stored object is not of type " + typeof(T).FullName);
        }

        /// <summary>
        /// Deserializes an object from byte array and casts it to specified type. If deserialized
        /// object is not of specified type, returns default value for that type.
        /// </summary>
        /// <typeparam name="T">Expected type of serialized object</typeparam>
        /// <param name="buffer">Buffer holding serialized object</param>
        /// <returns>Deserialized object if types match, or <c>null</c> of serialized object does not match specified type.</returns>
        public static T DeserializeSafe<T>(byte[] buffer)
        {
            object toReturn = Deserialize(buffer);
            if (toReturn == null)
                return default(T);

            if (toReturn is T)
                return (T)toReturn;
            else
                return default(T);
        }

        /// <summary>
        /// Creates a deep clone of specified object. Object must be serializable in order
        /// to be cloned using this method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(T obj) where T : class
        {
            if (obj == null)
                return null;
            return Deserialize<T>(Serialize(obj));
        }

        /// <summary>
        /// Creates a deep clone of specified object. Object must be serializable in order
        /// to be cloned using this method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object DeepClone(object obj)
        {
            if (obj == null)
                return null;
            return Deserialize(Serialize(obj));
        }
    }
}
