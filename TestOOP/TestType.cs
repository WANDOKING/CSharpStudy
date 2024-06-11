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
        public void TestValueType()
        {
#pragma warning disable CS0183, CS0184, IDE0150
            int myNumber = 5;
            Assert.IsTrue(myNumber is object);
            Assert.IsTrue(myNumber is ValueType);
#pragma warning restore CS0183, CS0184, IDE0150
        }

        [TestMethod]
        public void TestReferenceType()
        {
#pragma warning disable CS0183, CS0184
            string myString = "Hello";
            Assert.IsTrue(myString is object);
            Assert.IsFalse(myString is ValueType);
#pragma warning restore CS0183, CS0184
        }
    }
}
