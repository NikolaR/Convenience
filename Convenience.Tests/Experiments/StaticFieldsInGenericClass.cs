using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests.Experiments
{
    [TestClass]
    public class StaticFieldsInGenericClass
    {
        [TestMethod]
        public void each_generic_instance_has_new_non_generic_static_field()
        {
            Assert.AreSame(Generic<object>.Obj, Generic<object>.Obj);
            Assert.AreNotSame(Generic<object>.Obj, Generic<TestClassAttribute>.Obj);
            Assert.AreNotSame(Generic<object>.GenericField, Generic<TestClassAttribute>.GenericField);
        }


        public class Generic<T> where T : class, new()
        {
            public static object Obj = new object();
            public static T GenericField = Activator.CreateInstance<T>();
        }
    }
}
