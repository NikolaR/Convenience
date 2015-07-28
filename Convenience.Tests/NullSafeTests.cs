using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convenience.Tests
{
    [TestClass]
    public class NullSafeTests
    {
        [TestMethod]
        public void null_safe_reads_equipment_class_null_description_collection()
        {
            EquipmentClassType eqClass = new EquipmentClassType();
            var description = eqClass.ValueOrDefault(e => e.Description.First().Value);
            Assert.AreEqual(description, null);
        }

        [TestMethod]
        public void null_safe_reads_equipment_class_empty_description_collection()
        {
            EquipmentClassType eqClass = new EquipmentClassType();
            eqClass.Description = new List<DescriptionType>();
            var description = eqClass.ValueOrDefault(e => e.Description.First().Value);
            Assert.AreEqual(description, null);
        }

        [TestMethod]
        public void null_safe_reads_equipment_class_description_with_null_element()
        {
            EquipmentClassType eqClass = new EquipmentClassType();
            eqClass.Description = new List<DescriptionType>();
            eqClass.Description.Add(new DescriptionType());
            var description = eqClass.ValueOrDefault(e => e.Description.First().Value);
            Assert.AreEqual(description, null);
        }

        [TestMethod]
        public void null_safe_reads_equipment_class_description_with_real_description()
        {
            EquipmentClassType eqClass = new EquipmentClassType();
            eqClass.Description = new List<DescriptionType>();
            eqClass.Description.Add(new DescriptionType() { Value = "descr" });
            var description = eqClass.ValueOrDefault(e => e.Description.First().Value);
            Assert.AreEqual(description, "descr");
        }

        [TestMethod]
        public void null_safe_reads_equipment_last_description()
        {
            EquipmentClassType eqClass = new EquipmentClassType();
            eqClass.Description = new List<DescriptionType>();
            eqClass.Description.Add(new DescriptionType() { Value = "first descr" });
            eqClass.Description.Add(new DescriptionType() { Value = "second descr" });
            eqClass.Description.Add(new DescriptionType() { Value = "third descr" });
            var description = eqClass.ValueOrDefault(e => e.Description.Last().Value);
            Assert.AreEqual(description, "third descr");
        }

        public class EquipmentClassType
        {
            public EquipmentClassType()
            {
                Description = new List<DescriptionType>();
            }

            public List<DescriptionType> Description { get; set; }
        }
        public class DescriptionType : TextType
        {
        }

        public class TextType
        {
            public string Value { get; set; }
        }
    }
}
