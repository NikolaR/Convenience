using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class CollectionUtilsTests
    {
        [TestMethod]
        public void subset_is_in_superset()
        {
            List<string> subset = new List<string>() { "Nikola", "Petar" };
            string[] superset = new[] { "Nikola", "Petar", "Milos" };
            Assert.IsTrue(CollectionUtils.IsSubset(subset, superset));
        }

        [TestMethod]
        public void disjunct_collection_not_in_superset()
        {
            List<string> subset = new List<string>() { "Nikola", "Petar", "Mladen" };
            string[] superset = new[] { "Nikola", "Petar", "Milos" };
            Assert.IsFalse(CollectionUtils.IsSubset(subset, superset));
        }

        [TestMethod]
        public void subset_is_not_in_superset_when_string_case_different()
        {
            List<string> subset = new List<string>() { "Nikola", "Petar" };
            string[] superset = new[] { "NikOLa", "PeTar", "MiloS" };
            Assert.IsFalse(CollectionUtils.IsSubset(subset, superset));
        }

        [TestMethod]
        public void subset_is_in_superset_with_custom_comparer()
        {
            List<string> subset = new List<string>() { "Nikola", "Petar" };
            string[] superset = new[] { "NikOLa", "PeTar", "MiloS" };
            Assert.IsTrue(CollectionUtils.IsSubset(subset, superset, (s, s1) => string.Equals(s, s1, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
