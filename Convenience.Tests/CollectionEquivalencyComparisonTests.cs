using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class CollectionEquivalencyComparisonTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void null_collection_throws_null_exception()
        {
            List<int> c1 = new List<int>();
            List<int> c2 = null;
            CollectionUtils.ContentEquivalent(c1, c2);
        }

        [TestMethod]
        public void empty_collections_have_equal_content()
        {
            List<int> c1 = new List<int>();
            List<int> c2 = new List<int>();
            Assert.AreEqual(CollectionUtils.ContentEquivalent(c1, c2), true);
        }

        [TestMethod]
        public void collections_of_same_size_have_different_content()
        {
            List<int> c1 = new List<int>() { 1, 2, 3 };
            List<int> c2 = new List<int>() { 1, 2, 4 };
            Assert.AreEqual(CollectionUtils.ContentEquivalent(c1, c2), false);
        }

        [TestMethod]
        public void collections_of_different_size_have_different_content()
        {
            List<int> c1 = new List<int>() { 1, 2, 3 };
            List<int> c2 = new List<int>() { 1, 2, 3, 4 };
            Assert.AreEqual(CollectionUtils.ContentEquivalent(c1, c2), false);
        }

        [TestMethod]
        public void collections_of_different_type_have_same_content()
        {
            List<string> c1 = new List<string>() { "a", "b" };
            string[] c2 = new[] { "b", "a" };
            Assert.AreEqual(CollectionUtils.ContentEquivalent(c1, c2), true);
        }

        [TestMethod]
        public void collections_of_different_type_have_different_content_using_custom_comparer()
        {
            List<string> c1 = new List<string>() { "b", "A" };
            string[] c2 = new[] { "a", "B" };
            Assert.AreEqual(CollectionUtils.ContentEquivalent(c1, c2, string.Equals), false);
        }

        [TestMethod]
        public void collections_of_different_type_have_same_content_using_custom_comparer()
        {
            List<string> c1 = new List<string>() { "b", "A" };
            string[] c2 = new[] { "a", "B" };
            Assert.AreEqual(CollectionUtils.ContentEquivalent(c1, c2, (x, y) => string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase)), true);
        }
    }
}
