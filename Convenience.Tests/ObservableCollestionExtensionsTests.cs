using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class ObservableCollestionExtensionsTests
    {
        public class DataHolder
        {
            public DataHolder(int x)
            {
                X = x;
            }
            public int X;
        }

        [TestMethod]
        public void sorting_members_directly()
        {
            List<int> sortedCol = new List<int>() { 1, 2, 3, 4 };
            ObservableCollection<int> col = new ObservableCollection<int>() { 1, 3, 2, 4 };
            col.Sort(x => x);
            Assert.IsTrue(CollectionUtils.ContentEqual(col, sortedCol));
        }

        [TestMethod]
        public void sorting_members_by_their_fields()
        {
            var v1 = new DataHolder(1);
            var v2 = new DataHolder(2);
            var v3 = new DataHolder(3);
            var v4 = new DataHolder(4);
            var v5 = new DataHolder(5);

            List<DataHolder> sortedCol = new List<DataHolder>() { v1, v2, v3, v4, v5 };
            ObservableCollection<DataHolder> col = new ObservableCollection<DataHolder>() { v5, v1, v3, v2, v4 };
            col.Sort(x => x.X);
            Assert.IsTrue(CollectionUtils.ContentEqual(col, sortedCol));
        }
    }
}
