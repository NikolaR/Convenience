using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class StringBuilder2Tests
    {
        [TestMethod]
        public void two_level_indent()
        {
            StringBuilder2 sb = new StringBuilder2();
            sb.AppendLine("Line1");
            sb.IndentLevel++;
            sb.AppendLine("Line2");
            sb.IndentLevel++;
            sb.Append("Okay");

            var result = sb.ToString();
            Assert.IsTrue(result.Contains("\r\n    Line2"));
            Assert.IsTrue(result.Contains("\r\n        Okay"));
        }
    }
}
