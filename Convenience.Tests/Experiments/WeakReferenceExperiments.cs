using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests.Experiments
{
    [TestClass]
    public class WeakReferenceExperiments
    {
        [TestMethod]
        public void disposed_object_is_alive()
        {
            WeakReference reference;
            using (var ms = new MemoryStream())
            {
                reference = new WeakReference(ms);
            }
            Assert.IsTrue(reference.IsAlive);
        }

        [TestMethod]
        public void gc_collect_cleans_up_weak_references()
        {
            WeakReference reference = new WeakReference(new object());
            GC.Collect();
            Assert.IsFalse(reference.IsAlive);
        }
    }
}
