using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class TypeCheckTests
    {
        [TestMethod]
        public void array_is_collection()
        {
            Assert.IsTrue(TypeUtils.IsCollection(typeof(Array)));
        }

        [TestMethod]
        public void array_is_collection_2()
        {
            var arr = new string[0];
            Assert.IsTrue(TypeUtils.IsCollection(arr));
        }

        [TestMethod]
        public void list_of_int_implements_generic_ienum()
        {
            var list = new List<int>();
            Assert.IsTrue(TypeUtils.BasedOnGenericDefinition(list, typeof(IEnumerable<>)));
            Assert.IsTrue(TypeUtils.BasedOnGenericDefinitionInterface(typeof(List<int>), typeof(IEnumerable<>)));
            Assert.IsFalse(TypeUtils.BasedOnGenericDefinitionType(typeof(List<int>), typeof(IEnumerable<>)));
            Assert.IsFalse(TypeUtils.BasedOnGenericDefinitionType(typeof(TestClassAttribute), typeof(IEnumerable<>)));
        }

        [TestMethod]
        public void implements_interface_handles_generic_types()
        {
            Assert.IsTrue(TypeUtils.ImplementsInterface(typeof(List<int>), typeof(IEnumerable<int>)));
            Assert.IsFalse(TypeUtils.ImplementsInterface(typeof(List<int>), typeof(IEnumerable<long>)));
            Assert.IsFalse(TypeUtils.ImplementsInterface(typeof(List<int>), typeof(IEnumerable<string>)));
        }
    }
}
