using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class TwoWayDictionaryTests
    {
        public TwoWayDictionary<string, string> Dict;

        [TestInitialize]
        public void TestInitialize()
        {
            Dict = new TwoWayDictionary<string, string>();
        }

        [TestMethod]
        public void accepts_and_removes_entries()
        {
            Dict.AddByFirst("foo", "bar");
            Dict.AddByFirst("foom", "barm");
            Assert.AreEqual(Dict.Count, 2);

            Dict.Clear();
            Assert.AreEqual(Dict.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_when_adding_null_key()
        {
            Dict.AddByFirst(null, "bar");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_when_adding_null_value()
        {
            Dict.AddByFirst("foo", null);
        }

        [TestMethod]
        public void finds_results()
        {
            Dict.AddByFirst("foo", "bar");
            Dict.AddByFirst("foom", "barm");

            Assert.AreEqual(Dict.GetByFirst("foo"), "bar");
            Assert.AreEqual(Dict.GetByFirst("foom"), "barm");
        }

        [TestMethod]
        public void doesnt_find_false_results()
        {
            Dict.AddByFirst("foo", "bar");
            Dict.AddByFirst("foom", "barm");

            Assert.AreNotEqual(Dict.GetByFirst("foom"), "bar");
            Assert.AreNotEqual(Dict.GetByFirst("foo"), "foom");
        }
    }
}
