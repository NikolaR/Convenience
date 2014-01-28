using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            public DataHolder(DateTime? d)
            {
                D = d;
            }
            public int X;
            public DateTime? D;
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

        [TestMethod]
        public void sorting_members_by_their_nullable_fields()
        {
            var v1 = new DataHolder((DateTime?)null);
            var v2 = new DataHolder(DateTime.Now.AddHours(1));
            var v3 = new DataHolder(DateTime.Now.AddHours(2));
            var v4 = new DataHolder(DateTime.Now.AddHours(3));
            var v5 = new DataHolder(DateTime.Now.AddHours(4));

            List<DataHolder> sortedCol = new List<DataHolder>() { v1, v2, v3, v4, v5 };
            ObservableCollection<DataHolder> col = new ObservableCollection<DataHolder>() { v5, v1, v3, v2, v4 };
            Func<DateTime?, DateTime?, int> comparer = (x, y) =>
            {
                if (x == null)
                    return (y == null) ? 0 : -1;
                if (y == null)
                    return 1;
                return (x.Value == y.Value) ? 0 : (x.Value > y.Value) ? 1 : 0;
            };
            col.Sort(x => x.D);
            Assert.IsTrue(CollectionUtils.ContentEqual(col, sortedCol));
        }

        [TestMethod]
        public void what_is_comparer()
        {
            var now = DateTime.Now;
            var v1 = new DataHolder(now);
            var v2 = new DataHolder(now);
            var list = new List<DataHolder>() { v1, v2 };
            list.OrderBy(x => x);
            //var comparer = Comparer<DataHolder>.Default;
            //Assert.IsFalse(comparer.Compare(v1, v2) == 0);
        }
    }
}
