using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests.Experiments
{
    [TestClass]
    public class TypeChecking
    {
        [TestMethod]
        public void get_interfaces_returns_second_level_interfaces()
        {
            var interfaces = typeof (C).GetInterfaces();
            CollectionAssert.Contains(interfaces, typeof(IA));
        }

        [TestMethod]
        public void get_generic_type_definition_and_original_definition_are_equal()
        {
            Type t1 = typeof (IC<>);
            Type t2 = typeof (C<>).GetInterfaces()[0].GetGenericTypeDefinition();

            Assert.AreSame(t1, t2);
        }

        public interface IA
        { }
        public interface IB : IA
        { }
        public interface IC:IB
        { }
        public class C : IC
        { }

        public interface IC<T>
        { }
        public class C<T> : IC<T>
        { }
    }
}
