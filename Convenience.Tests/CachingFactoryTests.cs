using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class CachingFactoryTests
    {
        [TestMethod]
        public void result_is_created_only_once()
        {
            Foo.InstantiationCount = 0;
            CachingFactory<int, Foo> fooFactory = new CachingFactory<int, Foo>(
                (num) => new Foo(num)
            );

            var foo1 = fooFactory[1];
            for (int i = 0; i < 10; i++)
                foo1 = fooFactory[1];
            Assert.AreEqual(Foo.InstantiationCount, 1);

            var foo2 = fooFactory[2];
            for (int i = 0; i < 10; i++)
                foo1 = fooFactory[2];
            Assert.AreEqual(Foo.InstantiationCount, 2);

            fooFactory.Evict(1);
            foo1 = fooFactory[1];
            Assert.AreEqual(Foo.InstantiationCount, 3);
        }

        [TestMethod]
        public void two_arg_factory_result_is_created_only_once()
        {
            Foo.InstantiationCount = 0;
            CachingFactory<int, int, Foo> fooFactory = new CachingFactory<int, int, Foo>(
                (num1, num2) => new Foo(num1)
            );

            var foo1 = fooFactory.Get(1, 2);
            for (int i = 0; i < 10; i++)
                foo1 = fooFactory.Get(1,2);
            Assert.AreEqual(Foo.InstantiationCount, 1);

            var foo2 = fooFactory.Get(3, 4);
            for (int i = 0; i < 10; i++)
                foo1 = fooFactory.Get(3, 4);
            Assert.AreEqual(Foo.InstantiationCount, 2);

            fooFactory.Evict(1, 2);
            foo1 = fooFactory.Get(1, 2);
            Assert.AreEqual(Foo.InstantiationCount, 3);
        }

        public class Foo
        {
            public static int InstantiationCount;

            public int Arg1;
            public int Arg2;

            public Foo(int arg)
            {
                Arg1 = arg;
                InstantiationCount++;
            }

            public Foo(int arg1, int arg2)
            {
                Arg1 = arg1;
                Arg2 = arg2;
                InstantiationCount++;
            }
        }
    }
}
