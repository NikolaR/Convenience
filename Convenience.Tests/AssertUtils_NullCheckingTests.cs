using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class AssertUtils_NullCheckingTests
    {
        [TestMethod]
        public void NotNull_runs_on_not_null_reference_field()
        {
            string test = "asdadgsd";
            AssertUtils.NotNull(() => test);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNull_fails_on_null_reference_field()
        {
            string test = null;
            AssertUtils.NotNull(() => test);
        }

        [TestMethod]
        public void NotNull_runs_on_value_field()
        {
            int test = 42;
            AssertUtils.NotNull(() => test);
        }

        [TestMethod]
        public void NotNull_runs_on_not_null_reference_property()
        {
            AssertUtils.NotNull(() => MyNotNullProperty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNull_fails_on_null_reference_property()
        {
            AssertUtils.NotNull(() => MyNullProperty);
        }

        [TestMethod]
        public void NotNull_runs_on_value_property()
        {
            AssertUtils.NotNull(() => MyValueProperty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNull_fails_on_null_nullable_value_property()
        {
            AssertUtils.NotNull(() => MyNullProperty);
        }

        public object MyNullProperty
        {
            get { return null; }
        }

        public object MyNotNullProperty
        {
            get { return new object(); }
        }

        public int MyValueProperty
        {
            get { return 42; }
        }

        public int? MyNullableValueNullProperty
        {
            get { return null; }
        }
    }
}
