using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Convenience;

namespace Convenience.Tests
{
    [TestClass]
    public class CollectionSizeComparisonTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void null_collection_throws_null_exception()
        {
            List<int> c1 = new List<int>();
            List<int> c2 = null;
            CollectionUtils.SizeEqual(c1, c2);
        }

        [TestMethod]
        public void empty_collections_have_same_size()
        {
            List<int> c1 = new List<int>();
            List<int> c2 = new List<int>();
            Assert.AreEqual(CollectionUtils.SizeEqual(c1, c2), true);
        }

        [TestMethod]
        public void collections_have_different_size()
        {
            List<int> c1 = new List<int>() { 1, 2, 3 };
            List<int> c2 = new List<int>() { 1, 2, 3, 4 };
            Assert.AreEqual(CollectionUtils.SizeEqual(c1, c2), false);
        }

        [TestMethod]
        public void collections_of_different_type_have_same_size()
        {
            List<string> c1 = new List<string>() { "", "" };
            string[] c2 = new[] { "", "" };
            Assert.AreEqual(CollectionUtils.SizeEqual(c1, c2), true);
        }
    }
}
