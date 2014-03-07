using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Convenience.Tests
{
    [TestClass]
    public class ReflectionTests
    {
        [TestMethod]
        public void array_implements_ienumerable()
        {
            Type arrayT = typeof(Array);
            Type ienumT = typeof(IEnumerable);

            Assert.IsTrue(ReflectionUtils.ImplementsInterface(arrayT, ienumT));
        }

        [TestMethod]
        public void open_list_of_t_implements_open_ienumerable_of_t()
        {
            Type t1Type = typeof(List<>);
            Type t2Type = typeof(IEnumerable<>);

            Assert.IsTrue(ReflectionUtils.ImplementsInterface(t1Type, t2Type));
        }

        [TestMethod]
        public void array_implements_ienumerable2()
        {
            Type arrayT = typeof(Array);

            Assert.IsTrue(ReflectionUtils.ImplementsInterface<IEnumerable>(arrayT));
        }

        [TestMethod]
        public void list_of_t_implements_open_ienumerable_of_t_generic()
        {
            Type ienumT = typeof(IEnumerable<>);
            List<int> list = new List<int>();

            Assert.IsTrue(ReflectionUtils.ImplementsInterface(list, ienumT));
        }

        [TestMethod]
        public void list_of_t_implements_ienumerable_of_t_generic()
        {
            Type t1Type = typeof(List<>);
            List<int> list = new List<int>();

            Assert.IsTrue(ReflectionUtils.ImplementsInterface<IEnumerable<int>>(list));
        }

        [TestMethod]
        public void list_of_t_implements_ienumerable_of_t()
        {
            Type ienumT = typeof(IEnumerable<int>);
            List<int> list = new List<int>();

            Assert.IsFalse(ReflectionUtils.ImplementsInterface(list, ienumT));
        }

        [TestMethod]
        public void list_of_t_doesnt_implement_bad_ienumerable_of_t2()
        {
            Type ienumT = typeof(IEnumerable<long>);
            List<int> list = new List<int>();

            Assert.IsFalse(ReflectionUtils.ImplementsInterface(list, ienumT));
        }
    }
}
