using System;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class BinarySerializationUtilsTests
    {
        public class NonSerializable
        {
            public int A;
            public string B;
        }

        [Serializable]
        public class SerializableClass
        {
            public int A;
            public string B;
        }

        [Serializable]
        public class SerializableClass2
        {
            public int A;
            public string B;
        }

        [TestMethod]
        public void can_clone_value_type()
        {
            int val = 10;
            object valObj = val;
            var cloneVal = SerializationUtils.DeepClone(valObj);

        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void non_serializable_fails_on_deep_clone()
        {
            var a = new NonSerializable();
            SerializationUtils.DeepClone(a);
        }

        [TestMethod]
        public void serialized_and_deserialized_objects_match()
        {
            var obj = new SerializableClass() { A = 1, B = "Works.." };
            var buffer = SerializationUtils.Serialize(obj);
            var objDeserialized = SerializationUtils.Deserialize<SerializableClass>(buffer);

            Assert.AreEqual(obj.A, objDeserialized.A);
            Assert.AreEqual(obj.B, objDeserialized.B);
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void unsafe_deserializing_using_wrong_generic_type_fails()
        {
            var obj = new SerializableClass() { A = 1, B = "Labor omnia vincit" };
            var buffer = SerializationUtils.Serialize(obj);
            SerializationUtils.Deserialize<SerializableClass2>(buffer);
        }

        [TestMethod]
        public void safe_deserializing_using_wrong_generic_type_returns_null()
        {
            var obj = new SerializableClass() { A = 1, B = "Labor omnia vincit" };
            var buffer = SerializationUtils.Serialize(obj);
            var objDeserialized = SerializationUtils.DeserializeSafe<SerializableClass2>(buffer);

            Assert.IsNull(objDeserialized);
        }
    }
}
