using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests.Experiments
{
    [TestClass]
    public class DictionaryKeyHandling
    {
        [TestMethod]
        public void struct_key_checked_only_for_content_not_address()
        {
            int a = 1;
            string b = "dire";
            object c = new object();

            var key1 = new CompositeKey() { Key1 = a, Key2 = b, Key3 = c };
            var key2 = new CompositeKey() { Key1 = a, Key2 = b, Key3 = c };
            var key3 = new CompositeKey() { Key1 = 2, Key2 = b, Key3 = c };

            var dict = new Dictionary<CompositeKey, string>();
            dict.Add(key1, "straits");

            Assert.IsTrue(dict.ContainsKey(key1));
            Assert.IsTrue(dict.ContainsKey(key2));
            Assert.IsFalse(dict.ContainsKey(key3));
        }

        [TestMethod]
        public void dictionary_remove_doesnt_fail_for_non_existant_key()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("foo", 33);

            dict.Remove("foo");
            dict.Remove("foo");
        }

        public struct CompositeKey
        {
            public object Key1;
            public object Key2;
            public object Key3;
        }
    }
}
