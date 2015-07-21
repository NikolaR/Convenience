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

        [TestMethod]
        public void four_level_indent_with_indentation_scope_token()
        {
            StringBuilder2 sb = new StringBuilder2();
            sb.AppendLine();
            using (var tok1 = sb.Indent())
            {
                sb.AppendLine("Line1");
                using (var tok2 = sb.Indent())
                {
                    sb.AppendLine("Line2");
                    using (var tok3 = sb.Indent(2))
                    {
                        sb.AppendLine("Line3");
                    }
                    sb.AppendLine("Line4");
                }
                sb.AppendLine("Line5");
            }

            var result = sb.ToString();
            Assert.IsTrue(result.Contains("\r\n    Line1"));
            Assert.IsTrue(result.Contains("\r\n        Line2"));
            Assert.IsTrue(result.Contains("\r\n                Line3"));
            Assert.IsTrue(result.Contains("\r\n        Line4"));
            Assert.IsTrue(result.Contains("\r\n    Line5"));
        }
    }
}
