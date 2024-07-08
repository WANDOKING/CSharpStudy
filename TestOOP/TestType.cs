using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOOP
{
    [TestClass]
    public class TestType
    {
        [TestMethod]
        public void ValueType()
        {
#pragma warning disable CS0183, CS0184, IDE0150
            int myNumber = 5;
            Assert.IsTrue(myNumber is object);
            Assert.IsTrue(myNumber is ValueType);
#pragma warning restore CS0183, CS0184, IDE0150
        }

        [TestMethod]
        public void ReferenceType()
        {
#pragma warning disable CS0183, CS0184
            string myString = "Hello";
            Assert.IsTrue(myString is object);
            Assert.IsFalse(myString is ValueType);
#pragma warning restore CS0183, CS0184
        }

        [TestMethod]
        public void MaxMinValue()
        {
            Assert.AreEqual(127, sbyte.MaxValue);
            Assert.AreEqual(-128, sbyte.MinValue);
            Assert.AreEqual(255, byte.MaxValue);
            Assert.AreEqual(0, byte.MinValue);
            Assert.AreEqual(32767, short.MaxValue);
            Assert.AreEqual(-32768, short.MinValue);
            Assert.AreEqual(65535, ushort.MaxValue);
            Assert.AreEqual(0, ushort.MinValue);
        }

        [TestMethod]
        public void DefaultValue()
        {
            int defaultIntValue = default;
            bool defaultBoolValue = default;
            double defaultDoubleValue = default;
            string? defaultStringValue = default;

            Assert.AreEqual(0, defaultIntValue);
            Assert.AreEqual(false, defaultBoolValue);
            Assert.AreEqual(0.0, defaultDoubleValue);
            Assert.AreEqual(null, defaultStringValue);
        }
    }
}
